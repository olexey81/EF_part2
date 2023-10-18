using Library_DAL_2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // account/login  - all
            // account/registration - librarian
            // account/logout - all


            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapGet("db", async (LibraryContext db) => await db.Books.ToArrayAsync());


            app.Map("/login/{username}", (string username) =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var key = builder.Configuration["JwtInfo:TokenKey"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var signature = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var jwt = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)), // время действия 2 days
                                signingCredentials: signature
                                ); ;

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            });
            app.MapControllers(); 


            app.Run();
        }
    }
}