using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJson.Angular.Models
{
    [Table(nameof(JsonPassThroughEntity))]
    public class JsonPassThroughEntity
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        
        [Column(TypeName="decimal(9,5)")]
        public decimal SchemaVersion { get; set; }

        [Required]
        public string SerializedData { get; set; }
    }
}
