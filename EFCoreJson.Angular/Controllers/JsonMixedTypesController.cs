using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreJson.Angular.Models;

namespace EFCoreJson.Angular.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class JsonMixedTypesController: Controller
    {
        const string ROUTE_GetEntityById = "GetMixedEntityByIdRoute";

        private readonly CodeCampSampleContext _context;

        public JsonMixedTypesController(CodeCampSampleContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}", Name = ROUTE_GetEntityById)]
        public IActionResult GetFormById(int id)
        {
            var found = GetEntityById(id);

            if (found != null)
                return Ok(found);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonMixedTypesModel newModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEntity = newModel.ToEntity();

            _context.JsonMixedTypesEntities.Add(newEntity);
            _context.SaveChanges();

            return CreatedAtRoute(ROUTE_GetEntityById, new { id = newEntity.Id }, newEntity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JsonMixedTypesModel updatedModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = GetEntityById(id);

            if (found == null)
                return NotFound();

            updatedModel.UpdateEntity(ref found);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!DeleteEntityById(id))
                return NotFound();

            return NoContent();
        }

        private JsonMixedTypesEntity GetEntityById(int id)
        {
            return _context.JsonMixedTypesEntities.FirstOrDefault(cx => cx.Id == id);
        }

        private bool DeleteEntityById(int id)
        {
            var found = GetEntityById(id);

            if (found != null)
            {
                _context.JsonMixedTypesEntities.Remove(found);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public class JsonMixedTypesModel
        {
            public decimal SchemaVersion { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
            
            public string Email { get; set; }
            
            public IEnumerable<JsonMixedTypesEntity.Address> Addresses { get; set; }

            public JsonMixedTypesEntity ToEntity()
            {
                return new JsonMixedTypesEntity
                {
                    SchemaVersion = this.SchemaVersion,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Addresses = this.Addresses
                };
            }

            public void UpdateEntity(ref JsonMixedTypesEntity entity)
            {
                entity.SchemaVersion = this.SchemaVersion;
                entity.FirstName = this.FirstName;
                entity.LastName = this.LastName;
                entity.Email = this.Email;
                entity.Addresses = this.Addresses;
            }
        }
    }
}
