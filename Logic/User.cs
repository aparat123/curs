using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [Serializable]
    public class User
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Acad_Group { get; set; }
        public List<Book> User_Books { get; set; }

        public User(string first_name, string last_name, int acad_group)
        {
            this.Acad_Group = acad_group;
            this.First_Name = first_name;
            this.Last_Name = last_name;
            User_Books = new List<Book>();

        }
        public string Change_First_Name(string first_name)
        {
            First_Name = first_name;
            return First_Name;
        }
        public string Change_Last_Name(string last_name)
        {
            Last_Name = last_name;
            return Last_Name;
        }
        public int Change_Acad_Group(int acad_group)
        {
            Acad_Group = acad_group;
            return Acad_Group;
        }
    }
}
