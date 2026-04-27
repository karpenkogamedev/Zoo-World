using System;

namespace ZooWorld.UI.TastyLabel
{
    public class TastyLabelFeedViewModel
    {
        public TastyLabelFeedViewModel(ITastyLabelRequestStream requestStream)
        {
            if (requestStream == null)
                throw new ArgumentNullException(nameof(requestStream));

            Requests = requestStream.Requests;
        }

        public IObservable<TastyLabelRequest> Requests { get; }
    }
}
