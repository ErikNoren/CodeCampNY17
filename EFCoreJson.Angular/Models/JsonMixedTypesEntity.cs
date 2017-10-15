using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJson.Angular.Models
{
    [Table("JsonMixedTypesEntity")]
    public class JsonMixedTypesEntity
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Column(TypeName = "decimal(9,5)")]
        public decimal SchemaVersion { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [JsonIgnore]
        public string SerializedAddresses
        {
            get
            {
                if (Addresses == null)
                    return "[]"; //We want to ensure SQL constraint is satisfied

                return JsonConvert.SerializeObject(Addresses);
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Addresses = null;
                else
                    Addresses = JsonConvert.DeserializeObject<IEnumerable<Address>>(value);
            }
        }

        [NotMapped] //Let EF store the string
        public IEnumerable<Address> Addresses { get; set; }
        

        public class Address
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }

            [MaxLength(10)]
            public string Zip { get; set; }
        }
    }
}
