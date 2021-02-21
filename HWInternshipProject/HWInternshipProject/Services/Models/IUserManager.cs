using HWInternshipProject.Models;

namespace HWInternshipProject.Services.Models
{
    public interface IUserManager
    {
        IUserManager Instance { get; }

        User Create(string login, string password);
        void Update();
        void Delete();
        bool IsLoginUnique(string login);
    }
}
