using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProjectLibrary;
using System;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Repositories;
using CapstoneProjectLibrary.Interfaces;

namespace CapstoneProjectAPI.Controllers
{
    public class GameController : Controller
    {
        public IRepository GameRepo { get; set; }
        public GameController(IRepository gameRepo)
        {
            GameRepo = gameRepo;
        }
        
        [Route("getGames")]
        [HttpGet]
        public List<GameItem> GetGamesWithPagination(int amount, int offset = 0)
        {
              return GameRepo.GetItemsWithPagination(amount, offset);         
        }

        [Route("editGame")]
        [HttpPost]
        public async Task<IActionResult> PostEditGame( int id, string name = null, string description = null,
           float price = 0, string genres = null)
        {
            
            await GameRepo.EditGame(id,name,description,price, genres);
            return Ok();
        }

        [Route("addGame")]
        [HttpPost]
        public int AddGame(string name, string description, float price, string genres)
        {
            var item = new GameItem();

            item.Name = name;
            item.Description = description;
            item.Price = price;
            item.Genres = genres;

            
            var id = GameRepo.AddGame(item);

            return id;
        }

        [Route("getGamesCount")]
        [HttpGet]
        public int GetCount()
        {
            return GameRepo.GetQuantity();
        }

        [Route("deleteGame")]
        [HttpPost]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await GameRepo.DeleteGame(id);

            return Ok();
        }

        [Route("getGame")]
        [HttpGet]
        public GameItem GetGame(int id)
        {
            return GameRepo.GetGame(id);
        }
    }
}
