﻿using AutoMapper;
using Library.Common.DTO.Authors;
using Library.Common.Interfaces.Authors;
using Library.Common.Models;
using Library.DAL;
using Library.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddAuthor(AuthorAddDTO newAuthor)
        {
            if (await FindAuthors(newAuthor.FirstName + " " + (newAuthor.MiddleName == null ? "" : (newAuthor.MiddleName + " ")) + newAuthor.LastName) != null)
                return new ServiceResult(false, "Author already exists");

            var author = _mapper.Map<Author>(newAuthor);

            await _context.Authors.AddAsync(author);

            if (await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Author added");
            return new ServiceResult(false, "No changes");
        }

        public async Task<ServiceResult> DeleteAuthor(int deleteAuthorID)
        {
            if (!await IsAuthorExistByID(deleteAuthorID))
                return new ServiceResult(false, "Author not found");

            var author = await _context.Authors.FirstOrDefaultAsync(b => b.AuthorID == deleteAuthorID);

            var bookAuthor = await _context.BooksAuthors.Where(ba => ba.BookID == deleteAuthorID).ToListAsync();
            _context.BooksAuthors.RemoveRange(bookAuthor);
            _context.Authors.Remove(author!);

            if(await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Author deleted");
            return new ServiceResult(false, "No changes");
        }

        public async Task<List<AuthorModel>?> FindAuthors(string query)
        {
            List<Author>? authors = null;
            List<AuthorModel>? result = null;

            if (await _context.Authors.AsNoTracking().AnyAsync(a => a.FullName!.Contains(query)))
            {
                authors = new();
                authors = await _context.Authors.AsNoTracking()
                            .Include(a => a.Books!)
                                .ThenInclude(b => b.BooksAuthors!)
                                   .ThenInclude(ba => ba.Author)
                            .Where(a => a.FullName!.Contains(query))
                            .ToListAsync();
            }
            if (authors != null)
                result = _mapper.Map<List<AuthorModel>>(authors);

            return result;
        }
        public async Task<bool> IsAuthorExistByID(int? authorID) => await _context.Authors.AsNoTracking().AnyAsync(b => b.AuthorID == authorID);

        public async Task<bool> IsAuthorExistByListID(List<int> authorListID)
        {
            bool result = false;
            if (authorListID.Count > 0)
            {
                foreach (var authID in authorListID)
                {
                    result = await _context.Authors.AsNoTracking().AnyAsync(b => b.AuthorID == authID);
                }
            }
            return result;
        }

        public async Task<ServiceResult> UpdateAuthor(AuthorUpdateDTO updateAuthor)
        {
            if (!await IsAuthorExistByID(updateAuthor.AuthorID))
                return new ServiceResult(false, "Author not found");

            var author = await _context.Authors.FirstOrDefaultAsync(b => b.AuthorID == updateAuthor.AuthorID);

            if (author != null)
            {
                author.FirstName = updateAuthor.FirstName ?? author.FirstName;
                author.MiddleName = updateAuthor.MiddleName ?? author.MiddleName;
                author.LastName = updateAuthor.LastName ?? author.LastName;
            }

            if(await _context.SaveChangesAsync() > 0)
                return new ServiceResult(true, "Author updated");
            return new ServiceResult(false, "No changes");
        }
    }
}
