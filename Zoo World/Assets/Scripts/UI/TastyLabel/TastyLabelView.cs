using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using ZooWorld.Configs.UI;

namespace ZooWorld.UI.TastyLabel
{
    public class TastyLabelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RectTransform _rectTransform;

        private CancellationTokenSource _showCancellation;

        public async UniTask ShowAsync(
            Camera worldCamera,
            RectTransform root,
            Vector3 worldPosition,
            TastyLabelConfig config)
        {
            CancelActiveShow();

            if (_rectTransform == null || worldCamera == null || root == null || config == null)
                return;

            gameObject.SetActive(true);
            CancellationTokenSource cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
            _showCancellation = cancellationSource;
            CancellationToken cancellationToken = cancellationSource.Token;

            try
            {
                if (_text != null)
                    _text.text = config.Text;

                if (TryGetAnchoredPosition(worldCamera, root, worldPosition, out Vector2 startPosition) == false)
                    return;

                Vector2 endPosition = startPosition + Vector2.up * config.RiseDistance * 100f;
                float lifetime = Mathf.Max(config.Lifetime, 0.01f);
                float fadeDuration = Mathf.Clamp(config.FadeDuration, 0.01f, lifetime);
                float elapsed = 0f;

                while (elapsed < lifetime)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    elapsed += Time.deltaTime;
                    float progress = Mathf.Clamp01(elapsed / lifetime);
                    Vector2 position = Vector2.Lerp(startPosition, endPosition, progress);

                    _rectTransform.anchoredPosition = ClampInsideRoot(root, position);
                    ApplyAlpha(lifetime, fadeDuration, elapsed);

                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }
            }
            finally
            {
                ResetVisualState();
                DisposeCancellation(cancellationSource);
            }
        }

        private void OnDestroy()
        {
            CancelActiveShow();
            DisposeCancellation(_showCancellation);
        }

        private void ApplyAlpha(float lifetime, float fadeDuration, float elapsed)
        {
            if (_text == null)
                return;

            Color color = _text.color;
            float fadeStart = lifetime - fadeDuration;
            float alpha = 1f;

            if (elapsed >= fadeStart)
                alpha -= Mathf.Clamp01((elapsed - fadeStart) / fadeDuration);

            _text.color = new Color(color.r, color.g, color.b, alpha);
        }

        private void ResetVisualState()
        {
            if (_text == null)
                return;

            Color color = _text.color;
            _text.color = new Color(color.r, color.g, color.b, 1f);
        }

        private void CancelActiveShow()
        {
            if (_showCancellation == null)
                return;

            if (_showCancellation.IsCancellationRequested == false)
                _showCancellation.Cancel();
        }

        private void DisposeCancellation(CancellationTokenSource cancellationSource)
        {
            if (cancellationSource == null)
                return;

            if (ReferenceEquals(_showCancellation, cancellationSource))
                _showCancellation = null;

            cancellationSource.Dispose();
        }

        private bool TryGetAnchoredPosition(
            Camera worldCamera,
            RectTransform root,
            Vector3 worldPosition,
            out Vector2 anchoredPosition)
        {
            Vector3 screenPosition = worldCamera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z <= 0f)
            {
                anchoredPosition = default;
                return false;
            }

            Canvas canvas = root.GetComponentInParent<Canvas>();
            Camera screenCamera = worldCamera;

            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                screenCamera = null;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, screenPosition, screenCamera, out anchoredPosition) == false)
                return false;

            anchoredPosition = ClampInsideRoot(root, anchoredPosition);
            return true;
        }

        private Vector2 ClampInsideRoot(RectTransform root, Vector2 anchoredPosition)
        {
            Vector2 size = _rectTransform.rect.size;
            Rect rect = root.rect;
            float halfWidth = size.x * 0.5f;
            float halfHeight = size.y * 0.5f;

            return new Vector2(
                Mathf.Clamp(anchoredPosition.x, rect.xMin + halfWidth, rect.xMax - halfWidth),
                Mathf.Clamp(anchoredPosition.y, rect.yMin + halfHeight, rect.yMax - halfHeight));
        }
    }
}
