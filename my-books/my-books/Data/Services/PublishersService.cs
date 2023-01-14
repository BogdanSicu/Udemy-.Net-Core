using Microsoft.VisualBasic;
using my_books.Data.Exceptions;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherViewModel publisherVM)
        {
            if(StringStartsWithNumber(publisherVM.Name))
            {
                throw new PublisherNameException("Name starts with number", publisherVM.Name);
            }

            var _publisher = new Publisher()
            {
                Name = publisherVM.Name 
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(n => n.Id == id);
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

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(obj => obj.Id == id);
            if(_publisher != null )
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            } else
            {
                throw new Exception($"The publisher with the id {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d");
        }
    }
}
