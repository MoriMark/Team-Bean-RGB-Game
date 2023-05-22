using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Task = RGBModell.modell.structs.Task;
using BoxColor = RGBModell.modell.enums.BoxColor;
using RGBModell.modell.enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RGB.View
{
    public class TaskView
    {
        private int width;
        private int height;
        private Team team;
        public TableLayoutPanel wrap { get; private set; }
        public TableLayoutPanel _view { get; private set; }
        public TableLayoutPanel _info { get; private set; }


        public TaskView(int width, int height, Task task, Team team)
        { 
            this.width = width;
            this.height = height;
            this.team = team;
            wrap = new TableLayoutPanel();
            wrap.Dock = DockStyle.Fill;
            wrap.Padding = new Padding(0);
            wrap.Margin = new Padding(0);
            wrap.ColumnCount = 2;
            wrap.RowCount = 1;
            for (int i = 0; i < wrap.ColumnCount; i++) 
            { wrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / wrap.ColumnCount)); }
            wrap.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / wrap.RowCount));
            
            _view = new TableLayoutPanel();
            _view.Dock = DockStyle.Fill;
            _view.Padding = new Padding(0);
            _view.Margin = new Padding(0);
            _view.ColumnCount = 3;
            _view.RowCount = 3;
            for (int i = 0; i < _view.ColumnCount; i++)
            { _view.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _view.ColumnCount)); }
            for (int i = 0; i < _view.RowCount; i++)
            { _view.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _view.RowCount)); }

            _info = new TableLayoutPanel();
            _info.Dock = DockStyle.Fill;
            _info.Padding = new Padding(0);
            _info.Margin = new Padding(0);
            _info.ColumnCount = 1;
            _info.RowCount = 4;
            for (int i = 0; i < _info.ColumnCount; i++)
            { _info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _info.ColumnCount)); }
            for (int i = 0; i < _info.RowCount; i++)
            { _info.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _info.RowCount)); }

            wrap.Controls.Add( _view,0,0);
            wrap.Controls.Add(_info, 1, 0);
            
            SetTask(task);
        }

        private void SetTask(Task task)
        {
            /*
            Label details = new Label();
            details.Dock = DockStyle.Fill;
            details.Padding = new Padding(0); details.Margin = new Padding(0);
            details.Text = $"Exit: {task.direction}\nTurns left: {task.expiration}";
            details.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            details.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            details.Dock = DockStyle.Fill;

            switch(team)
            {
                case Team.Red:
                    details.ForeColor = Color.Red;
                    break;
                case Team.Green:
                    details.ForeColor = Color.Green;
                    break;
                case Team.Blue:
                    details.ForeColor = Color.Blue;
                    break;
                case Team.Yellow:
                    details.ForeColor = Color.Goldenrod;
                    break;
            }

            wrap.Controls.Add( details,1,0);
            */

            Label tLeft = new Label();
            Label tLeftCount = new Label();
            Label exit = new Label();
            Label exitDir = new Label();

            tLeft.Text = "Turns Left:";
            tLeft.Font = new Font("Segoe UI", 10);

            tLeftCount.Text = $"{task.expiration}";
            tLeftCount.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tLeftCount.Anchor = AnchorStyles.None;

            exit.Text = "Exit:";
            exit.Font = new Font("Segoe UI", 10);

            exitDir.Text = $"{task.direction}";
            exitDir.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            exitDir.Anchor = AnchorStyles.None;

            switch (team)
            {
                case Team.Red:
                    tLeft.ForeColor = Color.Red;
                    tLeftCount.ForeColor = Color.Red;
                    exit.ForeColor = Color.Red;
                    exitDir.ForeColor = Color.Red;
                    break;
                case Team.Green:
                    tLeft.ForeColor = Color.Green;
                    tLeftCount.ForeColor = Color.Green;
                    exit.ForeColor = Color.Green;
                    exitDir.ForeColor = Color.Green;
                    break;
                case Team.Blue:
                    tLeft.ForeColor = Color.Blue;
                    tLeftCount.ForeColor = Color.Blue;
                    exit.ForeColor = Color.Blue;
                    exitDir.ForeColor = Color.Blue;
                    break;
                case Team.Yellow:
                    tLeft.ForeColor = Color.Goldenrod;
                    tLeftCount.ForeColor = Color.Goldenrod;
                    exit.ForeColor = Color.Goldenrod;
                    exitDir.ForeColor = Color.Goldenrod;
                    break;
            }

            _info.Controls.Add(tLeft,0,0);
            _info.Controls.Add(tLeftCount, 0, 1);
            _info.Controls.Add(exit,0,2);
            _info.Controls.Add(exitDir, 0, 3);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    PictureBox tile = new PictureBox();
                    tile.Dock = DockStyle.Fill;
                    switch (task.task[i, j])
                    {
                        case BoxColor.Blue:
                            tile.BackColor = Color.Blue;
                            _view.Controls.Add(tile, j, i);
                            break;
                        case BoxColor.Green:
                            tile.BackColor = Color.Green;
                            _view.Controls.Add(tile, j, i);
                            break;
                        case BoxColor.Red:
                            tile.BackColor = Color.Red;
                            _view.Controls.Add(tile, j, i);
                            break;
                        case BoxColor.Yellow:
                            tile.BackColor = Color.Yellow;
                            _view.Controls.Add(tile, j, i);
                            break;
                        case BoxColor.NoColor:
                            tile.BackColor = Color.Transparent;
                            _view.Controls.Add(tile, j, i);
                            break;
                    }
                }
            }
        }
    }
}
