using Naukri.MeshBuilder;
using UnityEditor;
using UnityEngine;

namespace Naukri.Editor.MeshBuilder
{
    [CustomEditor(typeof(MeshBuilderBase), true)]
    public class MeshBuiderEditor : UnityEditor.Editor
    {
        private MeshBuilderBase meshBuilder;

        private void OnEnable()
        {
            meshBuilder = serializedObject.targetObject as MeshBuilderBase;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Build"))
            {
                meshBuilder.RebuildMesh();
            }
            if (GUILayout.Button("Create Asset"))
            {
                CreateAsset();
            }
        }

        public void CreateAsset()
        {
            var newMesh = Instantiate(meshBuilder.MeshFilter.sharedMesh);
            var assetPath = $"Assets/{newMesh.name}.asset";
            AssetDatabase.CreateAsset(newMesh, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}