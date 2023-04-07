using RGB.modell.gameobjects;
using RGB.modell.boxlogic;
using RGB.modell.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RGB.modell;

namespace RGB.View
{
    public partial class GameView : Form
    {
        private GridButton[,]? _buttons;
        private int[] currentCoords;
        private int numOfPlayers;
        private int numOfTeams;
        private int remainingTime = 300;
        private System.Windows.Forms.Timer _timer;

        private Actions selectedAction;
        private GameHandler _gameHandler;

        public GameView(int players, int teams)
        {
            InitializeComponent();
            //Set trivial values
            currentCoords = new int[2]; currentCoords[0] = 0; currentCoords[1] = 0;
            numOfTeams = teams;
            numOfPlayers = players;
            _gameHandler = new GameHandler();
            //Setting up the buttons
            tableLayoutPanelButtons.RowCount = 7;
            tableLayoutPanelButtons.ColumnCount = 7;
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Padding = new Padding(0);
            tableLayoutPanelButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            _buttons = new GridButton[7, 7];

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    _buttons[i, j] = new GridButton(i - 3, j - 3); //-3, so that the middle is 0,0
                    if (Math.Abs(_buttons[i, j].GridX) + Math.Abs(_buttons[i, j].GridY) > 3)
                    {
                        _buttons[i, j].BackColor = Color.Black;
                        _buttons[i, j].Enabled = false;
                    }
                    else
                    {
                        _buttons[i, j].BackColor = Color.White;
                    }
                    _buttons[i, j].Dock = DockStyle.Fill;
                    _buttons[i, j].Click += new EventHandler(GridButton_Click);
                    _buttons[i, j].Padding = new Padding(0);
                    _buttons[i, j].Margin = new Padding(0);

                    tableLayoutPanelButtons.Controls.Add(_buttons[i, j], j, i);
                }
            }

            tableLayoutPanelButtons.RowStyles.Clear();
            tableLayoutPanelButtons.ColumnStyles.Clear();

            for (int i = 0; i < tableLayoutPanelButtons.RowCount; i++)
            {
                tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / 7));
            }
            for (int i = 0; i < tableLayoutPanelButtons.ColumnCount; i++)
            {
                tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / 7));
            }
            //Round timer setup
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += roundTimerTick;
            _timer.Enabled = true;
            //Show Table for the first player
            refreshViewTable();
        }

        private void refreshViewTable()
        {

        }

        private void nextRound()
        {
            remainingTime = 300;
        }

        private void roundTimerTick(object? sender, EventArgs e)
        {
            remainingTime--;
            if (remainingTime < 0)
            {
                nextRound();
            }
            else
            {
                remaningTimeBar.Value = remainingTime;
                remaningTimeLabel.Text = Convert.ToString(Convert.ToInt32(Math.Floor((double)remainingTime / 10))) + "." + remainingTime % 10;
            }
        }

        private void GridButton_Click(object? sender, EventArgs e)
        {
            if (sender is GridButton button)
            {
                testLabel.Text = button.GridX + "," + button.GridY + "\n" + numOfPlayers + "\n" + numOfTeams;
            }
        }
    }
}
