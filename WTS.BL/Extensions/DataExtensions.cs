using WTS.DL;
using WTS.DL.Entities;

namespace WTS.BL.Extensions
{
    public static class DataExtensions
    {
        public static void DeleteById<T>(this AppDbContext _db, long id) where T : class, IPkidEntity
        {
            _db.Delete<T>(x => x.Id == id);
            _db.SaveChanges();
        }


    }
}