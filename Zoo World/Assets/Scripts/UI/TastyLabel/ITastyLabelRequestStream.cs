using System;

namespace ZooWorld.UI.TastyLabel
{
    public interface ITastyLabelRequestStream
    {
        IObservable<TastyLabelRequest> Requests { get; }
    }
}
