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
        void LogOff();
        IdentityResult Register(RegisterViewModel model);
        ExternalLoginInfo GetExternalLoginInfo();
        SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo);
        IdentityResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, ExternalLoginInfo info);
    }
}
