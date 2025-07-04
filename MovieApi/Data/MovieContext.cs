using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
    public class MovieContext: DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Actor> Actors { get; set; }
       
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
