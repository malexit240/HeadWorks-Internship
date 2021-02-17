namespace HWInternshipProject.Services
{
    public interface ILoginValidatorService
    {
        LoginValidationStatus IsLoginValid(string login);
    }
}
