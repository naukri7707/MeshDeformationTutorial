using UnityEditor;

namespace Naukri.Editor.MeshDeformation
{
    [CustomEditor(typeof(DeformOnTriggerStay), true)]
    public class DeformOnTriggerStayEditor : UnityEditor.Editor
    {
        private DeformOnTriggerStay deformOnTriggerStay;

        private void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            var monoScriptProperty = serializedObject.FindProperty("m_Script");
            var deformerProperty = serializedObject.FindProperty(nameof(DeformOnTriggerStay.deformer));
            var filterProperty = serializedObject.FindProperty(nameof(DeformOnTriggerStay.filter));
            var distanceToleranceProperty = serializedObject.FindProperty(nameof(DeformOnTriggerStay.distanceTolerance));
            var angleToleranceProperty = serializedObject.FindProperty(nameof(DeformOnTriggerStay.angleTolerance));

            using (var scope = new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(monoScriptProperty);
            }
            EditorGUILayout.PropertyField(deformerProperty);
            EditorGUILayout.PropertyField(filterProperty);
            if (filterProperty.enumValueIndex == (int)DeformOnTriggerStay.Filter.TransformChanged)
            {
                EditorGUILayout.PropertyField(distanceToleranceProperty);
                EditorGUILayout.PropertyField(angleToleranceProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
