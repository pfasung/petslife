using System;
using System.Data.Entity;
using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetWalkersRepository : IPetWalkersRepository, IDisposable
    {
        private readonly PetsLifeDbContext _db = new PetsLifeDbContext();
        
        public IQueryable<PetWalker> GetAll()
        {
            return _db.PetWalkers;
        }
        public PetWalker GetById(int id)
        {
            return _db.PetWalkers.Find(id);
        }
        public void Add(PetWalker owner)
        {
            _db.PetWalkers.Add(owner);
            _db.SaveChanges();
        }
        public void Update(PetWalker owner)
        {
            _db.Entry(owner).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(PetWalker owner)
        {
            _db.PetWalkers.Remove(owner);
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