using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    private Coroutine _coroutine;

    public event Action Removed;

    public void RemoveAfter(int time, Action action) => _coroutine ??= StartCoroutine(RemoveCoroutine(time, action));

    private IEnumerator RemoveCoroutine(int time, Action action)
    {
        WaitForSeconds wait = new(time);

        yield return wait;

        Removed?.Invoke();
        action?.Invoke();
        _coroutine = null;
    }
}