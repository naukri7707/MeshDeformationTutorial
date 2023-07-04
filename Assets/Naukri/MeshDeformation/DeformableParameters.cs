using UnityEngine;

namespace Naukri.MeshDeformation
{
    [RequireComponent(typeof(DeformableObject))]
    public abstract class DeformableParameters : MonoBehaviour
    {
        protected virtual void Reset()
        {
            var deformableObject = GetComponent<DeformableObject>();
            deformableObject.parameters = this;
        }
    }
}