using Naukri.MeshBuilder;
using Naukri.MeshDeformation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Naukri.Editor.MeshDeformation
{
    [CustomEditor(typeof(DeformableParameters), true)]
    public class DeformableParametersEditor : UnityEditor.Editor
    {
        private DeformableParameters deformableParameters;

        private void OnEnable()
        {
            deformableParameters = serializedObject.targetObject as DeformableParameters;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Set Parameters"))
            {
                var deformableObject = deformableParameters.GetComponent<DeformableObject>();
                deformableObject.parameters = deformableParameters;
            }
        }
    }
}
