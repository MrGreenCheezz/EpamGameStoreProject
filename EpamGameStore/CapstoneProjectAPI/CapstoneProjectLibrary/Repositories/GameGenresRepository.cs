using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary.Repositories
{
    public class GameGenresRepository : IGenresRepository
    {
        public EntityContext entityContext;

        public GameGenresRepository()
        {
            entityContext = new EntityContext();
        }

        public GameGenresRepository(EntityContext entityContext)
        {
            this.entityContext = entityContext;
        }

        public int AddGenreToList(string GenreName)
        {
            int id = 0;
            var list = from i in entityContext.genresList
                       select i;
            var sortedList = list.OrderByDescending(i => i.Id);
            if (sortedList == null)
            {
                id = 1;
            }
            else
            {
                id = sortedList.FirstOrDefault().Id;
                id++;
            }
            entityContext.genresList.AddAsync(new Models.GameGenre() { Id = id, Name = GenreName });
            entityContext.SaveChanges();
            return id;
        }

        public void AsignGenresToGame(int gameId, int[] genresToAsign)
        {

            var listTodelete = from i in entityContext.GameGenres
                               where i.GameId == gameId
                               select i;
            entityContext.GameGenres.RemoveRange(listTodelete);
            entityContext.SaveChanges();

            foreach (var genre in genresToAsign)
            {
                int id = 0;
                var list = from i in entityContext.GameGenres
                           select i;
                var sortedList = list.OrderByDescending(i => i.Id);
                if(sortedList.Count() != 0)
                {
                    int? lastId = sortedList.First().Id;
                    if (lastId == null)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = (int)(lastId + 1);
                    }
                }
                else
                {
                    id = 1;
                }
               
                entityContext.GameGenres.AddAsync(new Models.GameGenres() { Id = id, GameId = gameId, GenreId = genre });
                entityContext.SaveChanges();
            }
        }

        public List<GameGenre> GetGenresList()
        {
            var list = from i in entityContext.genresList select i;
            return list.ToList();
        }

        public List<GameGenre> GetGameGenres(int gameId)
        {
            var genresIdList = from i in entityContext.GameGenres
                       where i.GameId == gameId
                       select i.GenreId;
            var genresNameList = entityContext.genresList.Where(e => genresIdList.Contains(e.Id)).Select(e => e);

            return genresNameList.ToList();

        }

    }
}
