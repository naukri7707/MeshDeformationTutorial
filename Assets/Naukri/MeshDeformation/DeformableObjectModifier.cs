using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class DeformableObjectModifier : ScriptableObject
    {
        protected DeformableObject _target;

        public DeformableObject target => _target;

        public Transform transform => target.transform;

        public GameObject gameobject => target.gameObject;

        internal virtual void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            Initial();
        }

        protected virtual void Initial() { }
    }
}
