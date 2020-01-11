using System;
using Xunit;
using Cyberkruz.PublishProfile.Web.Models;
using System.Text;

namespace Cyberkruz.PublishProfile.Web.Models.Tests
{
    public class WebDeployTests
    {
        [Fact]
        public void FromPublishProfile_NullXml_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => WebDeploy.FromPublishProfile(null));
            Assert.Throws<ArgumentNullException>(() => WebDeploy.FromPublishProfile(""));
            Assert.Throws<ArgumentNullException>(() => WebDeploy.FromPublishProfile(" "));
        }

        [Fact]
        public void FromPublishProfile_ValidXml_ReturnsObject()
        {
            var profile = BuildPublishProfile();
            var server = "https://super-cool-service.scm.azurewebsites.net:443/msdeploy.axd?site=super-cool-service";
            var websiteName = "super-cool-service";
            var username = "$super-cool-service";
            var password = "dUknNiskKSkdiKmnskdfSfSDKSDFssdfkjsSDKJsdfkj";

            var result = WebDeploy.FromPublishProfile(profile);

            Assert.Equal(server, result.Server);
            Assert.Equal(websiteName, result.Website);
            Assert.Equal(username, result.Username);
            Assert.Equal(password, result.Password);
        }

        [Fact]
        public void FromPublishProfile_ToYml_ReturnsYml()
        {
            var yml = BuildYml();
            var deploy = new WebDeploy()
            {
                Server = "bleh",
                Website = "www.site.com",
                Username = "username",
                Password = "password"
            };

            var result = deploy.ToYml();

            Assert.Equal(yml, result);
        }

        string BuildYml()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("deploy:");
            builder.AppendLine("  - provider: WebDeploy");
            builder.AppendLine("    server: bleh");
            builder.AppendLine("    website: www.site.com");
            builder.AppendLine("    username:");
            builder.AppendLine("      secure: username");
            builder.AppendLine("    password:");
            builder.AppendLine("      secure: password");
            return builder.ToString();
        }

        string BuildPublishProfile()
        {
string publish = "<publishData><publishProfile profileName=\"super-cool-service - Web Deploy\" publishMethod=\"MSDeploy\" publishUrl=\"super-cool-service.scm.azurewebsites.net:443\" msdeploySite=\"super-cool-service\" userName=\"$super-cool-service\" userPWD=\"dUknNiskKSkdiKmnskdfSfSDKSDFssdfkjsSDKJsdfkj\" destinationAppUrl=\"http://super-cool-service.azurewebsites.net\" SQLServerDBConnectionString=\"\" mySQLDBConnectionString=\"\" hostingProviderForumLink=\"\" controlPanelLink=\"\" webSystem=\"WebSites\"><databases /></publishProfile><publishProfile profileName=\"super-cool-service - FTP\" publishMethod=\"FTP\" publishUrl=\"ftp://waws-prod-bay-069.ftp.azurewebsites.windows.net/site/wwwroot\" ftpPassiveMode=\"True\" userName=\"super-cool-service\\$super-cool-service\" userPWD=\"dUknNiskKSkdiKmnskdfSfSDKSDFssdfkjsSDKJsdfkj\" destinationAppUrl=\"http://super-cool-service.azurewebsites.net\" SQLServerDBConnectionString=\"\" mySQLDBConnectionString=\"\" hostingProviderForumLink=\"\" controlPanelLink=\"\" webSystem=\"WebSites\"><databases /></publishProfile></publishData>";
            return publish;
        }
    }
}
