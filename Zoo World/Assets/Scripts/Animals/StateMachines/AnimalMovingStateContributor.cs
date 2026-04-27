using System;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalMovingStateContributor : IAnimalStateContributor
    {
        public bool IsInitialState => false;
        public Type StateType => typeof(AnimalMovingState);

        public void Contribute(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy)
        {
            stateMachine.AddState(new AnimalMovingState(animal, movementStrategy));
        }
    }
}
