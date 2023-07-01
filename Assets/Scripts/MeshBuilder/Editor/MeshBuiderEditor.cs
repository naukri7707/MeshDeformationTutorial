using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshBuilder), true)]
public class MeshBuiderEditor : Editor
{
    private MeshBuilder meshBuilder;

    private void OnEnable()
    {
        meshBuilder = serializedObject.targetObject as MeshBuilder;
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
        string assetPath = $"Assets/{newMesh.name}.asset";
        AssetDatabase.CreateAsset(newMesh, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
