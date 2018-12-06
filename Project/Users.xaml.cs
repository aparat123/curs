
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interface
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
       static StudLibSys studLibSys;
        Changes changes;
        BooksList booksList;
 
        public Users(StudLibSys studLib)
        {
           // studLibSys = new StudLibSys();
            changes = new Changes();
            booksList = new BooksList();

            studLibSys = studLib;

            InitializeComponent();

            ListView_Users.DataContext = studLibSys;

            changes.btn_change.Click += Btn_Edit;
            booksList.btn_return_book.Click += Btn_Return_Book;
            booksList.btn_back.Click += Btn_Back_BookList;
            changes.btn_back.Click += Change_Btn_Back;
      
        }
        //закрити вікно редагування імені/прізвища/акад групи
        private void Btn_Back_BookList(object sender, RoutedEventArgs e)
        {
            //booksList.ListView_BookList.DataContext = null;
            booksList.Hide();

        }

        //кнопка повернення книги
        private void Btn_Return_Book(object sender, RoutedEventArgs e)
        {
            if (booksList.ListView_BookList.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть книгу");
            }
            else
            {
                studLibSys.Delete_book_from_user(studLibSys.Users[ListView_Users.SelectedIndex],
                                            studLibSys.Users[ListView_Users.SelectedIndex].User_Books[booksList.ListView_BookList.SelectedIndex]);
                MessageBox.Show("Книгу успішно повернено до бібліотеки.");
                booksList.ListView_BookList.Items.Refresh();
                

            }
        }
        //редагування імені/прізвища/групи користувача, показ його книг
        private void Btn_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"[А-Я]{1}[а-я]{2,15}";
                if (changes.text_changes.Text == "Введіть нове ім'я")
                {
                    if (Regex.IsMatch(changes.new_changes.Text, pattern))
                    {
                        studLibSys.Change_User(studLibSys.Users[ListView_Users.SelectedIndex], changes.new_changes.Text, 1);

                    }
                    else
                    {
                        throw new Exception("Ім'я написано неправильно");
                    }
                }
                else if (changes.text_changes.Text == "Введіть нове прізвище")
                {
                    if (Regex.IsMatch(changes.new_changes.Text, pattern))
                    {
                        studLibSys.Change_User(studLibSys.Users[ListView_Users.SelectedIndex], changes.new_changes.Text, 2);
                    }
                    else
                    {
                        throw new Exception("Прізвище написано неправильно");
                    }
                }
                else if (changes.text_changes.Text == "Введіть нову акад групу")
                {
                    if ((Int32.Parse(changes.new_changes.Text)) > 0)
                    {
                        studLibSys.Change_User(studLibSys.Users[ListView_Users.SelectedIndex], changes.new_changes.Text, 3);
                    }
                    else
                    {
                        throw new Exception("Академічну группу написано неправильно");
                    }
                }
                changes.new_changes.Text = "";
                changes.Hide();
                ListView_Users.Items.Refresh();
               
            }
            catch (Exception l)
            {
                MessageBox.Show(l.Message);
            }
        }
        //кнопка редагування імені в контекстному меню
        private void menuitem_change_first_name(object sender, RoutedEventArgs e)
        {
            changes.text_changes.Text = "Введіть нове ім'я";
            changes.Show();
        }
        //кнопка редагування імені в контекстному меню
        private void menuitem_showBooks(object sender, RoutedEventArgs e)
        {
            
            booksList.ListView_BookList.DataContext = studLibSys.Users[ListView_Users.SelectedIndex].User_Books;
            booksList.ListView_BookList.Items.Refresh();
            booksList.Show();
            booksList.ListView_BookList.DataContext = studLibSys.Users[ListView_Users.SelectedIndex].User_Books;

            
        }
        //перегляд даних користувача
        private void menuitem_showInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(studLibSys.Users[ListView_Users.SelectedIndex].First_Name + " " + 
                studLibSys.Users[ListView_Users.SelectedIndex].Last_Name + ", академічна група номер " + 
                studLibSys.Users[ListView_Users.SelectedIndex].Acad_Group + ", кількість книг: " + 
                studLibSys.Users[ListView_Users.SelectedIndex].User_Books.Count);
        }
        //кнопка редагування прізвища в контекстному меню
        private void menuitem_change_last_name(object sender, RoutedEventArgs e)
        {
            changes.text_changes.Text = "Введіть нове прізвище";
            changes.Show();
        }
        //кнопка редагування академічної групи в контекстному меню
        private void menuitem_chnge_acad_group(object sender, RoutedEventArgs e)
        {
            changes.text_changes.Text = "Введіть нову акад групу";
            changes.Show();
        }
        //кнопка закриття вікна редагування
        private void Change_Btn_Back(object sender, RoutedEventArgs e)
        {
            changes.Hide();
        }

        //зміни в інтерфейсі при додаванні нового користувача
        private void Button_Add_User(object sender, RoutedEventArgs e)
        {
            Add_User.Visibility = Visibility.Hidden;
            User_Delete.Visibility = Visibility.Hidden;
            New_First_Name.Visibility = Visibility.Visible;
            New_Last_Name.Visibility = Visibility.Visible;
            New_Acad_Group.Visibility = Visibility.Visible;
            Add_user_btn.Visibility = Visibility.Visible;
            Btn_Back.Visibility = Visibility.Visible;
            New_First_Name_Text.Visibility = Visibility.Visible;
            New_Last_Name_Text.Visibility = Visibility.Visible;
            New_Acad_Group_Text.Visibility = Visibility.Visible;
            Search_Btn.Visibility = Visibility.Hidden;
            Search_TextBox.Visibility = Visibility.Hidden;
            Back_Search_Btn.Visibility = Visibility.Hidden;
            Search_TextBlock.Visibility = Visibility.Hidden;
        }
        //зміни в інтерфейсі після додавання нового користувача
        private void Button_Back_User(object sender, RoutedEventArgs e)
        {
            Add_User.Visibility = Visibility.Visible;
            User_Delete.Visibility = Visibility.Visible;
            New_First_Name.Visibility = Visibility.Hidden;
            New_Last_Name.Visibility = Visibility.Hidden;
            New_Acad_Group.Visibility = Visibility.Hidden;
            Add_user_btn.Visibility = Visibility.Hidden;
            Btn_Back.Visibility = Visibility.Hidden;
            New_First_Name_Text.Visibility = Visibility.Hidden;
            New_Last_Name_Text.Visibility = Visibility.Hidden;
            New_Acad_Group_Text.Visibility = Visibility.Hidden;
            Search_Btn.Visibility = Visibility.Visible;
            Search_TextBox.Visibility = Visibility.Visible;
            Search_TextBlock.Visibility = Visibility.Visible;
        }
        //створення нового користувача
        private void Button_New_User(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"[А-ЯЇІЄҐ]{1}[а-яїієґ]{2,15}";
                if (Regex.IsMatch(New_First_Name.Text, pattern))
                {
                    if (Regex.IsMatch(New_Last_Name.Text, pattern))
                    {

                        if (int.Parse(New_Acad_Group.Text) > 0)
                        {
                            User user = new User(New_First_Name.Text, New_Last_Name.Text, int.Parse(New_Acad_Group.Text));
                            studLibSys.Add_User(user);


                        }
                        else
                        {
                            throw new Exception("Неправильний номер групи");
                        }

                    }
                    else
                    {
                        throw new Exception("Прізвище написано неправильно");
                    }
                }
                else
                {
                    throw new Exception("Ім'я написано неправильно");
                }
                ListView_Users.Items.Refresh();
                New_First_Name.Text = "";
                New_Last_Name.Text = "";
                New_Acad_Group.Text = "";
                Add_User.Visibility = Visibility.Visible;
                User_Delete.Visibility = Visibility.Visible;
                New_First_Name.Visibility = Visibility.Hidden;
                New_Last_Name.Visibility = Visibility.Hidden;
                New_Acad_Group.Visibility = Visibility.Hidden;
                Add_user_btn.Visibility = Visibility.Hidden;
                Btn_Back.Visibility = Visibility.Hidden;
                New_First_Name_Text.Visibility = Visibility.Hidden;
                New_Last_Name_Text.Visibility = Visibility.Hidden;
                New_Acad_Group_Text.Visibility = Visibility.Hidden;
                Search_Btn.Visibility = Visibility.Visible;
                Search_TextBox.Visibility = Visibility.Visible;
                Search_TextBlock.Visibility = Visibility.Visible;

                throw new Exception("Користувач успішно створений!");

            }
            catch (Exception l)
            {
                MessageBox.Show(l.Message);
            }
        }
        //видалення існуючого користувача
        private void Btn_User_Delete(object sender, RoutedEventArgs e)
        {
            if (ListView_Users.SelectedIndex == -1)
            {
                MessageBox.Show("Виберіть користувача зі списку.");
            }
            else
            {
                studLibSys.Delete_User(studLibSys.Users[ListView_Users.SelectedIndex]);
                ListView_Users.Items.Refresh();
            }
        }

        //кнопка закриття даного вікна
        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        //сортування списку користувачів за ім'ям
        private void Btn_Sort_FirstName(object sender, RoutedEventArgs e)
        {
            studLibSys.Users.Sort((a, b) => a.First_Name.CompareTo(b.First_Name));
            ListView_Users.Items.Refresh();
        }
        //сортування списку користувачів за прізвищем
        private void Btn_Sort_LastName(object sender, RoutedEventArgs e)
        {
            studLibSys.Users.Sort((a, b) => a.Last_Name.CompareTo(b.Last_Name));
            ListView_Users.Items.Refresh();
        }
        //сортування списку користувачів за академічною групою
        private void Btn_Sort_AcadGroup(object sender, RoutedEventArgs e)
        {
            studLibSys.Users.Sort((a, b) => a.Acad_Group.CompareTo(b.Acad_Group));
            ListView_Users.Items.Refresh();
        }
        //кнопка пошучу за ключовим словом
        private void Click_Search_Btn(object sender, RoutedEventArgs e)
        {
            ListView_Users.ItemsSource = studLibSys.Search_Users_by_word(Search_TextBox.Text);
            Back_Search_Btn.Visibility = Visibility.Visible;
        }
        //кнопка повернення всього списку
        private void Click_Back_Search_Btn(object sender, RoutedEventArgs e)
        {
            ListView_Users.ItemsSource = studLibSys.Users;
            Search_TextBox.Text = "";
            Back_Search_Btn.Visibility = Visibility.Hidden;
        }
    }
}
