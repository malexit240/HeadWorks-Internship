using System.Collections.Generic;
using System;
using HWInternshipProject.Models;
namespace HWInternshipProject.Services.Models
{
    public interface IProfileService
    {
        List<Profile> GetProfilesForUser(User user);
        void AddProfile(Profile profile);
        void UpdateProfile(Profile profile);
        void RemoveProfile(Profile profile);
    }
}
