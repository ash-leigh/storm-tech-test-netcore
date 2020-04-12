using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Services
{
    public static class Gravatar
    {
        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }
        public static async System.Threading.Tasks.Task<string> GetFullNameAsync(string emailAddress)
        {
            var requestUri = GetHash(emailAddress) + ".json";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.gravatar.com/");
                client.DefaultRequestHeaders.Add("User-Agent", "TodoApp");

                try
                {
                    var response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        dynamic json = JsonConvert.DeserializeObject(responseString);
                        var gravatarProfile = new GravatarProfile()
                        {
                            DisplayName = json.entry[0].displayName
                        };
                        return gravatarProfile.DisplayName;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
        }

        public class GravatarProfile
        { 
            public string DisplayName { get; set; }

        }
    }
}

