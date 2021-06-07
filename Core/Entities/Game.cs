using Core.Interfaces;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public List<Button> Buttons { get; set; }
        public List<User> Users { get; set; }
        public bool Status { get; set; }
        public string Route { get; set; }
        public string ConectionIdUserOne { get; set; }
        public string ConectionIdUserTwo { get; set; }
    }
}
