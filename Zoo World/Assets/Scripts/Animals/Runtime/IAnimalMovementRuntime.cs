using ZooWorld.Animals.Movement;

namespace ZooWorld.Animals.Runtime
{
    internal interface IAnimalMovementRuntime
    {
        IMovementStrategy MovementStrategy { get; }
    }
}
