using ChisteConsumerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChisteConsumerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JokesController : ControllerBase
    {
        private readonly ChisteDbContext _context;

        public JokesController(ChisteDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAllJokes()
        {
            var jokes = await _context.Jokes.ToListAsync();
            return Ok(jokes);
        }
    }
}
