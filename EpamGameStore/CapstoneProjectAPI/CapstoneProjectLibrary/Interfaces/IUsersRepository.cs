using CapstoneProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Interfaces
{
    public interface IUsersRepository
    {
        public int AddUser(string firstName, string lastName, string userName, string email, string password);
        public User GetUser(int id);
        public void DeleteUser(int id);
        public Task EditUser(int id, string firstName, string lastName, string userName, string email, string password);
    }
}
