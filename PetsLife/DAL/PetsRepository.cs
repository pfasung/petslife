using System;
using System.Data.Entity;
using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetsRepository : IPetsRepository, IDisposable
    {
        private readonly PetsLifeDbContext _db = new PetsLifeDbContext();

        public IQueryable<Pet> GetAll()
        {
            return _db.Pets;
        }
        public Pet GetById(int id)
        {
            return _db.Pets.Find(id);
        }
        public void Add(Pet pet)
        {
            _db.Pets.Add(pet);
            _db.SaveChanges();
        }
        public void Update(Pet pet)
        {
            _db.Entry(pet).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Pet pet)
        {
            _db.Pets.Remove(pet);
            _db.SaveChanges();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                }
            }
        }
    }
}