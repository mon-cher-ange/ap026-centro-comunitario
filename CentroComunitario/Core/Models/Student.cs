using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Student
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _DNI;

        public string DNI
        {
            get { return _DNI; }
            set { _DNI = value; }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
