using Core.Models;
using Core.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {
        }

        public CinemaDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<MovieDto> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<SessionDto> Sessions { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TicketDto> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>() 
                .HasNoKey(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=ARSARS;Initial Catalog=CinemaDB;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False");
        }
    }
}