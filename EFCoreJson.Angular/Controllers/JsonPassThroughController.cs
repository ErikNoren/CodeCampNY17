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
    public class JsonPassThroughController : Controller
    {
        private const string ROUTE_GetEntityById = "GetPassThroughEntityByIdRoute";

        private readonly CodeCampSampleContext _context;

        public JsonPassThroughController(CodeCampSampleContext context)
        {
            _context = context;
        }

        [HttpGet("{id}",Name=ROUTE_GetEntityById)]
        public IActionResult GetFormById(int id)
        {
            var found = GetEntityById(id);

            if (found != null)
                return Ok(found);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonPassThroughModel newForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEntity = newForm.ToEntity();

            _context.JsonPassThroughEntities.Add(newEntity);
            _context.SaveChanges();

            return CreatedAtRoute(ROUTE_GetEntityById, new { id = newEntity.Id }, newEntity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JsonPassThroughModel updatedForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var found = GetEntityById(id);

            if (found == null)
                return NotFound();

            updatedForm.UpdateEntity(ref found);
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

        private JsonPassThroughEntity GetEntityById(int id)
        {
            return _context.JsonPassThroughEntities.FirstOrDefault(pt => pt.Id == id);
        }

        private bool DeleteEntityById(int id)
        {
            var found = GetEntityById(id);

            if (found != null)
            {
                _context.JsonPassThroughEntities.Remove(found);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public class JsonPassThroughModel
        {
            public decimal SchemaVersion { get; set; }
            public string SerializedData { get; set; }

            public JsonPassThroughEntity ToEntity()
            {
                return new JsonPassThroughEntity { SchemaVersion = this.SchemaVersion, SerializedData = this.SerializedData };
            }
            
            public void UpdateEntity(ref JsonPassThroughEntity entity)
            {
                entity.SchemaVersion = this.SchemaVersion;
                entity.SerializedData = this.SerializedData;
            }
        }
    }
}
