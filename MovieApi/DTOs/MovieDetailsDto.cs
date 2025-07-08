namespace MovieApi.DTOs
{
    public class MovieDetailsDto
    {  // Filmdata
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }

        // MovieDetails
        public string Synopsis { get; set; }
        public string Language { get; set; }
        public int Budget { get; set; }

        // Lista recensioner
        public List<ReviewDto> Reviews { get; set; }

        // Lista skådespelare
        public List<ActorDto> Actors { get; set; }
    }
}
