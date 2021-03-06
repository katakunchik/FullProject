﻿using Autofac;
using BLL.Interfaces;
using BLL.Providers;
using BLL.Services.Identity;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
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
        private IAppBuilder _app;
        public DataModule(string connString, IAppBuilder app)
        {
            _connStr = connString;
            _app = app;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new AppDBContext(this._connStr)).As<IAppDBContext>().InstancePerRequest();
            builder.Register(ctx =>
            {
                var context = (AppDBContext)ctx.Resolve<IAppDBContext>();
                return context;
            }).AsSelf().InstancePerDependency();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<AppUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<AppRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<AppSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => _app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<AccountProvider>().AsSelf().InstancePerRequest();

            builder.RegisterType<SqlRepository>().As<ISqlRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<AccountProvider>().As<IAccountProvider>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
