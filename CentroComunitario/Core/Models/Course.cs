using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Course
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private int _quota;

        public int Quota
        {
            get { return _quota; }
            set { _quota = value; }
        }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        private int _stateId;

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }
        private int _teacherId;

        public int TeacherId
        {
            get { return _teacherId; }
            set { _teacherId = value; }
        }
    }
}
