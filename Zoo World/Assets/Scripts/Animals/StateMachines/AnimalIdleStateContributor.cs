using System;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalIdleStateContributor : IAnimalStateContributor
    {
        public bool IsInitialState => true;
        public Type StateType => typeof(AnimalIdleState);

        public void Contribute(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy)
        {
            stateMachine.AddState(new AnimalIdleState(animal.View));
        }
    }
}
