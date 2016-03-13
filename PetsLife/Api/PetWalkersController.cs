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
    public class PetWalkersController : ApiController
    {
        private readonly IPetWalkersRepository _repository;

        public PetWalkersController(IPetWalkersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/PetWalkers
        public IQueryable<PetWalker> GetPetWalkers()
        {
            return _repository.GetAll();
        }

        // GET: api/PetWalkers/5
        [ResponseType(typeof(PetWalker))]
        public IHttpActionResult GetPetWalker(int id)
        {
            var petWalker = _repository.GetById(id);
            if (petWalker == null)
            {
                return NotFound();
            }

            return Ok(petWalker);
        }

        [Route("api/petwalkers/IsApprovedByPetOwner/{walkerId:int}/{ownerId:int}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult IsApprovedByPetOwner(int walkerId, int ownerId)
        {
            var petWalker = _repository.GetById(walkerId);
            if (petWalker == null)
            {
                return NotFound();
            }

            var isApproved = petWalker.Approvals.Any(a => a.Pet.OwnerId == ownerId);
            return Ok(isApproved);
        }

        // PUT: api/PetWalkers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetWalker(int id, PetWalker petWalker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petWalker.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(petWalker);
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

        // POST: api/PetWalkers
        [ResponseType(typeof(PetWalker))]
        public IHttpActionResult PostPetWalker(PetWalker petWalker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(petWalker);
            return CreatedAtRoute("DefaultApi", new { id = petWalker.Id }, petWalker);
        }

        // DELETE: api/PetWalkers/5
        [ResponseType(typeof(PetWalker))]
        public IHttpActionResult DeletePetWalker(int id)
        {
            var petWalker = _repository.GetById(id);
            if (petWalker == null)
            {
                return NotFound();
            }

            _repository.Delete(petWalker);
            return Ok(petWalker);
        }
    }
}