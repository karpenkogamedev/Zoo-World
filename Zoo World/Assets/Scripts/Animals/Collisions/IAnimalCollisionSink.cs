using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.Collisions
{
    public interface IAnimalCollisionSink
    {
        void HandleCollision(IAnimalController other);
    }
}
