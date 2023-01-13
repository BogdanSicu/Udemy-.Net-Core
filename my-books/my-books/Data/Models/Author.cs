using System.Collections.Generic;

namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //navigation properties
        public List<Book_Author> Books_Authors { get; set; }
    }
}
