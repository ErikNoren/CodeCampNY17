using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJson.Angular.Models
{
    public class CodeCampSampleContext : DbContext
    {
        public CodeCampSampleContext(DbContextOptions<CodeCampSampleContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<JsonPassThroughEntity> JsonPassThroughEntities { get; set; }

        public DbSet<JsonMixedTypesEntity> JsonMixedTypesEntities { get; set; }

        public DbSet<JsonComplexEntity> JsonComplexEntities { get; set; }
        
    }
}
