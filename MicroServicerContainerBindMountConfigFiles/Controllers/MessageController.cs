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
            var message = _configuration["AppSettings:Message"] ?? "No message found";
            return Ok(new { Message = message });
        }
    }
}
