using Foxflix.Interfaces;
using Foxflix.Models;
using Foxflix.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foxflix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenre _genreRepository;

        public GenresController(IGenre genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> Get()
        {
            var genres = await _genreRepository.GetAllGenresAsync();
            var result = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> Get(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Post([FromBody] GenreDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest();
            }

            var genre = new Genre
            {
                Name = dto.Name
            };

            await _genreRepository.AddGenreAsync(genre);
            dto.Id = genre.Id;

            return CreatedAtAction(nameof(Get), new { id = genre.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            await _genreRepository.DeleteGenreAsync(genre);
            return NoContent();
        }
    }
}