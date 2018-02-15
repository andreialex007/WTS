using WTS.BL.Common;
using WTS.DL;

namespace WTS.BL.Services
{
    public abstract class EntityService
    {
        protected AppService App { get; set; }
        protected AppDbContext Db { get; set; }

        protected EntityService(AppService app, AppDbContext db)
        {
            App = app;
            Db = db;
        }

        public UserService User => App.User;
    }
}