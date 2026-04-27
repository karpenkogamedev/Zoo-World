using UnityEngine;
using ZooWorld.Animals.View;

namespace ZooWorld.Animals.StateMachines
{
    public class AnimalIdleState : IAnimalState
    {
        private readonly IAnimalView _view;

        public AnimalIdleState(IAnimalView view)
        {
            _view = view;
        }

        public void Enter()
        {
            Rigidbody rigidbody = _view.Rigidbody;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void Exit()
        {
        }

        public void Tick(float deltaTime)
        {
        }
    }
}
