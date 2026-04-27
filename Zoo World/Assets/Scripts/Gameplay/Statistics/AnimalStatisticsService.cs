using System.Collections.Generic;
using UniRx;

namespace ZooWorld.Gameplay.Statistics
{
    public class AnimalStatisticsService : IAnimalStatisticsService
    {
        private readonly Dictionary<AnimalStatisticsRole, ReactiveProperty<int>> _deathCounters = new();

        public IReadOnlyReactiveProperty<int> ObserveDeathCount(AnimalStatisticsRole role)
        {
            return GetCounter(role);
        }

        public void RegisterDeath(AnimalStatisticsRole role)
        {
            ReactiveProperty<int> counter = GetCounter(role);
            counter.Value++;
        }

        private ReactiveProperty<int> GetCounter(AnimalStatisticsRole role)
        {
            if (_deathCounters.TryGetValue(role, out ReactiveProperty<int> counter))
                return counter;

            counter = new ReactiveProperty<int>(0);
            _deathCounters[role] = counter;
            return counter;
        }
    }
}
