using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HWInternshipProject.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string ImageDestination { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

        public static Profile Create(Guid user_id, string nick, string name, string description = "", string imageDestination = "")
        {
            using (var context = new Context())
            {

                if (context.users.Find(user_id) == null)
                    throw new NullReferenceException();

                var profile = new Profile()
                {
                    UserId = user_id,
                    ImageDestination = imageDestination,
                    NickName = nick,
                    Name = name,
                    Description = description
                };

                context.profiles.Add(profile);
                context.SaveChanges();
                return profile;
            }
        }

        public static void Update(Profile profile)
        {
            using (var context = new Context())
            {
                context.profiles.Update(profile);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            Update(this);
        }

        public void Delete()
        {
            Delete(this);
        }

        public static void Delete(Profile profile)
        {
            using (var context = new Context())
            {
                context.profiles.Remove(profile);
                context.SaveChanges();
            }
        }

        public static List<Profile> GetProfilesForUser(User user)
        {
            using (var context = new Context())
            {
                return (from usr in context.users.Include(u => u.Profiles) where usr.UserId == user.UserId select usr).First().Profiles;
            }
        }
    }
}
