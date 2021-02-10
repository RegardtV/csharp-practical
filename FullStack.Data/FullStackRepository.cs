using System;
using System.Collections.Generic;
using System.Text;
using FullStack.Data.Entities;

namespace FullStack.Data
{
    public interface IFullStackRepository
    {
        User GetUser(int id);
        List<User> GetUsers();
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);

        //Do the same for all the other entities, Invoices, Invoice Items, etc

    }
    public class FullStackRepository: IFullStackRepository
    {
        private FullStackDbContext _ctx;
        public FullStackRepository(FullStackDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
            //return _ctx.Users.ToList();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
            //return _ctx.Users.Find(id);
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();

            //_ctx.Users.Add(e);
            //_ctx.SaveChanges();
            //return e;
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();

            //var existing = _ctx.Users.SingleOrDefault(em => em.Id == e.Id);
            //if (existing == null) return null;

            //_ctx.Entry(existing).State = EntityState.Detached;
            //_ctx.Users.Attach(e);
            //_ctx.Entry(e).State = EntityState.Modified;
            //_ctx.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();

            //var entity = _ctx.Users.Find(noteId);
            //_ctx.Users.Remove(entity); //CAREFULL!! here when you copy and paste, change _ctx.Users to the new DBSet
            //_ctx.SaveChanges();
        }

        
    }
}
