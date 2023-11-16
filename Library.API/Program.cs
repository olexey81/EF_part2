using Library.Services;
using Library.Repository;
using Library.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // account/login  - not auth, all
            // account/registration/reader - auth, librarian
            // account/registration/librarian - auth, admin

            // common/books/find - auth, all, search book

            // librarian/books/add - auth, librarian, add book
            // librarian/books/delete - auth, librarian, remove book
            // librarian/books/return - auth, librarian, return book
            // librarian/books/update - auth, librarian, updating of books
            // librarian/authors/add - auth, librarian, add of author
            // librarian/authors/delete - auth, librarian, remove author
            // librarian/authors/update - auth, librarian, add author

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<JwtInfo>(builder.Configuration.GetSection("JwtInfo"));



            builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));



            //// Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(opt =>
                            {
                                opt.TokenValidationParameters = new TokenValidationParameters
                                {
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtInfo:TokenKey"] ?? throw new ArgumentNullException("TokenKey"))),
                                    ValidateIssuerSigningKey = true,
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true
                                };
                            });

            builder.Services.AddControllers();
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapControllers(); 

            app.Run();
        }
    }
}