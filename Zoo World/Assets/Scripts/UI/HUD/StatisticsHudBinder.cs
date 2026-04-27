using System;
using Zenject;

namespace ZooWorld.UI.HUD
{
    public class StatisticsHudBinder : IInitializable, IDisposable
    {
        private readonly StatisticsHudView _view;
        private readonly StatisticsHudViewModel _viewModel;

        public StatisticsHudBinder(StatisticsHudView view, StatisticsHudViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Initialize()
        {
            if (_view == null)
                throw new InvalidOperationException("Statistics HUD view is not bound.");

            _view.Bind(_viewModel);
        }

        public void Dispose()
        {
            if (_view == null)
                return;

            _view.Unbind();
        }
    }
}
