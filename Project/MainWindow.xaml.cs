using Logic;
using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Users users;
        Books books;
        static StudLibSys studLibSys;

        public MainWindow()
        {
            studLibSys = new StudLibSys();
            books = new Books(studLibSys);
            users = new Users(studLibSys);
            InitializeComponent();
        }

       //відкриття вікна управління користувачами
        private void Button_Users_Click(object sender, RoutedEventArgs e)
        {
            users.Show();

        }
        //відкриття вікна управління книгами
        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            books.Show();
        }
        //завершення роботи
        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
