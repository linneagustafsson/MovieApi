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
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* ---------- N:M  Movie <‑‑ MovieActor ‑‑> Actor ---------- */
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });   // composite PK

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            /* ---------- 1:1  Movie ↔ MovieDetails ---------- */
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MovieDetails)
                .WithOne(md => md.Movie)                      // 
                .HasForeignKey<MovieDetails>(md => md.Id);

            /* ---------- 1:M  Movie ↔ Review ---------- */
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId);
        }
    }
}
