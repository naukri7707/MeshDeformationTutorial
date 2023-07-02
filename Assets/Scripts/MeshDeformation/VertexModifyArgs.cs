﻿using UnityEngine;

namespace Naukri.MeshHelper
{
    public class VertexModifyArgs
    {
        public readonly MeshDeformer meshDeformer;

        public readonly int vertexIndex;

        public Vector3 vector;

        private bool isCompleted;

        public VertexModifyArgs(MeshDeformer meshDeformer, int vertexIndex, Vector3 vector)
        {
            this.meshDeformer = meshDeformer;
            this.vertexIndex = vertexIndex;
            this.vector = vector;
        }

        public bool IsCompleted => isCompleted;

        public void Completed()
        {
            isCompleted = true;
        }
    }

}