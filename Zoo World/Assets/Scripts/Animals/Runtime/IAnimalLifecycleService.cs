using UnityEngine;

namespace ZooWorld.Animals.Runtime
{
    public interface IAnimalLifecycleService
    {
        void PrepareForPooling(IAnimalController animal, Transform poolRoot);
        void Activate(IAnimalController animal, Vector3 position, Vector3 spawnScale);
        void Deactivate(IAnimalController animal, Transform poolRoot);
        void Destroy(IAnimalController animal);
    }
}
