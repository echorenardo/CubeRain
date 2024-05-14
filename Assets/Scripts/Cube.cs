using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Ground _ground;
    private Color32 _defaultColor;
    private Timer _landTimer;

    private bool _isFell;

    private readonly int _minLandTime = 2;
    private readonly int _maxLandTime = 5;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _landTimer = new Timer(UserUtils.GetRandomNumber(_minLandTime, _maxLandTime), Remove);
    }

    private void Update()
    {
        if (_isFell)
            _landTimer.Countdown(Time.deltaTime);
    }

    public void Init(Vector3 spawnPoint, Color32 color)
    {
        _defaultColor = color;
        Colorize(_defaultColor);
        SetSpawnPosition(spawnPoint);
    }

    public void Enable() => gameObject.SetActive(true);

    public void Disable() => gameObject.SetActive(false);

    public void SetSpawnPosition(Vector3 spawnPoint) => transform.position = spawnPoint;

    private void Colorize(Color32 color) => _renderer.material.color = color;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _ground) && _isFell == false)
        {
            _isFell = true;
            Colorize(UserUtils.GetRandomColor());
        }
    }

    private void Remove()
    {
        Disable();
        Colorize(_defaultColor);
        _isFell = false;
        _ground = null;
    }
}