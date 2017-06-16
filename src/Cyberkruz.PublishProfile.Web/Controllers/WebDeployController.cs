using System;
using Cyberkruz.PublishProfile.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cyberkruz.PublishProfile.Web.Controllers
{
    [Route("api/webdeploy")]
    public class WebDeployController : Controller
    {
        [HttpPost]
        public IActionResult Index(string model)
        {
            WebDeploy result = null;

            try
            {
                result = WebDeploy.FromPublishProfile(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
            return Ok(result);
        }
    }
}
