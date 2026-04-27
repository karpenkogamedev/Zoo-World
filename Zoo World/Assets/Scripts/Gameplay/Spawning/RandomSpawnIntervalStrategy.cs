using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Gameplay.Spawning
{
    public class RandomSpawnIntervalStrategy : ISpawnIntervalStrategy
    {
        private readonly GameRuleConfig _gameRuleConfig;
        private readonly CompositeDisposable _spawnDisposables = new();
        private Action _onTick;

        public RandomSpawnIntervalStrategy(GameRuleConfig gameRuleConfig)
        {
            _gameRuleConfig = gameRuleConfig;
        }

        public bool IsRunning { get; private set; }

        public void Start(Action onTick)
        {
            if (IsRunning || onTick == null)
                return;

            IsRunning = true;
            _onTick = onTick;
            ScheduleNextTick();
        }

        public void Stop()
        {
            if (IsRunning == false)
                return;

            IsRunning = false;
            _onTick = null;
            _spawnDisposables.Clear();
        }

        private void ScheduleNextTick()
        {
            if (IsRunning == false || _onTick == null)
                return;

            _spawnDisposables.Clear();

            float interval = Random.Range(_gameRuleConfig.MinSpawnInterval, _gameRuleConfig.MaxSpawnInterval);

            IDisposable spawnTimer = Observable
                .Timer(TimeSpan.FromSeconds(interval))
                .Subscribe(_ =>
                {
                    if (IsRunning == false || _onTick == null)
                        return;

                    _onTick.Invoke();

                    if (IsRunning)
                        ScheduleNextTick();
                });

            _spawnDisposables.Add(spawnTimer);
        }
    }
}
