using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class BarleyBreakContext : IdentityDbContext<User>
    {
        public BarleyBreakContext(DbContextOptions<BarleyBreakContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Button> Buttons { get; set; }

    }
}
