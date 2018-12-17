using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic.Tests
{
    [TestClass]
    public class StudLibSysTests
    {
        [TestMethod]
        public void Add_User_Test()
        {
            //arrange
            string first_name = "Dima";
            string last_name = "Holubenko";
            int acad_group = 220;

            //act
            User user = new User(first_name, last_name, acad_group);
            string profile = user.First_Name + user.Last_Name + user.Acad_Group;

            //assert
            Assert.AreEqual(first_name + last_name + acad_group, profile);
        }
        [TestMethod]
        public void Add_Book_Test()
        {
            //arrange
            string book_name = "Book";
            string book_author = "Author";

            //act
            Book book = new Book(book_name, book_author);
            string book_profile = book.Book_Author + book.Book_Name;

            //assert
            Assert.AreEqual(book_author + book_name, book_profile);
        }
        [TestMethod]
        public void Delete_User_Test()
        {
            List<User> users = new List<User>();
            string first_name1 = "Dimaaaa";
            string last_name1 = "Holubenko";
            int acad_group1 = 220;
            User user1 = new User(first_name1, last_name1, acad_group1);
            StudLibSys studLibSys = new StudLibSys();

            studLibSys.Add_User(user1);
            studLibSys.Delete_User(user1);

            Assert.IsNull(studLibSys.Search_Users_by_word(first_name1));
        }
        [TestMethod]
        public void Delete_Book_Test()
        {
            string book_name1 = "Books";
            string book_author1 = "Author";
            Book book1 = new Book(book_name1, book_author1);
            StudLibSys studLibSys1 = new StudLibSys();

            studLibSys1.Add_Book(book1);
            studLibSys1.Delete_Book(book1);

            Assert.IsNull(studLibSys1.Search_Book_by_word(book1.Book_Name));
        }
        [TestMethod]
        public void Change_Book_Test()
        {
            string new_book_name = "qwerty123";
            string new_book_author = "New_Author";
            int option1 = 1;
            int option2 = 2;
            Book book2 = new Book("qwerty", "author");
            StudLibSys studLibSys2 = new StudLibSys();

            studLibSys2.Add_Book(book2);
            studLibSys2.Change_Book(book2, new_book_name, option1);
            studLibSys2.Change_Book(book2, new_book_author, option2);

            Assert.AreEqual(new_book_name, book2.Book_Name);
            Assert.AreEqual(new_book_author, book2.Book_Author);
        }
        [TestMethod]
        public void Change_User_Test()
        {
            string new_first_name = "Firts_Name";
            string new_last_name = "Last_Name";
            string new_acad_group = "321";
            int option1 = 1;
            int option2 = 2;
            int option3 = 3;
            User user2 = new User("User", "Dima", 123);
            StudLibSys studLibSys3 = new StudLibSys();

            studLibSys3.Add_User(user2);
            studLibSys3.Change_User(user2, new_first_name, option1);
            studLibSys3.Change_User(user2, new_last_name, option2);
            studLibSys3.Change_User(user2, new_acad_group, option3);

            Assert.AreEqual(new_first_name, user2.First_Name);
            Assert.AreEqual(new_last_name, user2.Last_Name);
            Assert.AreEqual(Int32.Parse(new_acad_group), user2.Acad_Group);
        }
        [TestMethod]
        public void Search_Users_by_word_Test()
        {
            string user_word = "Dima";
            User user3 = new User("Dima", "Holubenko", 220);
            StudLibSys studLibSys4 = new StudLibSys();

            studLibSys4.Add_User(user3);

            Assert.IsNotNull(studLibSys4.Search_Users_by_word(user_word));
        }
        [TestMethod]
        public void Search_Book_by_word_Test()
        {
            string book_word = "Book";
            Book book3 = new Book("Book", "Author");
            StudLibSys studLibSys5 = new StudLibSys();

            studLibSys5.Add_Book(book3);

            Assert.IsNotNull(studLibSys5.Search_Book_by_word(book_word));
        }
        [TestMethod]
        public void Delete_book_from_user_Test()
        {
            User user = new User("Ivan", "Ivanov", 250);
            Book book = new Book("Book_name", "Book_author");
            StudLibSys studLibSys = new StudLibSys();

            studLibSys.Add_Book_From_User(user, book);
            studLibSys.Delete_book_from_user(user, book);
            
            Assert.IsTrue(user.User_Books.Find(x => x.Book_Name == book.Book_Name) == null);
        }
        [TestMethod]
        public void Add_Book_From_User_Test()
        {
            User user = new User("Petro", "Petrov", 34);
            Book book = new Book("Book_names", "Book_authors");
            StudLibSys studLibSys = new StudLibSys();

            studLibSys.Add_Book_From_User(user, book);

            Assert.IsFalse(user.User_Books.Find(x => x.Book_Name == book.Book_Name) == null);
        }
    }
}
