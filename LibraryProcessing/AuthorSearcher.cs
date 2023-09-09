using Library_DAL_2;
using Library_DAL_2.Models;
using Microsoft.EntityFrameworkCore;

namespace UseContextInfo
{
    public class AuthorSearcher
    {
        private readonly string? _name;
        public List<Author>? Authors { get; private set; }

        public AuthorSearcher(string? name)
        {
            _name = name;
            Authors = null;
        }
        public List<Author>? Search()
        {
            using var context = new LibraryContext();
            if (context.Authors.Any(a => a.FullName.Contains(_name)))
            {
                Authors = context.Authors
                       .Include(a => a.Books)
                           .ThenInclude(b => b.BooksAuthors)
                               .ThenInclude(ba => ba.Author)
                       .Where(a => a.FullName.Contains(_name))
                       .ToList();
                if (Authors.Count > 0)
                {
                    Console.WriteLine("Found the follow authors:");

                    foreach (var a in Authors)
                    {
                        Console.WriteLine(a);
                    }
                }
                return Authors;
            }
            else
            {
                Console.WriteLine("Any authors weren't found.");
                return null;
            }
        }

    }
}
