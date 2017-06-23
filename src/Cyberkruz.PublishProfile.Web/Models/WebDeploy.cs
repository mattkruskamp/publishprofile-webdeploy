using System;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cyberkruz.PublishProfile.Web.Models
{
    public class WebDeploy
    {
        public string Server { get; set; }
        
        public string Website { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
        public string Yml
        {
            get { return this.ToYml(); }
        }

        public static WebDeploy FromPublishProfile(string profile, string apiToken = null)
        {
            if(string.IsNullOrWhiteSpace(profile))
                throw new ArgumentNullException("profile");

            var doc = XDocument.Parse(profile);
                      
            var element = (from e in doc.Descendants("publishProfile")
                          where (string)e.Attribute("publishMethod") == "MSDeploy"
                          select new WebDeploy()
                          {
                              Server = ParseServer(e),
                              Website = (string)e.Attribute("msdeploySite"),
                              Username = (string)e.Attribute("userName"),
                              Password = (string)e.Attribute("userPWD")
                          }).FirstOrDefault();

            // secure username and password
            if (!string.IsNullOrEmpty(apiToken))
            {
                element.Username = Task.Run(() => { return EncryptData(element.Username, apiToken); }).Result;
                element.Password = Task.Run(() => { return EncryptData(element.Password, apiToken); }).Result;
            }

            return element;
        }
        
        private static string ParseServer(XElement e)
        {
            string publishUrl = (string)e.Attribute("publishUrl");
            string msdeploySite = (string)e.Attribute("msdeploySite");
            string server = $"https://{publishUrl}/msdeploy.axd?site={msdeploySite}";
            return server;
        }

        public string ToYml()
        {
            var builder = new StringBuilder();
            builder.AppendLine("deploy:");
            builder.AppendLine($"  - provider: WebDeploy");
            builder.AppendLine($"    server: {Server}");
            builder.AppendLine($"    website: {Website}");
            builder.AppendLine($"    username:");
            builder.AppendLine($"      secure: {Username}");
            builder.AppendLine($"    password:");
            builder.AppendLine($"      secure: {Password}");
            return builder.ToString();
        }

        private static async Task<string> EncryptData(string data, string apiToken)
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                string json = await Task.Run(() => JsonConvert.SerializeObject(
                    new { PlainValue = data }
                ));

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                // get the list of roles
                using (var response = await client.PostAsync("https://ci.appveyor.com/api/account/encrypt", httpContent))
                {
                    response.EnsureSuccessStatusCode();
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }

            return result;
        }
    }
}