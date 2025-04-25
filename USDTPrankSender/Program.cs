using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Serialization;

namespace USDTSender
{
    internal static class Program
    {
        // Local path to store the activation key
        public static string KeyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "USDTSender",
            "key.txt");

        // Replace this with your actual raw GitHub JSON URL
        private static readonly string RemoteKeyUrl = "https://raw.githubusercontent.com/sqr400-official/the-app/main/activation.json";

        // Class to match the structure of the remote JSON

        public class KeyModel
        {
            [JsonPropertyName("key")]
            public required string Key { get; set; }
        }


        // Fetch activation key from GitHub and sanitize it
        public static async Task<string> FetchRemoteKeyAsync()
        {
            using var httpClient = new HttpClient();
            var url = "https://raw.githubusercontent.com/sqr400-official/the-app/main/activation.json";


            try
            {
                var response = await httpClient.GetStringAsync(url);
                var keyObj = JsonSerializer.Deserialize<KeyModel>(response);

                return keyObj?.Key ?? string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Couldn't fetch activation key: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Show splash screen first
                using (var splash = new SplashScreen())
                {
                    splash.ShowDialog();
                }

                // Then show main form
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
