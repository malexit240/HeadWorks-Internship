using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using HWInternshipProject.Models;
namespace HWInternshipProject.Services.Models
{
    public class ProfileService : IProfileService
    {
        ProfileManager _profileManager;
        public ProfileService(ProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        public List<Profile> GetProfilesForUser(User user)
        {
            using (var context = new Context())
            {
                return (from usr in context.users.Include(u => u.Profiles) where usr.UserId == user.UserId select usr).First().Profiles;
            }
        }

        public void AddProfile(Profile profile)
        {

            _profileManager.Create(profile);
        }

        public void UpdateProfile(Profile profile)
        {
            _profileManager.Update(profile);
        }

        public void RemoveProfile(Profile profile)
        {
            _profileManager.Delete(profile);
        }
    }
}
