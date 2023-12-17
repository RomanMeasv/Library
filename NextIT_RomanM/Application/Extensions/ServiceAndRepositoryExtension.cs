using Microsoft.AspNetCore.Authentication;
using NextIT_RomanM.Application.BackgroundServices;
using NextIT_RomanM.Core.Application.Interfaces;
using NextIT_RomanM.Core.Application.Services;
using NextIT_RomanM.Core.Domain.Identity.Managers;
using NextIT_RomanM.Core.Domain.Interfaces;
using NextIT_RomanM.Core.Domain.Models;
using NextIT_RomanM.Infrastructure.Repositories;
using NextIT_RomanM.Infrastructure.Repositories.UserEvent;
using System.Collections.Concurrent;

namespace NextIT_RomanM.Application.Extensions
{
    public static class ServiceAndRepositoryExtension
    {
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<SignInManager>();
            services.AddScoped<UserManager>();

            #endregion

            #region Repositories

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserEventRepository, UserEventRepository>();

            #endregion

            services.AddTransient<ISystemClock, SystemClock>();

            services.AddScoped<UserEventTracker>();
            services.AddSingleton<ConcurrentQueue<UserEvent>>();
            services.AddHostedService<UserEventExportHostedService>();

            return services;
        }
    }
}
