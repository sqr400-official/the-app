using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace USDTSender.Utils
{
    public static class RemoteDataFetcher
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> FetchWebsiteUrlAsync()
        {
            try
            {
                string remoteUrl = "https://raw.githubusercontent.com/sqr400-official/the-app/refs/heads/main/website.json"; // Replace with your real link
                var response = await client.GetStringAsync(remoteUrl);
                using JsonDocument doc = JsonDocument.Parse(response);
                return doc.RootElement.GetProperty("url").GetString();
            }
            catch
            {
                return "https://wa.me/+13134875056"; // Fallback
            }
        }
    }
}
