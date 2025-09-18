using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Repositories
{
    public class StateRepository : IStateRepository
    {
        private List<State> _states;
        private int _nextId;

        public StateRepository()
        {
            _states = new List<State>
            {
                new State
                {
                    Id = 1,
                    Name = "Programado",
                    StateTypeId = 1
                },
                new State
                {
                    Id = 2,
                    Name = "En Curso",
                    StateTypeId = 1
                },
                new State
                {
                    Id = 3,
                    Name = "Finalizado",
                    StateTypeId = 1
                },
                new State
                {
                    Id = 4,
                    Name = "Cancelado",
                    StateTypeId = 1
                }
            };

            _nextId = _states.Max(s => s.Id) + 1;
        }

        public State FindById(int id)
        {
            return _states.FirstOrDefault(s => s.Id == id);
        }

        public State FindByName(string name)
        {
            return _states.FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<State> GetAllStates()
        {
            return _states;
        }

        public void Add(State state)
        {
            throw new NotImplementedException();
        }

        public bool Update(State state)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
