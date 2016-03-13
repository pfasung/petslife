using System.Collections.Generic;
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
    public class PetOwnersController : ApiController
    {
        private readonly IPetOwnersRepository _repository;

        public PetOwnersController(IPetOwnersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/PetOwners
        public ICollection<PetOwner> GetPetOwners()
        {
            return _repository.GetAll().ToArray();
        }

        // GET: api/PetOwners/5
        [ResponseType(typeof(PetOwner))]
        public IHttpActionResult GetPetOwner(int id)
        {
            var petOwner = _repository.GetById(id);
            if (petOwner == null)
            {
                return NotFound();
            }

            return Ok(petOwner);
        }

        // PUT: api/PetOwners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetOwner(int id, PetOwner petOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petOwner.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(petOwner);
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

        // POST: api/PetOwners
        [ResponseType(typeof(PetOwner))]
        public IHttpActionResult PostPetOwner(PetOwner petOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(petOwner);
            return CreatedAtRoute("DefaultApi", new { id = petOwner.Id }, petOwner);
        }

        // DELETE: api/PetOwners/5
        [ResponseType(typeof(PetOwner))]
        public IHttpActionResult DeletePetOwner(int id)
        {
            var petOwner = _repository.GetById(id);
            if (petOwner == null)
            {
                return NotFound();
            }

            _repository.Delete(petOwner);
            return Ok(petOwner);
        }
    }
}