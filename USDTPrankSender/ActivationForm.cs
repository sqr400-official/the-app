using System;
using System.IO;
using System.Windows.Forms;

namespace USDTSender
{
    public partial class ActivationForm : Form
    {
        public ActivationForm()
        {
            InitializeComponent();
            this.AcceptButton = btnActivate;
            this.txtKey.KeyDown += TxtKey_KeyDown;
        }

        private void TxtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnActivate.PerformClick();
            }
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                ShowError("Please enter an activation key");
                return;
            }

            var inputKey = txtKey.Text.Trim();
            var remoteKey = await Program.FetchRemoteKeyAsync();

            if (string.IsNullOrWhiteSpace(remoteKey))
            {

                ShowError($"Could not fetch remote activation key. Try again later.");
                return;
            }

            if (inputKey == remoteKey)
            {
                try
                {
                    var keyDir = Path.GetDirectoryName(Program.KeyFilePath);
                    if (!string.IsNullOrEmpty(keyDir))
                    {
                        Directory.CreateDirectory(keyDir);
                    }

                    File.WriteAllText(Program.KeyFilePath, inputKey);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    ShowError($"Failed to save key: {ex.Message}");
                }
            }
            else
            {
                ShowError("Invalid activation key. Please contact support.");
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Activation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtKey.Focus();
            txtKey.SelectAll();
        }
    }
}
