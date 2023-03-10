using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Linq;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorViewModel authorVM)
        {
            var _author = new Author()
            {
                FullName = authorVM.FullName,
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksViewModel GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(obj => obj.Id == authorId).Select(obj => new AuthorWithBooksViewModel()
            {
                FullName = obj.FullName,
                BookTitles = obj.Books_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}
