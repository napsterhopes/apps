using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

#nullable disable

namespace CoreBackend.Models
{
    public partial class SiemensTaskContext : DbContext
    {
        public SiemensTaskContext()
        {
        }

        public SiemensTaskContext(DbContextOptions<SiemensTaskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WebImage> WebImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<WebImage>(entity =>
            {
                entity.HasKey(e => e.WebImgId);

                entity.Property(e => e.Picture).IsRequired();

                entity.Property(e => e.WebImgName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
