using WTS.BL.Services;
using WTS.DL;

namespace WTS.BL.Common
{
    public class AppService : ServiceBase
    {
        public UserService User { get; set; }
        public AccountService Account { get; set; }

        public AppService(AppDbContext db) : base(db)
        {
            User = new UserService(this, db);
            Account = new AccountService(this, db);
        }
    }
}