using AcmeCorporationApi.Extensions;
using Microsoft.EntityFrameworkCore;


namespace AcmeCorporationApi.Models
{
    public class AcmeCorporationContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=AcmeCorporation;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Age)
                    .IsRequired();

                entity.Property(e => e.Document)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Seed();
        }
    }
}
