using ZooWorld.Animals.Collisions;

namespace ZooWorld.Animals.Runtime
{
    internal interface IAnimalCollisionRuntime
    {
        IAnimalCollisionSink CollisionSink { get; }
    }
}
