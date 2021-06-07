using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarleyBreakApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub
    {
        private readonly IAccountService accountService;
        private readonly IGameService gameService;

        public GameHub(IGameService gameService, IAccountService accountService)
        {
            this.accountService = accountService;
            this.gameService = gameService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task ChangeButtonPosition(List<ButtonModel> buttonsPlayerOne, List<ButtonModel> buttonsPlayerTwo, 
            List<ButtonModel> winButtonsPlayerOne, List<ButtonModel> winButtonsPlayerTwo, GameModel gameModel)
        {
            var currentGameModel = gameService.GetGame(x => x.Id == gameModel.Id);
            await Clients.Client(currentGameModel.ConectionIdUserOne).SendAsync("takeNewPosition", buttonsPlayerOne, buttonsPlayerTwo, winButtonsPlayerOne, winButtonsPlayerTwo);
            await Clients.Client(currentGameModel.ConectionIdUserTwo).SendAsync("takeNewPosition", buttonsPlayerOne, buttonsPlayerTwo, winButtonsPlayerOne, winButtonsPlayerTwo);
            //await Clients.All.SendAsync("takeNewPosition", buttonsPlayerOne, buttonsPlayerTwo, winButtonsPlayerOne, winButtonsPlayerTwo);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task TakeId(GameModel gameModel)
        {
            var result = this.accountService.GetInfo(Context.User.Identity.Name);
            var currentGame = this.gameService.GetGame(x => x.Id == gameModel.Id);
            if (currentGame.ConectionIdUserOne == null)
            {
                gameModel.ConectionIdUserOne = this.Context.ConnectionId;
                this.gameService.UpdateGame(gameModel.Id, gameModel);
            }
            else if(currentGame.ConectionIdUserTwo == null)
            {
                gameModel.ConectionIdUserOne = currentGame.ConectionIdUserOne;
                gameModel.ConectionIdUserTwo = this.Context.ConnectionId;
                this.gameService.UpdateGame(gameModel.Id, gameModel);
            }
            else if(currentGame.ConectionIdUserTwo == "waitForUser2")
            {
                gameModel.ConectionIdUserTwo = this.Context.ConnectionId;
            }
            else if(this.Context.ConnectionId != currentGame.ConectionIdUserOne && this.Context.ConnectionId != currentGame.ConectionIdUserTwo)
            {
                gameModel.ConectionIdUserOne = this.Context.ConnectionId;
                gameModel.ConectionIdUserTwo = "waitForUser2";
                this.gameService.UpdateGame(gameModel.Id, gameModel);
            }
            foreach (var user in gameModel.Users)
            {
                if(user.Id == result.Id)
                {
                    await Clients.Caller.SendAsync("thisConnectionId", user.UserName);
                }
            }
        }
        public async Task TakeCurrentGame(string route)
        {
            var game = this.gameService.GetGame(x => x.Route == route);
            List<string> usersId = new List<string>();
            usersId.Add(game.Users.FirstOrDefault().Id);
            usersId.Add(game.Users.LastOrDefault().Id);
            await Clients.Caller.SendAsync("thisGame", game);
        }
    }
}
