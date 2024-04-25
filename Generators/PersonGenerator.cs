using Bogus;
using NameParser.Packages;
using NameParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Person = NameParser.Models.Person;

namespace NameParser.Generators
{
    public class PersonGenerator
    {
        public PersonGenerator(RegionsPackage package)
        {
            RegionsPack = package;
        }

        public List<Person> GeneratePeople(string region, int seed, int count, int startAutoIncrement)
        {
            if (!RegionsPack.CheckRegion(region))
            {
                throw new ArgumentException();
            }

            Faker<Person> personFaker = new Faker<Person>(RegionsPack.Regions[region]).UseSeed(seed)
                .RuleFor(p => p.IndexNumber, f => startAutoIncrement++)
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.Adress, f => $"{region}, {f.Address.City()}, {f.Address.StreetSuffix()}, {f.Address.City()}, {f.Address.ZipCode()}")
                .RuleFor(p => p.Country, f => region)
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumberFormat())
                .RuleFor(p => p.ZipCode, f => f.Address.ZipCode())
                .RuleFor(p => p.email, f => f.Internet.Email());

            List<Person> generatedPeople = personFaker.GenerateBetween(count, count);

            return generatedPeople;
        }

        public RegionsPackage RegionsPack { get; private set; }
    }
}
