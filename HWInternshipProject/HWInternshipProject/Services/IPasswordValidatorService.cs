namespace HWInternshipProject.Services
{
    public interface IPasswordValidatorService
    {
        PasswordValidationStatus IsPasswordValid(string password);
    }
}
