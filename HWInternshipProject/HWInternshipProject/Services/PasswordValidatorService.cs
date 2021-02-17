using System;
using System.Linq;

namespace HWInternshipProject.Services
{
    public class PasswordValidatorService : IPasswordValidatorService
    {
        public PasswordValidationStatus IsPasswordValid(string password)
        {
            if (password.Length < 8 || password.Length > 16)
                return PasswordValidationStatus.InvalidLength;

            if ((from c in password where Char.IsDigit(c) select c).Count() == 0 ||
                //(from c in password where Char.IsLetter(c) select c).Count() == 0 ||
                (from c in password where Char.IsUpper(c) select c).Count() == 0)
                return PasswordValidationStatus.InvalidContent;

            return PasswordValidationStatus.Valid;
        }
    }
}
