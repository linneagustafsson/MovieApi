using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ReviewCreateDto
    {

        [Required]
        public string ReviewerName { get; set; }

        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}

