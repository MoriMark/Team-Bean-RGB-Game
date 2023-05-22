using RGB.View;
using Timer = System.Windows.Forms.Timer;

namespace RGB
{
    /// <summary>
    /// Main menu
    /// </summary>
    /// <returns></returns>
    public partial class Form1 : Form
    {
        private static HelpForm help = null!;
        private static GameView gameView = null!;
        private Timer animTimer;
        private int animTime = 1000;
        private int waitTime = 1000;
        private int animFrame = 0;

        private int numOfRobots = 0;
        private int numOfTeams = 0;
        private GridButton[,] _buttons;

        public Form1()
        {
            InitializeComponent();
            help = new HelpForm();

            animTimer = new Timer();
            animTimer.Interval = 16;
            animTimer.Tick += Animate;
            animTimer.Start();

            layoutRobotsPanel.RowCount = 4;
            layoutRobotsPanel.ColumnCount = 4;
            layoutRobotsPanel.Margin = new Padding(0);
            layoutRobotsPanel.Padding = new Padding(0);
            layoutRobotsPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            //GridButton setup
            _buttons = new GridButton[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _buttons[i, j] = new GridButton(i, j);
                    _buttons[i, j].Dock = DockStyle.Fill;
                    _buttons[i, j].Font = new Font("Segoe UI", 9);
                    _buttons[i, j].Click += new EventHandler(GridButton_Click);
                    _buttons[i, j].MouseEnter += new EventHandler(GridButton_MouseEnter);
                    _buttons[i, j].MouseLeave += new EventHandler(GridButton_MouseLeave);
                    _buttons[i, j].Padding = new Padding(0);
                    _buttons[i, j].Margin = new Padding(0);
                    _buttons[i, j].FlatStyle = FlatStyle.Flat;
                    _buttons[i, j].FlatAppearance.BorderSize = 0;
                    _buttons[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    _buttons[i, j].BackColor = Color.White;

                    layoutRobotsPanel.Controls.Add(_buttons[i, j], j, i);
                }
            }
            layoutRobotsPanel.RowStyles.Clear();
            layoutRobotsPanel.ColumnStyles.Clear();

            for (int i = 0; i < layoutRobotsPanel.RowCount; i++)
            {
                layoutRobotsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / layoutRobotsPanel.RowCount));
            }
            for (int i = 0; i < layoutRobotsPanel.ColumnCount; i++)
            {
                layoutRobotsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / layoutRobotsPanel.ColumnCount));
            }
            buttonGameStart.MouseEnter += Button_MouseEnterLeave;
            buttonGameStart.MouseLeave += Button_MouseEnterLeave;

            buttonHelp.MouseEnter += Button_MouseEnterLeave;
            buttonHelp.MouseLeave += Button_MouseEnterLeave;

        }

        /// <summary>
        /// Opens the help
        /// </summary>
        /// <returns></returns>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            help = new HelpForm();
            help.Show();
        }

        /// <summary>
        /// starts the game with the selected number of teams and players
        /// </summary>
        /// <returns></returns>
        private void buttonGameStart_Click(object sender, EventArgs e)
        {
            if (numOfRobots > 0 && numOfTeams > 0)
            {
                gameView = new GameView(Convert.ToInt32(numOfRobots), Convert.ToInt32(numOfTeams));
                this.Hide();
                gameView.Show();
                gameView.FormClosing += QuitGame;
            }
            else
            {
                MessageBox.Show("Please select a number of robots!");
            }
        }

        /// <summary>
        /// Handles the event if an active game is closed
        /// </summary>
        /// <returns></returns>
        private void QuitGame(object? sender, EventArgs e)
        {
            gameView.Hibernate();
            gameView?.Dispose();
            gameView = null!;
            this.Show();
        }

        /// <summary>
        /// Primitive logo animation logic
        /// </summary>
        /// <returns></returns>
        private void Animate(object? sender, EventArgs e)
        {
            if (waitTime > 0)
            {
                waitTime -= 16;
            }
            else if (waitTime < 0)
            {
                if (animTime > 0)
                {
                    animFrame += 1;
                    animTime -= 16;
                    logoPicture.Location = new Point(animFrame * (this.Size.Width / 60), 0);
                }
                else
                {
                    animTimer.Stop();
                    logoPicture.Visible = false;
                }
            }

        }

        private void GridButton_Click(object? sender, EventArgs e)
        {
            if (sender is GridButton && null != sender)
            {
                GridButton btn = (GridButton)sender;
                numOfRobots = btn.GridX + 1;
                numOfTeams = btn.GridY + 1;
            }
        }

        private void GridButton_MouseEnter(object? sender, EventArgs e)
        {
            GridButton hoveredBtn;
            GridButton currentBtn;
            if (sender is GridButton && null != sender)
            {
                hoveredBtn = (GridButton)sender;


                foreach (Control c in layoutRobotsPanel.Controls)
                {
                    if (c is GridButton)
                    {
                        currentBtn = (GridButton)c;
                        if (currentBtn.GridX <= hoveredBtn.GridX && currentBtn.GridY <= hoveredBtn.GridY)
                        {
                            switch (currentBtn.GridY)
                            {
                                case 0:
                                    currentBtn.BackgroundImage = Properties.Resources.red_down;
                                    break;
                                case 1:
                                    currentBtn.BackgroundImage = Properties.Resources.blue_down;
                                    break;
                                case 2:
                                    currentBtn.BackgroundImage = Properties.Resources.green_down;
                                    break;
                                case 3:
                                    currentBtn.BackgroundImage = Properties.Resources.yellow_down;
                                    break;
                                default:
                                    currentBtn.BackgroundImage = null;
                                    break;
                            }
                        }
                        else
                        {
                            currentBtn.BackgroundImage = null;
                        }
                    }
                }
            }
        }

        private void GridButton_MouseLeave(object? sender, EventArgs e)
        {
            GridButton btn;
            foreach (Control c in layoutRobotsPanel.Controls)
            {
                if (c is GridButton)
                {
                    btn = (GridButton)c;
                    if (btn.GridX > numOfRobots - 1 || btn.GridY > numOfTeams - 1)
                    {
                        btn.BackgroundImage = null;
                    }
                    else
                    {
                        switch (btn.GridY)
                        {
                            case 0:
                                btn.BackgroundImage = Properties.Resources.red_down;
                                break;
                            case 1:
                                btn.BackgroundImage = Properties.Resources.blue_down;
                                break;
                            case 2:
                                btn.BackgroundImage = Properties.Resources.green_down;
                                break;
                            case 3:
                                btn.BackgroundImage = Properties.Resources.yellow_down;
                                break;
                            default:
                                btn.BackgroundImage = null;
                                break;
                        }
                    }
                }
            }
        }

        private void Button_MouseEnterLeave(object? sender, EventArgs e) 
        {
            Button btn;
            if (sender is Button)
            {
                btn = (Button)sender;
                invertButtonColors(btn);
            }
        }

        private void invertButtonColors(Button btn)
        {
            Color tmp = btn.BackColor;
            btn.BackColor = btn.ForeColor;
            btn.ForeColor = tmp;
        }
    }
}