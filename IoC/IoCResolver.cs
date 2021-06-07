using AutoMapper;
using BLL.Services;
using Core.Entities;
using Core.Infrastructures;
using Core.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class IoCResolver
    {
        /// <summary>
        /// DI Register from DAL and BLL
        /// </summary>
        public static void Load(IServiceCollection services, string connection)
        {
            services.AddDbContext<BarleyBreakContext>(options => options.UseSqlServer(connection));
            services.AddScoped<BarleyBreakContext>();
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<BarleyBreakContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EnterpriseProfile());
            }).CreateMapper());
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IButtonService, ButtonService>();
        }
    }
}
