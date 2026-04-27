using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public interface IAnimalFoodChainService
    {
        bool TryHandle(IAnimalController source, IAnimalController target);
    }
}
