using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieCreateDto
    {
        [Required] //gör att fältet måste skickas med POST
        public string Title { get; set; } = null!;

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public int Year { get; set; }

        [Required] //gör att fältet måste skickas med POST
        public string Genre { get; set; } = null!;

        [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes")]
        public int Duration { get; set; }
    }
}
