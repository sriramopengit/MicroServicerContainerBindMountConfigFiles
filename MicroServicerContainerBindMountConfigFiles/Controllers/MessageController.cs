using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MicroServicerContainerBindMountConfigFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // The configuration file is fetched dynamically based on the environment variable DOCKER_ENV in Program.cs
            // This allows for different configurations to be loaded based on the environment
            var message = _configuration["AppSettings:Message"] ?? "No message found";
            return Ok(new { Message = message });
        }
    }
}
