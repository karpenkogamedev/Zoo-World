using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;
using ZooWorld.Animals.StateMachines;

namespace ZooWorld.Animals.Factories.Interfaces
{
    public interface IStateMachineFactory
    {
        IAnimalStateMachine Create();
        void Populate(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy);
    }
}
