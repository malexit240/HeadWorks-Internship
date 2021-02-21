using System;
using System.Linq;
using HWInternshipProject.Models;

namespace HWInternshipProject.Services.Models
{
    public class UserManager : IUserManager
    {
        public IUserManager Instance { get => Instance != null ? Instance : Instance = new UserManager(); set => Instance = value; }

        public bool IsLoginUnique(string login)
        {
            using (var context = new Context())
            {
                return !context.users.Any(u => u.Login == login);
            }
        }

        public User Create(string login, string password)
        {
            using (var context = new Context())
            {

                if (IsLoginUnique(login))
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

        public void Update()
        {
            using (var context = new Context())
            {
                context.users.Update(User.Current);
                context.SaveChanges();
            }
        }



        public void Delete()
        {
            using (var context = new Context())
            {
                context.users.Remove(User.Current);
                context.SaveChanges();
            }
        }
    }
}
