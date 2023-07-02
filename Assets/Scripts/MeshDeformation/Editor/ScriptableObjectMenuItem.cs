using UnityEngine;
using UnityEditor;

public static class CreateScriptableObjectByScriptAsset
{
    public const string itemName = "Assets/Create/Scriptable Object";

    [MenuItem(itemName, false, 0)]
    private static void CreateAsset()
    {
        var selectedScript = Selection.activeObject as MonoScript;
        if (selectedScript != null && typeof(ScriptableObject).IsAssignableFrom(selectedScript.GetClass()))
        {
            ScriptableObject newAsset = ScriptableObject.CreateInstance(selectedScript.GetClass());
            ProjectWindowUtil.CreateAsset(newAsset, $"New {selectedScript.name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    [MenuItem(itemName, true)]
    private static bool ValidateCreateAsset()
    {
        var selectedScript = Selection.activeObject as MonoScript;
        return selectedScript != null && typeof(ScriptableObject).IsAssignableFrom(selectedScript.GetClass());
    }
}
