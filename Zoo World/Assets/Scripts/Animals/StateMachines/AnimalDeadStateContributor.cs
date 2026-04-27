using System;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalDeadStateContributor : IAnimalStateContributor
    {
        public bool IsInitialState => false;
        public Type StateType => typeof(AnimalDeadState);

        public void Contribute(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy)
        {
            stateMachine.AddState(new AnimalDeadState(animal.View));
        }
    }
}
