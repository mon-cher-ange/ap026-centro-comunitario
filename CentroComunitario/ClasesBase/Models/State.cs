using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class State
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
        private int _stateTypeId;

        public int StateTypeId
        {
            get { return _stateTypeId; }
            set { _stateTypeId = value; }
        }
    }
}
