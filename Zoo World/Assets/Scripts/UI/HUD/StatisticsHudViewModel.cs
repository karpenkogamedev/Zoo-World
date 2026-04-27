using System;
using System.Collections.Generic;
using System.Globalization;
using UniRx;
using ZooWorld.Configs.UI;
using ZooWorld.Gameplay.Statistics;

namespace ZooWorld.UI.HUD
{
    public class StatisticsHudViewModel : IDisposable
    {
        private readonly List<StatisticsHudRowViewModel> _rows = new();
        private readonly CompositeDisposable _disposables = new();

        public StatisticsHudViewModel(UIDisplayConfig displayConfig, IAnimalStatisticsReadModel statisticsReadModel)
        {
            if (displayConfig == null)
                throw new ArgumentNullException(nameof(displayConfig));

            if (displayConfig.StatisticsEntries == null)
                throw new InvalidOperationException("Statistics HUD entries are not configured.");

            if (statisticsReadModel == null)
                throw new ArgumentNullException(nameof(statisticsReadModel));

            foreach (StatisticsHudEntryConfig entry in displayConfig.StatisticsEntries)
            {
                if (entry == null)
                    throw new InvalidOperationException("Statistics HUD entry is not configured.");

                ReadOnlyReactiveProperty<string> text = statisticsReadModel.ObserveDeathCount(entry.StatisticsRole)
                    .Select(count => string.Format(CultureInfo.InvariantCulture, entry.CounterFormat, entry.Label, count, entry.StatisticsRole))
                    .ToReadOnlyReactiveProperty()
                    .AddTo(_disposables);

                _rows.Add(new StatisticsHudRowViewModel(entry.StatisticsRole, text));
            }
        }

        public IReadOnlyList<StatisticsHudRowViewModel> Rows => _rows;

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
