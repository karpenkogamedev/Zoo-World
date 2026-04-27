using UnityEngine;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Movement
{
    [CreateAssetMenu(fileName = "JumpMovementConfig", menuName = "ZooWorld/Configs/Animals/Movement/Jump")]
    public class JumpMovementConfig : AnimalMovementConfig
    {
        [SerializeField] private float _linearSpeed = 2f;
        [SerializeField] private float _jumpDistance = 2f;
        [SerializeField] private float _jumpInterval = 1f;
        [SerializeField] private float _jumpHeight = 1f;

        public float LinearSpeed => _linearSpeed;
        public float JumpDistance => _jumpDistance;
        public float JumpInterval => _jumpInterval;
        public float JumpHeight => _jumpHeight;
    }
}
