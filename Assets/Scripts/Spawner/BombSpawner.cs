using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _currentPosition;

    private void OnEnable() => _cubeSpawner.Spawned += OnCubeSpawn;

    private void OnDisable() => _cubeSpawner.Spawned -= OnCubeSpawn;

    public override Vector3 GetSpawnPoint() => _currentPosition;

    private void OnCubeSpawn(SpawnObject cube)
    {
        Cube currentCube = cube as Cube;
        currentCube.Dead += OnCubeDestroyed;
    }

    private void OnCubeDestroyed(Cube cube)
    {
        cube.Dead -= OnCubeDestroyed;
        _currentPosition = cube.transform.position;
        Bomb currentBomb = Give() as Bomb;

        Spawn();

        currentBomb.Explode();
    }
}