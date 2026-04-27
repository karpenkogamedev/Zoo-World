using UnityEngine;

namespace ZooWorld.Configs.Gameplay
{
    [CreateAssetMenu(fileName = "GameRuleConfig", menuName = "ZooWorld/Configs/Gameplay/Game Rule Config")]
    public class GameRuleConfig : ScriptableObject
    {
        [SerializeField] private float _minSpawnInterval = 1f;
        [SerializeField] private float _maxSpawnInterval = 2f;
        [SerializeField] private float _minViewportX = 0.1f;
        [SerializeField] private float _maxViewportX = 0.9f;
        [SerializeField] private float _minViewportY = 0.1f;
        [SerializeField] private float _maxViewportY = 0.9f;
        [SerializeField] private int _animalPoolDefaultCapacity = 16;
        [SerializeField] private int _animalPoolMaxSize = 64;
        [SerializeField] private int _maxActiveAnimals = 128;

        public float MinSpawnInterval => _minSpawnInterval;
        public float MaxSpawnInterval => _maxSpawnInterval;
        public float MinViewportX => _minViewportX;
        public float MaxViewportX => _maxViewportX;
        public float MinViewportY => _minViewportY;
        public float MaxViewportY => _maxViewportY;
        public int AnimalPoolDefaultCapacity => _animalPoolDefaultCapacity;
        public int AnimalPoolMaxSize => _animalPoolMaxSize;
        public int MaxActiveAnimals => _maxActiveAnimals;
    }
}
