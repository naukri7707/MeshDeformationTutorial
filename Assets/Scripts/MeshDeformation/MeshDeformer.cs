using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshHelper
{
    public abstract class MeshDeformer : MonoBehaviour
    {
        protected virtual void Awake()
        {

        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<DeformableObject>(out var deformable))
            {
                OnDeform(deformable);
            }
        }

        protected abstract void OnDeform(DeformableObject deformable);
    }
}