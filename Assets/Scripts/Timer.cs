using System;
using Unity.VisualScripting;
using UnityEngine;

public class Timer
{
    private float _elapsedTime;

    private readonly float _resetTime;
    private readonly Action _complitionAction;

    public Timer(float resetTime, Action action)
    {
        _complitionAction = action;
        _resetTime = resetTime;
    }

    public void Countdown(float step)
    {
        if (_elapsedTime < _resetTime)
            _elapsedTime += step;
        else
        {
            Reset();
            _complitionAction?.Invoke();
        }
    }

    public void Reset() => _elapsedTime = 0;
}