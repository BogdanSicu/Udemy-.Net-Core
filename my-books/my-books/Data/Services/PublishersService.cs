using Microsoft.VisualBasic;
using my_books.Data.Models;
using my_books.Data.ViewModels;

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
    }
}
