using Core.Interfaces;

namespace Core.Entities
{
    public class Button : IEntity
    {
        public int Id { get; set; }
        public int CurrentPosition { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
    }
}
