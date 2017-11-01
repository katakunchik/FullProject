using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    internal class DBInitializer : CreateDatabaseIfNotExists<ApplicationDBContext>
    {
        protected override void Seed(ApplicationDBContext context)
        {
            base.Seed(context);
        }
    }
}
