// USDTSender/MainForm.Events.cs
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USDTSender
{
    public partial class MainForm
    {
        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            btnConnect.Text = "Connecting...";

            // Simulate update process
            await Task.Delay(3000);

            lblConnected.Visible = true;
            EnableControls();
            ShowToast("App connected successfully!");
            btnConnect.Text = "Connected";
        }

        private void DisableControls()
        {
            txtAddress.Enabled = false;
            txtAmount.Enabled = false;
            btnSend.Enabled = false;

            foreach (Control control in networkPanel.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = false;
                }
            }
        }

        private void EnableControls()
        {
            txtAddress.Enabled = true;
            txtAmount.Enabled = true;
            btnSend.Enabled = true;

            foreach (Control control in networkPanel.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = true;
                }
            }
        }

        // USDTSender/MainForm.Events.cs
        private async void BtnSend_Click(object sender, EventArgs e)
        {
            // Check cooldown status first
            if (!btnSend.Enabled) return;

            var remoteKey = await Program.FetchRemoteKeyAsync();

            if (string.IsNullOrWhiteSpace(remoteKey))
            {
                MessageBox.Show("Unable to validate license. Please try again later.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(Program.KeyFilePath) || File.ReadAllText(Program.KeyFilePath).Trim() != remoteKey)
            {
                using (var activationForm = new ActivationForm())
                {
                    activationForm.StartPosition = FormStartPosition.CenterParent;
                    activationForm.ShowDialog(this);
                }

                remoteKey = await Program.FetchRemoteKeyAsync();

                if (string.IsNullOrWhiteSpace(remoteKey) ||
                    !File.Exists(Program.KeyFilePath) ||
                    File.ReadAllText(Program.KeyFilePath).Trim() != remoteKey)
                {
                    ShowToast("Invalid activation key. Please contact support.");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                ShowToast("Please enter a wallet address");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out _))
            {
                ShowToast("Please enter a valid amount");
                return;
            }

            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                if (amount > 100000)
                {
                    ShowToast("You can only send up to 100,000 USDT at a time");
                    return;
                }
                if (amount <= 0)
                {
                    ShowToast("Please enter a valid amount");
                    return;
                }
            }

            btnSend.Enabled = false;
            progressBar.Width = 0;
            progressBar.Visible = true;

            for (int i = 0; i <= 100; i += 5)
            {
                progressBar.Width = this.ClientSize.Width * i / 100;
                await Task.Delay(300);
            }

            progressBar.Visible = false;

            string network = "";
            foreach (Button btn in networkPanel.Controls)
            {
                if (btn.BackColor == Color.FromArgb(76, 175, 80))
                {
                    network = btn.Tag.ToString();
                    break;
                }
            }

            string hash = Guid.NewGuid().ToString("N").Substring(0, 16);
            string entry = $"{DateTime.Now}: Sent {txtAmount.Text} USDT to {txtAddress.Text} via {network} - TX HASH: 0x{hash}";

            AddHistoryEntry(entry);

            string dir = Path.GetDirectoryName(historyFile);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            File.AppendAllLines(historyFile, new[] { entry });

            // Record the successful transaction
            RecordTransaction();

            ShowToast("Transaction completed!");

            txtAddress.Text = "";
            txtAmount.Text = "";

            if (networkPanel.Controls.Count > 0)
            {
                foreach (Button b in networkPanel.Controls)
                {
                    b.BackColor = Color.FromArgb(240, 240, 240);
                    b.ForeColor = Color.Black;
                }

                Button first = networkPanel.Controls.OfType<Button>().FirstOrDefault();
                if (first != null)
                {
                    first.BackColor = Color.FromArgb(76, 175, 80);
                    first.ForeColor = Color.White;
                }
            }

            // Check if we need to enable cooldown
            CheckCooldownStatus();
        }

        private void AddHistoryEntry(string entry)
        {
            Label lblEntry = new Label()
            {
                Text = entry,
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 10)
            };

            historyPanel.Controls.Add(lblEntry);
            historyPanel.Controls.SetChildIndex(lblEntry, 0);
        }

        private void LoadHistory()
        {
            if (File.Exists(historyFile))
            {
                var lines = File.ReadAllLines(historyFile);
                foreach (var line in lines.Reverse())
                {
                    AddHistoryEntry(line);
                }
            }
        }

        private async void ShowToast(string message)
        {
            Label toast = new Label()
            {
                Text = message,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10),
                Location = new Point(this.Width / 2 - 100, 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(toast);
            toast.BringToFront();
            await Task.Delay(2500);
            this.Controls.Remove(toast);
            toast.Dispose();
        }
    }
}