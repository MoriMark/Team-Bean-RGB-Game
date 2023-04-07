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
            setButtonLayout(ButtonLayouts.Default);
            //Setting up the grid buttons
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

        private void actionButton_Click(object? sender, EventArgs e)
        {
            testLabel.Text = "Action Recieved!";
            if (sender is  ActionButton abutton) 
            { 
                Actions pendingAction = abutton.getAction();

                switch (pendingAction)
                {
                    case Actions.Move:
                        setButtonLayout(ButtonLayouts.Move);
                        break;
                    case Actions.Rotate:
                        setButtonLayout(ButtonLayouts.Rotate);
                        break;
                    case Actions.Cancel:
                        setButtonLayout(ButtonLayouts.Default);
                        break;

                    default:
                        _gameHandler.doAction(pendingAction);
                        break;
                }
            }
        }



        private void setButtonLayout(ButtonLayouts layout)
        {
            switch (layout)
            {
                case ButtonLayouts.Default:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 6;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    Button moveButton = new ActionButton(Actions.Move);
                    Button rotateButton = new ActionButton(Actions.Rotate);
                    Button weldButton = new ActionButton(Actions.Weld);
                    Button unWeldButton = new ActionButton(Actions.Unweld);
                    Button connectButton = new ActionButton(Actions.Connect);
                    Button disConnectButton = new ActionButton(Actions.Disconnect);

                    moveButton.Dock = DockStyle.Fill;
                    rotateButton.Dock = DockStyle.Fill;
                    weldButton.Dock = DockStyle.Fill;
                    unWeldButton.Dock = DockStyle.Fill;
                    connectButton.Dock = DockStyle.Fill;
                    disConnectButton.Dock = DockStyle.Fill;

                    moveButton.BackColor = Color.White;
                    rotateButton.BackColor = Color.White;
                    weldButton.BackColor = Color.White;
                    unWeldButton.BackColor = Color.White;
                    connectButton.BackColor = Color.White;
                    disConnectButton.BackColor = Color.White;

                    moveButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    rotateButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    weldButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    unWeldButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    connectButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    disConnectButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                    moveButton.Text = "Move";
                    rotateButton.Text = "Rotate";
                    weldButton.Text = "Weld";
                    unWeldButton.Text = "Unweld";
                    connectButton.Text = "Connect";
                    disConnectButton.Text = "Disconnect";

                    moveButton.Click += actionButton_Click;
                    rotateButton.Click += actionButton_Click;
                    weldButton.Click += actionButton_Click;
                    unWeldButton.Click += actionButton_Click;
                    connectButton.Click += actionButton_Click;
                    disConnectButton.Click += actionButton_Click;

                    actionButtons.Margin = new Padding(0);
                    actionButtons.Padding = new Padding(0);
                    actionButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

                    for (int i = 0; i < actionButtons.ColumnCount; i++)
                    {
                        actionButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / actionButtons.ColumnCount));
                    }
                    actionButtons.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    actionButtons.Controls.Add(moveButton,          0, 0);
                    actionButtons.Controls.Add(rotateButton,        1, 0);
                    actionButtons.Controls.Add(weldButton,          2, 0);
                    actionButtons.Controls.Add(unWeldButton,        3, 0);
                    actionButtons.Controls.Add(connectButton,       4, 0);
                    actionButtons.Controls.Add(disConnectButton,    5, 0);

                    break;
                case ButtonLayouts.Move:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 5;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    Button moveUpButton = new ActionButton(Actions.Move);
                    Button moveDownButton = new ActionButton(Actions.Move);
                    Button moveLeftButton = new ActionButton(Actions.Move);
                    Button moveRightButton = new ActionButton(Actions.Move);
                    Button cancelMoveButton = new ActionButton(Actions.Cancel);

                    moveUpButton.Dock = DockStyle.Fill;
                    moveDownButton.Dock = DockStyle.Fill;
                    moveLeftButton.Dock = DockStyle.Fill;
                    moveRightButton.Dock = DockStyle.Fill;
                    cancelMoveButton.Dock = DockStyle.Fill;

                    moveUpButton.BackColor = Color.White;
                    moveDownButton.BackColor = Color.White;
                    moveLeftButton.BackColor = Color.White;
                    moveRightButton.BackColor = Color.White;
                    cancelMoveButton.BackColor = Color.White;
                    

                    moveUpButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    moveDownButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    moveLeftButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    moveRightButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    cancelMoveButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                    moveUpButton.Text = "Up";
                    moveDownButton.Text = "Down";
                    moveLeftButton.Text = "Left";
                    moveRightButton.Text = "Right";
                    cancelMoveButton.Text = "Cancel";

                    moveUpButton.Click += actionButton_Click;
                    moveDownButton.Click += actionButton_Click;
                    moveLeftButton.Click += actionButton_Click;
                    moveRightButton.Click += actionButton_Click;
                    cancelMoveButton.Click += actionButton_Click;

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
                    actionButtons.Controls.Add(cancelMoveButton, 4, 0);

                    break;
                case ButtonLayouts.Rotate:
                    actionButtons.Controls.Clear();
                    actionButtons.ColumnCount = 3;
                    actionButtons.RowCount = 1;
                    actionButtons.RowStyles.Clear();
                    actionButtons.ColumnStyles.Clear();

                    Button rotateLeftButton = new ActionButton(Actions.Move);
                    Button rotateRightButton = new ActionButton(Actions.Move);
                    Button cancelRotateButton = new ActionButton(Actions.Cancel);

                    rotateLeftButton.Dock = DockStyle.Fill;
                    rotateRightButton.Dock = DockStyle.Fill;
                    cancelRotateButton.Dock = DockStyle.Fill;

                    rotateLeftButton.BackColor = Color.White;
                    rotateRightButton.BackColor = Color.White;
                    cancelRotateButton.BackColor = Color.White;

                    rotateLeftButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    rotateRightButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    cancelRotateButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                    rotateLeftButton.Text = "Left";
                    rotateRightButton.Text = "Right";
                    cancelRotateButton.Text = "Cancel";

                    rotateLeftButton.Click += actionButton_Click;
                    rotateRightButton.Click += actionButton_Click;
                    cancelRotateButton.Click += actionButton_Click;

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
                    actionButtons.Controls.Add(cancelRotateButton, 2, 0);

                    break;
            }
        }
    }
}
