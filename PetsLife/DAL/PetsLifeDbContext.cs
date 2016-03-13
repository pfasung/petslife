using System.Data.Entity;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetsLifeDbContext : DbContext
    {
        public PetsLifeDbContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<PetOwner> PetOwners { get; set; }
        public virtual DbSet<PetApproval> PetApprovals { get; set; }
        public virtual DbSet<PetWalker> PetWalkers { get; set; }
        
    }
}