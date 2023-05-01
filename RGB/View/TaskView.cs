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

namespace RGB.View
{
    public class TaskView
    {
        private int width;
        private int height;
        public TableLayoutPanel wrap { get; private set; }
        public TableLayoutPanel _view { get; private set; }


        public TaskView(int width, int height, Task task)
        { 
            this.width = width;
            this.height = height;
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
            _view.ColumnCount = width;
            _view.RowCount = height;
            for (int i = 0; i < _view.ColumnCount; i++)
            { _view.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _view.ColumnCount)); }
            for (int i = 0; i < _view.RowCount; i++)
            { _view.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _view.RowCount)); }

            wrap.Controls.Add( _view,0,0);
            
            SetTask(task);
        }

        private void SetTask(Task task)
        {
            Label details = new Label();
            details.Dock = DockStyle.Fill;
            details.Padding = new Padding(0); details.Margin = new Padding(0);
            details.Text = $"{task.direction}\n{task.expiration}";
            wrap.Controls.Add( details,1,0);

            for (int i = 0; i < _view.ColumnCount; i++)
            {
                for (int j = 0; j < _view.RowCount; j++)
                {
                    PictureBox tile = new PictureBox();
                    tile.Dock = DockStyle.Fill;
                    switch (task.task[i,j])
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
                            tile.BackColor = Color.White;
                            _view.Controls.Add(tile, j, i);
                            break;
                    }
                }
            }
        }
    }
}
