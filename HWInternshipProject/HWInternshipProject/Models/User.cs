using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HWInternshipProject.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public List<Profile> Profiles { get; set; } = new List<Profile>();

        public static User SignIn(string login, string password)
        {
            using (var context = new Context())
            {
                if (!context.users.Any(u => u.Login == login && RFCHasher.Verify(password, u.HashPassword)))
                    return null;

                return (from user in context.users.Include(u => u.Profiles) where user.Login == login select user).First();
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
