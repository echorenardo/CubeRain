using UnityEngine;

public class CubeSpawner : Spawner
{
    [SerializeField] private SpawnArea _area;

    private void Start() => StartCoroutine(SpawnPeriodically());

    public override Vector3 GetSpawnPoint() => _area.GetRandomPoint();
}