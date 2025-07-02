namespace MovieApi.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; }
        public string Language { get; set; }
        public int Budget { get; set; }

        // Navigering tillbaka till filmen
        public int MovieId { get; set; } //foreign key,koppling till db detta ger en 1:1-relation, pekar på en specifik rad i Movie-tabellen
        public Movie Movie { get; set; } 
        //navigation prop, används för att komma åt alla Movie-detaljer direkt fårn te x Review, MovieDetails

    }
}
