using System;
using HWInternshipProject.Models;

namespace HWInternshipProject.Services
{
    public class LoginValidatorService : ILoginValidatorService
    {
        public LoginValidationStatus IsLoginValid(string login)
        {
            if (login.Length < 4 || login.Length > 16)
                return LoginValidationStatus.InvalidLength;
            if (Char.IsDigit(login[0]))
                return LoginValidationStatus.StartsWithDigit;

            if (!User.IsLoginUnique(login))
                return LoginValidationStatus.LoginNotUnique;

            return LoginValidationStatus.Valid;
        }
    }
}
