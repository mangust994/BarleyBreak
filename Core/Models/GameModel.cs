using System.Collections.Generic;

namespace Core.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public List<ButtonModel> Buttons { get; set; }
        public List<UserModel> Users { get; set; }
        public bool Status { get; set; }
        public string Route { get; set; }
        public string ConectionIdUserOne { get; set; }
        public string ConectionIdUserTwo { get; set; }
    }
}
