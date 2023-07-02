using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshHelper
{
    public abstract class VertexModifier : ScriptableObject
    {
        protected DeformableObject _target;

        protected DeformableObject target => _target;

        protected Transform transform => target.transform;

        protected GameObject gameobject => target.gameObject;

        internal void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            Initial();
        }

        protected abstract void Initial();

        internal void OnVertexModifyImpl(VertexModifyArgs args)
        {
            OnVertexModify(args);
        }

        protected abstract void OnVertexModify(VertexModifyArgs args);
    }
}