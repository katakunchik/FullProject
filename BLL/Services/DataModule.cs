using Autofac;
using BLL.Services.Identity;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BLL.Services
{
    public class DataModule : Module
    {
        private string _connStr;
        public DataModule(string connString)
        {
            _connStr = connString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new ApplicationDBContext(this._connStr)).As<IApplicationDBContext>().InstancePerRequest();
            builder.Register(c => new ApplicationDBContext(this._connStr)).AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            //builder.RegisterType<AppUserManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<AppRoleManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<AppSignInManager>().AsSelf().InstancePerRequest();
            //builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            //builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();
            //builder.RegisterType<AccountIdentityProvider>().As<IAccountProvider>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
