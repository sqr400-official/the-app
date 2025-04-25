using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace USDTSender
{
    public class SplashScreen : Form
    {
        private System.Windows.Forms.Timer timer;
        private Label lblTitle;
        private Label lblVersion;
        private Panel progressBar;
        private PictureBox logo;

        public SplashScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.Size = new Size(500, 300);
            this.ShowInTaskbar = false;
            this.Icon = LoadEmbeddedIcon("USDTSender.Resources.logo.ico");

            // Load logo from embedded resources
            logo = new PictureBox()
            {
                Size = new Size(100, 100),
                Location = new Point(200, 30),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = LoadEmbeddedImage("USDTSender.Resources.logo.png")
            };

            lblTitle = new Label()
            {
                Text = "USDT SENDER",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80),
                Size = new Size(480, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 140)
            };

            lblVersion = new Label()
            {
                Text = "Version 5.1.0",
                Font = new Font("Arial", 8),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(220, 190)
            };

            progressBar = new Panel()
            {
                BackColor = Color.FromArgb(76, 175, 80),
                Size = new Size(0, 4),
                Location = new Point(0, 296)
            };

            this.Controls.Add(logo);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblVersion);
            this.Controls.Add(progressBar);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Width < this.Width)
            {
                progressBar.Width += 10;
            }
            else
            {
                timer.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(76, 175, 80)),
                new Rectangle(0, 0, this.Width - 1, this.Height - 1)
            );
        }

        private Icon LoadEmbeddedIcon(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            return new Icon(stream!);
        }

        private Image LoadEmbeddedImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            return Image.FromStream(stream!);
        }
    }
}
