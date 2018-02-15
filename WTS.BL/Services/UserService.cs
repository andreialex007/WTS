using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTS.BL.Common;
using WTS.BL.Models;
using WTS.DL;

namespace WTS.BL.Services
{
    public class UserService : EntityService
    {
        public List<UserItem> All()
        {
            var users = Db.Users.Select(x => new UserItem
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password,
                    Phone = x.Phone,
                    Role = x.Role
                })
                .ToList()
                .OrderBy(x => x.FullName)
                .ToList();

            return users;
        }

        public UserItem Get(int id)
        {
            return new UserItem();
        }

        public UserService(AppService app, AppDbContext db) : base(app, db)
        {
        }
    }
}
