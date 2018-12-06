using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serialization;

namespace Logic
{
    [Serializable]
    public class StudLibSys
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }

        public StudLibSys()
        {
            Users = new List<User>();
            Books = new List<Book>();

            StudLibSys studL = (StudLibSys)BinarySerialization.Deserialize();
            if (studL != null)
            {
                foreach (User user in studL.Users)
                {
                    Users.Add(user);
                }
                foreach (Book book in studL.Books)
                {
                    Books.Add(book);
                }
            }
        }

        public void Add_User(User user)
        {
            Users.Add(user);
            Serialize_System();
        }
        public void Add_Book(Book book)
        {
            Books.Add(book);
            Serialize_System();
        }
        public void Delete_book_from_user(User user, Book book)
        {
            user.User_Books.Remove(book);
            book.users.Remove(user);
            Serialize_System();
        }
        public void Serialize_System()
        {
           // BinarySerialization.Deserialize();
            BinarySerialization.Serialize(this);
            
        }
        

        public void Change_User(User user, string new_string, int option)
        {
            if (option == 1)
            {
                user.Change_First_Name(new_string);
            }
            else if(option == 2)
            {
                user.Change_Last_Name(new_string);
            }
            else if (option == 3)
            {
                user.Change_Acad_Group(Int32.Parse(new_string));
            }
            Serialize_System();
        }
        public void Delete_User(User user)
        {
            Users.Remove(user);
            Serialize_System();
        }
        public void Delete_Book(Book book)
        {
            Books.Remove(book);
            Serialize_System();
        }
        public List<User> Search_Users_by_word(string str)
        {
            List<User> users = new List<User>();
            string pattern = $@"{str}";
            foreach (User user in Users)
            {
                if (Regex.IsMatch(user.First_Name, pattern) ||
                    Regex.IsMatch(user.Last_Name, pattern) ||
                    Regex.IsMatch(user.First_Name.ToLower(), pattern) ||
                    Regex.IsMatch(user.Last_Name.ToLower(), pattern))
                {
                    users.Add(user);
                }
            }
            return users;
        }
        public List<Book> Search_Book_by_word(string str)
        {
            List<Book> books = new List<Book>();
            string pattern = $@"{str}";
            foreach (Book book in Books)
            {
                if (Regex.IsMatch(book.Book_Name, pattern) ||
                    Regex.IsMatch(book.Book_Author, pattern) ||
                    Regex.IsMatch(book.Book_Name.ToLower(), pattern) ||
                    Regex.IsMatch(book.Book_Author.ToLower(), pattern))
                {
                    books.Add(book);
                }
            }
            return books;
        }
        public void Change_Book(Book book, string new_string, int option)
        {
            if (option == 1)
            {
                book.Change_Book_Name(new_string);
            }
            else if (option == 2)
            {
                book.Change_Author_Book(new_string);
            }
            
            Serialize_System();
        }
        public void Add_Book_From_User(User user, Book book)
        {
                book.users.Add(user);
                user.User_Books.Add(book);
                Serialize_System();
            
        }


    }
}
