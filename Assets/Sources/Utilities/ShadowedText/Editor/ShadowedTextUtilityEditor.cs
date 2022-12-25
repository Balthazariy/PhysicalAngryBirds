using UnityEditor;
using UnityEngine;

namespace Balthazariy.Utilities
{
    [CustomEditor(typeof(ShadowedTextUtility))]
    public class ShadowedTextUtilityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ShadowedTextUtility script = (ShadowedTextUtility)target;

            EditorGUILayout.Space();
            if (GUILayout.Button("Init text and shadow"))
            {
                script.InitTextInEditor();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Reinit text and shadow"))
            {
                script.ReinitTextInEditor();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Remove text and shadow"))
            {
                script.RemoveTextInEditor();
            }
        }
    }
}