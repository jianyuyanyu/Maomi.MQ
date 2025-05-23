using Maomi.MQ;
using Microsoft.AspNetCore.Mvc;
using ConsumerWeb.Models;

namespace ConsumerWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexController : ControllerBase
{
    private readonly IMessagePublisher _messagePublisher;

    public IndexController(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    [HttpGet("publish")]
    public async Task<string> Publisher()
    {
        for (var i = 0; i < 1; i++)
        {
            await _messagePublisher.PublishAsync(exchange: string.Empty, routingKey: "ConsumerWeb", message: new TestEvent
            {
                Id = i
            }, properties =>
            {
                properties.Expiration = "6000";
            });
        }

        return "ok";
    }
}