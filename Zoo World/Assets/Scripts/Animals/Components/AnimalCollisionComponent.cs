using UnityEngine;
using ZooWorld.Animals.Collisions;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Animals.Components
{
    public class AnimalCollisionComponent : MonoBehaviour
    {
        private IAnimalController _owner;
        private IAnimalCollisionSink _sink;

        public void Bind(IAnimalController owner, IAnimalCollisionSink sink)
        {
            _owner = owner;
            _sink = sink;
        }

        public void Unbind()
        {
            _owner = null;
            _sink = null;
        }

        private bool TryGetOwner(out IAnimalController owner)
        {
            owner = _owner;
            return owner != null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_sink == null)
                return;

            AnimalCollisionComponent otherCollision = collision.collider.GetComponentInParent<AnimalCollisionComponent>();

            if (otherCollision == null || otherCollision == this)
                return;

            if (otherCollision.TryGetOwner(out IAnimalController otherAnimal) == false || otherAnimal == _owner)
                return;

            _sink.HandleCollision(otherAnimal);
        }
    }
}
