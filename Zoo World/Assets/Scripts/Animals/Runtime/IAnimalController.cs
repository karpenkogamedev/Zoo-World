using ZooWorld.Animals.View;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Runtime
{
    public interface IAnimalController : IAnimalFixedUpdated
    {
        AnimalData Data { get; }
        IAnimalView View { get; }
        bool IsReleased { get; }

        void Activate();
        void Deactivate();
        void HandleCollision(IAnimalController other);
        void Release();
    }
}
