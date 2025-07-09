namespace MovieApi.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BirthYear { get; set; }

        // N:M till filmer 
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
