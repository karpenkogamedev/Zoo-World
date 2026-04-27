using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Movement;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Factories
{
    public class MovementFactory : IMovementFactory
    {
        private readonly IReadOnlyList<IMovementStrategyBuilder> _builders;

        public MovementFactory(IEnumerable<IMovementStrategyBuilder> builders)
        {
            _builders = builders.ToList();
        }

        public IMovementStrategy Create(AnimalData data)
        {
            if (data == null)
                throw new InvalidOperationException("AnimalData is required to create movement strategy.");

            IMovementStrategyBuilder builder = _builders.FirstOrDefault(x => x.CanBuild(data));

            if (builder == null)
                throw new InvalidOperationException($"No movement strategy builder for {data.GetType().Name}");

            return builder.Build(data);
        }
    }
}
