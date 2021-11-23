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

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__AppUser__536C85E5DB397204");

                entity.ToTable("AppUser");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PetId);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_COMMENT_APPUSER_USERNAME");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.Breed).HasMaxLength(20);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.LastSeen).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PET_USERID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
