using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;
using ZooWorld.Configs.UI;

namespace ZooWorld.UI.TastyLabel
{
    public class TastyLabelFeedBinder : IInitializable, IDisposable
    {
        private readonly Camera _worldCamera;
        private readonly TastyLabelConfig _config;
        private readonly TastyLabelPool _pool;
        private readonly TastyLabelFeedViewModel _viewModel;
        private IDisposable _subscription;

        public TastyLabelFeedBinder(
            Camera worldCamera,
            TastyLabelConfig config,
            TastyLabelPool pool,
            TastyLabelFeedViewModel viewModel)
        {
            _worldCamera = worldCamera;
            _config = config;
            _pool = pool;
            _viewModel = viewModel;
        }

        public void Initialize()
        {
            if (_worldCamera == null)
                throw new InvalidOperationException("Tasty label world camera is not configured.");

            _subscription = _viewModel.Requests.Subscribe(request => PresentAsync(request).Forget());
        }

        public void Dispose()
        {
            if (_subscription == null)
                return;

            _subscription.Dispose();
            _subscription = null;
        }

        private async UniTaskVoid PresentAsync(TastyLabelRequest request)
        {
            TastyLabelView view = _pool.Rent();

            if (view == null)
                return;

            try
            {
                Vector3 worldPosition = request.WorldPosition + _config.WorldOffset;
                await view.ShowAsync(_worldCamera, _pool.Root, worldPosition, _config);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            finally
            {
                _pool.Return(view);
            }
        }
    }
}
