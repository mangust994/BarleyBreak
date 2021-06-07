using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarleyBreakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService GameService;
        public GameController(IGameService GameService)
        {
            this.GameService = GameService;
        }

        [HttpGet]
        public IEnumerable<GameModel> Get()
        {
            return this.GameService.GetGames(x => true);
        }


        [HttpGet("Free")]
        public IEnumerable<GameModel> GetFiltered()
        {
            return this.GameService.GetGames(x => x.Status == true);
        }


        [HttpGet("{id}")]
        public GameModel GetGame(string id)
        {
            id = $"/{id}";
            var game =  this.GameService.GetGames(x => x.Route == id).FirstOrDefault();
            return game;
        }


        [HttpPost]
        public void Post([FromBody] GameModel model)
        {
            this.GameService.AddGame(model);
        }


        [HttpPut]
        public void Put([FromBody] GameModel model)
        {
            this.GameService.UpdateGame(model.Id, model);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.GameService.RemoveGame(id);
        }
    }
}
