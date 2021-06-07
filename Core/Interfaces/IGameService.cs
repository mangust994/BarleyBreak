using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameModel> GetGames(Expression<Func<Game, bool>> predicate);

        GameModel GetGameById(int id);

        GameModel GetGame(Expression<Func<Game, bool>> predicate);

        void AddGame(GameModel model);

        void UpdateGame(int id, GameModel model);

        void RemoveGame(int id);
    }
}
