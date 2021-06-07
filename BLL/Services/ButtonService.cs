using AutoMapper;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using Core.Entities;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class ButtonService : IButtonService
    {
        private readonly IRepository repository;
        private readonly IMapper _mapper;

        public ButtonService(IRepository repo, IMapper mapper)
        {
            this.repository = repo;
            this._mapper = mapper;
        }
        public void AddButton(ButtonModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }
            Button button = _mapper.Map<Button>(model);
            this.repository.AddAndSave<Button>(button);
        }
        public IEnumerable<ButtonModel> GetButtons()
        {
            return _mapper.Map<List<ButtonModel>>(this.repository.GetAll<Button>());
        }

        public ButtonModel GetButton(Expression<Func<Button, bool>> predicate)
        {
            return _mapper.Map<ButtonModel>(this.repository.FirstorDefault(predicate));
        }

        public ButtonModel GetButtonById(int id)
        {
            if (id == 0)
            {
                throw new NullReferenceException();
            }
            return _mapper.Map<ButtonModel>(this.repository.FirstorDefault<Button>(x => x.Id == id));
        }

        public void RemoveButton(int id)
        {
            var button = this.repository.FirstorDefault<Button>(x => x.Id == id);
            if (button == null)
            {
                throw new NullReferenceException();
            }
            this.repository.RemoveAndSave(button);
        }

        public void UpdateButton(int id, ButtonModel model)
        {
            var button = this.repository.FirstorDefault<Button>(x => x.Id == id);
            if (button == null)
            {
                throw new NullReferenceException();
            }
            button.Name = model.Name;
            button.CurrentPosition = model.CurrentPosition;
            this.repository.UpdateAndSave(button);
        }
    }
}
