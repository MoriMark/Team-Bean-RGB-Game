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
using RGB.modell.structs;
using RGB.modell.game_logic;

namespace RGB.View
{
    public partial class GameView : Form
    {
        private GridButton[,] _buttons = null!;
        private int selectionsNeeded;
        private int numOfPlayers;
        private int numOfTeams;
        private int totalRobots;
        private int tableSize;
        private int remainingTime = 300;
        private System.Windows.Forms.Timer _timer;

        private Symbol selectedSymbol;
        private Coordinate currentRobotCoords;
        private List<Coordinate> selectedTiles;
        private ButtonLayouts currentLayout;
        private Actions selectedAction;
        private GameHandler _gameHandler;

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
            setButtonLayout(ButtonLayouts.Default);
            currentLayout = ButtonLayouts.Default;
            currentRobotCoords = new Coordinate();
            selectedSymbol = Symbol.None;

            //Subscribe to gameHandler events
            _gameHandler.robotChanged += NextRobot;
            setUpSymbolButtons();
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
                        _buttons[i, j].BackColor = Color.DarkGray;
                        _buttons[i, j].Enabled = false;
                    }
                    else
                    {
                        _buttons[i, j].BackColor = Color.White;
                    }
                    _buttons[i, j].Dock = DockStyle.Fill;
                    _buttons[i, j].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    _buttons[i, j].ForeColor = Color.White;
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
            _gameHandler.StartGame();
            NextRobot(null, EventArgs.Empty);
        }

        private void setUpSymbolButtons()
        {
            SymbolButton upButton = new SymbolButton(Symbol.GoUp);
            SymbolButton downButton = new SymbolButton(Symbol.GoDown);
            SymbolButton leftButton = new SymbolButton(Symbol.GoLeft);
            SymbolButton rightButton = new SymbolButton(Symbol.GoRight);
            SymbolButton givingBoxButton = new SymbolButton(Symbol.GivingBoxToYou);
            SymbolButton givingCaravanButton = new SymbolButton(Symbol.GivingCaravanToYou);
            SymbolButton askForBoxesButton = new SymbolButton(Symbol.ObligateItFromYou);
            SymbolButton smileyButton = new SymbolButton(Symbol.Smiley);
            SymbolButton gloomyButton = new SymbolButton(Symbol.Gloomy);
            SymbolButton delightedButton = new SymbolButton(Symbol.Delighted);
            SymbolButton authButton = new SymbolButton(Symbol.Authorative);
            SymbolButton taskOneButton = new SymbolButton(Symbol.Task1);
            SymbolButton taskTwoButton = new SymbolButton(Symbol.Task2);
            SymbolButton taskThreeButton = new SymbolButton(Symbol.Task3);

            upButton.Dock = DockStyle.Fill;
            downButton.Dock = DockStyle.Fill;
            leftButton.Dock = DockStyle.Fill;
            rightButton.Dock = DockStyle.Fill;
            givingBoxButton.Dock = DockStyle.Fill;
            givingCaravanButton.Dock = DockStyle.Fill;
            askForBoxesButton.Dock = DockStyle.Fill;
            smileyButton.Dock = DockStyle.Fill;
            gloomyButton.Dock = DockStyle.Fill;
            delightedButton.Dock = DockStyle.Fill;
            authButton.Dock = DockStyle.Fill;
            taskOneButton.Dock = DockStyle.Fill;
            taskTwoButton.Dock = DockStyle.Fill;
            taskThreeButton.Dock = DockStyle.Fill;

            upButton.Click += SymbolButton_Click;
            downButton.Click += SymbolButton_Click;
            leftButton.Click += SymbolButton_Click;
            rightButton.Click += SymbolButton_Click;
            givingBoxButton.Click += SymbolButton_Click;
            givingCaravanButton.Click += SymbolButton_Click;
            askForBoxesButton.Click += SymbolButton_Click;
            smileyButton.Click += SymbolButton_Click;
            gloomyButton.Click += SymbolButton_Click;
            delightedButton.Click += SymbolButton_Click;
            authButton.Click += SymbolButton_Click;
            taskOneButton.Click += SymbolButton_Click;
            taskTwoButton.Click += SymbolButton_Click;
            taskThreeButton.Click += SymbolButton_Click;

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
            symbolLayoutPanel.Controls.Add(givingBoxButton, 4, 0);
            symbolLayoutPanel.Controls.Add(givingCaravanButton, 5, 0);
            symbolLayoutPanel.Controls.Add(askForBoxesButton, 6, 0);
            symbolLayoutPanel.Controls.Add(smileyButton, 0, 1);
            symbolLayoutPanel.Controls.Add(gloomyButton, 1, 1);
            symbolLayoutPanel.Controls.Add(delightedButton, 2, 1);
            symbolLayoutPanel.Controls.Add(authButton, 3, 1);
            symbolLayoutPanel.Controls.Add(taskOneButton, 4, 1);
            symbolLayoutPanel.Controls.Add(taskTwoButton, 5, 1);
            symbolLayoutPanel.Controls.Add(taskThreeButton, 6, 1);


        }

        private void AskForBoxesButton_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void refreshViewTable(Int32 x, Int32 y)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    GameObject currentField = null!;
                    Robot currentRobot = null!;
                    Box currentBox = null!;
                    TileType type;
                    if ((x + (i - 3) > -1 && y + (j - 3) > -1) &&
                        (x + (i - 3) < tableSize && y + (j - 3) < tableSize))
                    {
                        currentField = _gameHandler.GetFieldValue(x + (i - 3), y + (j - 3));
                        type = currentField.TileType();
                    }
                    else
                    {
                        type = TileType.Wall;
                    }
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
                    switch (type)
                    {
                        //draw non Robot and Box types
                        case TileType.Empty:
                            _buttons[i, j].BackColor = Color.White;
                            break;
                        case TileType.Wall:
                            _buttons[i, j].BackColor = Color.Black;
                            break;
                        case TileType.Obstacle:
                            _buttons[i, j].BackColor = Color.Gray;
                            break;
                        //draw Boxes
                        case TileType.RedBox:
                            _buttons[i, j].Text = $"{currentBox.health}";
                            _buttons[i, j].BackColor = Color.Red;
                            break;
                        case TileType.BlueBox:
                            _buttons[i, j].Text = $"{currentBox.health}";
                            _buttons[i, j].BackColor = Color.Blue;
                            break;
                        case TileType.YellowBox:
                            _buttons[i, j].Text = $"{currentBox.health}";
                            _buttons[i, j].BackColor = Color.Yellow;
                            _buttons[i, j].ForeColor = Color.Black;
                            break;
                        case TileType.GreenBox:
                            _buttons[i, j].Text = $"{currentBox.health}";
                            _buttons[i, j].BackColor = Color.Green;
                            break;
                        //draw Robots
                        case TileType.RedRobot:
                            if (currentRobot != null)
                            {
                                Direction d = currentRobot.facing;
                                switch (d)
                                {
                                    case Direction.Up:
                                        _buttons[i, j].Text = "A\nI";
                                        break;

                                    case Direction.Down:
                                        _buttons[i, j].Text = "I\nV";
                                        break;

                                    case Direction.Left:
                                        _buttons[i, j].Text = "<-";
                                        break;

                                    case Direction.Right:
                                        _buttons[i, j].Text = "->";
                                        break;
                                }
                            }
                            _buttons[i, j].BackColor = Color.Red;
                            break;
                        case TileType.BlueRobot:
                            if (currentRobot != null)
                            {
                                Direction d = currentRobot.facing;
                                switch (d)
                                {
                                    case Direction.Up:
                                        _buttons[i, j].Text = "A\nI";
                                        break;

                                    case Direction.Down:
                                        _buttons[i, j].Text = "I\nV";
                                        break;

                                    case Direction.Left:
                                        _buttons[i, j].Text = "<-";
                                        break;

                                    case Direction.Right:
                                        _buttons[i, j].Text = "->";
                                        break;
                                }
                            }
                            _buttons[i, j].BackColor = Color.Blue;
                            break;
                        case TileType.GreenRobot:
                            if (currentRobot != null)
                            {
                                Direction d = currentRobot.facing;
                                switch (d)
                                {
                                    case Direction.Up:
                                        _buttons[i, j].Text = "A\nI";
                                        break;

                                    case Direction.Down:
                                        _buttons[i, j].Text = "I\nV";
                                        break;

                                    case Direction.Left:
                                        _buttons[i, j].Text = "<-";
                                        break;

                                    case Direction.Right:
                                        _buttons[i, j].Text = "->";
                                        break;
                                }
                            }
                            _buttons[i, j].BackColor = Color.Green;
                            break;
                        case TileType.YellowRobot:
                            if (currentRobot != null)
                            {
                                Direction d = currentRobot.facing;
                                switch (d)
                                {
                                    case Direction.Up:
                                        _buttons[i, j].Text = "A\nI";
                                        break;

                                    case Direction.Down:
                                        _buttons[i, j].Text = "I\nV";
                                        break;

                                    case Direction.Left:
                                        _buttons[i, j].Text = "<-";
                                        break;

                                    case Direction.Right:
                                        _buttons[i, j].Text = "->";
                                        break;
                                }
                            }
                            _buttons[i, j].BackColor = Color.Yellow;
                            break;
                    }

                    //Disabling unseen tiles
                    if (Math.Abs(_buttons[i, j].GridX) + Math.Abs(_buttons[i, j].GridY) > 3)
                    {
                        _buttons[i, j].Text = "";
                        _buttons[i, j].BackColor = Color.DarkGray;
                        _buttons[i, j].Enabled = false;
                    }
                }
            }
        }

        private void NextRobot(object? sender, EventArgs e)
        {
            currentRobotCoords.X = _gameHandler.GetCurrentPlayer().i;
            currentRobotCoords.Y = _gameHandler.GetCurrentPlayer().j;
            remainingTime = 300;
            refreshViewTable(currentRobotCoords.X, currentRobotCoords.Y);
        }

        private void roundTimerTick(object? sender, EventArgs e)
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
            updateLabels();
        }

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

        private void actionButton_Click(object? sender, EventArgs e)
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
                            setButtonLayout(ButtonLayouts.Move);
                            currentLayout = ButtonLayouts.Move;
                            selectedAction = Actions.None;
                        }
                        break;

                    case Actions.Rotate:
                        if (!(currentLayout == ButtonLayouts.Rotate))
                        {
                            setButtonLayout(ButtonLayouts.Rotate);
                            currentLayout = ButtonLayouts.Rotate;
                            selectedAction = Actions.None;
                        }
                        break;
                    case Actions.Cancel:
                        if (!(currentLayout == ButtonLayouts.Default))
                        {
                            setButtonLayout(ButtonLayouts.Default);
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
                            setButtonLayout(ButtonLayouts.Unwelding);
                            currentLayout = ButtonLayouts.Unwelding;
                        }
                        if (selectionsNeeded == 0 && selectedTiles.Count == 2)
                        {
                            alertLabel.Text = "Ready to Unweld!";
                            _gameHandler.addAction(_gameHandler.GetCurrentPlayer(), selectedTiles, selectedAction);
                            setButtonLayout(ButtonLayouts.Default);
                            currentLayout = ButtonLayouts.Default;
                            selectedAction = Actions.None;
                        }
                        break;
                    default:
                        _gameHandler.addAction(_gameHandler.GetCurrentPlayer(), selectedTiles, selectedAction);
                        if (!(currentLayout == ButtonLayouts.Default))
                        {
                            setButtonLayout(ButtonLayouts.Default);
                            currentLayout = ButtonLayouts.Default;
                        }
                        selectedAction = Actions.None;
                        break;
                }
            }
        }

        private void SymbolButton_Click(object? sender, EventArgs e)
        {
            if (sender is SymbolButton)
            {
                SymbolButton symbolButton = (SymbolButton)sender;
                testLabel.Text = symbolButton.symbol.ToString();
            }
        }

        private void setButtonLayout(ButtonLayouts layout)
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

            moveButton.Click += actionButton_Click;
            rotateButton.Click += actionButton_Click;
            weldButton.Click += actionButton_Click;
            unWeldButton.Click += actionButton_Click;
            connectButton.Click += actionButton_Click;
            disConnectButton.Click += actionButton_Click;
            waitButton.Click += actionButton_Click;
            moveUpButton.Click += actionButton_Click;
            moveDownButton.Click += actionButton_Click;
            moveLeftButton.Click += actionButton_Click;
            moveRightButton.Click += actionButton_Click;
            rotateLeftButton.Click += actionButton_Click;
            rotateRightButton.Click += actionButton_Click;
            cleanButton.Click += actionButton_Click;
            cancelButton.Click += actionButton_Click;

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
        }

        private void updateLabels()
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
            int rounds = _gameHandler.round;
            int moves = _gameHandler.move;
            roundLabel.Text = $"Round {rounds}";
            moveLabel.Text = $"Move {moves}";
            testLabel.Text = string.Empty;
            foreach (RobotAction ra in _gameHandler.actionsThisTurn)
            {
                //testLabel.Text += $"{ra.action.ToString()} ";
            }
            //testLabel.Text += $"\nCurrent position\nX:{_gameHandler.GetCurrentPlayer().i} Y: {_gameHandler.GetCurrentPlayer().j}";
        }
    }
}
