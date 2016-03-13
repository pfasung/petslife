using System.Linq;
using PetsLife.Models;

namespace PetsLife.DAL
{
    public interface IPetApprovalsRepository
    {
        IQueryable<PetApproval> GetAll();
        PetApproval GetById(int id);
        void Add(PetApproval approval);
        void Update(PetApproval approval);
        void Delete(PetApproval approval);
    }
}