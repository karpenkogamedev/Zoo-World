using System;

namespace ZooWorld.Animals.StateMachines
{
    public interface IAnimalStateMachine
    {
        void AddState(IAnimalState state);
        void SwitchState<TState>() where TState : class, IAnimalState;
        void SwitchState(Type stateType);
        void Tick(float deltaTime);
    }
}
