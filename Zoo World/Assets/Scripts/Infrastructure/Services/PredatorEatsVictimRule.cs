using ZooWorld.Animals.Runtime;
using ZooWorld.Gameplay.Statistics;
using ZooWorld.UI.TastyLabel;

namespace ZooWorld.Infrastructure.Services
{
    public class PredatorEatsVictimRule : ICollisionRule
    {
        private readonly IAnimalStatisticsService _animalStatisticsService;
        private readonly IAnimalInteractionSemanticsResolver _semanticsResolver;
        private readonly ITastyLabelRequestPublisher _tastyLabelRequestPublisher;

        public PredatorEatsVictimRule(
            IAnimalStatisticsService animalStatisticsService,
            IAnimalInteractionSemanticsResolver semanticsResolver,
            ITastyLabelRequestPublisher tastyLabelRequestPublisher)
        {
            _animalStatisticsService = animalStatisticsService;
            _semanticsResolver = semanticsResolver;
            _tastyLabelRequestPublisher = tastyLabelRequestPublisher;
        }

        public int Priority => 200;

        public bool CanApply(IAnimalController source, IAnimalController target)
        {
            return _semanticsResolver.CanEatOtherAnimals(source) &&
                   _semanticsResolver.CanBeEatenByPredator(target) &&
                   !_semanticsResolver.CanFightPredators(target);
        }

        public void Apply(IAnimalController source, IAnimalController target)
        {
            target.Release();
            _animalStatisticsService.RegisterDeath(target.Data.StatisticsRole);
            _tastyLabelRequestPublisher.Publish(source.View.Transform.position);
        }
    }
}
