using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public interface IPetOwnersRepository
    {
        IQueryable<PetOwner> GetAll();
        PetOwner GetById(int id);
        void Add(PetOwner owner);
        void Update(PetOwner owner);
        void Delete(PetOwner owner);
    }
}