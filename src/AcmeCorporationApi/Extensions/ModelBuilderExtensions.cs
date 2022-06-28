using AcmeCorporationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorporationApi.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonDto>().HasData(
                new PersonDto
                {
                    Id = 1,
                    Name = "Sergio",
                    Age = 45,
                    Document = "59087066C"
                },
                new PersonDto
                {
                    Id = 2,
                    Name = "Carmen",
                    Age = 44,
                    Document = "40596167V"
                }
            );
        }
    }
}