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
