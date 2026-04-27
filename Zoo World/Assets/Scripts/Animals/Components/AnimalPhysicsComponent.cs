using UnityEngine;
using System;

namespace ZooWorld.Animals.Components
{
    public class AnimalPhysicsComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                    throw new InvalidOperationException($"Missing Rigidbody binding on {nameof(AnimalPhysicsComponent)} at {name}.");

                return _rigidbody;
            }
        }
    }
}
