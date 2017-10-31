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
    public class ApplicationContext: IdentityDbContext<ApplicationUser>, IApplicationContext
    {
        public ApplicationContext() : base("FinalDBConn")
        {
            Database.SetInitializer<ApplicationContext>(null);
        }

        public ApplicationContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<ApplicationContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
