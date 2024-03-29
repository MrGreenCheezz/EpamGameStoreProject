﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Authorization;

namespace CapstoneProjectAPI.Controllers
{
    public class GameController : Controller
    {
        public IRepository GameRepo { get; set; }
        public IGenresRepository GenresRepo { get; set; }
        public GameController(IRepository gameRepo, IGenresRepository genresRepo)
        {
            GameRepo = gameRepo;
            GenresRepo = genresRepo;
        }

        [Route("api/games/getGames")]
        [HttpGet]
        public async Task<IActionResult> GetGamesWithPagination(int amount, int offset = 0, List<int> genresFilter = null)
        {
                return Ok(GameRepo.GetItemsWithPagination(amount, offset, genresFilter));                   
        }

        [Route("api/games/editGame")]
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> PostEditGame( int id, string name = null, string description = null,
           float price = 0, IFormFile file = null)
        {
            
            await GameRepo.EditGame(id,name,description,price, file);
            return Ok();
        }
     

        [Route("api/games/addGame")]
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public int AddGame(string name, string description, float price, [FromForm] IFormFile file)
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
        [Authorize(Roles = "Admin, Manager")]
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
