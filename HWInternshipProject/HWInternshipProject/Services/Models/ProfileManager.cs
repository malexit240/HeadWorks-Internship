using System;
using HWInternshipProject.Models;
namespace HWInternshipProject.Services.Models
{
    public class ProfileManager : IProfileManager
    {
        public Profile Create(Profile profile)
        {
            using (var context = new Context())
            {
                if (context.users.Find(profile.UserId) == null)
                    throw new NullReferenceException();

                profile.CreationTime = DateTime.Now;

                context.profiles.Add(profile);
                context.SaveChanges();
                return profile;
            }
        }

        public Profile Create(Guid user_id, string nick, string name, string description = "", string imageDestination = "")
        {

            return Create(new Profile()
            {
                UserId = user_id,
                ImageDestination = imageDestination,
                NickName = nick,
                Name = name,
                Description = description,
            });
        }

        public void Update(Profile profile)
        {
            using (var context = new Context())
            {
                context.profiles.Update(profile);
                context.SaveChanges();

                profile.RaiseActualize();
            }
        }

        public void Delete(Profile profile)
        {
            using (var context = new Context())
            {
                context.profiles.Remove(profile);
                context.SaveChanges();
                profile.RaiseActualize();
            }
        }


    }
}
