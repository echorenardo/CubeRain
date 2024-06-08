using System;
using System.Collections;
using UnityEngine;

public class Remover : MonoBehaviour
{
    private Coroutine _coroutine;

    public event Action Removed;

    public void RemoveAfter(int time, Action actionAfter) => _coroutine ??= StartCoroutine(RemoveCoroutine(time, actionAfter));

    private IEnumerator RemoveCoroutine(int time, Action actionAfter)
    {
        WaitForSeconds wait = new(time);

        yield return wait;

        Removed?.Invoke();

        actionAfter?.Invoke();

        _coroutine = null;
    }
}