using System;
using UnityEngine;

namespace ZooWorld.Configs.Animals
{
    [Serializable]
    public class AnimalFoodChainCapabilities
    {
        [SerializeField] private bool _canBounceOnPreyCollision;
        [SerializeField] private bool _canBeEatenByPredator;
        [SerializeField] private bool _canEatOtherAnimals;
        [SerializeField] private bool _canFightPredators;

        public bool CanBounceOnPreyCollision => _canBounceOnPreyCollision;
        public bool CanBeEatenByPredator => _canBeEatenByPredator;
        public bool CanEatOtherAnimals => _canEatOtherAnimals;
        public bool CanFightPredators => _canFightPredators;
    }
}
