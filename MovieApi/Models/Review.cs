namespace MovieApi.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string ReviewerName { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }  // Mellan 1–5

        // Foreign key till filmen
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
