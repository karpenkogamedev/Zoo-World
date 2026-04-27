using UniRx;
using ZooWorld.Gameplay.Statistics;

namespace ZooWorld.UI.HUD
{
    public class StatisticsHudRowViewModel
    {
        public StatisticsHudRowViewModel(AnimalStatisticsRole statisticsRole, IReadOnlyReactiveProperty<string> text)
        {
            StatisticsRole = statisticsRole;
            Text = text;
        }

        public AnimalStatisticsRole StatisticsRole { get; }
        public IReadOnlyReactiveProperty<string> Text { get; }
    }
}
