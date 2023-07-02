using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshHelper
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class DeformableObject : MonoBehaviour
    {
        public MeshFilter MeshFilter { get; private set; }

        public MeshRenderer MeshRenderer { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }
    }
}