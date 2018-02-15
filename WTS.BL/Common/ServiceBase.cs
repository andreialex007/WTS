using System.Linq;
using System.Security.Claims;
using WTS.BL.Extensions;
using WTS.BL.Utils;
using WTS.DL;

namespace WTS.BL.Common
{
    public abstract class ServiceBase
    {
        protected AppDbContext Db;

        protected ServiceBase(AppDbContext db)
        {
            Db = db;
        }

        public ClaimsIdentity Identity => ServiceProviders.HttpContext.User.Identity.CastTo<ClaimsIdentity>();
        public int UserId => Identity.Claims.Single(x => x.Type == "Id").Value.TryParse<int>();
        public string UserEmail => Identity.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
        public string UserFirstName => Identity.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
        public string UserLastName => Identity.Claims.Single(x => x.Type == ClaimTypes.Surname).Value;
        public string UserFullName => UserFirstName + " " + UserLastName;
    }
}