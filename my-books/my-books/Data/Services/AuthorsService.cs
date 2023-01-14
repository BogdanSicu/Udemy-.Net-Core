using my_books.Data.Models;
using my_books.Data.ViewModels;

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
    }
}
