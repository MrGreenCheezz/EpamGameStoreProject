using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Repositories
{
    public class GameRepository : IRepository
    {
        public EntityContext entityContext;

        public GameRepository()
        {
            entityContext = new EntityContext();
        }

        public GameRepository(EntityContext entityContext)
        {
            this.entityContext = entityContext;
        }

        public int AddGame(GameItem item, IFormFile? file)
        {

            var id = CheckGame(item, entityContext);

            if(file != null)
            {
               item.ImageUrl = ImageTool.SaveImage(file);
            }      
            
            entityContext.Games.AddAsync(item);
            entityContext.SaveChangesAsync();
            return id;
        }

        public GameItem GetGame(int id)
        {

            CheckGame(id, entityContext);
            var item = entityContext.Games.FirstOrDefault(x => x.Id == id);
            return item;

        }

        public async Task DeleteGame(int id)
        {

            CheckGame(id, entityContext);

            var item = entityContext.Games.FirstOrDefault(i => i.Id == id);

            entityContext.Games.Remove(item);
            await entityContext.SaveChangesAsync();
        }

        public async Task<int> CopyGame(int id)
        {

            CheckGame(id, entityContext);
            var listItem = entityContext.Games.FirstOrDefault(i => i.Id == id);
            var newItem = listItem;
            newItem.Id = null;
            var newItemId = AddGame(listItem, null);
            return newItemId;

        }

        public  List<GameItem> GetItemsWithPagination(int amount, int offset = 0)
        {
            var returnList = entityContext.Games.OrderByDescending(item => item.Id).Skip(Math.Abs(offset * amount)).Take(Math.Abs(amount)).ToList();
            return returnList;
        }

        public int GetQuantity()
        {
            var count = entityContext.Games.Count();
            return count;
        }

        public async Task EditGame(int id, string name, string description, float price, string genres, IFormFile? file)
        {

            CheckGame(id, entityContext);

            var item = entityContext.Games.FirstOrDefault(i => i.Id == id);

            if (!string.IsNullOrEmpty(name))
            {
                item.Name = name;
            }

            if (!string.IsNullOrEmpty(description))
            {
                item.Description = description;
            }

            if (price != 0)
            {
                item.Price = price;
            }

            if (genres != null)
            {
                item.Genres = genres;
            }

            if(file != null)
            {
                item.ImageUrl = ImageTool.SaveImage(file);
            }

            await entityContext.SaveChangesAsync();
        }
        private int CheckGame(GameItem item, EntityContext baseContext)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "item cannot be null");
            }

            if (item.Id == null)
            {
                var list = from i in baseContext.Games
                           select i;
                int? idFirstStep;
                var sortedList = list.OrderByDescending(i => i.Id);
                try
                {
                    idFirstStep = sortedList.FirstOrDefault().Id;
                }
                catch
                {
                    idFirstStep = null;
                }

                if (idFirstStep == null)
                {
                    item.Id = 1;
                }
                else
                {
                    item.Id = idFirstStep + 1;
                }
                return (int)item.Id;
            }

            return (int)item.Id;
        }

        private void CheckGame(int id, EntityContext baseContext)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = baseContext.Games.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentException("No elements with same Id");
            }
        }
    }
}
