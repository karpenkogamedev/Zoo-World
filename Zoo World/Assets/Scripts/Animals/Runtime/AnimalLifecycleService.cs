using UnityEngine;
using ZooWorld.Animals.View;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Collisions;

namespace ZooWorld.Animals.Runtime
{
    public class AnimalLifecycleService : IAnimalLifecycleService
    {
        public void PrepareForPooling(IAnimalController animal, Transform poolRoot)
        {
            if (animal == null)
                return;

            DeactivateInternal(animal, poolRoot);
        }

        public void Activate(IAnimalController animal, Vector3 position, Vector3 spawnScale)
        {
            if (animal == null)
                return;

            if (animal is IAnimalMovementRuntime movementRuntime &&
                movementRuntime.MovementStrategy is IResettableMovementStrategy resettableMovementStrategy)
                resettableMovementStrategy.Reset();

            if (animal is not IAnimalCollisionRuntime collisionRuntime)
                throw new System.InvalidOperationException("Animal must expose collision runtime.");

            IAnimalView view = animal.View;
            Transform transform = view.Transform;
            Rigidbody rigidbody = view.Rigidbody;

            view.GameObject.SetActive(true);
            view.Collision.Bind(animal, collisionRuntime.CollisionSink);
            view.Render.ApplyMaterial(animal.Data.Material);
            transform.SetPositionAndRotation(position, Quaternion.identity);
            transform.localScale = spawnScale;

            ResetPhysicsBody(rigidbody, position);

            animal.Activate();
        }

        public void Deactivate(IAnimalController animal, Transform poolRoot)
        {
            if (animal == null)
                return;

            DeactivateInternal(animal, poolRoot);
        }

        public void Destroy(IAnimalController animal)
        {
            if (animal == null)
                return;

            Object.Destroy(animal.View.GameObject);
        }

        private void DeactivateInternal(IAnimalController animal, Transform poolRoot)
        {
            IAnimalView view = animal.View;
            Transform transform = view.Transform;
            Rigidbody rigidbody = view.Rigidbody;

            view.Collision.Unbind();
            ResetPhysicsBody(rigidbody, poolRoot.position);

            transform.SetParent(poolRoot, false);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            animal.Deactivate();
            view.GameObject.SetActive(false);
        }

        private static void ResetPhysicsBody(Rigidbody rigidbody, Vector3 position)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.position = position;
            rigidbody.rotation = Quaternion.identity;
        }
    }
}
