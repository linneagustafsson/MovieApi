using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewsController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("/api/movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId)
        {
            var movie = await _context.Movies.Include(m => m.Reviews)
                                             .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null) return NotFound();

            var reviewDtos = movie.Reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating
            });

            return Ok(reviewDtos);
        }

        [HttpPost("/api/movies/{movieId}/reviews")]
        public async Task<ActionResult<ReviewDto>> PostReviewForMovie(int movieId, ReviewCreateDto dto)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return NotFound();

            var review = new Review
            {
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                Movie = movie
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Skapa en ReviewDto att returnera
            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            };

            return CreatedAtAction(nameof(GetReviewsForMovie), new { movieId = movieId }, reviewDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 NoContent
        }


    }
}
