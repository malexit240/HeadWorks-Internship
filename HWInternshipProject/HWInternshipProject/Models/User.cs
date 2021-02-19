using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HWInternshipProject.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public List<Profile> Profiles { get; set; } = new List<Profile>();
        private static Guid _currentUserId = Guid.Empty;
        public static User Current
        {
            get
            {
                using (var context = new Context())
                {
                    return _currentUserId == Guid.Empty ? null : (from user in context.users.Include(u => u.Profiles) where user.UserId == _currentUserId select user).First();
                }

            }
        }

        public static async Task<User> SignInAsync(string login, string password)
        {
            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    var hashPasword = (from user in context.users where user.Login == login select user.HashPassword).FirstOrDefault();

                    if (RFCHasher.Verify(password, hashPasword ?? ""))
                    {
                        var user = (from usr in context.users.Include(u => u.Profiles) where usr.Login == login select usr).First();

                        User._currentUserId = user.UserId;
                        return user;
                    }

                    return null;
                }
            }
            );
        }

        public static void LogOut()
        {
            User._currentUserId = Guid.Empty;
        }

        public static bool IsLoginUnique(string login)
        {
            using (var context = new Context())
            {
                return !context.users.Any(u => u.Login == login);
            }
        }

        public static User Create(string login, string password)
        {
            using (var context = new Context())
            {

                if (context.users.Any(u => u.Login == login))
                    return null;

                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    Login = login,
                    HashPassword = RFCHasher.GetHash(password)
                };

                context.users.Add(user);
                context.SaveChanges();
                return user;
            }


        }

        public static void Update(User user)
        {
            using (var context = new Context())
            {
                context.users.Update(user);
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

        public static void Delete(User user)
        {
            using (var context = new Context())
            {
                context.users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
