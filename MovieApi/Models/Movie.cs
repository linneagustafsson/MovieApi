namespace MovieApi.Models
{
    public class Movie
    {
        public int Id { get; set; }              // Primärnyckel
        public string Title { get; set; }        // Filmtitel
        public int Year { get; set; }            // Utgivningsår
        public string Genre { get; set; }        // Genre (normaliseras ev. senare)
        public int Duration { get; set; }        // Längd i minuter
       
         public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public MovieDetails MovieDetails { get; set; } // Navigering till MovieDetails (1:1-relation)

        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<Review> Reviews { get; set; }  // Navigering till Reviews (1:M-relation)
    }
}
