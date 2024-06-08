using UnityEngine;

public interface IExplodable
{
    public void TakeExplosion(float force, Vector3 position, float radius);
}