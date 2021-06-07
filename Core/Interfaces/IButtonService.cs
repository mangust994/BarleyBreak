using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Interfaces
{
    public interface IButtonService
    {
        IEnumerable<ButtonModel> GetButtons();

        ButtonModel GetButtonById(int id);

        ButtonModel GetButton(Expression<Func<Button, bool>> predicate);

        void AddButton(ButtonModel model);

        void UpdateButton(int id, ButtonModel model);

        void RemoveButton(int id);
    }
}
