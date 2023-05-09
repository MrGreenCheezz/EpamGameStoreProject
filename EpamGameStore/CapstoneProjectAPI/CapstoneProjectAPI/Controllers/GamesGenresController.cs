using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CapstoneProjectAPI.Controllers
{
    public class GamesGenresController : Controller
    {
        public IRepository GameRepo { get; set; }
        public IGenresRepository GenresRepo { get; set; }
        public GamesGenresController(IRepository gameRepo, IGenresRepository genresRepo)
        {
            GameRepo = gameRepo;
            GenresRepo = genresRepo;
        }

        [Route("api/genres/addGenre")]
        [HttpPost]
        public int AddGenre(string genreName)
        {
            int id = GenresRepo.AddGenreToList(genreName);
            return id;
        }
        [Route("api/genres/asignGenres")]
        [HttpPost]
        public void AsignGenres(int gameId, int[] genres)
        {
           GenresRepo.AsignGenresToGame(gameId, genres);
        }
        [Route("api/genres/getGenresList")]
        [HttpGet]
        public List<GameGenre> GetGenresList()
        {
            return GenresRepo.GetGenresList();
        }
        [Route("api/genres/getGameGenres")]
        [HttpGet]
        public List<GameGenre> GetGameGenres(int gameId)
        {
            return  GenresRepo.GetGameGenres(gameId);
        }
    }
}
