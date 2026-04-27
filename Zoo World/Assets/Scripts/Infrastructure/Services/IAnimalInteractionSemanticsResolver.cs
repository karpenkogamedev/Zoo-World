using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public interface IAnimalInteractionSemanticsResolver
    {
        bool CanBounceOnPreyCollision(IAnimalController animal);
        bool CanBeEatenByPredator(IAnimalController animal);
        bool CanEatOtherAnimals(IAnimalController animal);
        bool CanFightPredators(IAnimalController animal);
    }
}
