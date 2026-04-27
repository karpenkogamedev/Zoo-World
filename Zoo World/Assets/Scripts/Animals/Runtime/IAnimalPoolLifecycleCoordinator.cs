using UnityEngine;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Runtime
{
    public interface IAnimalPoolLifecycleCoordinator
    {
        void PrepareForPooling(IAnimalController animal);
        void ActivateFromPool(IAnimalController animal, Vector3 position, AnimalData data);
        void ReleaseToPool(IAnimalController animal);
        void Destroy(IAnimalController animal);
    }
}
