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
    public class JsonComplexController : Controller
    {
        private const string ROUTE_GetEntityById = "GetComplexEntityByIdRoute";

        private readonly CodeCampSampleContext _context;

        public JsonComplexController(CodeCampSampleContext context)
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

        [HttpGet("[action]/{invoiceId}")]
        public IActionResult FindByInvoiceId(string invoiceId)
        {
            return Ok(_context.JsonComplexEntities.JsonWhereInvoiceId(invoiceId).ToArray());
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonComplexModel newModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEntity = newModel.ToEntity();

            _context.JsonComplexEntities.Add(newEntity);
            _context.SaveChanges();

            return CreatedAtRoute(ROUTE_GetEntityById, new { id = newEntity.Id }, newEntity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JsonComplexModel updatedForm)
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

        private JsonComplexEntity GetEntityById(int id)
        {
            return _context.JsonComplexEntities.FirstOrDefault(cx => cx.Id == id);
        }

        private bool DeleteEntityById(int id)
        {
            var found = GetEntityById(id);

            if (found != null)
            {
                _context.JsonComplexEntities.Remove(found);
                _context.SaveChanges();
                return true;
            }

            return false;
        }


        public class JsonComplexModel
        {
            public decimal SchemaVersion { get; set; }
            public string CreatedBy { get; set; }
            public string LastModifiedBy { get; set; }
            public string SerializedData { get; set; }

            public JsonComplexEntity ToEntity()
            {
                return new JsonComplexEntity
                {
                    SchemaVersion = this.SchemaVersion,
                    CreatedBy = this.CreatedBy,
                    SerializedData = this.SerializedData,

                    CreatedOn = DateTime.UtcNow
                };
            }

            public void UpdateEntity(ref JsonComplexEntity entity)
            {
                entity.SchemaVersion = this.SchemaVersion;
                entity.LastModifiedBy = this.LastModifiedBy;
                entity.SerializedData = this.SerializedData;
                
                entity.LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
