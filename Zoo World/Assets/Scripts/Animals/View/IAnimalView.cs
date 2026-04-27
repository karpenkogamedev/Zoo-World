using UnityEngine;
using ZooWorld.Animals.Components;

namespace ZooWorld.Animals.View
{
    public interface IAnimalView
    {
        GameObject GameObject { get; }
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        AnimalRenderComponent Render { get; }
        AnimalCollisionComponent Collision { get; }
    }
}
