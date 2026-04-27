using UniRx;
namespace ZooWorld.Gameplay.Statistics
{
    public interface IAnimalStatisticsReadModel
    {
        IReadOnlyReactiveProperty<int> ObserveDeathCount(AnimalStatisticsRole role);
    }
}
