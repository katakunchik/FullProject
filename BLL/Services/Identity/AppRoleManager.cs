using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Identity
{
    public class AppRoleManager : RoleManager<ApplicationRole>
    {
        public AppRoleManager(IRoleStore<ApplicationRole> store) : base(store) { }
    }
}
