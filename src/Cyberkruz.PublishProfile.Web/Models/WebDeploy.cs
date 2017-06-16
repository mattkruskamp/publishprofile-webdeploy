using System;
using System.Text;
using System.Xml.Linq;
using System.Linq;

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

        public static WebDeploy FromPublishProfile(string profile)
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
    }
}