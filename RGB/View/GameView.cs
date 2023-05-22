using RGBModell.modell.gameobjects;
using RGBModell.modell.boxlogic;
using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RGBModell.modell;
using RGBModell.modell.structs;
using RGBModell.modell.game_logic;
using RGBModell.modell.events;
using System.Drawing.Drawing2D;

namespace RGB.View
{
    public partial class GameView : Form
    {
        private int viewDist = 4;
        private GridButton[,] _buttons = null!;
        private int selectionsNeeded;
        private int numOfPlayers;
        private int numOfTeams;
        private int totalRobots;
        private int tableSize;
        private int remainingTime = 300;
        private System.Windows.Forms.Timer _timer;
        //0 - red, 1 - blue, 2 - green, 3 - yellow
        private int[] points;

        private Color redTeamBcgColor;
        private Color greenTeamBcgColor;
        private Color blueTeamBcgColor;
        private Color yellowTeamBcgColor;

        private Symbol selectedSymbol;
        private Coordinate currentRobotCoords;
        private List<Coordinate> selectedTiles;
        private ButtonLayouts currentLayout;
        private Actions selectedAction;
        private GameHandler _gameHandler;
        private MapForm map = null!;
        private List<Exit> exits;
        private GameObject[,] _viewField = null!;

        public GameView(int players, int teams)
        {
            InitializeComponent();
            //Set trivial values
            numOfTeams = teams;
            numOfPlayers = players;
            _gameHandler = new GameHandler(players, teams);
            totalRobots = players * teams;
            tableSize = totalRobots * 4;
            selectedTiles = new List<Coordinate>();
            SetButtonLayout(ButtonLayouts.Default);
            currentLayout = ButtonLayouts.Default;
            currentRobotCoords = new Coordinate();
            selectedSymbol = Symbol.None;
            points = new int[] { 0, 0, 0, 0 };

            redTeamBcgColor = Color.MistyRose;
            blueTeamBcgColor = Color.LightCyan;
            greenTeamBcgColor = Color.FromArgb(255, 181, 255, 179);
            yellowTeamBcgColor = Color.FromArgb(255, 251, 254, 180);

            redTeamScore.BackColor = redTeamBcgColor;
            blueTeamScore.BackColor = blueTeamBcgColor;
            greenTeamScore.BackColor = greenTeamBcgColor;
            yellowTeamScore.BackColor = yellowTeamBcgColor;

            //Subscribe to other events
            sendButton.MouseEnter += Button_MouseEnterLeave;
            sendButton.MouseLeave += Button_MouseEnterLeave;

            mapButton.MouseEnter += Button_MouseEnterLeave;
            mapButton.MouseLeave += Button_MouseEnterLeave;

            mapmodeNormalButton.MouseEnter += Button_MouseEnterLeave;
            mapmodeNormalButton.MouseLeave += Button_MouseEnterLeave;

            mapmodeGroupButton.MouseEnter += Button_MouseEnterLeave;
            mapmodeGroupButton.MouseLeave += Button_MouseEnterLeave;

            //Subscribe to gameHandler events
            _gameHandler.robotChanged += NextRobot;
            sendButton.Click += SendButton_Click;
            mapButton.Click += MapButton_Click;
            _gameHandler.gameRule.UpdateFields += RefreshTable;
            _gameHandler.gameRule.UpdateTasks += RefreshTaskDisplays;
            SetUpSymbolButtons();
            //Setting up the grid buttons
            tableLayoutPanelButtons.RowCount = viewDist * 2 + 1;
            tableLayoutPanelButtons.ColumnCount = viewDist * 2 + 1;
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Padding = new Padding(0);
            tableLayoutPanelButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            _buttons = new GridButton[viewDist * 2 + 1, viewDist * 2 + 1];

            for (int i = 0; i < viewDist * 2 + 1; i++)
            {
                for (int j = 0; j < viewDist * 2 + 1; j++)
                {
                    _buttons[i, j] = new GridButton(i - viewDist, j - viewDist); //-3, so that the middle is 0,0
                    if (Math.Abs(_buttons[i, j].GridX) + Math.Abs(_buttons[i, j].GridY) > viewDist)
                    {
                        _buttons[i, j].BackColor = Color.DarkGray;
                        _buttons[i, j].Enabled = false;
                        _buttons[i, j].seen = false;
                    }
                    else
                    {
                        _buttons[i, j].BackColor = Color.White;
                    }
                    _buttons[i, j].Dock = DockStyle.Fill;
                    _buttons[i, j].Font = new Font("Segoe UI", 9);
                    _buttons[i, j].ForeColor = Color.White;
                    _buttons[i, j].Click += new EventHandler(GridButton_Click);
                    _buttons[i, j].Padding = new Padding(0);
                    _buttons[i, j].Margin = new Padding(0);
                    _buttons[i, j].FlatStyle = FlatStyle.Flat;
                    _buttons[i, j].FlatAppearance.BorderSize = 0;
                    _buttons[i, j].BackgroundImageLayout = ImageLayout.Zoom;

                    tableLayoutPanelButtons.Controls.Add(_buttons[i, j], j, i);
                }
            }

            tableLayoutPanelButtons.RowStyles.Clear();
            tableLayoutPanelButtons.ColumnStyles.Clear();

            for (int i = 0; i < tableLayoutPanelButtons.RowCount; i++)
            {
                tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / (viewDist * 2 + 1)));
            }
            for (int i = 0; i < tableLayoutPanelButtons.ColumnCount; i++)
            {
                tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / (viewDist * 2 + 1)));
            }
            //Round timer setup
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += RoundTimerTick;
            _timer.Enabled = true;

            exits = _gameHandler.gameRule.exits;
            testLabel.Text = string.Empty;

            //Show Table for the first player
            _gameHandler.StartGame();
            NextRobot(null, EventArgs.Empty);
        }

        /// <summary>
        /// Map button event handler
        /// </summary>
        /// <returns></returns>
        private void MapButton_Click(object? sender, EventArgs e)
        {
            map = new MapForm(_gameHandler.GetCurrentPlayer());
            map.Show();
            map.FormClosing += Map_Closed;
        }

        /// <summary>
        /// Disables field of view of game
        /// </summary>
        /// <returns></returns>
        private void DisableRobotFov()
        {
            for (int i = 0; i < viewDist * 2 + 1; i++)
            {
                for (int j = 0; j < viewDist * 2 + 1; j++)
                {
                    _buttons[i, j].BackgroundImage = null;
                    _buttons[i, j].Enabled = false;
                    _buttons[i, j].Text = string.Empty;
                    _buttons[i, j].BackColor = Color.DarkGray;
                }
            }
        }

        /// <summary>
        /// Updates tableTaskView content with current teams tasks
        /// </summary>
        /// <returns></returns>
        private void RefreshTaskDisplays(object? sender, UpdateTasksEventArgs e)
        {
            tableTaskView.Controls.Clear();

            tableTaskView.RowStyles.Clear();
            tableTaskView.ColumnStyles.Clear();
            tableTaskView.Padding = new Padding(0);
            tableTaskView.Margin = new Padding(0);

            tableTaskView.RowCount = 1;
            tableTaskView.ColumnCount = 5;

            for (int i = 0; i < tableTaskView.ColumnCount; i++)
            {
                tableTaskView.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / 5));
            }

            for (int i = 0; i < e.tasks.Count; i++)
            {
                TaskView t = new TaskView
                    (e.tasks[i].task.GetLength(1), e.tasks[i].task.GetLength(0),
                    e.tasks[i], _gameHandler.GetCurrentPlayer().team);

                tableTaskView.Controls.Add(t.wrap, i, 0);
            }
        }

        /// <summary>
        /// Updates the stored field of view in gameView
        /// </summary>
        /// <returns></returns>
        private void RefreshTable(object? o, UpdateFieldsEventArgs e)
        {
            _viewField = e.gameObjects;
        }

        /// <summary>
        /// Draws field of view of currently playing robot based on the stored fov.
        /// </summary>
        /// <returns></returns>
        private void RefreshViewTable(GameObject[,] field, MapModes mode)
        {
            switch (mode)
            {
                case MapModes.Normal:
                    for (int i = 0; i < viewDist * 2 + 1; i++)
                    {
                        for (int j = 0; j < viewDist * 2 + 1; j++)
                        {
                            if (_buttons[i, j].seen)
                            {
                                _buttons[i, j].BackgroundImage = null;

                                bool isExit = false;
                                GameObject currentField = field[i, j];
                                Robot currentRobot = null!;
                                Box currentBox = null!;
                                TileType type = currentField.TileType();

                                if (currentField is Robot)
                                {
                                    currentRobot = (Robot)currentField;
                                }
                                else if (currentField is Box)
                                {
                                    currentBox = (Box)currentField;
                                }
                                _buttons[i, j].Enabled = true;
                                _buttons[i, j].Text = "";
                                _buttons[i, j].ForeColor = Color.White;
                                int x = currentRobotCoords.X;
                                int y = currentRobotCoords.Y;
                                foreach (Exit e in exits)
                                {
                                    if (e.Coordinate.X == (x + (i - viewDist)) && e.Coordinate.Y == (y + (j - viewDist)))
                                    {
                                        isExit = true;
                                        _buttons[i, j].BackColor = Color.LightGreen;
                                        _buttons[i, j].Text = "Exit";
                                        _buttons[i, j].ForeColor = Color.Black;
                                        switch (e.Direction)
                                        {
                                            case Direction.Up:
                                                _buttons[i, j].Text += "\nA";
                                                break;
                                            case Direction.Down:
                                                _buttons[i, j].Text += "\nV";
                                                break;
                                            case Direction.Left:
                                                _buttons[i, j].Text += "\n<";
                                                break;
                                            case Direction.Right:
                                                _buttons[i, j].Text += "\n>";
                                                break;
                                        }
                                    }
                                }
                                switch (type)
                                {
                                    //draw non Robot and Box types
                                    case TileType.Empty:
                                        if (!isExit)
                                            _buttons[i, j].BackColor = Color.White;
                                        break;
                                    case TileType.Wall:
                                        _buttons[i, j].BackColor = Color.Violet;
                                        break;
                                    case TileType.Obstacle:
                                        _buttons[i, j].BackgroundImage = Properties.Resources.asteroid;
                                        break;
                                    //draw Boxes
                                    case TileType.RedBox:
                                        _buttons[i, j].Text = $"{currentBox.health}";
                                        _buttons[i, j].BackgroundImage = Properties.Resources.redbox;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.BlueBox:
                                        _buttons[i, j].Text = $"{currentBox.health}";
                                        _buttons[i, j].BackgroundImage = Properties.Resources.bluebox;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.YellowBox:
                                        _buttons[i, j].Text = $"{currentBox.health}";
                                        _buttons[i, j].BackgroundImage = Properties.Resources.yellowbox;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.GreenBox:
                                        _buttons[i, j].Text = $"{currentBox.health}";
                                        _buttons[i, j].BackgroundImage = Properties.Resources.greenbox;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    //draw Robots
                                    case TileType.RedRobot:
                                        if (currentRobot != null)
                                        {
                                            Direction d = currentRobot.facing;
                                            switch (d)
                                            {
                                                case Direction.Up:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.red_up;
                                                    break;

                                                case Direction.Down:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.red_down;
                                                    break;

                                                case Direction.Left:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.red_left;
                                                    break;

                                                case Direction.Right:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.red_right;
                                                    break;
                                            }
                                            if (null != currentRobot.Attached)
                                            {
                                                _buttons[i, j].Text += "\nCon";
                                            }
                                        }
                                        _buttons[i, j].BackColor = Color.White;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.BlueRobot:
                                        if (currentRobot != null)
                                        {
                                            Direction d = currentRobot.facing;
                                            switch (d)
                                            {
                                                case Direction.Up:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.blue_up;
                                                    break;

                                                case Direction.Down:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.blue_down;
                                                    break;

                                                case Direction.Left:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.blue_left;
                                                    break;

                                                case Direction.Right:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.blue_right;
                                                    break;
                                            }
                                        }
                                        _buttons[i, j].BackColor = Color.White;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.GreenRobot:
                                        if (currentRobot != null)
                                        {
                                            Direction d = currentRobot.facing;
                                            switch (d)
                                            {
                                                case Direction.Up:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.green_up;
                                                    break;

                                                case Direction.Down:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.green_down;
                                                    break;

                                                case Direction.Left:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.green_left;
                                                    break;

                                                case Direction.Right:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.green_right;
                                                    break;
                                            }
                                        }
                                        _buttons[i, j].BackColor = Color.White;
                                        _buttons[i, j].ForeColor = Color.White;
                                        break;
                                    case TileType.YellowRobot:
                                        if (currentRobot != null)
                                        {
                                            Direction d = currentRobot.facing;
                                            switch (d)
                                            {
                                                case Direction.Up:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.yellow_up;
                                                    break;

                                                case Direction.Down:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.yellow_down;
                                                    break;

                                                case Direction.Left:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.yellow_left;
                                                    break;

                                                case Direction.Right:
                                                    _buttons[i, j].Text = $"{currentRobot.name}";
                                                    _buttons[i, j].BackgroundImage = Properties.Resources.yellow_right;
                                                    break;
                                            }
                                        }
                                        _buttons[i, j].BackColor = Color.White;
                                        _buttons[i, j].ForeColor = Color.Black;
                                        break;
                                }
                            }
                        }
                    }
                    break;

                case MapModes.Groups:
                    Random rng = new Random();
                    int maxGroup = 0;
                    foreach (GameObject go in _viewField)
                    {
                        if (go is Box)
                        {
                            Box currentBox = (Box)go;
                            if (currentBox.ingroup > maxGroup)
                            {
                                maxGroup = currentBox.ingroup;
                            }
                        }
                    }

                    List<Color> groupColors = new List<Color>();

                    for (int i = 0; i < maxGroup; i++)
                    {
                        Color groupCol = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
                        groupColors.Add(groupCol);
                    }

                    for (int i = 0; i < viewDist * 2 + 1; i++)
                    {
                        for (int j = 0; j < viewDist * 2 + 1; j++)
                        {
                            _buttons[i, j].BackgroundImage = null;
                            _buttons[i, j].Text = "";
                            _buttons[i, j].BackColor = Color.White;

                            if (!_viewField[i, j].IsEmpty())
                            {
                                _buttons[i, j].BackColor = Color.LightGray;
                            }

                            if (_viewField[i, j] is Box)
                            {
                                Box currentBox = (Box)_viewField[i, j];
                                if (currentBox.ingroup != 0)
                                {
                                    _buttons[i, j].BackColor = groupColors[currentBox.ingroup - 1];
                                }
                            }



                            //Disabling unseen tiles
                            if (Math.Abs(_buttons[i, j].GridX) + Math.Abs(_buttons[i, j].GridY) > viewDist)
                            {
                                _buttons[i, j].Text = "";
                                _buttons[i, j].BackColor = Color.DarkGray;
                                _buttons[i, j].Enabled = false;
                                _buttons[i, j].BackgroundImage = null;
                            }
                        }
                    }
                    break;
            }


        }

        /// <summary>
        /// Updates teamMessagePanel with the current robot's team messages.
        /// </summary>
        /// <returns></returns>
        private void RefreshMessages()
        {
            teamMessagePanel.Controls.Clear();

            List<RGBModell.modell.structs.Message> msgs =
                _gameHandler.messageHandler.GetTeamMessages
                (_gameHandler.GetCurrentPlayer().team);

            teamMessagePanel.ColumnCount = 2;
            teamMessagePanel.RowCount = 8;

            teamMessagePanel.RowStyles.Clear();
            teamMessagePanel.ColumnStyles.Clear();

            teamMessagePanel.Margin = new Padding(0);
            teamMessagePanel.Padding = new Padding(0);
            teamMessagePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            for (int i = 0; i < teamMessagePanel.ColumnCount; i++)
            {
                teamMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / teamMessagePanel.ColumnCount));
            }
            for (int i = 0; i < teamMessagePanel.RowCount; i++)
            {
                teamMessagePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / teamMessagePanel.RowCount));
            }
            Label msgHeader1 = new Label();
            Label msgHeader2 = new Label();

            msgHeader1.Text = "Message";
            msgHeader2.Text = "Board";
            msgHeader1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            msgHeader2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            msgHeader1.Dock = DockStyle.Fill;
            msgHeader2.Dock = DockStyle.Fill;

            switch (_gameHandler.GetCurrentPlayer().team)
            {
                case Team.Red:
                    msgHeader1.ForeColor = Color.Red;
                    msgHeader2.ForeColor = Color.Red;
                    break;
                case Team.Blue:
                    msgHeader1.ForeColor = Color.Blue;
                    msgHeader2.ForeColor = Color.Blue;
                    break;
                case Team.Green:
                    msgHeader1.ForeColor = Color.Green;
                    msgHeader2.ForeColor = Color.Green;
                    break;
                case Team.Yellow:
                    msgHeader1.ForeColor = Color.Goldenrod;
                    msgHeader2.ForeColor = Color.Goldenrod;
                    break;
            }

            teamMessagePanel.Controls.Add(msgHeader1, 0, 0);
            teamMessagePanel.Controls.Add(msgHeader2, 1, 0);

            int msgNum = 1;
            for (int i = msgs.Count - 1; i > -1; i--)
            {
                Label msgSender = new Label();
                PictureBox msgContent = new PictureBox();
                msgContent.Height = 44;
                msgContent.Width = 44;
                switch (msgs[i].symbol)
                {
                    case Symbol.GoDown:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.godown);
                        break;
                    case Symbol.GoUp:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.goup);
                        break;
                    case Symbol.GoLeft:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.goleft);
                        break;
                    case Symbol.GoRight:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.goright);
                        break;
                    case Symbol.Weld:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.weld);
                        break;
                    case Symbol.Question:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.question);
                        break;
                    case Symbol.Angry:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.angry);
                        break;
                    case Symbol.Smile:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.smile);
                        break;
                    case Symbol.Sad:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.sad);
                        break;
                    case Symbol.Task1:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.task1);
                        break;
                    case Symbol.Task2:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.task2);
                        break;
                    case Symbol.Task3:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.task3);
                        break;
                    case Symbol.Task4:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.task4);
                        break;
                    case Symbol.Task5:
                        msgContent.BackgroundImage = new Bitmap(Properties.Resources.task5);
                        break;

                }

                msgSender.Text = $"{msgs[i].robot.team}, {msgs[i].robot.name}";
                msgSender.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                msgSender.Dock = DockStyle.Fill;

                switch (msgs[i].robot.team)
                {
                    case Team.Red:
                        msgSender.ForeColor = Color.Red;
                        break;
                    case Team.Blue:
                        msgSender.ForeColor = Color.Blue;
                        break;
                    case Team.Green:
                        msgSender.ForeColor = Color.Green;
                        break;
                    case Team.Yellow:
                        msgSender.ForeColor = Color.Goldenrod;
                        break;
                }

                if (msgNum < 8)
                {
                    teamMessagePanel.Controls.Add(msgSender, 0, msgNum);
                    teamMessagePanel.Controls.Add(msgContent, 1, msgNum);
                }
                msgNum++;
            }
        }

        /// <summary>
        /// Deletes current reference of the map
        /// </summary>
        /// <returns></returns>
        private void Map_Closed(object? sender, EventArgs e)
        {
            map = null!;
        }

        /// <summary>
        /// SendButton event handler
        /// </summary>
        /// <returns></returns>
        private void SendButton_Click(object? sender, EventArgs e)
        {
            if (selectedSymbol != Symbol.None)
            {
                _gameHandler.messageHandler.CreateMessage
                (_gameHandler.GetCurrentPlayer(), selectedSymbol);
            }
            RefreshMessages();
        }

        /// <summary>
        /// Function to handle intra-turn part of the game.
        /// </summary>
        /// <returns></returns>
        private void NextRobot(object? sender, EventArgs e)
        {
            if (map != null)
            {
                map.Close();
            }

            DisableRobotFov();
            _timer.Stop();
            remainingTime = 300;
            UpdateTeamColor();
            MessageBox.Show($"Next Player: {_gameHandler.GetCurrentPlayer().team}, {_gameHandler.GetCurrentPlayer().name}");
            currentRobotCoords.X = _gameHandler.GetCurrentPlayer().i;
            currentRobotCoords.Y = _gameHandler.GetCurrentPlayer().j;
            _timer.Start();
            RefreshMessages();
            UpdatePoints();
            SuccessUpdate();
            RefreshViewTable(_viewField, MapModes.Normal);
        }

        /// <summary>
        /// _timer Tick event handler
        /// </summary>
        /// <returns></returns>
        private void RoundTimerTick(object? sender, EventArgs e)
        {
            remainingTime--;
            if (remainingTime < 0)
            {
                _gameHandler.addAction(_gameHandler.GetCurrentPlayer(), selectedTiles, Actions.Wait);
            }
            else
            {
                remaningTimeBar.Value = remainingTime;
                remaningTimeLabel.Text = Convert.ToString(Convert.ToInt32(Math.Floor((double)remainingTime / 10))) + "." + remainingTime % 10;
            }
            UpdateLabels();
        }

        /// <summary>
        /// GridButton Click event handler
        /// </summary>
        /// <returns></returns>
        private void GridButton_Click(object? sender, EventArgs e)
        {
            if (sender is GridButton button)
            {
                if (selectionsNeeded > 0)
                {
                    Coordinate coord = new Coordinate(_gameHandler.GetCurrentPlayer().i + button.GridX, button.GridY + _gameHandler.GetCurrentPlayer().j);
                    selectedTiles.Add(coord);
                    selectionsNeeded--;
                }

                //testLabel.Text = "Player position\nX: " + _gameHandler.GetCurrentPlayer().i + " Y: " + _gameHandler.GetCurrentPlayer().j
                //                + "\nClicked Tile X: " + (button.GridX + _gameHandler.GetCurrentPlayer().i) + " Y: " + (button.GridY + _gameHandler.GetCurrentPlayer().j);
            }
        }

        /// <summary>
        /// Actionbutton Click event handler
        /// </summary>
        /// <returns></returns>
        private void ActionButton_Click(object? sender, EventArgs e)
        {
            //testLabel.Text = "Action Recieved!";
            if (sender is ActionButton abutton)
            {
                selectedAction = abutton.getAction();
                //Actions that only change the buttonlayout
                switch (selectedAction)
                {
                    case Actions.Move:
                        if (!(currentLayout == ButtonLayouts.Move))
                        {
                            SetButtonLayout(ButtonLayouts.Move);
                            currentLayout = ButtonLayouts.Move;
                            selectedAction = Actions.None;
                        }
                        break;

                    case Actions.Rotate:
                        if (!(currentLayout == ButtonLayouts.Rotate))
                        {
                            SetButtonLayout(ButtonLayouts.Rotate);
                            currentLayout = ButtonLayouts.Rotate;
                            selectedAction = Actions.None;
                        }
                        break;
                    case Actions.Cancel:
                        if (!(currentLayout == ButtonLayouts.Default))
                        {
                            SetButtonLayout(ButtonLayouts.Default);
                            currentLayout = ButtonLayouts.Default;
                            selectionsNeeded = 0;
                            selectedTiles.Clear();
                            selectedAction = Actions.None;
                        }
                        break;
                    //actions that interact with the field and end your turn
                    case Actions.Unweld:
                        if (!(currentLayout == ButtonLayouts.Unwelding))
                        {
                            selectedTiles.Clear();
                            selectionsNeeded = 2;
                            SetButtonLayout(ButtonLayouts.Unwelding);
                            currentLayout = ButtonLayouts.Unwelding;
                        }
                        if (selectionsNeeded == 0 && selectedTiles.Count == 2)
                        {
                            alertLabel.Text = "Ready to Unweld!";
                            _gameHandler.addAction(_gameHandler.GetCurrentPlayer(), selectedTiles, selectedAction);
                            SetButtonLayout(ButtonLayouts.Default);
                            currentLayout = ButtonLayouts.Default;
                            selectedAction = Actions.None;
                        }
                        break;
                    default:
                        _gameHandler.addAction(_gameHandler.GetCurrentPlayer(), selectedTiles, selectedAction);
                        if (!(currentLayout == ButtonLayouts.Default))
                        {
                            SetButtonLayout(ButtonLayouts.Default);
                            currentLayout = ButtonLayouts.Default;
                        }
                        selectedAction = Actions.None;

                        break;
                }
            }
        }

        /// <summary>
        /// Symbolbutton click event handler
        /// </summary>
        /// <returns></returns>
        private void SymbolButton_Click(object? sender, EventArgs e)
        {
            if (sender is SymbolButton)
            {
                SymbolButton symbolButton = (SymbolButton)sender;
                selectedSymbol = symbolButton.symbol;
                testLabel.Text = selectedSymbol.ToString();
            }
        }

        /// <summary>
        /// Sets the UI button layout if the action button clicked does not immediately 
        /// do action
        /// </summary>
        /// <returns></returns>
        private void SetButtonLayout(ButtonLayouts layout)
        {
            Button moveButton = new ActionButton(Actions.Move);
            Button rotateButton = new ActionButton(Actions.Rotate);
            Button weldButton = new ActionButton(Actions.Weld);
            Button unWeldButton = new ActionButton(Actions.Unweld);
            Button connectButton = new ActionButton(Actions.Connect);
            Button disConnectButton = new ActionButton(Actions.Disconnect);
            Button waitButton = new ActionButton(Actions.Wait);
            Button moveUpButton = new ActionButton(Actions.MoveUp);
            Button moveDownButton = new ActionButton(Actions.MoveDown);
            Button moveLeftButton = new ActionButton(Actions.MoveLeft);
            Button moveRightButton = new ActionButton(Actions.MoveRight);
            Button rotateLeftButton = new ActionButton(Actions.RotateLeft);
            Button rotateRightButton = new ActionButton(Actions.RotateRight);
            Button cleanButton = new ActionButton(Actions.Clean);
            Button cancelButton = new ActionButton(Actions.Cancel);

            moveButton.Dock = DockStyle.Fill;
            rotateButton.Dock = DockStyle.Fill;
            weldButton.Dock = DockStyle.Fill;
            unWeldButton.Dock = DockStyle.Fill;
            connectButton.Dock = DockStyle.Fill;
            disConnectButton.Dock = DockStyle.Fill;
            waitButton.Dock = DockStyle.Fill;
            moveUpButton.Dock = DockStyle.Fill;
            moveDownButton.Dock = DockStyle.Fill;
            moveLeftButton.Dock = DockStyle.Fill;
            moveRightButton.Dock = DockStyle.Fill;
            rotateLeftButton.Dock = DockStyle.Fill;
            rotateRightButton.Dock = DockStyle.Fill;
            cleanButton.Dock = DockStyle.Fill;
            cancelButton.Dock = DockStyle.Fill;

            moveButton.BackColor = Color.White;
            rotateButton.BackColor = Color.White;
            weldButton.BackColor = Color.White;
            unWeldButton.BackColor = Color.White;
            connectButton.BackColor = Color.White;
            disConnectButton.BackColor = Color.White;
            waitButton.BackColor = Color.White;
            moveUpButton.BackColor = Color.White;
            moveDownButton.BackColor = Color.White;
            moveLeftButton.BackColor = Color.White;
            moveRightButton.BackColor = Color.White;
            rotateLeftButton.BackColor = Color.White;
            rotateRightButton.BackColor = Color.White;
            cleanButton.BackColor = Color.White;
            cancelButton.BackColor = Color.White;

            moveButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            rotateButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            weldButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            unWeldButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            connectButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            disConnectButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            waitButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            moveUpButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            moveDownButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            moveLeftButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            moveRightButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            rotateLeftButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            rotateRightButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            cleanButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            cancelButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            moveButton.Text = "Move";
            rotateButton.Text = "Rotate";
            weldButton.Text = "Weld";
            unWeldButton.Text = "Unweld";
            connectButton.Text = "Connect";
            disConnectButton.Text = "Disconnect";
            waitButton.Text = "Wait";
            moveUpButton.Text = "Up";
            moveDownButton.Text = "Down";
            moveLeftButton.Text = "Left";
            moveRightButton.Text = "Right";
            rotateLeftButton.Text = "Left";
            rotateRightButton.Text = "Right";
            cleanButton.Text = "Clean";
            cancelButton.Text = "Cancel";

            moveButton.Click += ActionButton_Click;
            rotateButton.Click += ActionButton_Click;
            weldButton.Click += ActionButton_Click;
            unWeldButton.Click += ActionButton_Click;
            connectButton.Click += ActionButton_Click;
            disConnectButton.Click += ActionButton_Click;
            waitButton.Click += ActionButton_Click;
            moveUpButton.Click += ActionButton_Click;
            moveDownButton.Click += ActionButton_Click;
            moveLeftButton.Click += ActionButton_Click;
            moveRightButton.Click += ActionButton_Click;
            rotateLeftButton.Click += ActionButton_Click;
            rotateRightButton.Click += ActionButton_Click;
            cleanButton.Click += ActionButton_Click;
            cancelButton.Click += ActionButton_Click;


            switch (layout)
            {
                case ButtonLayouts.Default:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 8;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    actionButtons.Margin = new Padding(0);
                    actionButtons.Padding = new Padding(0);
                    actionButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

                    for (int i = 0; i < actionButtons.ColumnCount; i++)
                    {
                        actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / actionButtons.ColumnCount));
                    }
                    actionButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    actionButtons.Controls.Add(moveButton, 0, 0);
                    actionButtons.Controls.Add(rotateButton, 1, 0);
                    actionButtons.Controls.Add(weldButton, 2, 0);
                    actionButtons.Controls.Add(unWeldButton, 3, 0);
                    actionButtons.Controls.Add(connectButton, 4, 0);
                    actionButtons.Controls.Add(disConnectButton, 5, 0);
                    actionButtons.Controls.Add(cleanButton, 6, 0);
                    actionButtons.Controls.Add(waitButton, 7, 0);

                    break;
                case ButtonLayouts.Move:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 5;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    actionButtons.Margin = new Padding(0);
                    actionButtons.Padding = new Padding(0);
                    actionButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

                    for (int i = 0; i < actionButtons.ColumnCount; i++)
                    {
                        actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / actionButtons.ColumnCount));
                    }
                    actionButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    actionButtons.Controls.Add(moveLeftButton, 0, 0);
                    actionButtons.Controls.Add(moveUpButton, 1, 0);
                    actionButtons.Controls.Add(moveDownButton, 2, 0);
                    actionButtons.Controls.Add(moveRightButton, 3, 0);
                    actionButtons.Controls.Add(cancelButton, 4, 0);

                    break;
                case ButtonLayouts.Rotate:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 3;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    actionButtons.Margin = new Padding(0);
                    actionButtons.Padding = new Padding(0);
                    actionButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

                    for (int i = 0; i < actionButtons.ColumnCount; i++)
                    {
                        actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / actionButtons.ColumnCount));
                    }
                    actionButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    actionButtons.Controls.Add(rotateLeftButton, 0, 0);
                    actionButtons.Controls.Add(rotateRightButton, 1, 0);
                    actionButtons.Controls.Add(cancelButton, 2, 0);

                    break;
                case ButtonLayouts.Unwelding:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 2;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    actionButtons.Margin = new Padding(0);
                    actionButtons.Padding = new Padding(0);
                    actionButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

                    for (int i = 0; i < actionButtons.ColumnCount; i++)
                    {
                        actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / actionButtons.ColumnCount));
                    }
                    actionButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    actionButtons.Controls.Add(unWeldButton, 0, 0);
                    actionButtons.Controls.Add(cancelButton, 1, 0);

                    break;
            }
            ActionButton abtn;
            int count = 0;
            int col = 0;
            foreach (Control c in actionButtons.Controls)
            {
                if (c is ActionButton)
                {
                    abtn = (ActionButton)c;
                    abtn.MouseEnter += Button_MouseEnterLeave;
                    abtn.MouseLeave += Button_MouseEnterLeave;
                    abtn.FlatStyle = FlatStyle.Flat;
                    col = count % 3;
                    switch (col)
                    {
                        case 0:
                            abtn.ForeColor = Color.Red;
                            break;
                        case 1:
                            abtn.ForeColor = Color.Green;
                            break;
                        case 2:
                            abtn.ForeColor = Color.Blue;
                            break;
                    }
                    count++;
                }
            }
        }

        /// <summary>
        /// Sets up the emote panel during initialization
        /// </summary>
        /// <returns></returns>
        private void SetUpSymbolButtons()
        {
            SymbolButton upButton = new SymbolButton(Symbol.GoUp);
            SymbolButton downButton = new SymbolButton(Symbol.GoDown);
            SymbolButton leftButton = new SymbolButton(Symbol.GoLeft);
            SymbolButton rightButton = new SymbolButton(Symbol.GoRight);
            SymbolButton weldButton = new SymbolButton(Symbol.Weld);
            SymbolButton questionButton = new SymbolButton(Symbol.Question);
            SymbolButton angryButton = new SymbolButton(Symbol.Angry);
            SymbolButton smileButton = new SymbolButton(Symbol.Smile);
            SymbolButton sadButton = new SymbolButton(Symbol.Sad);
            SymbolButton taskOneButton = new SymbolButton(Symbol.Task1);
            SymbolButton taskTwoButton = new SymbolButton(Symbol.Task2);
            SymbolButton taskThreeButton = new SymbolButton(Symbol.Task3);
            SymbolButton taskFourButton = new SymbolButton(Symbol.Task4);
            SymbolButton taskFiveButton = new SymbolButton(Symbol.Task5);

            upButton.Dock = DockStyle.Fill;
            downButton.Dock = DockStyle.Fill;
            leftButton.Dock = DockStyle.Fill;
            rightButton.Dock = DockStyle.Fill;
            weldButton.Dock = DockStyle.Fill;
            questionButton.Dock = DockStyle.Fill;
            angryButton.Dock = DockStyle.Fill;
            smileButton.Dock = DockStyle.Fill;
            sadButton.Dock = DockStyle.Fill;
            taskOneButton.Dock = DockStyle.Fill;
            taskTwoButton.Dock = DockStyle.Fill;
            taskThreeButton.Dock = DockStyle.Fill;
            taskFourButton.Dock = DockStyle.Fill;
            taskFiveButton.Dock = DockStyle.Fill;

            upButton.Click += SymbolButton_Click;
            downButton.Click += SymbolButton_Click;
            leftButton.Click += SymbolButton_Click;
            rightButton.Click += SymbolButton_Click;
            weldButton.Click += SymbolButton_Click;
            questionButton.Click += SymbolButton_Click;
            angryButton.Click += SymbolButton_Click;
            smileButton.Click += SymbolButton_Click;
            sadButton.Click += SymbolButton_Click;
            taskOneButton.Click += SymbolButton_Click;
            taskTwoButton.Click += SymbolButton_Click;
            taskThreeButton.Click += SymbolButton_Click;
            taskFourButton.Click += SymbolButton_Click;
            taskFiveButton.Click += SymbolButton_Click;

            upButton.BackgroundImage = Properties.Resources.goup;
            downButton.BackgroundImage = Properties.Resources.godown;
            leftButton.BackgroundImage = Properties.Resources.goleft;
            rightButton.BackgroundImage = Properties.Resources.goright;
            weldButton.BackgroundImage = Properties.Resources.weld;
            questionButton.BackgroundImage = Properties.Resources.question;
            angryButton.BackgroundImage = Properties.Resources.angry;
            smileButton.BackgroundImage = Properties.Resources.smile;
            sadButton.BackgroundImage = Properties.Resources.sad;
            taskOneButton.BackgroundImage = Properties.Resources.task1;
            taskTwoButton.BackgroundImage = Properties.Resources.task2;
            taskThreeButton.BackgroundImage = Properties.Resources.task3;
            taskFourButton.BackgroundImage = Properties.Resources.task4;
            taskFiveButton.BackgroundImage = Properties.Resources.task5;

            symbolLayoutPanel.RowCount = 2;
            symbolLayoutPanel.ColumnCount = 7;
            symbolLayoutPanel.RowStyles.Clear();
            symbolLayoutPanel.ColumnStyles.Clear();

            symbolLayoutPanel.Margin = new Padding(0);
            symbolLayoutPanel.Padding = new Padding(0);
            symbolLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            for (int i = 0; i < symbolLayoutPanel.ColumnCount; i++)
            {
                symbolLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / symbolLayoutPanel.ColumnCount));
            }
            for (int i = 0; i < symbolLayoutPanel.RowCount; i++)
            {
                symbolLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / symbolLayoutPanel.RowCount));
            }

            symbolLayoutPanel.Controls.Add(upButton, 0, 0);
            symbolLayoutPanel.Controls.Add(downButton, 1, 0);
            symbolLayoutPanel.Controls.Add(leftButton, 2, 0);
            symbolLayoutPanel.Controls.Add(rightButton, 3, 0);
            symbolLayoutPanel.Controls.Add(weldButton, 4, 0);
            symbolLayoutPanel.Controls.Add(questionButton, 5, 0);
            symbolLayoutPanel.Controls.Add(angryButton, 6, 0);
            symbolLayoutPanel.Controls.Add(smileButton, 0, 1);
            symbolLayoutPanel.Controls.Add(sadButton, 1, 1);
            symbolLayoutPanel.Controls.Add(taskOneButton, 2, 1);
            symbolLayoutPanel.Controls.Add(taskTwoButton, 3, 1);
            symbolLayoutPanel.Controls.Add(taskThreeButton, 4, 1);
            symbolLayoutPanel.Controls.Add(taskFourButton, 5, 1);
            symbolLayoutPanel.Controls.Add(taskFiveButton, 6, 1);

            foreach (Control c in symbolLayoutPanel.Controls)
            {
                if (c is SymbolButton)
                {
                    SymbolButton current = (SymbolButton)c;
                    current.FlatStyle = FlatStyle.Flat;
                    current.BackColor = Color.White;
                    current.ForeColor = Color.Red;
                    current.MouseEnter += Button_MouseEnterLeave;
                    current.MouseLeave += Button_MouseEnterLeave;
                }
            }
        }

        /// <summary>
        /// Handles both enter and leave events of all buttons for visual effect
        /// </summary>
        /// <returns></returns>
        private void Button_MouseEnterLeave(object? sender, EventArgs e)
        {
            SymbolButton sbtn;
            ActionButton abtn;
            Button btn;
            if (sender is SymbolButton)
            {
                sbtn = (SymbolButton)sender;
                invertButtonColors(sbtn);
            }
            else if (sender is ActionButton)
            {
                abtn = (ActionButton)sender;
                invertButtonColors(abtn);
            }
            else if (sender is Button)
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

        private void invertButtonColors(SymbolButton btn)
        {
            Color tmp = btn.BackColor;
            btn.BackColor = btn.ForeColor;
            btn.ForeColor = tmp;
        }

        private void invertButtonColors(ActionButton btn)
        {
            Color tmp = btn.BackColor;
            btn.BackColor = btn.ForeColor;
            btn.ForeColor = tmp;
        }

        /// <summary>
        /// Updates certain labels every tick
        /// </summary>
        /// <returns></returns>
        private void UpdateLabels()
        {
            if (selectionsNeeded > 0)
            {
                alertLabel.Text = $"Select {selectionsNeeded} more block(s)";
            }
            else
            {
                if (selectedAction == Actions.Unweld)
                {
                    alertLabel.Text = "Ready to unweld!";
                }
                else
                {
                    alertLabel.Text = string.Empty;
                }
            }
            //testLabel.Text = $"{symbolLayoutPanel.Controls[0].Width}, {symbolLayoutPanel.Controls[0].Height}";
            int rounds = _gameHandler.round;
            int moves = _gameHandler.move;
            roundLabel.Text = $"Round {rounds}";
            moveLabel.Text = $"Move {moves}";
        }

        /// <summary>
        /// Sets theme based on current robot's team
        /// </summary>
        /// <returns></returns>
        private void UpdateTeamColor()
        {
            switch (_gameHandler.GetCurrentPlayer().team)
            {
                case Team.Red:
                    tableTaskView.BackColor = redTeamBcgColor;
                    teamMessagePanel.BackColor = redTeamBcgColor;
                    sendButton.BackColor = Color.White;
                    mapButton.BackColor = Color.White;
                    mapmodeNormalButton.BackColor = Color.White;
                    mapmodeGroupButton.BackColor = Color.White;
                    break;
                case Team.Blue:
                    tableTaskView.BackColor = blueTeamBcgColor;
                    teamMessagePanel.BackColor = blueTeamBcgColor;
                    sendButton.BackColor = Color.White;
                    mapButton.BackColor = Color.White;
                    mapmodeNormalButton.BackColor = Color.White;
                    mapmodeGroupButton.BackColor = Color.White;
                    break;
                case Team.Green:
                    tableTaskView.BackColor = greenTeamBcgColor;
                    teamMessagePanel.BackColor = greenTeamBcgColor;
                    sendButton.BackColor = Color.White;
                    mapButton.BackColor = Color.White;
                    mapmodeNormalButton.BackColor = Color.White;
                    mapmodeGroupButton.BackColor = Color.White;
                    break;
                case Team.Yellow:
                    tableTaskView.BackColor = yellowTeamBcgColor;
                    teamMessagePanel.BackColor = yellowTeamBcgColor;
                    sendButton.BackColor = Color.White;
                    mapButton.BackColor = Color.White;
                    mapmodeNormalButton.BackColor = Color.White;
                    mapmodeGroupButton.BackColor = Color.White;
                    break;
            }
        }

        /// <summary>
        /// Updates point table
        /// </summary>
        /// <returns></returns>
        private void UpdatePoints()
        {
            points[0] = _gameHandler.GetTeamPoints(Team.Red);
            points[1] = _gameHandler.GetTeamPoints(Team.Blue);
            points[2] = _gameHandler.GetTeamPoints(Team.Green);
            points[3] = _gameHandler.GetTeamPoints(Team.Yellow);

            labelRedTeamScore.Text = $"{points[0]}";
            labelBlueTeamScore.Text = $"{points[1]}";
            labelGreenTeamScore.Text = $"{points[2]}";
            labelYellowTeamScore.Text = $"{points[3]}";
        }

        private void SuccessUpdate()
        {
            if (_gameHandler.GetCurrentPlayer().actionsucces)
            {
                successBox.BackColor = Color.Green;
            }
            else
            {
                successBox.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// Used in form1 to stop closed games from generating popups
        /// </summary>
        /// <returns></returns>
        public void Hibernate()
        {
            _timer.Tick -= RoundTimerTick;
        }

        private void mapmodeNormalButton_Click(object sender, EventArgs e)
        {
            RefreshViewTable(_viewField, MapModes.Normal);
        }

        private void mapmodeGroupButton_Click(object sender, EventArgs e)
        {
            RefreshViewTable(_viewField, MapModes.Groups);
        }
    }
}
