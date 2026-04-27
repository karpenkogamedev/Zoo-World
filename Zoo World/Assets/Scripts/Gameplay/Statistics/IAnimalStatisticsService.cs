namespace ZooWorld.Gameplay.Statistics
{
    public interface IAnimalStatisticsService : IAnimalStatisticsReadModel
    {
        void RegisterDeath(AnimalStatisticsRole role);
    }
}
