using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PetsLife.DAL;
using PetsLife.Models;

namespace PetsLife.Api
{
    [Authorize]
    public class PetsController : ApiController
    {
        private readonly IPetsRepository _repository;

        public PetsController(IPetsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Pets
        public ICollection<Pet> GetPets()
        {
            return _repository.GetAll().ToArray();
        }

        [Route("api/pets/GetPetsByFilter/{Id:int},{underAge:int},{ownerId:int}")]
        public ICollection<Pet> GetPetsByFilter(int id, int underAge, int ownerId)
        {
            return _repository.GetAll()
                .Where(p => (id == -1 || id == p.Id))
                .Where(p => (ownerId == -1 || ownerId == p.OwnerId))

                // materialize before using computed members
                .ToArray()  
                .Where(p => (underAge == -1 || underAge > p.Age))
                .ToArray();
        }

        [Route("api/PetOwners/{ownerId}/Pets")]
        public ICollection<Pet> GetPetsByOwnerId(int ownerId)
        {
            var pets = _repository.GetAll().Where(p => p.OwnerId == ownerId).ToArray();
            return pets;
        }

        // GET: api/Pets/5
        [ResponseType(typeof(Pet))]
        public IHttpActionResult GetPet(int id)
        {
            var pet = _repository.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // PUT: api/Pets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPet(int id, Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pet.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(pet);
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

        // POST: api/Pets
        [ResponseType(typeof(Pet))]
        public IHttpActionResult PostPet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(pet);
            return CreatedAtRoute("DefaultApi", new { id = pet.Id }, pet);
        }

        // DELETE: api/Pets/5
        [ResponseType(typeof(Pet))]
        public IHttpActionResult DeletePet(int id)
        {
            var pet = _repository.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            _repository.Delete(pet);
            return Ok(pet);
        }
    }
}