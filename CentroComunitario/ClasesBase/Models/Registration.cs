using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Registration
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private int _courseId;

        public int CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }
        private int _studentId;

        public int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }
        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }
    }
}
