using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private LayerMask _damageableLayer;
    [SerializeField] private ParticleSystem _explosionEffect;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Collider[] _hits;
    private IExplodable _currentExplodableObject;

    public void Explode()
    {
        _hits = Physics.OverlapSphere(transform.position, _explosionRadius, _damageableLayer);

        foreach (Collider collider in _hits)
        {
            if (collider.TryGetComponent<IExplodable>(out _currentExplodableObject))
                _currentExplodableObject.TakeExplosion(_explosionForce, transform.position, _explosionRadius);
        }

        Instantiate(_explosionEffect, transform.position, transform.rotation);
    }

    private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, _explosionRadius);
}