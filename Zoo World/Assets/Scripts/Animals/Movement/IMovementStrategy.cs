using ZooWorld.Animals.View;

namespace ZooWorld.Animals.Movement
{
    public interface IMovementStrategy
    {
        void Tick(IAnimalView view, float deltaTime);
    }
}
