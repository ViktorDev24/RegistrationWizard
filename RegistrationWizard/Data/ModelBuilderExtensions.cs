using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Country A" },
            new Country { Id = 2, Name = "Country B" }
        );

        modelBuilder.Entity<Province>().HasData(
            new Province { Id = 1, Name = "Province A1", CountryId = 1 },
            new Province { Id = 2, Name = "Province A2", CountryId = 1 },
            new Province { Id = 3, Name = "Province A3", CountryId = 1 },
            new Province { Id = 4, Name = "Province B1", CountryId = 2 },
            new Province { Id = 5, Name = "Province B2", CountryId = 2 }
        );
    }
}
