using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naukri.MeshBuilder
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class MeshBuilderBase : MonoBehaviour
    {
        private MeshFilter meshFilter;

        public MeshFilter MeshFilter
        {
            get
            {
                if (meshFilter == null)
                {
                    meshFilter = GetComponent<MeshFilter>();
                }
                return meshFilter;
            }
        }

        private MeshRenderer meshRenderer;

        public MeshRenderer MeshRenderer
        {
            get
            {
                if (meshRenderer == null)
                {
                    meshRenderer = GetComponent<MeshRenderer>();
                }
                return meshRenderer;
            }
        }

        [SerializeField]
        protected string meshName;

        public void BuildMesh()
        {
            var mesh = new Mesh()
            {
                name = meshName
            };

            OnMeshBuilding(mesh);
            MeshFilter.mesh = mesh;
        }

        public void DestoryMesh()
        {
            MeshFilter.sharedMesh = null;
        }

        public void RebuildMesh()
        {
            DestoryMesh();
            BuildMesh();
        }

        protected abstract void OnMeshBuilding(Mesh mesh);
    }
}