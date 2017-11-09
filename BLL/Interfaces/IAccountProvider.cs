using BLL.ViewModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountProvider
    {
        SignInStatus Login(LoginViewModel model);
        Task<SignInStatus> LoginAsync(LoginViewModel model);
        void LogOff();
        IdentityResult Register(RegisterViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        ExternalLoginInfo GetExternalLoginInfo();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo);
        IdentityResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, ExternalLoginInfo info);
        Task<IdentityResult> ExternalLoginConfirmationAsync(ExternalLoginConfirmationViewModel model, ExternalLoginInfo info);
    }
}
