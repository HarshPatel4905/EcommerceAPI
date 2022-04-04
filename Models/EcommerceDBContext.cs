using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ecommerce_Project_Api.Models
{
    public partial class EcommerceDBContext : DbContext
    {
        public EcommerceDBContext()
        {
        }

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=EcommerceDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lname");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("mobile");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Roles).HasColumnName("roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
