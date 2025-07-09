using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movieDtos = await _context.Movies
        .Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Year = m.Year,
            Genre = m.Genre,
            Duration = m.Duration
        })
        .ToListAsync();

            return Ok(movieDtos);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movies
          .Where(m => m.Id == id)
          .Select(m => new MovieDto
          {
              Id = m.Id,
              Title = m.Title,
              Year = m.Year,
              Genre = m.Genre,
              Duration = m.Duration
          })
          .FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }
        // GET: api/movies/id
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id)
        {
            var movie = await _context.Movies
        .Include(m => m.MovieDetails)
        .Include(m => m.Actors)
        .Include(m => m.Reviews)
        .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDetailDto = new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails?.Synopsis,
                Language = movie.MovieDetails?.Language,
                Budget = movie.MovieDetails?.Budget ?? 0,
                Actors = movie.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList(),
                Reviews = movie.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList()
            };

            return Ok(movieDetailDto);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            if(!MovieExists(id))
                return NotFound();
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            movie.Title = movieUpdateDto.Title;
            movie.Year = movieUpdateDto.Year;
            movie.Genre = movieUpdateDto.Genre;
            movie.Duration = movieUpdateDto.Duration;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Movies

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto movieCreateDto)
        {

            var movie = new Movie
            {
                Title = movieCreateDto.Title,
                Year = movieCreateDto.Year,
                Genre = movieCreateDto.Genre,
                Duration = movieCreateDto.Duration

            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
            };

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }
}
