using System;
using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetsLifeDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PetsLifeDbContext>
    {
        protected override void Seed(PetsLifeDbContext context)
        {
            var owners = new []
            {
                new PetOwner { FirstName = "Ernest", LastName = "Hemingway", EmailAddress = "hemingway@petslife.com" },
                new PetOwner { FirstName = "Mark", LastName = "Twain", EmailAddress = "twain@petslife.com" },
                new PetOwner { FirstName = "Charles", LastName = "Dickens", EmailAddress = "dickens@petslife.com" },
                new PetOwner { FirstName = "Wiliam", LastName = "Shakespeare", EmailAddress = "shakespeare@petslife.com" },
                new PetOwner { FirstName = "Leo", LastName = "Tolstoy", EmailAddress = "tolstoy@petslife.com" },
                new PetOwner { FirstName = "Oscar", LastName = "Wilde", EmailAddress = "wilde@petslife.com" },
                new PetOwner { FirstName = "John", LastName = "Steinbeck", EmailAddress = "steinbeck@petslife.com" },
                new PetOwner { FirstName = "Douglas", LastName = "Adams", EmailAddress = "adams@petslife.com" },
                new PetOwner { FirstName = "Norman", LastName = "Mailer", EmailAddress = "mailer@petslife.com" },
            };
            context.PetOwners.AddRange(owners);

            var ownersByLastName = owners.ToDictionary(k => k.LastName);
            var pets = new []
            {
                new Pet { Name = "Toto", BirthDate = DateTime.Parse("2012/1/10"), Owner = ownersByLastName["Hemingway"] },
                new Pet { Name = "Benji", BirthDate = DateTime.Parse("2014/8/6"), Owner = ownersByLastName["Hemingway"] },
                new Pet { Name = "Pluto", BirthDate = DateTime.Parse("2010/12/11"), Owner = ownersByLastName["Twain"] },
                new Pet { Name = "Odie", BirthDate = DateTime.Parse("2014/4/8"), Owner = ownersByLastName["Twain"] },
                new Pet { Name = "Nana", BirthDate = DateTime.Parse("2011/1/6"), Owner = ownersByLastName["Shakespeare"] },
                new Pet { Name = "Blue", BirthDate = DateTime.Parse("2015/9/6"), Owner = ownersByLastName["Shakespeare"] },
                new Pet { Name = "Copper", BirthDate = DateTime.Parse("2011/5/15"), Owner = ownersByLastName["Dickens"] },
                new Pet { Name = "Hooch", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Dickens"] },

                new Pet { Name = "Wishbone", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Wilde"] },
                new Pet { Name = "Marley", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Wilde"] },
                new Pet { Name = "Beethoven", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Steinbeck"] },
                new Pet { Name = "Balto", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Adams"] },
                new Pet { Name = "Lassie", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Adams"] },
                new Pet { Name = "Hachiko", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Mailer"] },
                new Pet { Name = "Snoopy", BirthDate = DateTime.Parse("2009/11/11"), Owner = ownersByLastName["Mailer"] },

            };
            context.Pets.AddRange(pets);
            var petsByName = pets.ToDictionary(k => k.Name);

            var walkers = new[]
            {
                new PetWalker { FirstName = "Johnie", LastName = "Walker", EmailAddress = "walker@petslife.com", PhoneNumber = "+30 123 492 156" }
            };
            context.PetWalkers.AddRange(walkers);

            var walkersByLastName = walkers.ToDictionary(k => k.LastName);
            var approvals = new[]
            {
                new PetApproval { Pet = petsByName["Toto"], Walker = walkersByLastName["Walker"], Occurence = "every wednesday 17:00 - 19:00", DateApproved = DateTime.Parse("2011-05-18") },
                new PetApproval { Pet = petsByName["Toto"], Walker = walkersByLastName["Walker"], Occurence = "every thursday 17:00 - 19:00", DateApproved = DateTime.Parse("2011-05-18") },
                new PetApproval { Pet = petsByName["Toto"], Walker = walkersByLastName["Walker"], Occurence = "every friday 17:00 - 19:00", DateApproved = DateTime.Parse("2011-05-18") },
                new PetApproval { Pet = petsByName["Odie"], Walker = walkersByLastName["Walker"], Occurence = "every tuesday 17:00 - 19:00", DateApproved = DateTime.Parse("2012-06-21") },
                new PetApproval { Pet = petsByName["Nana"], Walker = walkersByLastName["Walker"], Occurence = "every monday 12:00 - 13:00", DateApproved = DateTime.Parse("2014-09-22") },
                new PetApproval { Pet = petsByName["Blue"], Walker = walkersByLastName["Walker"], Occurence = "every tuesday 14:30 - 16:00", DateApproved = DateTime.Parse("2016-11-10") },
                new PetApproval { Pet = petsByName["Hachiko"], Walker = walkersByLastName["Walker"], Occurence = "every sunday 10:30 - 12:30", DateApproved = DateTime.Parse("2016-03-12") },
                new PetApproval { Pet = petsByName["Snoopy"], Walker = walkersByLastName["Walker"], Occurence = "every sunday 10:00 - 11:00", DateApproved = DateTime.Parse("2016-03-12") },
                new PetApproval { Pet = petsByName["Beethoven"], Walker = walkersByLastName["Walker"], Occurence = "every thursday 08:30 - 09:30", DateApproved = DateTime.Parse("2016-01-10") },
                new PetApproval { Pet = petsByName["Hooch"], Walker = walkersByLastName["Walker"], Occurence = "every monday 08:30 - 11:00", DateApproved = DateTime.Parse("2016-02-10") },
                new PetApproval { Pet = petsByName["Hooch"], Walker = walkersByLastName["Walker"], Occurence = "every wednesday 08:30 - 11:00", DateApproved = DateTime.Parse("2016-02-10") },
                new PetApproval { Pet = petsByName["Hooch"], Walker = walkersByLastName["Walker"], Occurence = "every friday 08:30 - 11:00", DateApproved = DateTime.Parse("2016-02-10") },
                new PetApproval { Pet = petsByName["Copper"], Walker = walkersByLastName["Walker"], Occurence = "every saturday 11:00 - 12:00", DateApproved = DateTime.Parse("2016-01-11") },
                new PetApproval { Pet = petsByName["Wishbone"], Walker = walkersByLastName["Walker"], Occurence = "every monday 11:00 - 12:30", DateApproved = DateTime.Parse("2016-02-09") },
                new PetApproval { Pet = petsByName["Balto"], Walker = walkersByLastName["Walker"], Occurence = "every monday 14:30 - 16:30", DateApproved = DateTime.Parse("2011-12-11") },
                new PetApproval { Pet = petsByName["Marley"], Walker = walkersByLastName["Walker"], Occurence = "every monday 15:00 - 18:00", DateApproved = DateTime.Parse("2011-11-19") },
                new PetApproval { Pet = petsByName["Marley"], Walker = walkersByLastName["Walker"], Occurence = "every tuesday 15:00 - 18:00", DateApproved = DateTime.Parse("2011-11-19") },
                new PetApproval { Pet = petsByName["Marley"], Walker = walkersByLastName["Walker"], Occurence = "every wednesday 15:00 - 18:00", DateApproved = DateTime.Parse("2011-11-19") },

            };
            context.PetApprovals.AddRange(approvals);

            context.SaveChanges();
        }
    }
}