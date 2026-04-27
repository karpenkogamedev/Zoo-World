using ZooWorld.Animals.Collisions;

namespace ZooWorld.Animals.Runtime
{
    public class AnimalCollisionSink : IAnimalCollisionSink
    {
        private readonly IAnimalController _animal;

        public AnimalCollisionSink(IAnimalController animal)
        {
            _animal = animal;
        }

        public void HandleCollision(IAnimalController other)
        {
            _animal.HandleCollision(other);
        }
    }
}
