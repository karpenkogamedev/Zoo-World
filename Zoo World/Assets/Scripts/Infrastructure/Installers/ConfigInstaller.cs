using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using ZooWorld.Configs.Animals;
using ZooWorld.Configs.Gameplay;
using ZooWorld.Configs.UI;

namespace ZooWorld.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private GameRuleConfig _gameRuleConfig;
        [SerializeField] private TastyLabelConfig _tastyLabelConfig;
        [SerializeField] private UIDisplayConfig _uiDisplayConfig;
        [SerializeField] private List<AnimalData> _animalCatalog = new();

        public override void InstallBindings()
        {
            if (_gameRuleConfig == null)
                throw new InvalidOperationException("ConfigInstaller requires a game rule config.");

            if (_tastyLabelConfig == null)
                throw new InvalidOperationException("ConfigInstaller requires a tasty label config.");

            if (_uiDisplayConfig == null)
                throw new InvalidOperationException("ConfigInstaller requires a UI display config.");

            if (_animalCatalog == null || _animalCatalog.Count == 0)
                throw new InvalidOperationException("ConfigInstaller requires at least one animal config.");

            if (_animalCatalog.Exists(animal => animal == null))
                throw new InvalidOperationException("ConfigInstaller contains a null animal config.");

            Container.Bind<GameRuleConfig>().FromInstance(_gameRuleConfig).AsSingle();
            Container.Bind<TastyLabelConfig>().FromInstance(_tastyLabelConfig).AsSingle();
            Container.Bind<UIDisplayConfig>().FromInstance(_uiDisplayConfig).AsSingle();
            Container.Bind<IReadOnlyList<AnimalData>>().FromInstance(_animalCatalog).AsSingle();
        }
    }
}
