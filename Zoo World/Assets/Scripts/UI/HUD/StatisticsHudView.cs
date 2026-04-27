using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace ZooWorld.UI.HUD
{
    public class StatisticsHudView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rowsRoot;
        [SerializeField] private TMP_Text _rowTemplate;
        private readonly CompositeDisposable _bindings = new();
        private readonly List<TMP_Text> _rows = new();

        private void OnDisable()
        {
            Unbind();
        }

        public void Bind(StatisticsHudViewModel viewModel)
        {
            if (viewModel == null)
                throw new System.ArgumentNullException(nameof(viewModel));

            if (_rowsRoot == null)
                throw new System.InvalidOperationException("Statistics HUD rows root is not configured.");

            if (_rowTemplate == null)
                throw new System.InvalidOperationException("Statistics HUD row template is not configured.");

            Unbind();
            EnsureTemplateHidden();

            foreach (StatisticsHudRowViewModel rowViewModel in viewModel.Rows)
            {
                TMP_Text rowText = CreateRow();

                rowViewModel.Text
                    .Subscribe(value => SetCounter(rowText, value))
                    .AddTo(_bindings);
            }
        }

        public void Unbind()
        {
            _bindings.Clear();
            ClearRows();
        }

        private TMP_Text CreateRow()
        {
            TMP_Text instance = Instantiate(_rowTemplate, _rowsRoot);
            instance.gameObject.SetActive(true);
            _rows.Add(instance);

            return instance;
        }

        private void ClearRows()
        {
            foreach (TMP_Text row in _rows)
            {
                if (row == null)
                    continue;

                Destroy(row.gameObject);
            }

            _rows.Clear();
            EnsureTemplateHidden();
        }

        private void EnsureTemplateHidden()
        {
            if (_rowTemplate == null)
                return;

            _rowTemplate.gameObject.SetActive(false);
        }

        private void SetCounter(TMP_Text textView, string text)
        {
            if (textView == null)
                return;

            textView.text = text;
        }
    }
}
