using UnityEngine;

public readonly struct FaceInfo
{
    public readonly Vector3 origin;

    public readonly Vector3 normal;

    public FaceInfo(Vector3 origin, Vector3 normal)
    {
        this.origin = origin;
        this.normal = normal;
    }
}
