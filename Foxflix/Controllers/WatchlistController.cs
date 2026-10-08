using Foxflix.Interfaces;
using Foxflix.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Foxflix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlist _watchlistRepository;

        public WatchlistController(IWatchlist watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var movies = await _watchlistRepository.GetUserWatchlistAsync(userId);
            var result = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                YearOfRelease = m.YearOfRelease,
                Duration = m.Duration,
                PosterPath = m.PosterPath,
                VideoPath = m.VideoPath,
                Genres = m.Genres.Select(g => g.Name).ToList()
            });

            return Ok(result);
        }

        [HttpPost("{movieId}")]
        public async Task<IActionResult> Post(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _watchlistRepository.AddAsync(userId, movieId);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _watchlistRepository.RemoveAsync(userId, movieId);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}