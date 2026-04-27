using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public interface ICollisionRule
    {
        int Priority { get; }
        bool CanApply(IAnimalController source, IAnimalController target);
        void Apply(IAnimalController source, IAnimalController target);
    }
}
