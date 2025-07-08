using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Extensions
{
    public static class SeedData
    {
        public static void EnsureSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieContext>();

            // Skapa databasen om den inte finns
            context.Database.EnsureCreated();

            if (!context.Movies.Any())
            {
                // Skapa sample actors
                var actor1 = new Actor { Name = "Tom Hanks", BirthYear = 1956 };
                var actor2 = new Actor { Name = "Meryl Streep", BirthYear = 1949 };

                // Skapa sample movies
                var movie1 = new Movie
                {
                    Title = "Forrest Gump",
                    Year = 1994,
                    Genre = "Drama",
                    Duration = 142,
                    Actors = new List<Actor> { actor1 }
                };

                var movie2 = new Movie
                {
                    Title = "The Iron Lady",
                    Year = 2011,
                    Genre = "Biography",
                    Duration = 105,
                    Actors = new List<Actor> { actor2 }
                };

                // Skapa MovieDetails
                movie1.MovieDetails = new MovieDetails
                {
                    Synopsis = "Life is like a box of chocolates...",
                    Language = "English",
                    Budget = 55000000
                };

                movie2.MovieDetails = new MovieDetails
                {
                    Synopsis = "Story of Margaret Thatcher...",
                    Language = "English",
                    Budget = 11000000
                };

                // Skapa reviews
                movie1.Reviews = new List<Review>
            {
                new Review { ReviewerName = "Alice", Comment = "Amazing movie!", Rating = 5 },
                new Review { ReviewerName = "Bob", Comment = "Very touching", Rating = 4 }
            };

                movie2.Reviews = new List<Review>
            {
                new Review { ReviewerName = "Charlie", Comment = "Inspiring!", Rating = 4 }
            };

                // Lägg till till context
                context.Movies.AddRange(movie1, movie2);

                // Spara ändringar
                context.SaveChanges();
            }
        }
    }
    }
