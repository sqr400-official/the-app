// USDTSender/MainForm.cs
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace USDTSender
{
    public partial class MainForm : Form
    {
        // Control declarations
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblSubtitle2;
        private Panel connectPanel;
        private Button btnConnect;
        private Label lblConnected;
        private TextBox txtAddress;
        private TextBox txtAmount;
        private FlowLayoutPanel networkPanel;
        private Button btnSend;
        private Label lblHistory;
        private FlowLayoutPanel historyPanel;
        private Panel progressBar;
        private System.Windows.Forms.Timer cooldownTimer;

        private string historyFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "USDTSender",
            "history.txt"
        );

        private string transactionsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "USDTSender",
            "transactions.dat"
        );

        public MainForm()
        {
            InitializeComponents();
            DisableControls();
            LoadHistory();
            InitializeCooldownTimer();
            CheckCooldownStatus();
        }

        private void InitializeCooldownTimer()
        {
            cooldownTimer = new System.Windows.Forms.Timer { Interval = 1000 }; // Check every second
            cooldownTimer.Tick += CooldownTimer_Tick;
            cooldownTimer.Start();
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            CheckCooldownStatus();
        }

        private void CheckCooldownStatus()
        {
            if (!File.Exists(transactionsFile)) return;

            var transactions = File.ReadAllLines(transactionsFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(DateTime.Parse)
                .OrderByDescending(dt => dt)
                .ToList();

            // Clean up old transactions (older than 24 hours)
            var recentTransactions = transactions.Where(dt => (DateTime.Now - dt).TotalHours <= 24).ToList();
            if (recentTransactions.Count != transactions.Count)
            {
                File.WriteAllLines(transactionsFile, recentTransactions.Select(dt => dt.ToString("o")));
            }

            if (recentTransactions.Count >= 10)
            {
                var cooldownEnd = recentTransactions[9].AddHours(1);
                var remaining = cooldownEnd - DateTime.Now;

                if (remaining > TimeSpan.Zero)
                {
                    btnSend.Enabled = false;
                    btnSend.Text = $"({remaining:mm\\:ss})";
                    return;
                }
            }

            btnSend.Enabled = true;
            btnSend.Text = "Send";
        }

        private void RecordTransaction()
        {
            var dir = Path.GetDirectoryName(transactionsFile);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.AppendAllText(transactionsFile, DateTime.Now.ToString("o") + Environment.NewLine);
        }
    }
}
