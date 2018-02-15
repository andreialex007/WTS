using WTS.BL.Common;
using WTS.DL;

namespace WTS.BL.Services
{
    public class AccountService : EntityService
    {
        public void Login(string userName, string password)
        {
            var allUsers = this.User.All();
        }

        public AccountService(AppService app, AppDbContext db) : base(app, db)
        {
        }
    }
}
