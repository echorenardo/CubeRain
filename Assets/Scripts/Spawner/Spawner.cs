using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnObject _prefab;
    [SerializeField] private float _poolSize = 80f;
    [SerializeField] private float _spawnDuration = 0.1f;

    private int _totalActive;
    private SpawnObject _currentObject;

    private readonly List<SpawnObject> _pool = new();
    private readonly bool _isSpawning = true;

    public event Action<SpawnObject> Spawned;

    private void Awake() => FillPool();

    public IEnumerator SpawnPeriodically()
    {
        WaitForSeconds wait = new(_spawnDuration);

        while (_isSpawning)
        {
            Spawn();
            yield return wait;
        }
    }

    public virtual Vector3 GetSpawnPoint() => transform.position;

    public int GetTotalActive()
    {
        _totalActive = 0;

        var activeObjects = _pool.Where(spawnObject => spawnObject.gameObject.activeSelf == true).Select(spawnObject => spawnObject);

        foreach (SpawnObject spawnObject in activeObjects)
            _totalActive++;

        return _totalActive;
    }

    protected SpawnObject Give() => _pool.FirstOrDefault(currentObject => currentObject.gameObject.activeSelf == false);

    protected virtual void Spawn()
    {
        _currentObject = Give();

        if (_currentObject != null)
        {
            _currentObject.SetSpawnPosition(GetSpawnPoint());
            _currentObject.Enable();

            Spawned?.Invoke(_currentObject);
        }
    }

    private void FillPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            SpawnObject spawnObject = Instantiate(_prefab);
            spawnObject.Init(GetSpawnPoint());
            spawnObject.Disable();
            _pool.Add(spawnObject);
        }
    }
}