using Bogus;
using CodeEvents.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeEvents.Api.Data
{
    public class SeedData
    {
        private static CodeEventsApiContext db;

        public static async Task InitAsync(CodeEventsApiContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            db = context;


            if (await db.CodeEvent.AnyAsync()) return;

            var faker = new Faker("sv");
            var events = new List<CodeEvent>();

            for (int i = 0; i < 50; i++)
            {
                events.Add(new CodeEvent
                {
                    Name = faker.Company.CompanySuffix() + faker.Random.Word(),
                    EventDate = DateTime.Now.AddDays(faker.Random.Int(-20, 20)),
                    Location = new Location()
                    {
                        Address = faker.Address.StreetAddress(),
                        CityTown = faker.Address.City(),
                        StateProvince = faker.Address.State(),
                        PostalCode = faker.Address.ZipCode(),
                        Country = faker.Address.Country()
                    },
                    Length = 1,
                    Lectures = new Lecture[]
                    {
                            new Lecture
                            {
                              Title = faker.Commerce.ProductName(),
                              Level = 100,
                              Speaker = new Speaker
                              {
                                FirstName = faker.Name.FirstName(),
                                LastName = faker.Name.LastName(),
                                BlogUrl = faker.Internet.Url(),
                                Company = faker.Company.CompanyName(),
                                CompanyUrl = faker.Internet.Url(),
                                GitHub = faker.Internet.Url()
                              }
                          },
                            new Lecture
                            {
                              Title = faker.Commerce.ProductName(),
                              Level = 100,
                              Speaker = new Speaker
                              {
                                FirstName = faker.Name.FirstName(),
                                LastName = faker.Name.LastName(),
                                BlogUrl = faker.Internet.Url(),
                                Company = faker.Company.CompanyName(),
                                CompanyUrl = faker.Internet.Url(),
                                GitHub = faker.Internet.Url()
                              }
                            }
                    }
                });

            }

            db.AddRange(events);
            await db.SaveChangesAsync();

        }

    }
}
