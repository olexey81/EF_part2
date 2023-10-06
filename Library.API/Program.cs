using Library_DAL_2;
using Microsoft.EntityFrameworkCore;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // получаем строку подключения из файла конфигурации
            //string? connectionString = builder.Configuration.GetConnectionString("Data Source=OLEKSII_HP\\SQLEXPRESS;Initial Catalog=EF_part2;Integrated Security=True;TrustServerCertificate=True");

            // добавляем контекст в качестве сервиса в приложение
            builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer("Data Source=OLEKSII_HP\\SQLEXPRESS;Initial Catalog=ADO_API_HW;Integrated Security=True;TrustServerCertificate=True"));



            //// Add services to the container.

            builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // получение данных

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("db", async (LibraryContext db) => await db.Books.ToArrayAsync());

            app.MapControllers(); // старт контроллеров по адресам


            app.Run();
        }
    }
}