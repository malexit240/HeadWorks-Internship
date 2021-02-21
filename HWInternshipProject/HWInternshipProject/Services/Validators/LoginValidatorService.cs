using System;
using HWInternshipProject.Services.Models;

namespace HWInternshipProject.Services.Validators
{
    public class LoginValidatorService : ILoginValidatorService
    {

        IUserManager _userManager;

        public LoginValidatorService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public LoginValidationStatus IsLoginValid(string login)
        {
            if (login.Length < 4 || login.Length > 16)
                return LoginValidationStatus.InvalidLength;
            if (Char.IsDigit(login[0]))
                return LoginValidationStatus.StartsWithDigit;

            if (!_userManager.IsLoginUnique(login))
                return LoginValidationStatus.LoginNotUnique;

            return LoginValidationStatus.Valid;
        }
    }
}
