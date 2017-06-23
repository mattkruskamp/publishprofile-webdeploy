using System;
using Cyberkruz.PublishProfile.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cyberkruz.PublishProfile.Web.Controllers
{
    [Route("api/webdeploy")]
    public class WebDeployController : Controller
    {
        [HttpPost]
        public IActionResult Index(string model, string apiToken = null)
        {
            WebDeploy result = null;

            try
            {
                result = WebDeploy.FromPublishProfile(model, apiToken);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
            return Ok(result);
        }
    }
}
