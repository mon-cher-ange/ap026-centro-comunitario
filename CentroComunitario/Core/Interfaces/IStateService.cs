using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interfaces
{
    public interface IStateService
    {
        State GetStateById(int stateId);
        State GetStateByName(string stateName);
        IEnumerable<State> GetAllStates();
        void AddState(State state);
        void UpdateState(State state);
        void DeleteState(int stateId);
    }
}
