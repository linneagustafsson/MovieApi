using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;

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
    }
}
