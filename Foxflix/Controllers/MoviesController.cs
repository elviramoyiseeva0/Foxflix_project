using Foxflix.Interfaces;
using Foxflix.Models;
using Foxflix.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foxflix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovie _movieRepository;
        private readonly IGenre _genreRepository;

        public MoviesController(IMovie movieRepository, IGenre genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Get([FromQuery] string? search, [FromQuery] int? genreId)
        {
            IEnumerable<Movie> movies;

            if (!string.IsNullOrEmpty(search))
            {
                movies = await _movieRepository.SearchMoviesByTitleAsync(search);
            }
            else if (genreId.HasValue)
            {
                movies = await _movieRepository.GetMoviesByGenreAsync(genreId.Value);
            }
            else
            {
                movies = await _movieRepository.GetAllMoviesAsync();
            }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                YearOfRelease = movie.YearOfRelease,
                Duration = movie.Duration,
                PosterPath = movie.PosterPath,
                VideoPath = movie.VideoPath,
                Genres = movie.Genres.Select(g => g.Name).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> Post([FromBody] CreateMovieDto dto)
        {
            var genresList = new List<Genre>();
            if (dto.GenreIds != null && dto.GenreIds.Count > 0)
            {
                foreach (var genreId in dto.GenreIds)
                {
                    var genre = await _genreRepository.GetGenreByIdAsync(genreId);
                    if (genre != null)
                    {
                        genresList.Add(genre);
                    }
                }
            }

            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                YearOfRelease = dto.YearOfRelease,
                Duration = dto.Duration,
                PosterPath = dto.PosterPath,
                VideoPath = dto.VideoPath,
                Genres = genresList
            };

            await _movieRepository.AddMovieAsync(movie);

            var createdDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                YearOfRelease = movie.YearOfRelease,
                Duration = movie.Duration,
                PosterPath = movie.PosterPath,
                VideoPath = movie.VideoPath,
                Genres = movie.Genres.Select(g => g.Name).ToList()
            };

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, createdDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieRepository.DeleteMovieAsync(movie);
            return NoContent();
        }
    }
}