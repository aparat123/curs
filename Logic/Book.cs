using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [Serializable]
    public class Book
    {
        public event EventHandler Event;

        public string Book_Name { get; set; }
        public string Book_Author { get; set; }
        public List<User> users { get; set; }
        public Book(string book_name, string book_author)
        {
            this.Book_Name = book_name;
            this.Book_Author = book_author;
            users = new List<User>();
        }
        public string Change_Book_Name(string book_name)
        {
            Book_Name = book_name;
            return Book_Name;
        }
        public string Change_Author_Book(string author_book)
        {
            Book_Author = author_book;
            return Book_Author;
        }
    }
}
