using UnityEngine;

namespace Naukri.MeshDeformation
{
    public readonly struct FaceInfo
    {
        public readonly Vector3 origin;

        public readonly Vector3 normal;

        public FaceInfo(Vector3 origin, Vector3 normal)
        {
            this.origin = origin;
            this.normal = normal;
        }

        public static FaceInfo GetFaceInfo(BoxCollider boxCollider, Direction style)
        {
            var transform = boxCollider.transform;
            var center = boxCollider.bounds.center;
            var extents = boxCollider.bounds.extents;

            return style switch
            {
                Direction.Right => new FaceInfo(center + new Vector3(extents.x, 0, 0), transform.right),
                Direction.Left => new FaceInfo(center + new Vector3(-extents.x, 0, 0), -transform.right),
                Direction.Up => new FaceInfo(center + new Vector3(0, extents.y, 0), transform.up),
                Direction.Down => new FaceInfo(center + new Vector3(0, -extents.y, 0), -transform.up),
                Direction.Forward => new FaceInfo(center + new Vector3(0, 0, extents.z), transform.forward),
                Direction.Back => new FaceInfo(center + new Vector3(0, 0, -extents.z), -transform.forward),
                _ => default
            };
        }

        public void Deconstruct(out Vector3 origin, out Vector3 normal)
        {
            origin = this.origin;
            normal = this.normal;
        }
    }
}