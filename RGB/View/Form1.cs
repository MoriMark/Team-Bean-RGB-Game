using RGB.View;

namespace RGB
{
    public partial class Form1 : Form
    {
        private static HelpForm help = null!;
        private static GameView gameView = null!;

        public Form1()
        {
            InitializeComponent();
            help = new HelpForm();

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            help = new HelpForm();
            help.Show();
        }

        private void buttonGameStart_Click(object sender, EventArgs e)
        {
            gameView = new GameView(Convert.ToInt32(numOfRobots.Value), Convert.ToInt32(numOfTeams.Value));
            this.Hide();
            gameView.ShowDialog();
            this.Show();
        }

    }
}