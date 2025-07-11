using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;


namespace MovieApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _context.Actors
                .Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToListAsync();

            return Ok(actors);
        }

        // GET: api/actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return NotFound();

            return Ok(new ActorDto
            {
                Id = actor.Id,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            });
        }
        // POST: api/actors
        [HttpPost]
        public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto dto)
        {
            var actor = new Actor { Name = dto.Name, BirthYear = dto.BirthYear };
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            var resultDto = new ActorDto
            {
                Id = actor.Id,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };

            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, resultDto);

        }
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId, [FromBody] MovieActorCreateDto dto)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return NotFound($"Movie with id {movieId} not found.");

            var actor = await _context.Actors.FindAsync(actorId);
            if (actor == null) return NotFound($"Actor with id {actorId} not found.");

            // Kontrollera om kopplingen redan finns
            var existing = await _context.MovieActors
                .FirstOrDefaultAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);

            if (existing != null)
            {
                return BadRequest("This actor is already assigned to the movie.");
            }

            var movieActor = new MovieActor
            {
                MovieId = movieId,
                ActorId = actorId,
                Role = dto.Role
            };

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            var movieActorDto = new MovieActorDto
            {
                MovieId = movieActor.MovieId,
                ActorId = movieActor.ActorId,
                Role = movieActor.Role
            };

            return CreatedAtRoute("GetMovieDetails", new { id = movieId }, movieActorDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorUpdateDto dto)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return NotFound();

            actor.Name = dto.Name;
            actor.BirthYear = dto.BirthYear;

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
