using System;
using System.Collections.Generic;
using UniRx;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public class UpdateHandler : IDisposable
    {
        private readonly List<IAnimalFixedUpdated> _fixedUpdatedAnimals = new();
        private readonly Dictionary<IAnimalFixedUpdated, int> _animalIndexes = new();
        private readonly List<IAnimalFixedUpdated> _pendingAdds = new();
        private readonly HashSet<IAnimalFixedUpdated> _pendingAddSet = new();
        private readonly List<IAnimalFixedUpdated> _pendingRemoves = new();
        private readonly HashSet<IAnimalFixedUpdated> _pendingRemoveSet = new();
        private readonly CompositeDisposable _disposables = new();
        private bool _isTicking;

        public UpdateHandler()
        {
            Observable.EveryFixedUpdate()
                .Subscribe(_ => FixedTick())
                .AddTo(_disposables);
        }

        private void Register(IAnimalFixedUpdated animal)
        {
            if (animal == null)
                return;

            if (_isTicking)
            {
                if (_pendingRemoveSet.Remove(animal))
                    return;

                if (_animalIndexes.ContainsKey(animal) || _pendingAddSet.Add(animal) == false)
                    return;

                _pendingAdds.Add(animal);
                return;
            }

            AddAnimal(animal);
        }

        public void Register(IAnimalController animal)
        {
            Register((IAnimalFixedUpdated)animal);
        }

        private void Unregister(IAnimalFixedUpdated animal)
        {
            if (animal == null)
                return;

            if (_isTicking)
            {
                if (_pendingAddSet.Remove(animal))
                    return;

                if (_animalIndexes.ContainsKey(animal) == false || _pendingRemoveSet.Add(animal) == false)
                    return;

                _pendingRemoves.Add(animal);
                return;
            }

            RemoveAnimal(animal);
        }

        public void Unregister(IAnimalController animal)
        {
            Unregister((IAnimalFixedUpdated)animal);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _fixedUpdatedAnimals.Clear();
            _animalIndexes.Clear();
            _pendingAdds.Clear();
            _pendingAddSet.Clear();
            _pendingRemoves.Clear();
            _pendingRemoveSet.Clear();
        }

        private void FixedTick()
        {
            if (_fixedUpdatedAnimals.Count == 0)
                return;

            _isTicking = true;
            foreach (IAnimalFixedUpdated animal in _fixedUpdatedAnimals)
            {
                animal.FixedTick();
            }

            _isTicking = false;
            ApplyPendingMutations();
        }

        private void ApplyPendingMutations()
        {
            ApplyPending(_pendingRemoves, _pendingRemoveSet, RemoveAnimal);
            _pendingRemoves.Clear();
            ApplyPending(_pendingAdds, _pendingAddSet, AddAnimal);
            _pendingAdds.Clear();
        }

        private void ApplyPending(
            List<IAnimalFixedUpdated> pendingAnimals,
            HashSet<IAnimalFixedUpdated> pendingSet,
            Action<IAnimalFixedUpdated> apply)
        {
            foreach (IAnimalFixedUpdated animal in pendingAnimals)
            {
                if (pendingSet.Remove(animal))
                    apply(animal);
            }
        }

        private void AddAnimal(IAnimalFixedUpdated animal)
        {
            if (_animalIndexes.ContainsKey(animal))
                return;

            _animalIndexes[animal] = _fixedUpdatedAnimals.Count;
            _fixedUpdatedAnimals.Add(animal);
        }

        private void RemoveAnimal(IAnimalFixedUpdated animal)
        {
            if (_animalIndexes.TryGetValue(animal, out int index) == false)
                return;

            int lastIndex = _fixedUpdatedAnimals.Count - 1;
            IAnimalFixedUpdated lastAnimal = _fixedUpdatedAnimals[lastIndex];

            _fixedUpdatedAnimals[index] = lastAnimal;
            _animalIndexes[lastAnimal] = index;

            _fixedUpdatedAnimals.RemoveAt(lastIndex);
            _animalIndexes.Remove(animal);
        }
    }
}
