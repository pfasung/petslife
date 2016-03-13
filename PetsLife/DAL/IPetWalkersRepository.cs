using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public interface IPetWalkersRepository
    {
        IQueryable<PetWalker> GetAll();
        PetWalker GetById(int id);
        void Add(PetWalker owner);
        void Update(PetWalker owner);
        void Delete(PetWalker owner);
    }
}