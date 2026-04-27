using System;
using System.Collections.Generic;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalStateMachine : IAnimalStateMachine
    {
        private readonly Dictionary<Type, IAnimalState> _states = new();
        private IAnimalState _currentState;

        public void AddState(IAnimalState state)
        {
            if (state == null)
                return;

            _states[state.GetType()] = state;
        }
        
        public void SwitchState<TState>() where TState : class, IAnimalState
        {
            SwitchState(typeof(TState));
        }

        public void SwitchState(Type stateType)
        {
            if (stateType == null)
                throw new ArgumentNullException(nameof(stateType));

            if (_states.TryGetValue(stateType, out IAnimalState nextState) == false)
                throw new InvalidOperationException($"State {stateType.Name} is not registered.");

            if (ReferenceEquals(_currentState, nextState))
                return;

            _currentState?.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        public void Tick(float deltaTime)
        {
            _currentState?.Tick(deltaTime);
        }
    }
}
