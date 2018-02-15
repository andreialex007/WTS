using System.Linq;
using WTS.BL.Utils;
using WTS.DL;
using WTS.DL.Entities;

namespace WTS
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext db)
        {
            db.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .
            InitUsers(db);
        }

        private static void InitUsers(AppDbContext db)
        {
            // Look for any students.
            if (db.Users.Any())
                return;

            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin.com",
                Role = 1,
                Password = "123456i".ToHash()
            };

            var tech = new User
            {
                FirstName = "Technician",
                LastName = "Technician",
                Email = "Technician@Technician.com",
                Role = 2,
                Password = "123456i".ToHash()
            };

            db.Users.Add(admin);
            db.Users.Add(tech);
            db.SaveChanges();
        }
    }
}
