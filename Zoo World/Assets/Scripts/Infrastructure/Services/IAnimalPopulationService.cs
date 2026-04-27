using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public interface IAnimalPopulationService
    {
        bool CanSpawn();
        void Register(IAnimalController animal);
        void Unregister(IAnimalController animal);
    }
}
