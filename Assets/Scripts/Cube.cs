using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Remover))]
public class Cube : MonoBehaviour
{
    private Remover _remover;
    private Renderer _renderer;
    private Color32 _defaultColor;

    private bool _isFell;

    private readonly int _minLandingTime = 2;
    private readonly int _maxLandingTime = 6;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _remover = GetComponent<Remover>();
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
        if (collision.gameObject.TryGetComponent<Ground>(out _) && _isFell == false)
        {
            _isFell = true;
            Colorize(UserUtils.GetRandomColor());

            _remover.RemoveAfter(Random.Range(_minLandingTime, _maxLandingTime), Remove);
        }
    }

    private void Remove()
    {
        Disable();
        Colorize(_defaultColor);
        _isFell = false;
    }
}