using System;
using System.Data.Entity;
using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public class PetApprovalsRepository : IPetApprovalsRepository, IDisposable
    {
        private readonly PetsLifeDbContext _db = new PetsLifeDbContext();

        public IQueryable<PetApproval> GetAll()
        {
            return _db.PetApprovals;
        }
        public PetApproval GetById(int id)
        {
            return _db.PetApprovals.Find(id);
        }
        public void Add(PetApproval approval)
        {
            _db.PetApprovals.Add(approval);
            _db.SaveChanges();
        }
        public void Update(PetApproval approval)
        {
            _db.Entry(approval).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(PetApproval approval)
        {
            _db.PetApprovals.Remove(approval);
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