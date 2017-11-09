using BLL.Interfaces;
using BLL.Services.Identity;
using BLL.ViewModels.Identity;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Providers
{
    public class AccountProvider: IAccountProvider
    {
        private readonly AppUserManager _userManager;
        private readonly AppSignInManager _signInManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountProvider(AppUserManager userManager, 
            AppSignInManager signInManager, 
            IAuthenticationManager authManager,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authManager;
            }
        }

        private IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public SignInStatus Login(LoginViewModel model)
        {
            var result = SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            return result;
        }

        public async Task<SignInStatus> LoginAsync(LoginViewModel model)
        {
            return await Task.Run(() => this.Login(model));
        }

        public void LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IdentityResult Register(RegisterViewModel model)
        {
            IdentityResult result = new IdentityResult();
            result = IdentityResult.Failed();
            try
            {
                using (var uow = UnitOfWork)
                {
                    uow.StartTransaction();
                    var user = new AppUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                    result = UserManager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        Guid guidImage = Guid.NewGuid();
                        UserProfile userProfile = new UserProfile
                        {
                            Id = user.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Image = guidImage.ToString()
                        };
                        _userRepository.Add(userProfile);
                        _userRepository.SaveChanges();
                        uow.CommitTransaction();
                        //SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                }
            }
            catch
            {
                UnitOfWork.Dispose();
                result = IdentityResult.Failed();
            }
            
            return result;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            return await Task.Run(() => this.Register(model));
        }

        public ExternalLoginInfo GetExternalLoginInfo()
        {
            return _authManager.GetExternalLoginInfo();
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _authManager.GetExternalLoginInfoAsync();
        }

        public SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo)
        {
            return _signInManager.ExternalSignIn(loginInfo, isPersistent: false);
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)
        {
            return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }

        public IdentityResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, ExternalLoginInfo info)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = UserManager.Create(user);
            if (result.Succeeded)
            {
                result = UserManager.AddLogin(user.Id, info.Login);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return result;
                }
            }
            return result;
        }

        public async Task<IdentityResult> ExternalLoginConfirmationAsync(ExternalLoginConfirmationViewModel model, ExternalLoginInfo info)
        {
            return await Task.Run(() => this.ExternalLoginConfirmation(model, info));
        }







        #region AsyncMethods

        //public async Task<SignInStatus> Login(LoginViewModel model)
        //{
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    return result;
        //}

        //public void LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //}

        //public async Task<IdentityResult> Register(RegisterViewModel model)
        //{
        //    var user = new AppUser {
        //        UserName = model.Email,
        //        Email = model.Email
        //    };
        //    var result = await UserManager.CreateAsync(user, model.Password);
        //    if(result.Succeeded)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return result;
        //}
        //public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        //{
        //    return await _authManager.GetExternalLoginInfoAsync();
        //}

        //public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo)
        //{
        //    return await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //}
        #endregion
    }
}
