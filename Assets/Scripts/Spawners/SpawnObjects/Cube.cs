using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Remover))]
public class Cube : SpawnObject, IExplodable
{
    private Remover _remover;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private bool _isFell;

    private readonly Color32 _defaultColor = Color.blue;
    private readonly int _minLandingTime = 2;
    private readonly int _maxLandingTime = 6;

    public event Action<Cube> Dead;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _remover = GetComponent<Remover>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => Colorize(_defaultColor);

    public void TakeExplosion(float force, Vector3 position, float radius) => _rigidbody.AddExplosionForce(force, position, radius);

    public override void Remove()
    {
        Dead?.Invoke(this);
        base.Remove();

        Colorize(_defaultColor);
        _isFell = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _) && _isFell == false)
        {
            _isFell = true;
            Colorize(UserUtils.GetRandomColor());

            _remover.RemoveAfter(Random.Range(_minLandingTime, _maxLandingTime), Remove);
        }
    }

    private void Colorize(Color32 color) => _renderer.material.color = color;
}