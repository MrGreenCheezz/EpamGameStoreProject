using CapstoneProjectLibrary.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Interfaces
{
    public interface IRepository
    {
        public int GetQuantity();
        public int AddGame(GameItem item, IFormFile file);
        public List<GameItem> GetItemsWithPagination(int amount, int offset = 0, List<int> genresFilter = null);
        public GameItem GetGame(int id);
        public Task DeleteGame(int id);
        public Task<int> CopyGame(int id);
        public Task EditGame(int id, string name, string description, float price, IFormFile file);
    }
}
