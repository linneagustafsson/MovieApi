using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class MovieDetails
    {
        [Key, ForeignKey("Movie")]
        public int Id { get; set; } // Primärnyckel,OCH foreign key till Movie.Id

        public string Synopsis { get; set; }
        public string Language { get; set; }
        public int Budget { get; set; }

        // Navigering tillbaka till filmen
        public int MovieId { get; set; } //foreign key,koppling till db detta ger en 1:1-relation, pekar på en specifik rad i Movie-tabellen
        public Movie Movie { get; set; } 
        //navigation prop, används för att komma åt alla Movie-detaljer direkt fårn te x Review, MovieDetails

    }
}
