using UnityEngine;
using ZooWorld.Animals.Runtime;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Factories.Interfaces
{
    public interface IAnimalInstanceFactory
    {
        IAnimalController CreateInstance(AnimalData data, Vector3 position);
    }
}
