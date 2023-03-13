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
        
        [Route("getItems")]
        [HttpGet]
        public List<GameItem> GetItemsWithPagination(int amount, int offset = 0)
        {
              return GameRepo.GetItemsWithPagination(amount, offset);         
        }

        [Route("editItem")]
        [HttpPost]
        public async Task<IActionResult> PostEditItem( int id, string name = null, string description = null,
           float price = 0, string genres = null)
        {
            
            await GameRepo.EditGame(id,name,description,price, genres);
            return Ok();
        }

        [Route("addItem")]
        [HttpPost]
        public async Task<IActionResult> AddItem(string name, string description, float price, string genres)
        {
            var item = new GameItem();

            item.Name = name;
            item.Description = description;
            item.Price = price;
            item.Genres = genres;

            
            var id = await GameRepo.AddGame(item);

            return Ok(id);
        }

        [Route("getItemsCount")]
        [HttpGet]
        public int GetCount()
        {
            return GameRepo.GetQuantity();
        }

        [Route("deleteItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await GameRepo.DeleteGame(id);

            return Ok();
        }
    }
}
