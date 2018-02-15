using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WTS.DL;
using WTS.DL.Entities;

namespace WTS.BL.Extensions
{
    public static class DbContextExtensions
    {
        public static void AttachIfDetached<T>(this AppDbContext db, T entity)
            where T : class
        {
            if (db.Entry(entity).State == EntityState.Detached)
                db.Set<T>().Attach(entity);
        }

        public static void Delete<T>(this AppDbContext db, Expression<Func<T, bool>> expression) where T : class
        {
            var items = db.Set<T>().Where(expression);
            db.Set<T>().RemoveRange(items);
        }

        public static void AddOrCreateEntity<T>(this AppDbContext db, T entity) where T : class, IPkidEntity
        {
            var entityState = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            db.Entry(entity).State = entityState;
            db.SaveChanges();
        }

        public static T CreateAndAdd<T>(this AppDbContext db) where T : class, IPkidEntity, new ()
        {
            var entity1 = new T();
            db.Set<T>().Add(entity1);
            return entity1;
        }

        public static void AttachAndAdd<T>(this AppDbContext db, T entity) where T : class, IPkidEntity
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Added;
        }

        public static void Save<T>(this AppDbContext db, T entity) where T : class, IPkidEntity
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            db.SaveChanges();
        }

        public static void Attach<TEntity>(this AppDbContext db, TEntity entity) where TEntity : class
        {
            db.Set<TEntity>().Attach(entity);
        }

        public static void Detach<TEntity>(this AppDbContext db, TEntity entity) where TEntity : class
        {
            db.Entry(entity).State = EntityState.Detached;
        }
    }
}