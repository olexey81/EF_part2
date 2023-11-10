using System.Text.Json.Serialization;

namespace Library_DAL_2.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; } = null!;
        public int Author { get; set; }
        public string PublCode { get; set; } = string.Empty;
        public int? PublCodeType { get; set; }
        public int Year { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? City { get; set; } = null!;
        public bool AtReader { get; set; }

        //[JsonIgnore]
        public Author? AuthorNavigation { get; set; }
        [JsonIgnore]
        public List<BooksAuthor>? BooksAuthors { get; set; }
        //[JsonIgnore]
        public PublCodeType? PublCodeTypeNavigation { get; set; }

        public override string ToString()
        {
            string authors = string.Join(", ", BooksAuthors!.Select(ba => ba.Author.FirstName + " " + ba.Author.LastName));

            if (AtReader == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return $"{BookID}. Book title: \"{Title}\"\tAuthors: {authors}  Warning - book is in use by a reader";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            return $"{BookID}. Book title: \"{Title}\"\tAuthors: {authors}";
        }

    }
}

