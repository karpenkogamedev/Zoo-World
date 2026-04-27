using UnityEngine;
using ZooWorld.Animals.Components;
using System;

namespace ZooWorld.Animals.View
{
    public class AnimalView : MonoBehaviour, IAnimalView
    {
        [SerializeField] private AnimalRenderComponent _render;
        [SerializeField] private AnimalPhysicsComponent _physics;
        [SerializeField] private AnimalCollisionComponent _collision;

        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public AnimalRenderComponent Render => _render;
        public AnimalPhysicsComponent Physics => _physics;
        public AnimalCollisionComponent Collision => _collision;
        public Rigidbody Rigidbody => _physics.Rigidbody;

        private void Awake()
        {
            ValidateRequiredBindings();
        }

        private void ValidateRequiredBindings()
        {
            if (_render == null)
                throw new InvalidOperationException($"Missing {nameof(AnimalRenderComponent)} on {name}.");

            if (_physics == null)
                throw new InvalidOperationException($"Missing {nameof(AnimalPhysicsComponent)} on {name}.");

            if (_collision == null)
                throw new InvalidOperationException($"Missing {nameof(AnimalCollisionComponent)} on {name}.");
        }
    }
}
