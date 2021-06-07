using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : IdentityUser, IEntity
    {
        public List<Game> Games { get; set; }
    }
}
