using Interface;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Interface
{
    /// <summary>
    /// Логика взаимодействия для Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        static StudLibSys studLibSys;
        Changes changes;
        BooksList booksList;
        Choose_User choose_User;
        public Books(StudLibSys studLib)
        {
           // studLibSys = new StudLibSys();
            changes = new Changes();
            choose_User = new Choose_User();
            booksList = new BooksList();

            studLibSys = studLib;
            
            InitializeComponent();
            ListView_Books.DataContext = studLibSys;
              changes.btn_change.Click += Btn_Edit;
              changes.btn_back.Click += Change_Btn_Back;
            choose_User.Choose_Btn.Click += Choose_Book_For_User;

        }

        // вибір користувача якому видати книгу
        private void Choose_Book_For_User(object sender, RoutedEventArgs e)
        {
            choose_User.ListView_ChooseUser.DataContext = studLibSys.Users;
            choose_User.ListView_ChooseUser.Items.Refresh();
            if (ListView_Books.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть користувача зі списку.");
            }
            
            else
            {
                if (studLibSys.Books[ListView_Books.SelectedIndex].users.Count >= 1)
                {
                    MessageBox.Show("Книги немає в наявності.");
                }
                else
                {
                    if (choose_User.ListView_ChooseUser.SelectedIndex == -1)
                    {
                        MessageBox.Show("Оберіть користувача");
                    }
                    else if (studLibSys.Users[choose_User.ListView_ChooseUser.SelectedIndex].User_Books.Count >= 4)
                    {
                        MessageBox.Show("Користувач взяв максимальну кількість книг.");
                    }
                    else
                    {
                        studLibSys.Add_Book_From_User(studLibSys.Users[choose_User.ListView_ChooseUser.SelectedIndex], studLibSys.Books[ListView_Books.SelectedIndex]);
                        MessageBox.Show("Книгу успішно видано.");
                        choose_User.Hide();
                    }
                }
            }
        }


        //зміни в інтерфейсі при додаванні нового користувача
        private void Button_Add_Book(object sender, RoutedEventArgs e)
        {
            Add_Book.Visibility = Visibility.Hidden;
            Book_Delete.Visibility = Visibility.Hidden;
            Give_Out_Book.Visibility = Visibility.Hidden;
            Add_book_btn.Visibility = Visibility.Visible;
            New_Book_Name.Visibility = Visibility.Visible;
            New_Book_Author.Visibility = Visibility.Visible;
            Btn_Back.Visibility = Visibility.Visible;
            New_Book_Name_Text.Visibility = Visibility.Visible;
            New_Book_Author_Text.Visibility = Visibility.Visible;
            Search_Btn.Visibility = Visibility.Hidden;
            Search_TextBox.Visibility = Visibility.Hidden;
            Back_Search_Btn.Visibility = Visibility.Hidden;
            Search_TextBlock.Visibility = Visibility.Hidden;
        }
        //кнопка закриття даного вікна
        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        //кнопка редагування імені в контекстному меню
        private void menuitem_change_book_name(object sender, RoutedEventArgs e)
        {
            changes.text_changes.Text = "Введіть нову назву книги";
            changes.Show();
        }
        //перегляд даних книги
        private void menuitem_showInfo(object sender, RoutedEventArgs e)
        {
            if(studLibSys.Books[ListView_Books.SelectedIndex].users.Count == 0)
            {
                MessageBox.Show("Назва книги: " + studLibSys.Books[ListView_Books.SelectedIndex].Book_Name +
                                ", Автор книги: " + studLibSys.Books[ListView_Books.SelectedIndex].Book_Author +
                                ", наявність: є в бібліотецію");
            }
            else
            {
                MessageBox.Show("Назва книги: " + studLibSys.Books[ListView_Books.SelectedIndex].Book_Name +
                                ", Автор книги: " + studLibSys.Books[ListView_Books.SelectedIndex].Book_Author +
                                ", наявність: взяв користувач з іменем " + studLibSys.Books[ListView_Books.SelectedIndex].users[0].First_Name + 
                                " " + studLibSys.Books[ListView_Books.SelectedIndex].users[0].Last_Name);
            }
        }
        //кнопка редагування імені в контекстному меню
        private void menuitem_showBooksAvai(object sender, RoutedEventArgs e)
        {
            studLibSys.Serialize_System();
            if (studLibSys.Books[ListView_Books.SelectedIndex].users.Count == 0)
            {
                MessageBox.Show("Книга є в наявності.");
            } 
            else
            {
                
                MessageBox.Show("Книга знаходиться у користувача з іменем " + studLibSys.Books[ListView_Books.SelectedIndex].users[0].First_Name + " " + studLibSys.Books[ListView_Books.SelectedIndex].users[0].Last_Name);
            }
        }
        //кнопка редагування прізвища в контекстному меню
        private void menuitem_change_book_author(object sender, RoutedEventArgs e)
        {
            changes.text_changes.Text = "Введіть нового автора книги";
            changes.Show();
        }
        //сортування списку книг за назвою
        private void Btn_Sort_Book_Name(object sender, RoutedEventArgs e)
        {
            //
            studLibSys.Books.Sort((a, b) => a.Book_Name.CompareTo(b.Book_Name));
            ListView_Books.Items.Refresh();
        }
        //сортування списку книг за автором
        private void Btn_Sort_Author_Book(object sender, RoutedEventArgs e)
        {
            //
            studLibSys.Books.Sort((a, b) => a.Book_Author.CompareTo(b.Book_Author));
            ListView_Books.Items.Refresh();
        }
        //створення нової книги
        private void Button_New_Book(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"[А-ЯЇІЄҐ]{1}[а-яїієґ]{2,15}";
                if (Regex.IsMatch(New_Book_Name.Text, pattern))
                {
                    if (Regex.IsMatch(New_Book_Author.Text, pattern))
                    {
                            Book book = new Book(New_Book_Name.Text, New_Book_Author.Text);
                            studLibSys.Add_Book(book);
                    }
                    else
                    {
                        throw new Exception("Автора книги написано неправильно");
                    }
                }
                else
                {
                    throw new Exception("Назву книги написано неправильно");
                }
                ListView_Books.Items.Refresh();
                New_Book_Name.Text = "";
                New_Book_Author.Text = "";
                Add_Book.Visibility = Visibility.Visible;
                Give_Out_Book.Visibility = Visibility.Visible;
                Book_Delete.Visibility = Visibility.Visible;
                Add_book_btn.Visibility = Visibility.Hidden;
                New_Book_Name.Visibility = Visibility.Hidden;
                New_Book_Author.Visibility = Visibility.Hidden;
                Btn_Back.Visibility = Visibility.Hidden;
                New_Book_Name_Text.Visibility = Visibility.Hidden;
                New_Book_Author_Text.Visibility = Visibility.Hidden;
                Search_Btn.Visibility = Visibility.Visible;
                Search_TextBox.Visibility = Visibility.Visible;
                Back_Search_Btn.Visibility = Visibility.Hidden;
                Search_TextBlock.Visibility = Visibility.Visible;

                throw new Exception("Книгу успішно створено!");

            }
            catch (Exception l)
            {
                MessageBox.Show(l.Message);
            }
        }
        //зміни в інтерфейсі після додавання нової книги
        private void Button_Back_User(object sender, RoutedEventArgs e)
        {
            Add_Book.Visibility = Visibility.Visible;
            Book_Delete.Visibility = Visibility.Visible;
            Give_Out_Book.Visibility = Visibility.Visible;
            New_Book_Name.Visibility = Visibility.Hidden;
            New_Book_Author.Visibility = Visibility.Hidden;
            Btn_Back.Visibility = Visibility.Hidden;
            New_Book_Name_Text.Visibility = Visibility.Hidden;
            New_Book_Author_Text.Visibility = Visibility.Hidden;
            Search_Btn.Visibility = Visibility.Visible;
            Search_TextBox.Visibility = Visibility.Visible;
            Back_Search_Btn.Visibility = Visibility.Hidden;
            Search_TextBlock.Visibility = Visibility.Visible;
            Add_book_btn.Visibility = Visibility.Hidden;
        }
        //видалення існуючої книги
        private void Btn_Book_Delete(object sender, RoutedEventArgs e)
        {
            if (ListView_Books.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть книгу зі списку.");
            }
            else
            {
                studLibSys.Delete_Book(studLibSys.Books[ListView_Books.SelectedIndex]);
                ListView_Books.Items.Refresh();
            }
        }
        //кнопка пошучу за ключовим словом
        private void Click_Search_Btn(object sender, RoutedEventArgs e)
        {
            ListView_Books.ItemsSource = studLibSys.Search_Book_by_word(Search_TextBox.Text);
            Back_Search_Btn.Visibility = Visibility.Visible;
        }
        //кнопка повернення всього списку
        private void Click_Back_Search_Btn(object sender, RoutedEventArgs e)
        {
            ListView_Books.ItemsSource = studLibSys.Books;
            Search_TextBox.Text = "";
            Back_Search_Btn.Visibility = Visibility.Hidden;
        }
        //редагування імені/прізвища/групи користувача, показ його книг
        private void Btn_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"[А-Я]{1}[а-я]{2,15}";
                if (changes.text_changes.Text == "Введіть нову назву книги")
                {
                    if (Regex.IsMatch(changes.new_changes.Text, pattern))
                    {
                        studLibSys.Change_Book(studLibSys.Books[ListView_Books.SelectedIndex], changes.new_changes.Text, 1);

                    }
                    else
                    {
                        throw new Exception("Нову назву книги написано неправильно");
                    }
                }
                else if (changes.text_changes.Text == "Введіть нового автора книги")
                {
                    if (Regex.IsMatch(changes.new_changes.Text, pattern))
                    {
                        studLibSys.Change_Book(studLibSys.Books[ListView_Books.SelectedIndex], changes.new_changes.Text, 2);
                    }
                    else
                    {
                        throw new Exception("Нового автора книги написано неправильно");
                    }
                }
                changes.new_changes.Text = "";
                changes.Hide();
                ListView_Books.Items.Refresh();
            }
            catch (Exception l)
            {
                MessageBox.Show(l.Message);
            }
        }
        //кнопка закриття вікна редагування
        private void Change_Btn_Back(object sender, RoutedEventArgs e)
        {
            changes.Hide();
        }

        private void Give_Out_Book_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Books.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть книгу зі списку.");
            }
            else
            {
               
                choose_User.ListView_ChooseUser.DataContext = studLibSys.Users;
                choose_User.Show();
            }
        }
    }
}
