using System;
using HWInternshipProject.Models;
namespace HWInternshipProject.Services.Models
{
    public interface IProfileManager
    {
        Profile Create(Profile profile);
        Profile Create(Guid user_id, string nick, string name, string description = "", string imageDestination = "");
        void Update(Profile profile);
        void Delete(Profile profile);

    }
}
