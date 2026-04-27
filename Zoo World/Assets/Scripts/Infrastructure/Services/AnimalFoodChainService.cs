using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public class AnimalFoodChainService : IAnimalFoodChainService
    {
        private readonly IReadOnlyList<ICollisionRule> _rules;
        private readonly HashSet<CollisionPair> _processedPairs = new();
        private int _lastProcessedFrame = -1;

        public AnimalFoodChainService(IEnumerable<ICollisionRule> rules)
        {
            _rules = rules.OrderByDescending(x => x.Priority).ToList();
        }

        public bool TryHandle(IAnimalController source, IAnimalController target)
        {
            if (CanProcess(source, target) == false)
                return false;

            BeginFrame();

            if (_processedPairs.Add(new CollisionPair(source, target)) == false)
                return false;

            if (TryApply(source, target))
                return true;

            if (CanProcess(source, target) == false)
                return false;

            return TryApply(target, source);
        }

        private bool CanProcess(IAnimalController source, IAnimalController other)
        {
            return source != null &&
                   other != null &&
                   !source.IsReleased &&
                   !other.IsReleased;
        }

        private bool TryApply(IAnimalController source, IAnimalController target)
        {
            ICollisionRule rule = _rules.FirstOrDefault(x => x.CanApply(source, target));

            if (rule == null)
                return false;

            rule.Apply(source, target);
            return true;
        }

        private void BeginFrame()
        {
            int frame = Time.frameCount;

            if (_lastProcessedFrame == frame)
                return;

            _processedPairs.Clear();
            _lastProcessedFrame = frame;
        }

        private readonly struct CollisionPair
        {
            private readonly IAnimalController _first;
            private readonly IAnimalController _second;

            public CollisionPair(IAnimalController first, IAnimalController second)
            {
                _first = first;
                _second = second;
            }

            public override bool Equals(object obj)
            {
                return obj is CollisionPair other && Equals(other);
            }

            private bool Equals(CollisionPair other)
            {
                return ReferenceEquals(_first, other._first) && ReferenceEquals(_second, other._second) ||
                       ReferenceEquals(_first, other._second) && ReferenceEquals(_second, other._first);
            }

            public override int GetHashCode()
            {
                int firstHash = _first == null ? 0 : RuntimeHelpers.GetHashCode(_first);
                int secondHash = _second == null ? 0 : RuntimeHelpers.GetHashCode(_second);
                return firstHash ^ secondHash;
            }
        }
    }
}
