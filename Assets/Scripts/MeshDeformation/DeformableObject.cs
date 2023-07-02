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

        public VertexModifier[] vertexModifiers;

        // Start is called before the first frame update
        private void Awake()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
            // 使用克隆體進行計算，避免多個 DeformableObject 同時使用一個 VertexModifier 的情況
            for (int i = 0; i < vertexModifiers.Length; i++)
            {
                vertexModifiers[i] = Instantiate(vertexModifiers[i]);
            }
        }

        private void Start()
        {
            // 初始化 VertexModifier
            foreach (var vertexModifier in vertexModifiers)
            {
                vertexModifier.InitialImpl(this);
            }
        }

        public Vector3 ModifyVertex(VertexModifyArgs args)
        {
            // 執行 VertexModifier
            foreach (var modifier in vertexModifiers)
            {
                modifier.OnVertexModifyImpl(args);
                // 如果被標註為已完成則跳出迴圈
                if(args.IsCompleted)
                {
                    break;
                }
            }
            return args.vector;
        }
    }
}