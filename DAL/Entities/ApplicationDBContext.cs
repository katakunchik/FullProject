using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ApplicationDBContext: IdentityDbContext<ApplicationUser>, IApplicationDBContext
    {
        public ApplicationDBContext() : base("FinalDBConn")
        {
            Database.SetInitializer<ApplicationDBContext>(null);
        }

        public ApplicationDBContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<ApplicationDBContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
