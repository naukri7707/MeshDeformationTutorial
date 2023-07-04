using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class MeshDeformer : MonoBehaviour
    {
        public static float modifyDistanceThreshold = 0.001F;

        protected virtual void Awake()
        {

        }

        public void Deform(DeformableObject deformable)
        {
            OnDeform(deformable);
        }

        protected abstract void OnDeform(DeformableObject deformable);
    }
}