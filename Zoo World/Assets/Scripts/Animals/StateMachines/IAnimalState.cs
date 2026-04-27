namespace ZooWorld.Animals.StateMachines
{
    public interface IAnimalState
    {
        void Enter();
        void Exit();
        void Tick(float deltaTime);
    }
}
