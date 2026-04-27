using ZooWorld.Animals.Collisions;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.StateMachines;
using ZooWorld.Animals.View;
using ZooWorld.Configs.Animals;
using ZooWorld.Infrastructure.Services;

namespace ZooWorld.Animals.Runtime
{
    public class AnimalController : IAnimalController, IAnimalMovementRuntime, IAnimalCollisionRuntime
    {
        private readonly IAnimalPoolService _animalPoolService;
        private readonly IAnimalFoodChainService _animalFoodChainService;
        private readonly IMovementStrategy _movementStrategy;
        private readonly IAnimalStateMachine _stateMachine;
        private readonly IAnimalCollisionSink _collisionSink;

        public AnimalController(
            AnimalData data,
            IAnimalView view,
            IMovementStrategy movementStrategy,
            IAnimalStateMachine stateMachine,
            IAnimalPoolService animalPoolService,
            IAnimalFoodChainService animalFoodChainService)
        {
            Data = data;
            View = view;
            _movementStrategy = movementStrategy;
            _stateMachine = stateMachine;
            _collisionSink = new AnimalCollisionSink(this);
            _animalPoolService = animalPoolService;
            _animalFoodChainService = animalFoodChainService;
        }

        public AnimalData Data { get; }
        public IAnimalView View { get; }
        public bool IsReleased { get; private set; }

        public void Activate()
        {
            IsReleased = false;
            _stateMachine.SwitchState<AnimalMovingState>();
        }

        public void Deactivate()
        {
            _stateMachine.SwitchState<AnimalIdleState>();
        }

        public void FixedTick()
        {
            if (IsReleased)
                return;

            _stateMachine.Tick(UnityEngine.Time.fixedDeltaTime);
        }

        public void HandleCollision(IAnimalController other)
        {
            if (IsReleased || other == null || other.IsReleased)
                return;

            _animalFoodChainService.TryHandle(this, other);
        }

        public void Release()
        {
            if (IsReleased)
                return;

            IsReleased = true;
            _stateMachine.SwitchState<AnimalDeadState>();
            _animalPoolService.Release(this);
        }

        IMovementStrategy IAnimalMovementRuntime.MovementStrategy => _movementStrategy;
        IAnimalCollisionSink IAnimalCollisionRuntime.CollisionSink => _collisionSink;
    }
}
