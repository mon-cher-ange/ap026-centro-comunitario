using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public interface IStateRepository
    {
        State FindById(int id);
        State FindByName(string name);
        IEnumerable<State> GetAllStates();
        void Add(State state);
        bool Update(State state);
        bool Remove(int id);
    }
}
