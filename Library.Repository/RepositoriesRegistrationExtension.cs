using Library.Common.Interfaces.Accounts;
using Library.Common.Interfaces.Authors;
using Library.Common.Interfaces.Books;
using Library.Common.Interfaces.Infos;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Repository
{
    public static class RepositoriesRegistrationExtension
    {
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
            serviceCollection.AddScoped<IBookRepository, BookRepository>();
            serviceCollection.AddScoped<IAuthorRepository, AuthorRepository>();
            serviceCollection.AddScoped<IInfoRepository, InfoRepository>();
        }
    }
}
