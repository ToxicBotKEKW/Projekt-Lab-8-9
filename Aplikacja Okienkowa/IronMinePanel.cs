using Projekt_Lab_8_9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Okienkowa
{
    public partial class IronMinePanel : UserControl
    {
        private MineGame mineGame;

        public IronMinePanel() {
            mineGame = MineGame.GetInstance();
            InitComponent();
        }

        private void InitComponent()
        {
            this.BackgroundImage = Image.FromFile("../../../Resources/mine-1.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Click += (s, v) =>
            {
                string result = mineGame.PointForClick(ResourceType.Iron);

                Point mousePos = this.PointToClient(Cursor.Position);

                Label floatingLabel = new Label
                {
                    Text = result,
                    ForeColor = Color.WhiteSmoke,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Location = new Point(mousePos.X + 10, mousePos.Y - 10),
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };

                this.Controls.Add(floatingLabel);
                floatingLabel.BringToFront();

                System.Windows.Forms.Timer hideTimer = new System.Windows.Forms.Timer
                {
                    Interval = 500
                };

                hideTimer.Tick += (sender2, args2) =>
                {
                    this.Controls.Remove(floatingLabel);
                    floatingLabel.Dispose();
                    hideTimer.Stop();
                    hideTimer.Dispose();
                };

                hideTimer.Start();
            };
        }
    }
}
