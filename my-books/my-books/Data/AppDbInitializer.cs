using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_books.Data.Models;
using System;
using System.Linq;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st book title",
                        Description = "1st book description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        Author = "First Author",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now
                    }, 
                    new Book()
                    {
                        Title = "2nd book title",
                        Description = "2nd book description",
                        IsRead = false,
                        Genre = "Biography",
                        Author = "First Author",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
