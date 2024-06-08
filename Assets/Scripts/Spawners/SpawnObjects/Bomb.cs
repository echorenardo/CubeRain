using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Hider))]
public class Bomb : SpawnObject, IExplodable
{
    [SerializeField] private Exploder _exploder;

    private Rigidbody _rigidbody;
    private Hider _hider;

    private readonly int _minLandingTime = 2;
    private readonly int _maxLandingTime = 6;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hider = GetComponent<Hider>();
    }

    public void TakeExplosion(float force, Vector3 position, float radius) => _rigidbody.AddExplosionForce(force, position, radius);

    public void Explode() => _hider.HideGradually(Random.Range(_minLandingTime, _maxLandingTime), Remove);

    public override void Remove()
    {
        _exploder.Explode();
        base.Remove();
        _hider.Show();
    }
}