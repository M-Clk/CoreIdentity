using CoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Infrastucture
{
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            
            List<IdentityError> errors = new List<IdentityError>();
            //Validation isleminde bir data ortaya cikarsa onu errros listesine atiyoruz. Tum hatalari aldiktan sonra sayfada gorunmesi icin gonderiyoruz.

            //Sifrenin icinde kullanici adi varsa hata verir
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PassworddContainsUserName",
                    Description = "Password cannot contain username"
                });
            }
            //sifrenin icinde sirali numerik sayilar varsa hata verir. Ornek olarak 123 yazdik. Genisletilebilir.
            if (password.Contains("123"))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContainsSequence",
                    Description = "{Password cannot contain numeric suquence"
                });
            }
            //Hatanin varligina gore IdentityResult gonder
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : 
                IdentityResult.Failed(errors.ToArray()));
        }
    }
}
