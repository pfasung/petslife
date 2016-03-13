using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PetsLife.DAL;
using PetsLife.Models;

namespace PetsLife.Api
{
    [Authorize]
    public class PetApprovalsController : ApiController
    {
        private readonly IPetApprovalsRepository _repository;

        public PetApprovalsController(IPetApprovalsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/PetApprovals
        public IQueryable<PetApproval> GetPetApprovals()
        {
            return _repository.GetAll();
        }

        // GET: api/PetApprovals/5
        [ResponseType(typeof(PetApproval))]
        public IHttpActionResult GetPetApproval(int id)
        {
            var petApproval = _repository.GetById(id);
            if (petApproval == null)
            {
                return NotFound();
            }

            return Ok(petApproval);
        }

        // PUT: api/PetApprovals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetApproval(int id, PetApproval petApproval)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petApproval.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(petApproval);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_repository.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PetApprovals
        [ResponseType(typeof(PetApproval))]
        public IHttpActionResult PostPetApproval(PetApproval petApproval)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(petApproval);
            return CreatedAtRoute("DefaultApi", new { id = petApproval.Id }, petApproval);
        }

        // DELETE: api/PetApprovals/5
        [ResponseType(typeof(PetApproval))]
        public IHttpActionResult DeletePetApproval(int id)
        {
            var petApproval = _repository.GetById(id);
            if (petApproval == null)
            {
                return NotFound();
            }

            _repository.Delete(petApproval);
            return Ok(petApproval);
        }
    }
}