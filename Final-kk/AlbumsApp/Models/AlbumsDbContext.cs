using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlbumsApp.Models
{
    public class AlbumsDbContext : IdentityDbContext<User>
    {
        public AlbumsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // call base class version to setup Indentity relations:
            base.OnModelCreating(modelBuilder);

            // Seed studios:
            modelBuilder.Entity<Studio>().HasData(
                new Studio() { StudioId = 1, Name = "Abbey Road Studios", Address = "3 Abbey Rd", City = "London", ZipCode = "11017", Url = "https://www.abbeyroad.com/" },
                new Studio() { StudioId = 2, Name = "Columbia Studios", Address = "799 Seventh Avenue", City = "New York", ZipCode = "10019", Url = "http://www.columbiarecords.com/" },
                new Studio() { StudioId = 3, Name = "Sunset Sound Recorders", Address = "6650 Sunset Blvd", City = "Hollywood", ZipCode = "90028", Url = "https://www.sunsetsound.com/" },
                new Studio() { StudioId = 4, Name = "Sun Studio", Address = "706 Union Ave", City = "Memphis", ZipCode = "38103", Url = "https://www.sunstudio.com/" },
                new Studio() { StudioId = 5, Name = "Capitol Studios", Address = "1750 North Vine Street", City = "Hollywood", ZipCode = "90028", Url = "https://www.capitolstudios.com/" }
                );

            // Seed albums:
            modelBuilder.Entity<Album>().HasData(
                new Album() { AlbumId = 1, Name = "Rubber Soul", Rating = 9.5, YearProduced = 1965, StudioId = 1},
                new Album() { AlbumId = 2, Name = "The White Album", Rating = 9.9, YearProduced = 1968, StudioId = 1 },
                new Album() { AlbumId = 3, Name = "Bob Dylan", Rating = 9, YearProduced = 1962, StudioId = 2 },
                new Album() { AlbumId = 4, Name = "The Times They Are a-Changin\'", Rating = 8.9, YearProduced = 1964, StudioId = 2 },
                new Album() { AlbumId = 5, Name = "Pet Sounds", Rating = 9.8, YearProduced = 1966, StudioId = 3 },
                new Album() { AlbumId = 6, Name = "Surfin\' USA", Rating = 7.5, YearProduced = 1963, StudioId = 5 },
                new Album() { AlbumId = 7, Name = "Elvis Presley", Rating = 10, YearProduced = 1956, StudioId = 4 }
                );
        }
    }
}
