using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class ReunitePetsDbContext : DbContext
    {
        public ReunitePetsDbContext()
        {
        }

        public ReunitePetsDbContext(DbContextOptions<ReunitePetsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Comment");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.Commenter)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PetId);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.Breed).HasMaxLength(20);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.LastSeen)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
