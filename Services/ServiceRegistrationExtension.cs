using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Auth;
using Library.Common.Interfaces.Authors;
using Library.Common.Interfaces.Books;
using Library.Common.Interfaces.Infos;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Services
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddSingleton<IHashService, HashService>();
            serviceCollection.AddScoped<IAccountService, AccountService>();
            serviceCollection.AddScoped<IBookService, BookService>();
            serviceCollection.AddScoped<IAuthorService, AuthorService>();
            serviceCollection.AddScoped<IInfoService, InfoService>();
        }
    }
}
