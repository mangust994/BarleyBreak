using AutoMapper;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using Core.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IButtonService buttonService;
        private readonly IRepository repository;
        private readonly IMapper _mapper;

        public GameService(IRepository repo, IMapper mapper, IButtonService buttonService)
        {
            this.repository = repo;
            this._mapper = mapper;
            this.buttonService = buttonService;
        }
        public void AddGame(GameModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }
            Game game = _mapper.Map<Game>(model);
            List<User> users = new List<User>();
            foreach (var user in model.Users)
            {
                users.Add(repository.FirstorDefault<User>(x => x.Id == user.Id));
            }
            game.Users = users;
            this.repository.AddAndSave<Game>(game);
        }
        public IEnumerable<GameModel> GetGames(Expression<Func<Game, bool>> predicate)
        {
            var games =  _mapper.Map<List<GameModel>>(this.repository.GetWithInclude<Game>(predicate, p=>p.Users, x=> x.Buttons));
            return games;
        }

        public GameModel GetGame(Expression<Func<Game, bool>> predicate)
        {
            var gameModel = _mapper.Map<GameModel>(this.repository.GetWithInclude(predicate, x => x.Users, x => x.Buttons).FirstOrDefault());
            return gameModel;
        }

        public GameModel GetGameById(int id)
        {
            if (id == 0)
            {
                throw new NullReferenceException();
            }
            return _mapper.Map<GameModel>(this.repository.FirstorDefault<Game>(x => x.Id == id));
        }

        public void RemoveGame(int id)
        {
            var game = this.repository.FirstorDefault<Game>(x => x.Id == id);
            if (game == null)
            {
                throw new NullReferenceException();
            }
            this.repository.RemoveAndSave(game);
        }

        public void UpdateGame(int id, GameModel model)
        {
            var game = this.repository.GetWithInclude<Game>(x=> x.Id == id, p=> p.Users).FirstOrDefault();
            if (game == null)
            {
                throw new NullReferenceException();
            }
            List<Button> buttons = new List<Button>();
            foreach (var buttonModel in model.Buttons)
            {
                buttonService.UpdateButton(buttonModel.Id, buttonModel);
            }
            foreach (var userModel in model.Users)
            {
                if (!game.Users.Any(x => x.Id == userModel.Id))
                {
                    var user = this.repository.FirstorDefault<User>(x => x.Id == userModel.Id);
                    game.Users.Add(user);
                }
            }
            game.Status = model.Status;
            game.Buttons = buttons;
            if(model.ConectionIdUserOne != null)
            {
                game.ConectionIdUserOne = model.ConectionIdUserOne;
            }
            if (model.ConectionIdUserTwo !=null)
            {
                game.ConectionIdUserTwo = model.ConectionIdUserTwo;
            }
            this.repository.UpdateAndSave(game);
        }
    }
}
