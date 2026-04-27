using UnityEngine;
using UnityEngine.Serialization;
using ZooWorld.Animals.View;
using ZooWorld.Gameplay.Statistics;
using System;

namespace ZooWorld.Configs.Animals
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "ZooWorld/Configs/Animals/Animal Data")]
    public class AnimalData : ScriptableObject
    {
        [FormerlySerializedAs("_role")]
        [SerializeField] private AnimalStatisticsRole _statisticsRole = AnimalStatisticsRole.Prey;
        [SerializeField] private AnimalMovementType _movementType = AnimalMovementType.Linear;
        [SerializeField] private AnimalView _viewPrefab;
        [SerializeField] private Material _material;
        [SerializeField] private AnimalMovementConfig _movementConfig;
        [SerializeField] private float _collisionImpulse = 2f;
        [SerializeField] private Vector3 _spawnScale = Vector3.one;
        [SerializeField] private AnimalFoodChainCapabilities _foodChainCapabilities = new();

        public AnimalStatisticsRole StatisticsRole => _statisticsRole;
        public AnimalMovementType MovementType => _movementType;
        public AnimalView ViewPrefab => _viewPrefab;
        public Material Material => _material;
        public AnimalMovementConfig MovementConfig => _movementConfig ?? throw new InvalidOperationException($"Movement config is not assigned on {name}.");
        public float CollisionImpulse => _collisionImpulse;
        public Vector3 SpawnScale => _spawnScale;
        public AnimalFoodChainCapabilities FoodChainCapabilities => _foodChainCapabilities ?? throw new InvalidOperationException($"Food-chain capabilities are not assigned on {name}.");
    }
}
