namespace HWInternshipProject.Services.Validators
{
    public interface ILoginValidatorService
    {
        LoginValidationStatus IsLoginValid(string login);
    }
}
