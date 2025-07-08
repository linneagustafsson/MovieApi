using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;

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

    }
}
