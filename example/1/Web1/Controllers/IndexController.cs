using Maomi.MQ;
using Microsoft.AspNetCore.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        private readonly ILogger<IndexController> _logger;

        public IndexController(ILogger<IndexController> logger, IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _messagePublisher = messagePublisher;
        }

        [HttpGet("publish")]
        public async Task<string> Publisher()
        {
            for(var i = 0; i < 100; i++)
            {
                await _messagePublisher.PublishAsync("web1",new TestEvent
                {
                    Message = i.ToString()
                });
            }

            return "ok";
        }
    }
}