using Microsoft.VisualBasic;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Linq;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherViewModel publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name 
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public PublisherWithBooksAndAuthorsViewModel GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(obj => obj.Id== publisherId)
                .Select(obj => new PublisherWithBooksAndAuthorsViewModel()
                {
                    Name = obj.Name,
                    BookAuthors = obj.Books.Select(n => new BookAuthorViewModel()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Books_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }
    }
}
