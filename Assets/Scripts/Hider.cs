using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Hider : MonoBehaviour
{
    private Renderer _renderer;
    private Coroutine _coroutine;

    public readonly float _minAlpha = 0f;
    public readonly float _maxAlpha = 1f;
    public readonly float _hideDelay = 1f;

    private void Awake() => _renderer = GetComponent<Renderer>();

    public void Show() => SetAlpha(_maxAlpha);

    public void HideGradually(int time, Action actionAfter) => _coroutine ??= StartCoroutine(HideCoroutine(time, actionAfter));

    private void SetAlpha(float alpha)
    {
        if (alpha < 0)
            alpha = 0;

        Color oldColor = _renderer.material.color;
        Color newColor = new(oldColor.r, oldColor.g, oldColor.b, alpha);
        _renderer.material.color = newColor;
    }

    private IEnumerator HideCoroutine(int time, Action actionAfter)
    {
        WaitForSeconds wait = new(_hideDelay);

        while (_renderer.material.color.a > 0)
        {
            FadeOut(_maxAlpha / time);

            yield return wait;
        }

        actionAfter?.Invoke();

        _coroutine = null;
    }
    private void FadeOut(float fadeStep)
    {
        if (fadeStep > _maxAlpha)
            fadeStep = _maxAlpha;

        if (fadeStep < _minAlpha)
            fadeStep = _minAlpha;

        SetAlpha(_renderer.material.color.a - fadeStep);
    }
}