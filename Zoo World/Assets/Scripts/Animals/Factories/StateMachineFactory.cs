using System;
using System.Collections.Generic;
using System.Linq;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;
using ZooWorld.Animals.StateMachines;
using ZooWorld.Animals.Factories.Interfaces;

namespace ZooWorld.Animals.Factories
{
    public class StateMachineFactory : IStateMachineFactory
    {
        private readonly IReadOnlyList<IAnimalStateContributor> _contributors;

        public StateMachineFactory(IEnumerable<IAnimalStateContributor> contributors)
        {
            _contributors = contributors.ToList();
        }

        public IAnimalStateMachine Create()
        {
            return new AnimalStateMachine();
        }

        public void Populate(IAnimalStateMachine stateMachine, IAnimalController animal, IMovementStrategy movementStrategy)
        {
            IAnimalStateContributor initialContributor = null;

            foreach (IAnimalStateContributor contributor in _contributors)
            {
                contributor.Contribute(stateMachine, animal, movementStrategy);

                if (contributor.IsInitialState)
                    initialContributor = contributor;
            }

            if (initialContributor == null)
                throw new InvalidOperationException("No initial animal state contributor is registered.");

            stateMachine.SwitchState(initialContributor.StateType);
        }
    }
}
