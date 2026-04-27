using UnityEngine;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Movement
{
    [CreateAssetMenu(fileName = "LinearMovementConfig", menuName = "ZooWorld/Configs/Animals/Movement/Linear")]
    public class LinearMovementConfig : AnimalMovementConfig
    {
        [SerializeField] private float _speed = 2f;

        public float Speed => _speed;
    }
}
