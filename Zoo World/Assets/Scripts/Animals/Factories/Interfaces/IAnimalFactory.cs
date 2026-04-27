using UnityEngine;
using ZooWorld.Animals.Runtime;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Factories.Interfaces
{
    public interface IAnimalFactory
    {
        bool TryCreate(AnimalData data, Vector3 position, out IAnimalController animal);
    }
}
