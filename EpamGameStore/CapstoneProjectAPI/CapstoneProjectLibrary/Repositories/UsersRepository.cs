using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public EntityContext entityContext;

        public UsersRepository()
        {
            entityContext = new EntityContext();
        }

        public UsersRepository(EntityContext entityContext)
        {
            this.entityContext = entityContext;
        }


        public int AddUser(string firstName, string lastName, string userName, string email, string password)
        {
            int id = 0;
            var list = from i in entityContext.users
                       select i;
            var sortedList = list.OrderByDescending(i => i.id);
            if (sortedList.Count() == 0)
            {
                id = 0;
            }
            else
            {
                id = sortedList.FirstOrDefault().id;
                id++;
            }
            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };
            user.id = id;
            entityContext.users.AddAsync(user);
            entityContext.SaveChanges();
            return id;
        }

        public User GetUser(int id)
        {
            var item = entityContext.users.FirstOrDefault(x => x.id == id);
            if(item == null)
            {
                throw new ArgumentException("No elements with this id!");
            }
            return item;
        }

        public void DeleteUser(int id)
        {
            var item = entityContext.users.FirstOrDefault(x => x.id == id);
            if (item == null)
            {
                throw new ArgumentException("No elements with this id!");
            }
            entityContext.users.Remove(item);
            entityContext.SaveChangesAsync();
        }

        public async Task EditUser(int id, string firstName, string lastName, string userName, string email, string password)
        {

            var item = entityContext.users.FirstOrDefault(i => i.id == id);

            if( item == null )
            {
                throw new ArgumentException("User with that id not found!");
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                item.FirstName = firstName;
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                item.LastName = lastName;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                item.UserName = userName;
            }

            if (!string.IsNullOrEmpty(email))
            {
                item.Email = email;
            }

            if (!string.IsNullOrEmpty(password))
            {
                item.Password = password;
            }

            await entityContext.SaveChangesAsync();
        }
    }
}
