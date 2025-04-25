// MainForm.Designer.cs
using System.Drawing;
using System.Windows.Forms;
using USDTSender.Utils;
using System.Reflection;
using System.IO;

namespace USDTSender
{
    public partial class MainForm
    {
        private void InitializeComponents()
        {
            this.Text = "USDT SENDER - To get the Activation Key, visit our official website by clicking the link on the bottom right.";
            this.ClientSize = new Size(900, 700);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);
            this.MaximizeBox = false;
            this.Icon = IconHelper.LoadIcon("logo.ico");


            InitializeTitleSection();
            InitializeConnectSection();
            InitializeDivider();
            InitializeRecipientSection();
            InitializeAmountSection();
            InitializeNetworkSection();
            InitializeSendButton();
            InitializeHistorySection();
            InitializeProgressBar();
            InitializeLinkLabel();
        }

        private void InitializeTitleSection()
        {
            lblTitle = new Label()
            {
                Text = "USDT SENDER",
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            lblSubtitle = new Label()
            {
                Text = "Connect to to the server",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 70)
            };

            lblSubtitle2 = new Label()
            {
                Text = "to start sending USDT (Tether)",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 90)
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSubtitle);
            this.Controls.Add(lblSubtitle2);
        }

        private void InitializeConnectSection()
        {
            connectPanel = new Panel()
            {
                Location = new Point(20, 110),
                Size = new Size(860, 40)
            };

            btnConnect = new Button()
            {
                Text = "Connect to Server",
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 30),
                Location = new Point(0, 5)
            };
            btnConnect.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
            btnConnect.Click += BtnConnect_Click;

            lblConnected = new Label()
            {
                Text = "Connected",
                Font = new Font("Arial", 10),
                ForeColor = Color.FromArgb(76, 175, 80),
                AutoSize = true,
                Location = new Point(160, 10),
                Visible = false
            };

            connectPanel.Controls.Add(btnConnect);
            connectPanel.Controls.Add(lblConnected);
            this.Controls.Add(connectPanel);
        }

        private void InitializeDivider()
        {
            Panel divider = new Panel()
            {
                BackColor = Color.FromArgb(238, 238, 238),
                Size = new Size(860, 1),
                Location = new Point(20, 160)
            };
            this.Controls.Add(divider);
        }

        private void InitializeRecipientSection()
        {
            Label lblRecipient = new Label()
            {
                Text = "Recipient Address:",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 180)
            };

            txtAddress = new TextBox()
            {
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Size = new Size(860, 30),
                Location = new Point(20, 210),
                PlaceholderText = "Recipient Wallet Address"
            };

            this.Controls.Add(lblRecipient);
            this.Controls.Add(txtAddress);
        }

        private void InitializeAmountSection()
        {
            Label lblAmount = new Label()
            {
                Text = "Amount:",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 260)
            };

            txtAmount = new TextBox()
            {
                Font = new Font("Arial", 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Size = new Size(400, 30),
                Location = new Point(20, 280),
                PlaceholderText = "Amount to send"
            };

            this.Controls.Add(lblAmount);
            this.Controls.Add(txtAmount);
        }

        private void InitializeNetworkSection()
        {
            Label lblNetwork = new Label()
            {
                Text = "Network:",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(440, 260)
            };

            networkPanel = new FlowLayoutPanel()
            {
                Location = new Point(440, 280),
                Size = new Size(440, 30),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            string[] networks = { "BEP20", "TRC20", "PoS", "ERC20", "SOL", "TON", "AVAX" };
            foreach (var network in networks)
            {
                Button btn = new Button()
                {
                    Text = network,
                    Font = new Font("Arial", 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(240, 240, 240),
                    ForeColor = Color.Black,
                    Size = new Size(70, 30),
                    Margin = new Padding(0, 0, 5, 0),
                    Tag = network,
                    Enabled = false
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);

                if (network == "BEP20")
                {
                    btn.BackColor = Color.FromArgb(76, 175, 80);
                    btn.ForeColor = Color.White;
                }

                btn.Click += (sender, e) =>
                {
                    foreach (Button b in networkPanel.Controls)
                    {
                        b.BackColor = Color.FromArgb(240, 240, 240);
                        b.ForeColor = Color.Black;
                    }
                    Button clicked = (Button)sender;
                    clicked.BackColor = Color.FromArgb(76, 175, 80);
                    clicked.ForeColor = Color.White;
                };

                networkPanel.Controls.Add(btn);
            }

            this.Controls.Add(lblNetwork);
            this.Controls.Add(networkPanel);
        }

        private void InitializeSendButton()
        {
            btnSend = new Button()
            {
                Text = "Send",
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 30),
                Location = new Point(780, 330),
                Enabled = false
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += BtnSend_Click;

            this.Controls.Add(btnSend);
        }

        private void InitializeHistorySection()
        {
            lblHistory = new Label()
            {
                Text = "TRANSACTION HISTORY:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(20, 380)
            };

            historyPanel = new FlowLayoutPanel()
            {
                Location = new Point(20, 410),
                Size = new Size(860, 250),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };

            this.Controls.Add(lblHistory);
            this.Controls.Add(historyPanel);
        }

        private void InitializeProgressBar()
        {
            progressBar = new Panel()
            {
                Size = new Size(0, 3),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(76, 175, 80),
                Visible = false
            };
            this.Controls.Add(progressBar);
        }

        private async void InitializeLinkLabel()
        {
            string url = await RemoteDataFetcher.FetchWebsiteUrlAsync();

            LinkLabel linkLabel = new LinkLabel()
            {
                Text = File.Exists(Program.KeyFilePath) ? "✔ Visit Our Website" : "✘ Get Activation Key",
                AutoSize = true,
                BackColor = Color.FromArgb(76, 175, 80),
                LinkBehavior = LinkBehavior.NeverUnderline,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Padding = new Padding(10, 5, 10, 5),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };

            linkLabel.LinkColor = Color.FromArgb(255, 255, 255);
            linkLabel.ActiveLinkColor = Color.FromArgb(255, 255, 255);
            linkLabel.VisitedLinkColor = Color.FromArgb(255, 255, 255);

            linkLabel.Location = new Point(
                this.ClientSize.Width - linkLabel.PreferredWidth - 25,
                this.ClientSize.Height - linkLabel.PreferredHeight - 10
            );

            linkLabel.LinkClicked += (sender, e) =>
            {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not open link: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            };

            this.Controls.Add(linkLabel);
        }

    }
}
