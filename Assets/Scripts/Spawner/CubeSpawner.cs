using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private SpawnArea _area;
    [SerializeField] private Cube _cube;

    private List<Cube> _pool = new();

    private readonly bool _isSpawning = true;
    private readonly float _poolSize = 50f;
    private readonly float _spawnDuration = 0.1f;
    private readonly Color _defaultColor = Color.blue;

    private void Awake() => FillPool();

    private void Start() => StartCoroutine(SpawnPeriodically());

    private void FillPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Cube cube = Instantiate(_cube);
            cube.Init(_area.GetRandomPoint(), _defaultColor);
            cube.Disable();
            _pool.Add(cube);
        }
    }

    private void Spawn()
    {
        Cube cube = _pool.FirstOrDefault(currentCube => currentCube.gameObject.activeSelf == false);

        if (cube != null)
        {
            cube.SetSpawnPosition(_area.GetRandomPoint());
            cube.Enable();
        }
    }

    private IEnumerator SpawnPeriodically()
    {
        WaitForSeconds wait = new(_spawnDuration);

        while (_isSpawning)
        {
            Spawn();
            yield return wait;
        }
    }
}