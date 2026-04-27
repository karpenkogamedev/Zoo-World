using UnityEngine;

namespace ZooWorld.UI.TastyLabel
{
    public struct TastyLabelRequest
    {
        public TastyLabelRequest(Vector3 worldPosition)
        {
            WorldPosition = worldPosition;
        }

        public Vector3 WorldPosition { get; }
    }
}
