namespace MovieApi.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; } // Foreign key to Movie
        public Movie Movie { get; set; }// Navigation property to Movie

        public int ActorId { get; set; } // Foreign key to Actor
        public Actor Actor { get; set; }// Navigation property to Actor
       // public string? Role { get; set; }
    }
}
