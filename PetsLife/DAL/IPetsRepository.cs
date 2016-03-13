using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public interface IPetsRepository
    {
        IQueryable<Pet> GetAll();
        Pet GetById(int id);
        void Add(Pet pet);
        void Update(Pet pet);
        void Delete(Pet pet);
    }
}