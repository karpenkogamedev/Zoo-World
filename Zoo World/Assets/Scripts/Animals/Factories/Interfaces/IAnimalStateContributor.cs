using System;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;
using ZooWorld.Animals.StateMachines;

namespace ZooWorld.Animals.Factories.Interfaces
{
    public interface IAnimalStateContributor
    {
        bool IsInitialState { get; }
        Type StateType { get; }
        void Contribute(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy);
    }
}
