using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private MeshRenderer _sky;

    private float _skySizeX;
    private float _skySizeZ;
    private float _skyPositionX;
    private float _skyPositionY;
    private float _skyPositionZ;

    private void Awake()
    {
        _skySizeX = _sky.bounds.extents.x;
        _skySizeZ = _sky.bounds.extents.z;
        _skyPositionX = _sky.bounds.center.x;
        _skyPositionY = _sky.bounds.center.y;
        _skyPositionZ = _sky.bounds.center.z;
    }

    public Vector3 GetRandomPoint()
    {
        float pointX = UserUtils.GetRandomNumber((int)(_skyPositionX - _skySizeX), (int)(_skyPositionX + _skySizeX));
        float pointZ = UserUtils.GetRandomNumber((int)(_skyPositionZ - _skySizeZ), (int)(_skyPositionZ + _skySizeZ));

        return new Vector3(pointX, _skyPositionY, pointZ);
    }
}
