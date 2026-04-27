using UnityEngine;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public interface IAnimalMovementRedirectService
    {
        void Redirect(IAnimalController animal, Vector3 direction, float impulse);
    }
}
