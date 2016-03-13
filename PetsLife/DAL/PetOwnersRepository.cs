using System;
using System.Data.Entity;
using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetOwnersRepository : IPetOwnersRepository, IDisposable
    {
        private readonly PetsLifeDbContext _db = new PetsLifeDbContext();
        
        public IQueryable<PetOwner> GetAll()
        {
            return _db.PetOwners;
        }
        public PetOwner GetById(int id)
        {
            return _db.PetOwners.Find(id);
        }
        public void Add(PetOwner owner)
        {
            _db.PetOwners.Add(owner);
            _db.SaveChanges();
        }
        public void Update(PetOwner owner)
        {
            _db.Entry(owner).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(PetOwner owner)
        {
            _db.PetOwners.Remove(owner);
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