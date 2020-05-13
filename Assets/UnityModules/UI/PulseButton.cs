namespace Utility
{
    using DG.Tweening;
    using UnityEngine;

    public class PulseButton : MonoBehaviour, IAnimatable
    {
        [SerializeField] private float flipSpeed = 0.1f;
        [SerializeField] private float pulseSpeed = 0.3f;
        [SerializeField] private float pulseScale = 1.25f;
        private Tween _tween;
        private bool _isOverButton;
        private int _siblingIndex;

        private void Awake()
        {
            _siblingIndex = GetComponent<Transform>().GetSiblingIndex();
        }

        public void OnEnter(UIButton doButton)
        {
            _isOverButton = true;
            if (_tween != null && _tween.IsActive()) _tween.Kill();
            doButton.thisTrans.SetAsLastSibling();
            Pulsate(doButton);
            InputBridge.instance.Haptic();
        }

        public void OnExit(UIButton doButton)
        {
            _isOverButton = false;
            _tween.Kill();
            _tween = doButton.thisTrans.DOScale(1, flipSpeed)
                .OnComplete(() => doButton.thisTrans.SetSiblingIndex(_siblingIndex));
        }

        public void OnDown(UIButton doButton)
        {
            _tween.Kill();
            _tween = doButton.thisTrans.DOLocalRotate(new Vector3(0, 0, 180), flipSpeed);
            InputBridge.instance.Haptic();
        }

        public void OnUp(UIButton doButton)
        {
            _tween.Kill();
            _tween = doButton.thisTrans.DOLocalRotate(Vector3.zero, flipSpeed)
                .OnComplete(() =>
                {
                    if (_isOverButton)
                    {
                        _tween = doButton.thisTrans.DOScale(Vector3.one, flipSpeed)
                            .OnComplete(() => Pulsate(doButton));
                    }

                    doButton.Execute();
                });
        }

        private void Pulsate(UIButton doButton)
        {
            _tween = doButton.thisTrans.DOScale(pulseScale, pulseSpeed)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.OutCubic);
        }
    }
}