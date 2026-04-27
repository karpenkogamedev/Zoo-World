using UnityEngine;

namespace ZooWorld.UI.TastyLabel
{
    public interface ITastyLabelRequestPublisher
    {
        void Publish(Vector3 worldPosition);
    }
}
