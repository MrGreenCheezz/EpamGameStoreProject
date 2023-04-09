using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneProjectLibrary;
using System;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Repositories;
using CapstoneProjectLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CapstoneProjectAPI.Controllers
{
    public class GameController : Controller
    {
        public IRepository GameRepo { get; set; }
        public GameController(IRepository gameRepo)
        {
            GameRepo = gameRepo;
        }
        
        [Route("api/games/getGames")]
        [HttpGet]
        public List<GameItem> GetGamesWithPagination(int amount, int offset = 0)
        {
              return GameRepo.GetItemsWithPagination(amount, offset);         
        }

        [Route("api/games/editGame")]
        [HttpPost]
        public async Task<IActionResult> PostEditGame( int id, string name = null, string description = null,
           float price = 0, string genres = null, IFormFile file = null)
        {
            
            await GameRepo.EditGame(id,name,description,price, genres, file);
            return Ok();
        }
     

        [Route("api/games/addGame")]
        [HttpPost]
        public int AddGame(string name, string description, float price, string genres, [FromForm] IFormFile file)
        {
            var item = new GameItem();

            item.Name = name;
            item.Description = description;
            item.Price = price;
            
            
            var id = GameRepo.AddGame(item, file);

            return id;
        }

        [Route("api/games/getGamesCount")]
        [HttpGet]
        public int GetCount()
        {
            return GameRepo.GetQuantity();
        }

        [Route("api/games/deleteGame")]
        [HttpPost]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await GameRepo.DeleteGame(id);

            return Ok();
        }

        [Route("api/games/getGame")]
        [HttpGet]
        public GameItem GetGame(int id)
        {
            return GameRepo.GetGame(id);
        }
    }
}
