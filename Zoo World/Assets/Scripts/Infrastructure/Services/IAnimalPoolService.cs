using UnityEngine;
using ZooWorld.Animals.Runtime;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Infrastructure.Services
{
    public interface IAnimalPoolService
    {
        bool TryGet(AnimalData data, Vector3 position, out IAnimalController animal);
        void Release(IAnimalController animal);
        void Clear();
    }
}
