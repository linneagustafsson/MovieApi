using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ActorUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1900, 2100)]
        public int BirthYear { get; set; }
    }
}

