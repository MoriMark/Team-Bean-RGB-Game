using RGB.View;
using Timer = System.Windows.Forms.Timer;

namespace RGB
{
    public partial class Form1 : Form
    {
        private static HelpForm help = null!;
        private static GameView gameView = null!;
        private Timer animTimer;
        private int animTime = 1000;
        private int waitTime = 1000;
        private int animFrame = 0;

        public Form1()
        {
            InitializeComponent();
            help = new HelpForm();

            animTimer = new Timer();
            animTimer.Interval = 16;
            animTimer.Tick += Animate;
            animTimer.Start();
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
            gameView.Show();
            gameView.FormClosing += QuitGame;
        }
        private void QuitGame(object? sender, EventArgs e)
        {
            gameView.Hibernate();
            gameView?.Dispose();
            gameView = null!;
            this.Show();
        }

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
    }
}