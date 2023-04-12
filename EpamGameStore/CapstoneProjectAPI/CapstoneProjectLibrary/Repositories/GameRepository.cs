using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Tools;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

            var id = CheckGameId(item, entityContext);

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

        public  List<GameItem> GetItemsWithPagination(int amount, int offset = 0, List<string> genresFilter = null)
        {
            var returnList = entityContext.Games.OrderByDescending(item => item.Id);
            var test = entityContext.GameGenres.Select(e => e);
           
            if(genresFilter != null && genresFilter.Count != 0)
            {
                var genresNameList = entityContext.genresList.Where(e => genresFilter.Contains(e.Name)).Select(e => e.Id);
                //Попробовать провернуть как тут var genresNameList = entityContext.genresList.Where(e => genresIdList.Contains(e.Id)).Select(e => e); но только 
                //добавив Distinct
                //Вариант поэтапно отсеивать из списка всех игр с жанрами при этом каждый след шаг будет отсеивать уже предыдущий результат
                var genresList = entityContext.genresList.Where(i => genresFilter.Contains(i.Name)).Select(e => e);
                foreach(var genres in genresNameList)
                {
                    test = test.Where(g => g.GenreId == genres).Select(e => e);
                }
                var ids = test.Select(e => e.GameId);

                var newReturnList = returnList.Where(e => ids.Contains((int)e.Id)).Select(g => g);

                return newReturnList.Skip(Math.Abs(offset * amount)).Take(Math.Abs(amount)).ToList();
            }

            return returnList.Skip(Math.Abs(offset * amount)).Take(Math.Abs(amount)).ToList();
        }

        public int GetQuantity()
        {
            var count = entityContext.Games.Count();
            return count;
        }

        public async Task EditGame(int id, string name, string description, float price, IFormFile? file)
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

            if(file != null)
            {
                item.ImageUrl = ImageTool.SaveImage(file);
            }

            await entityContext.SaveChangesAsync();
        }
        private int CheckGameId(GameItem item, EntityContext baseContext)
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
