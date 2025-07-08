using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieUpdateDto
    {
        [Required]
        public string Title { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        public string Genre { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }
    }
}
