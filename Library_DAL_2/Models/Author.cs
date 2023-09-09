﻿using System.Text;

namespace Library_DAL_2.Models
{
    public class Author
    {
        public int? AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FullName
        {
            get => FirstName + MiddleName + LastName;
            set { }
        }
        public List<Book> Books { get; set; } 
        public List<BooksAuthor> BooksAuthors { get; set; }

        public override string ToString()
        {
            return $"Author ID: {AuthorID}, First name: {FirstName}, Middle name: {MiddleName}, Last name: {LastName}";
        }

    }
}