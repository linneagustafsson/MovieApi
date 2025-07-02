namespace MovieApi.Models
{
    public class Movie
    {
        public int Id { get; set; }              // Primärnyckel
        public string Title { get; set; }        // Filmtitel
        public int Year { get; set; }            // Utgivningsår
        public string Genre { get; set; }        // Genre (normaliseras ev. senare)
        public int Duration { get; set; }        // Längd i minuter

        public ICollection<Actor> Actors { get; set; } = new List<Actor>();// Lista över skådespelare i filmen

    }
}
