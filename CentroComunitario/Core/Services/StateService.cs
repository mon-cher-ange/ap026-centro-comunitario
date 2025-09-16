using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Interfaces;
using Core.Repositories;

namespace Core.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public State GetStateById(int stateId)
        {
            return _stateRepository.FindById(stateId);
        }

        public State GetStateByName(string stateName)
        {
            return _stateRepository.FindByName(stateName);
        }

        public IEnumerable<State> GetAllStates()
        {
            return _stateRepository.GetAllStates();
        }

        public void AddState(State state)
        {
            throw new NotImplementedException();
        }

        public void UpdateState(State state)
        {
            throw new NotImplementedException();
        }

        public void DeleteState(int stateId)
        {
            throw new NotImplementedException();
        }
    }
}
