using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class RoleDto
    {

        [Required]
        public string Role { get; set; }
    }
}
