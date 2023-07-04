using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class DeformableObjectModifier : ScriptableObject
    {
        internal virtual void InitialImpl(DeformableObject deformableObject)
        {
            Initial(deformableObject);
        }

        protected virtual void Initial(DeformableObject deformableObject) { }
    }
}
