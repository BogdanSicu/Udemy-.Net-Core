using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public BookWithAuthorsViewModel GetBooksById (int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(obj => obj.Id == bookId).Select(myBook => new BookWithAuthorsViewModel()
            {
                Title = myBook.Title,
                Description = myBook.Description,
                IsRead = myBook.IsRead,
                DateRead = myBook.IsRead ? myBook.DateRead.Value : null,
                Rate = myBook.IsRead ? myBook.Rate.Value : null,
                Genre = myBook.Genre,
                CoverUrl = myBook.CoverUrl,
                PublisherName = myBook.Publisher.Name,
                AuthorsNames = myBook.Books_Authors.Select(obj => obj.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public void AddBookWithAuthors(BookViewModel book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorsIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public Book UpdateBookById(int id, BookViewModel bookViewModel)
        {
            var _book = _context.Books.FirstOrDefault(book => book.Id == id);
            if(_book != null)
            {
                _book.Title = bookViewModel.Title;
                _book.Description = bookViewModel.Description;
                _book.IsRead = bookViewModel.IsRead;
                _book.DateRead = bookViewModel.IsRead ? bookViewModel.DateRead.Value : null;
                _book.Rate = bookViewModel.IsRead ? bookViewModel.Rate.Value : null;
                _book.Genre = bookViewModel.Genre;
                _book.CoverUrl = bookViewModel.CoverUrl;

                _context.SaveChanges();
            }

            return _book;
        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(book => book.Id == id);
            if(_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
