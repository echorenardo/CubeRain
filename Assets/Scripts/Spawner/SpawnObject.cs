using UnityEngine;

public class SpawnObject: MonoBehaviour
{
    public void Enable() => gameObject.SetActive(true);

    public void Disable() => gameObject.SetActive(false);

    public void SetSpawnPosition(Vector3 spawnPoint) => transform.position = spawnPoint;

    public virtual void Init(Vector3 spawnPoint) => SetSpawnPosition(spawnPoint);

    public virtual void Remove() => Disable();
}