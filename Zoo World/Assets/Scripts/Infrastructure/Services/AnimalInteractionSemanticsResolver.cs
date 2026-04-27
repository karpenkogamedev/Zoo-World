using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public class AnimalInteractionSemanticsResolver : IAnimalInteractionSemanticsResolver
    {
        public bool CanBounceOnPreyCollision(IAnimalController animal)
        {
            return GetCapabilities(animal).CanBounceOnPreyCollision;
        }

        public bool CanBeEatenByPredator(IAnimalController animal)
        {
            return GetCapabilities(animal).CanBeEatenByPredator;
        }

        public bool CanEatOtherAnimals(IAnimalController animal)
        {
            return GetCapabilities(animal).CanEatOtherAnimals;
        }

        public bool CanFightPredators(IAnimalController animal)
        {
            return GetCapabilities(animal).CanFightPredators;
        }

        private static ZooWorld.Configs.Animals.AnimalFoodChainCapabilities GetCapabilities(IAnimalController animal)
        {
            if (animal == null)
                throw new System.InvalidOperationException("Animal is required to resolve food-chain capabilities.");

            return animal.Data.FoodChainCapabilities;
        }
    }
}
