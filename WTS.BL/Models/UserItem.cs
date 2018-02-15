using System;
using System.Collections.Generic;
using System.Text;

namespace WTS.BL.Models
{
    public class UserItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public int Role { get; set; }
    }
}
