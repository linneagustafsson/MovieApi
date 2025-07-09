using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ActorCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Range(1900, 2025)]
        public int BirthYear { get; set; }
    }
}
