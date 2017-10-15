using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJson.Angular.Models
{
    [Table("JsonComplexEntity")]
    public class JsonComplexEntity
    {

        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }


        [Column(TypeName = "decimal(9,5)")]
        public decimal SchemaVersion { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        
        public DateTime? LastModifiedOn { get; set; }

        
        //Finally we have the field that stores the form data that can change schema over time
        [Required]
        public string SerializedData { get; set; }
    }

    public static class JsonComplexEntityExtensions
    {
        public static IQueryable<JsonComplexEntity> JsonWhereInvoiceId(this IQueryable<JsonComplexEntity> entity, string invoiceId)
        {
            return entity
                .FromSql(
@"SELECT entity.* 
FROM JsonComplexEntity entity
CROSS APPLY OPENJSON(entity.[SerializedData]) 
WITH (
    [InvoiceId] nvarchar(25)
) json WHERE json.[InvoiceId] = @p0", invoiceId);
        }
    }
}
