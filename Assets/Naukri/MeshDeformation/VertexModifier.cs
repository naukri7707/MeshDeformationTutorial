using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class VertexModifier : ScriptableObject
    {
        public class Args
        {
            public readonly MeshDeformer meshDeformer;

            public readonly int vertexIndex;

            public Vector3 vector;

            private bool isCompleted;

            public Args(MeshDeformer meshDeformer, int vertexIndex, Vector3 vector)
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

        protected DeformableObject _target;

        protected DeformableObject target => _target;

        protected Transform transform => target.transform;

        protected GameObject gameobject => target.gameObject;

        internal void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            Initial();
        }

        protected virtual void Initial() { }

        internal void ModifyVertex(Args args)
        {
            OnVertexModify(args);
        }

        protected abstract void OnVertexModify(Args args);
    }
}