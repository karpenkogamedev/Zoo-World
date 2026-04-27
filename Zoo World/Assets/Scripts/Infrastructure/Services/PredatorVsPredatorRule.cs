using ZooWorld.Animals.Runtime;
using ZooWorld.Gameplay.Statistics;
using UnityEngine;
using ZooWorld.UI.TastyLabel;

namespace ZooWorld.Infrastructure.Services
{
    public class PredatorVsPredatorRule : ICollisionRule
    {
        private readonly IAnimalStatisticsService _animalStatisticsService;
        private readonly IAnimalInteractionSemanticsResolver _semanticsResolver;
        private readonly ITastyLabelRequestPublisher _tastyLabelRequestPublisher;

        public PredatorVsPredatorRule(
            IAnimalStatisticsService animalStatisticsService,
            IAnimalInteractionSemanticsResolver semanticsResolver,
            ITastyLabelRequestPublisher tastyLabelRequestPublisher)
        {
            _animalStatisticsService = animalStatisticsService;
            _semanticsResolver = semanticsResolver;
            _tastyLabelRequestPublisher = tastyLabelRequestPublisher;
        }

        public int Priority => 300;

        public bool CanApply(IAnimalController source, IAnimalController target)
        {
            return _semanticsResolver.CanFightPredators(source) &&
                   _semanticsResolver.CanFightPredators(target);
        }

        public void Apply(IAnimalController source, IAnimalController target)
        {
            IAnimalController survivor = SelectSurvivor(source, target);
            IAnimalController victim = source;

            if (survivor == source)
                victim = target;

            if (victim.IsReleased)
                return;

            victim.Release();
            _animalStatisticsService.RegisterDeath(victim.Data.StatisticsRole);
            _tastyLabelRequestPublisher.Publish(survivor.View.Transform.position);
        }

        private IAnimalController SelectSurvivor(IAnimalController first, IAnimalController second)
        {
            return Random.value < 0.5f ? first : second;
        }
    }
}
