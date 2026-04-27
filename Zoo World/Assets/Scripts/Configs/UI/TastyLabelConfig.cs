using UnityEngine;

namespace ZooWorld.Configs.UI
{
    [CreateAssetMenu(fileName = "TastyLabelConfig", menuName = "ZooWorld/Configs/UI/Tasty Label Config")]
    public class TastyLabelConfig : ScriptableObject
    {
        [SerializeField] private string _text = "Tasty!";
        [SerializeField] private float _lifetime = 1f;
        [SerializeField] private Vector3 _worldOffset = new(0f, -0.5f, 0f);
        [SerializeField] private float _riseDistance = 0.5f;
        [SerializeField] private float _fadeDuration = 0.2f;

        public string Text => _text;
        public float Lifetime => _lifetime;
        public Vector3 WorldOffset => _worldOffset;
        public float RiseDistance => _riseDistance;
        public float FadeDuration => _fadeDuration;
    }
}
