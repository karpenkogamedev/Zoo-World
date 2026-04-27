using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZooWorld.UI.TastyLabel
{
    public class TastyLabelPool
    {
        private readonly RectTransform _root;
        private readonly TastyLabelView _template;
        private readonly Queue<TastyLabelView> _availableViews = new();

        public TastyLabelPool(RectTransform root, TastyLabelView template)
        {
            _root = root ?? throw new System.InvalidOperationException("Tasty label root is not configured.");
            _template = template ?? throw new System.InvalidOperationException("Tasty label template is not configured.");

            _template.gameObject.SetActive(false);
        }

        public RectTransform Root => _root;

        public TastyLabelView Rent()
        {
            if (_availableViews.Count > 0)
                return _availableViews.Dequeue();

            TastyLabelView instance = Object.Instantiate(_template, _root);
            instance.gameObject.SetActive(false);
            return instance;
        }

        public void Return(TastyLabelView view)
        {
            if (view == null)
                return;

            view.gameObject.SetActive(false);
            _availableViews.Enqueue(view);
        }
    }
}
