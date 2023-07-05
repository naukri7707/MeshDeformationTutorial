using UnityEngine;

namespace Naukri.MeshDeformation
{
    public class VertexModifierArgs
    {
        public readonly MeshDeformer deformer;

        public readonly DeformableObject deformable;

        public readonly int vertexIndex;

        public readonly Vector3 oldPosition;

        public readonly Vector3 newPosition;

        public readonly Vector3 oldWorldPosition;

        public readonly Vector3 newWorldPosition;

        public VertexModifierArgs(
            MeshDeformer deformer, DeformableObject deformable, int vertexIndex,
            Vector3 oldPosition, Vector3 newPosition,
            Vector3 oldWorldPosition, Vector3 newWorldPosition
            )
        {
            this.deformer = deformer;
            this.deformable = deformable;
            this.vertexIndex = vertexIndex;
            this.oldPosition = oldPosition;
            this.newPosition = newPosition;
            this.oldWorldPosition = oldWorldPosition;
            this.newWorldPosition = newWorldPosition;
        }
    }
}