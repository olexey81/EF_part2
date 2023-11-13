using System.Text;
using System.Text.Json.Serialization;

namespace Library_DAL_2.Models
{
    public class Author
    {
        public int? AuthorID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FullName
        {
            get => FirstName + " " + (MiddleName == null ? "" : (MiddleName + " ")) + LastName;
            set { }
        }
        [JsonIgnore]
        public List<Book>? Books { get; set; }
        [JsonIgnore]
        public List<BooksAuthor>? BooksAuthors { get; set; }

        public override string ToString()
        {
            return $"Author ID: {AuthorID}, First name: {FirstName}, Middle name: {MiddleName}, Last name: {LastName}";
        }

    }
}
