using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalMovingState : IAnimalState
    {
        private readonly IAnimalController _animal;
        private readonly Movement.IMovementStrategy _movementStrategy;

        public AnimalMovingState(IAnimalController animal, Movement.IMovementStrategy movementStrategy)
        {
            _animal = animal;
            _movementStrategy = movementStrategy;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Tick(float deltaTime)
        {
            _movementStrategy.Tick(_animal.View, deltaTime);
        }
    }
}
