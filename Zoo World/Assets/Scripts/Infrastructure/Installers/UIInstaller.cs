using UnityEngine;
using System;
using Zenject;
using ZooWorld.UI.HUD;
using ZooWorld.UI.TastyLabel;

namespace ZooWorld.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Camera _worldCamera;
        [SerializeField] private StatisticsHudView _statisticsHudView;
        [SerializeField] private RectTransform _tastyLabelRoot;
        [SerializeField] private TastyLabelView _tastyLabelTemplate;

        public override void InstallBindings()
        {
            if (_worldCamera == null)
                throw new InvalidOperationException("UIInstaller requires a world camera.");

            if (_statisticsHudView == null)
                throw new InvalidOperationException("UIInstaller requires a statistics HUD view.");

            if (_tastyLabelRoot == null)
                throw new InvalidOperationException("UIInstaller requires a tasty label root.");

            if (_tastyLabelTemplate == null)
                throw new InvalidOperationException("UIInstaller requires a tasty label template.");

            Container.Bind<StatisticsHudView>().FromInstance(_statisticsHudView).AsSingle();
            Container.BindInterfacesAndSelfTo<StatisticsHudViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatisticsHudBinder>().AsSingle();
            Container.BindInterfacesAndSelfTo<TastyLabelFeed>().AsSingle();
            Container.BindInterfacesAndSelfTo<TastyLabelFeedViewModel>().AsSingle();
            Container.Bind<TastyLabelPool>().AsSingle().WithArguments(_tastyLabelRoot, _tastyLabelTemplate);
            Container.BindInterfacesAndSelfTo<TastyLabelFeedBinder>().AsSingle()
                .WithArguments(_worldCamera);
        }
    }
}
