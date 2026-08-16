using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.DataAccess.Context
{
    public class MusicContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-5UHGBDGV;initial catalog=JwtMusicCaseDb;integrated security=True;" +
                "trust server certificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<ListeningHistory>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ListeningHistory>()
                .HasOne(x => x.Song)
                .WithMany()
                .HasForeignKey(x => x.SongId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Playlist and AppUser
            modelBuilder.Entity<Playlist>()
                .HasOne(x => x.User) // Each Playlist has one User
                .WithMany(x => x.Playlists) // One User can have many Playlists
                .HasForeignKey(x => x.UserId) // UserId is the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Do not delete the User when a Playlist is deleted

            // Configure the composite primary key for PlaylistSong
            modelBuilder.Entity<PlaylistSong>()
                .HasKey(x => new { x.PlaylistId, x.SongId }); // PlaylistId + SongId must be unique together

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(x => x.Playlist)
                .WithMany(x => x.PlaylistSongs)
                .HasForeignKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(x => x.Song)
                .WithMany(x => x.PlaylistSongs)
                .HasForeignKey(x => x.SongId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<ListeningHistory> ListeningHistories { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs  { get; set; }
    }
}
