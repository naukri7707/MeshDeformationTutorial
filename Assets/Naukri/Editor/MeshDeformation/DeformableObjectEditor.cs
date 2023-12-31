using Naukri.MeshDeformation;
using UnityEditor;

namespace Naukri.Editor.MeshDeformation
{
    [CustomEditor(typeof(DeformableObject), true)]
    public class DeformableObjectEditor : UnityEditor.Editor
    {
        private DeformableObject deformableObject;

        private void OnEnable()
        {
            deformableObject = serializedObject.targetObject as DeformableObject;
        }

        public override void OnInspectorGUI()
        {
            var monoScriptProperty = serializedObject.FindProperty("m_Script");
            var parametersProperty = serializedObject.FindProperty(nameof(DeformableObject.parameters));
            var vertexModifiersProperty = serializedObject.FindProperty(nameof(DeformableObject.vertexModifiers));
            var changeShaderPassDynamiclyProperty = serializedObject.FindProperty(nameof(DeformableObject.changeShaderPassDynamicly));
            var materialsProperty = serializedObject.FindProperty(nameof(DeformableObject.materials));
            var shaderPassLayersProperty = serializedObject.FindProperty(nameof(DeformableObject.shaderPassLayers));

            using (var scope = new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(monoScriptProperty);
            }
            EditorGUILayout.PropertyField(parametersProperty);
            EditorGUILayout.PropertyField(vertexModifiersProperty);
            EditorGUILayout.PropertyField(changeShaderPassDynamiclyProperty);
            if (changeShaderPassDynamiclyProperty.boolValue)
            {
                EditorGUILayout.PropertyField(materialsProperty);
                EditorGUILayout.PropertyField(shaderPassLayersProperty);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
