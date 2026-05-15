using ChisteFetcherAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChisteFetcherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JokesController : ControllerBase
    {
        private readonly IChisteService _ChisteService;
        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public JokesController(IChisteService jokeService, RabbitMqPublisher rabbitMqPublisher)
        {
            _ChisteService = jokeService;
            _rabbitMqPublisher = rabbitMqPublisher;
        }
        
        [HttpGet("random")]
        public async Task<IActionResult> GetAndSendJoke()
        {
            var joke = await _ChisteService.GetRandomJokeAsync();
            if (joke == null) return StatusCode(500, "No se pudo obtener un chiste.");

            await _rabbitMqPublisher.PublishJokeAsync(joke);
            return Ok(new { message = "Chiste publicado en RabbitMQ", joke = joke.Value });
        }
    }
}
