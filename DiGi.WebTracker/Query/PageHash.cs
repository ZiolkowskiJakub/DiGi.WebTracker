using System.Security.Cryptography;
using System.Text;

namespace DiGi.WebTracker
{
    public static partial class Query
    {
        public static async Task<string> PageHash(HttpClient client, string url)
        {
            try
            {
                string @string = await client.GetStringAsync(url);
                using MD5 md5 = MD5.Create();
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(@string));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Error fetching page: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
