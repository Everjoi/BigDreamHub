using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MySite.Models;

public partial class BigDreamHdbContext : DbContext
{
    public BigDreamHdbContext()
    {
    }

    public BigDreamHdbContext(DbContextOptions<BigDreamHdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=BigDreamHDb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.HasIndex(e => e.Name, "IX_Author_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Songs).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorSong",
                    r => r.HasOne<Song>().WithMany().HasForeignKey("SongsId"),
                    l => l.HasOne<Author>().WithMany().HasForeignKey("AuthorsId"),
                    j =>
                    {
                        j.HasKey("AuthorsId", "SongsId");
                        j.ToTable("AuthorSong");
                        j.HasIndex(new[] { "SongsId" }, "IX_AuthorSong_SongsId");
                    });
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Song");

            entity.HasMany(d => d.Styles).WithMany(p => p.Songs)
                .UsingEntity<Dictionary<string, object>>(
                    "SongStyle",
                    r => r.HasOne<Style>().WithMany().HasForeignKey("StylesId"),
                    l => l.HasOne<Song>().WithMany().HasForeignKey("SongsId"),
                    j =>
                    {
                        j.HasKey("SongsId", "StylesId");
                        j.ToTable("SongStyle");
                        j.HasIndex(new[] { "StylesId" }, "IX_SongStyle_StylesId");
                    });
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.ToTable("Style");

            entity.HasIndex(e => e.Name, "IX_Style_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
