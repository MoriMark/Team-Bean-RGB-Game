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

namespace RGB.View
{
    public partial class GameView : Form
    {
        public GameView()
        {
            InitializeComponent();
            Box a = new Box(0, 1, BoxColor.Yellow);
            Box b = new Box(0,0, BoxColor.Green);
            Box cbox = new Box(1,1, BoxColor.Blue);
            
            BoxGroup g = new BoxGroup(a, b);
            g.AddBox(a, cbox);
            List<Box> boxlist = g.boxes;
            for (int i = 0; i < boxlist.Count; i++)
            {
                
                    textBox1.Text += boxlist[i].id.ToString();
                
            }
            BoxColor[,] c = g.ConvertToMatrix();
            string s;
            for(int i=0; i<c.GetLength(0); i++)
            {
                for(int j=0; j<c.GetLength(1); j++)
                {
                    textBox1.Text += c[i,j].ToString();
                }
            }
        }

        private void GameView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
