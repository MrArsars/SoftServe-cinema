using Core.Models;
using Core.Models.Movie;
using Core.Models.Session;
using Core.Models.Ticket;
using Core.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CinemaDbContext : IdentityDbContext<User>
    {
        public CinemaDbContext()
        {
        }

        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public virtual DbSet<MovieDto> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<SessionDto> Sessions { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public override DbSet<User> Users { get; set; }
        public virtual DbSet<TicketDto> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Place>() 
                .HasNoKey(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-N96A4PM;Database=CinemaDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;Connect Timeout=10;");
        }
    }
}