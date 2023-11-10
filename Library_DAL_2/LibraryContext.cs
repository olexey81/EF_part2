using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Library_DAL_2
{
    public class LibraryContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksAuthor> BooksAuthors { get; set; }
        public DbSet<DocumentsType> DocumentsTypes { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<PublCodeType> PublCodeTypes { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<History> Histories { get; set; }
        public LibraryContext() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath("D:\\OneDrive\\Coding\\СSh_Pro\\EF_part2\\Library.API"/*Directory.GetCurrentDirectory()*/);
            configBuilder.AddJsonFile("appsettings.json");

            optionsBuilder
                .UseSqlServer(configBuilder.Build()
                .GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(20);
                entity.Property(e => e.PasswordHash)
                    .IsRequired();
                entity.Property(e => e.PasswordSalt)
                     .IsRequired();

                var (hash, salt) = GetHash("987654");
                entity.HasData(
                    new Admin()
                    {
                        Login = "AngryAdmin",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "super@admin.net"
                    });
            });

            modelBuilder.Entity<Author>(entity =>
            {
            entity.HasKey(e => e.AuthorID);

            entity.Property(e => e.FirstName)
                .HasMaxLength(20);
            entity.Property(e => e.LastName)
                .HasMaxLength(20);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20);
            entity.Property(e => e.FullName)
                .HasMaxLength(60);

            entity.HasData(
                new Author() { AuthorID = 1, FirstName = "J.K.", LastName = "Rowling", MiddleName = null },
                new Author() { AuthorID = 2, FirstName = "George", LastName = "Orwell", MiddleName = null },
                new Author() { AuthorID = 3, FirstName = "Mykova", LastName = "Gogol", MiddleName = "Vasylyovich" },
                new Author() { AuthorID = 4, FirstName = "New", LastName = "Rowling", MiddleName = null } );
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookID);

                entity.Property(e => e.City)
                    .HasMaxLength(20);
                entity.Property(e => e.Country)
                    .HasMaxLength(20);
                entity.Property(e => e.PublCode)
                    .HasMaxLength(20);
                entity.Property(e => e.Title)
                    .HasMaxLength(50);
                entity.Property(e => e.Genre)
                    .HasMaxLength(50);
                entity.Property(e => e.Year)
                    .HasMaxLength(4);

                entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                    .HasForeignKey(d => d.Author);

                entity.HasOne(d => d.PublCodeTypeNavigation).WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublCodeType);

                entity.HasData(
                new Book()
                {
                    BookID = 1,
                    Title = "Harry Potter and the Philosopher''s Stone",
                    Author = 1,
                    PublCode = "966-7047-39-3",
                    Genre = "Tale",
                    PublCodeType = 1,
                    Year = 1997,
                    Country = "UK",
                    City = "London",
                },
                new Book()
                {
                    BookID = 2,
                    Title = "1984",
                    Author = 2,
                    PublCode = "978-966-2355-57-4",
                    Genre = "Dystopia",
                    PublCodeType = 1,
                    Year = 1949,
                    Country = "UK",
                    City = "London"
                },
                new Book()
                {
                    BookID = 3,
                    Title = "Taras Bulba",
                    Author = 3,
                    PublCode = "966-663-179-2",
                    Genre = "Fiction",
                    PublCodeType = 2,
                    Year = 1835,
                    Country = "Ukraine"
                });
            });

            modelBuilder.Entity<BooksAuthor>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasOne(d => d.Author).WithMany(p => p.BooksAuthors)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(d => d.AuthorID);

                entity.HasOne(d => d.Book).WithMany(p => p.BooksAuthors)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(d => d.BookID);

                entity.HasData(
                    new BooksAuthor() { ID = 1, BookID = 1, AuthorID = 1 },
                    new BooksAuthor() { ID = 2, BookID = 2, AuthorID = 2 },
                    new BooksAuthor() { ID = 3, BookID = 3, AuthorID = 3 },
                    new BooksAuthor() { ID = 4, BookID = 2, AuthorID = 1 },
                    new BooksAuthor() { ID = 5, BookID = 3, AuthorID = 2 } );
            });

            modelBuilder.Entity<DocumentsType>(entity =>
            {
                entity.HasKey(e => e.DocTypeID);

                entity.Property(e => e.DocTypeID)
                    .HasMaxLength (2);
                entity.Property(e => e.TypeName)
                    .HasMaxLength(50);
               
                entity.HasData(
                    new DocumentsType() { DocTypeID = 1, TypeName = "Passport" },
                    new DocumentsType() { DocTypeID = 2, TypeName = "Driver's License" },
                    new DocumentsType() { DocTypeID = 3, TypeName = "ID card" },
                    new DocumentsType() { DocTypeID = 4, TypeName = "Resident permission" } );
            });

            modelBuilder.Entity<Librarian>(entity =>
            {
                entity.ToTable("Librarians");
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(20);
                entity.Property(e => e.Email)
                    .HasMaxLength(50);
                entity.Property(e => e.PasswordHash)
                    .IsRequired();
                entity.Property(e => e.PasswordSalt)
                     .IsRequired();

                var (hash, salt) = GetHash("00000");
                entity.HasData(
                    new Librarian()
                    {
                        Login = "mainLib",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "mainLib@lib.com.ua"
                    });

                (hash, salt) = GetHash("11111");
                entity.HasData(
                    new Librarian()
                    {
                        Login = "middleLib",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "middleLib@lib.com.ua"
                    });

                (hash, salt) = GetHash("22222");
                entity.HasData(
                    new Librarian()
                    {
                        Login = "youngLib",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "youngLib@lib.com.ua"
                    });

                (hash, salt) = GetHash("12345");
                entity.HasData(
                    new Librarian()
                    {
                        Login = "111",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "111@lib.com.ua"
                    }
                    );
            });

            modelBuilder.Entity<PublCodeType>(entity =>
            {
                entity.HasKey(e => e.PublCodeTypeID);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(10);
                
                entity.HasData(
                    new PublCodeType() { PublCodeTypeID = 1, TypeName = "ISBN" },
                    new PublCodeType() { PublCodeTypeID = 2, TypeName = "BBK" });
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(20);
                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(20);
                entity.Property(e => e.Email)
                    .HasMaxLength(50);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(20);
                entity.Property(e => e.LastName)
                    .HasMaxLength(20);
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20);
                entity.Property(e => e.FullName)
                    .HasMaxLength(60);
                entity.Property(e => e.PasswordHash)
                    .IsRequired();
                entity.Property(e => e.PasswordSalt)
                    .IsRequired();

                entity.HasOne(d => d.DocumentTypeNavigation).WithMany(p => p.Readers)
                    .HasForeignKey(d => d.DocumentType);

                var (hash, salt) = GetHash("00000");
                entity.HasData(
                    new Reader()
                    {
                        Login = "ad_dr",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "a.d@lib.com.ua",
                        FirstName = "Adam",
                        LastName = "Driser",
                        DocumentNumber = "N12345",
                        DocumentType = 2
                    }
                );

                (hash, salt) = GetHash("11111");
                entity.HasData(
                    new Reader()
                    {
                        Login = "ph_co",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "p.c@lib.com.ua",
                        FirstName = "Phill",
                        LastName = "Colins",
                        DocumentNumber = "w23423",
                        DocumentType = 1
                    }
                );

                (hash, salt) = GetHash("22222");
                entity.HasData(
                    new Reader()
                    {
                        Login = "pe_pe",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "p.p@lib.com.ua",
                        FirstName = "Petro",
                        LastName = "Petrenko",
                        MiddleName = "Andriyovich",
                        DocumentNumber = "789456",
                        DocumentType = 3
                    }
                );

                (hash, salt) = GetHash("33333");
                entity.HasData(
                    new Reader()
                    {
                        Login = "va_ko",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "v.k@lib.com.ua",
                        FirstName = "Vasyl",
                        LastName = "Kovtun",
                        MiddleName = "Ivanovych",
                        DocumentNumber = "654897",
                        DocumentType = 3
                    }
                );

                (hash, salt) = GetHash("12345");
                entity.HasData(
                    new Reader()
                    {
                        Login = "222",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "222@lib.com.ua",
                        FirstName = "222",
                        LastName = "222",
                        MiddleName = "222",
                        DocumentNumber = "222",
                        DocumentType = 3
                    }
                );

                (hash, salt) = GetHash("12345");
                entity.HasData(
                    new Reader()
                    {
                        Login = "333",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "333@lib.com.ua",
                        FirstName = "333",
                        LastName = "333",
                        MiddleName = "333",
                        DocumentNumber = "222",
                        DocumentType = 3
                    }
                );

                (hash, salt) = GetHash("44444");
                entity.HasData(
                    new Reader()
                    {
                        Login = "pa_lu",
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Email = "p.l@lib.com.ua",
                        FirstName = "Patrice",
                        LastName = "Lumumba",
                        DocumentNumber = "0123456J",
                        DocumentType = 4
                    }
                );

            });
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.AllowedRentDays)
                .HasMaxLength(3);

                entity.HasOne(h => h.Reader)
                .WithMany()
                .HasForeignKey(h => h.ReaderID);
            });
        }

        private (byte[], byte[]) GetHash(string password)
        {
            var hmac = new HMACSHA512();
            var PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var PasswordSalt = hmac.Key;
            hmac.Dispose();
            return (PasswordHash, PasswordSalt);
        }
    }
}