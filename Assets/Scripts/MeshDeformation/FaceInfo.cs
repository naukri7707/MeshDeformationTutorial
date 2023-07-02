using UnityEngine;

namespace Naukri.MeshHelper
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

        public static FaceInfo[] GetFaceInfos(BoxCollider boxCollider)
        {
            var transform = boxCollider.transform;
            var center = boxCollider.bounds.center;
            var extents = boxCollider.bounds.extents;

            var faces = new FaceInfo[6];

            faces[0] = new FaceInfo(center + new Vector3(extents.x, 0, 0), transform.right);
            faces[1] = new FaceInfo(center + new Vector3(-extents.x, 0, 0), -transform.right);
            faces[2] = new FaceInfo(center + new Vector3(0, extents.y, 0), transform.up);
            faces[3] = new FaceInfo(center + new Vector3(0, -extents.y, 0), -transform.up);
            faces[4] = new FaceInfo(center + new Vector3(0, 0, extents.z), transform.forward);
            faces[5] = new FaceInfo(center + new Vector3(0, 0, -extents.z), -transform.forward);

            return faces;
        }
    }
}