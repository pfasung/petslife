using System.Data.Entity.Migrations;
using PetsLife.DAL;

namespace PetsLife.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PetsLifeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PetsLifeDbContext context)
        {

        }
    }
}