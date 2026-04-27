using System;
using UniRx;
using UnityEngine;

namespace ZooWorld.UI.TastyLabel
{
    public class TastyLabelFeed : ITastyLabelRequestPublisher, ITastyLabelRequestStream, IDisposable
    {
        private readonly Subject<TastyLabelRequest> _requests = new();

        public IObservable<TastyLabelRequest> Requests => _requests;

        public void Publish(Vector3 worldPosition)
        {
            _requests.OnNext(new TastyLabelRequest(worldPosition));
        }

        public void Dispose()
        {
            _requests.OnCompleted();
            _requests.Dispose();
        }
    }
}
