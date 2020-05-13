using DG.Tweening;
using UnityEngine;

public class HighlightButton : MonoBehaviour, IAnimatable
{
    public Color32 HiLiteColor;
    public Color32 HiLiteTwo;

    [Range(0.5f, 1.5f)] public float Growth;

    private Tween _tween;
    private Color32 _originalcolor;
    private bool _isOverButton;

    public void OnEnter(UIButton doButton)
    {
        _isOverButton = true;
        _originalcolor = doButton.Images[0].color;
        if (_tween != null && _tween.IsActive()) _tween.Kill();
        _tween = doButton.Images[0].DOColor(HiLiteColor, 0.42f);
    }

    public void OnExit(UIButton doButton)
    {
        _isOverButton = false;
        _tween.Kill();
        _tween = doButton.Images[0].DOColor(_originalcolor, 0.42f);
    }

    public void OnDown(UIButton doButton)
    {
        var growthVector = new Vector3(1 + Growth * 0.1f, 1 + Growth * 0.1f, 1);
        _tween.Kill();
        _tween = DOTween.Sequence()
            .Join(doButton.Images[0].transform.DOScale(growthVector, 0.42f))
            .Join(doButton.Images[0].DOColor(HiLiteTwo, 0.42f));
    }

    public void OnUp(UIButton doButton)
    {
        _tween.Kill();
        _tween = DOTween.Sequence()
            .Join(doButton.Images[0].transform.DOScale(Vector3.one, 0.42f))
            .Join(doButton.Images[0].DOColor(DecideColor(), 0.42f))
            .OnComplete(doButton.Execute);
    }

    private Color32 DecideColor()
    {
        return _isOverButton ? HiLiteColor : _originalcolor;
    }
}
