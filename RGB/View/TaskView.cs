using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.View
{
    public class TaskView
    {
        private int size;
        public TableLayoutPanel _view { get; private set; }


        public TaskView(int size) 
        { 
            this.size = size;
            _view = new TableLayoutPanel();
            _view.Dock = DockStyle.Fill;
            _view.RowCount = size;
            _view.ColumnCount = size;
            for (int i = 0; i < size; i++) 
            { 
                _view.RowStyles.Add(new RowStyle(SizeType.Percent, 100/size));
                _view.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/size));
            }
            for (int i = 0; i < size; i++) 
            { 
                for (int j = 0; j < size; j++)
                {
                    PictureBox tile = new PictureBox();
                    tile.Dock = DockStyle.Fill;
                    tile.BackColor = Color.Blue;        //placeholder value
                    _view.Controls.Add(tile, i, j);
                }
            }
        }

        public void setTask()   //Task will be the argument
        {

        }
    }
}
