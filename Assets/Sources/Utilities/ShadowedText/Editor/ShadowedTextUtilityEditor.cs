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

            GUIStyle style = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,

                normal = new GUIStyleState()
                {
                    background = Texture2D.grayTexture,
                    textColor= Color.black
                },
                hover = new GUIStyleState()
                {
                    background = Texture2D.linearGrayTexture,
                    textColor = Color.black
                },
                active = new GUIStyleState()
                {
                    background = Texture2D.blackTexture,
                    textColor = Color.black
                }
            };

            ShadowedTextUtility script = (ShadowedTextUtility)target;

            EditorGUILayout.Space();
            style.normal.background = MakeBackgroundTexture(10,10, Color.yellow);
            if (GUILayout.Button("Init text and shadow", style))
            {
                script.InitTextInEditor();
            }

            EditorGUILayout.Space();
            style.normal.background = MakeBackgroundTexture(10, 10, Color.green);
            if (GUILayout.Button("Reinit text and shadow", style))
            {
                script.ReinitTextInEditor();
            }
            EditorGUILayout.Space();
            style.normal.background = MakeBackgroundTexture(10, 10, Color.red);
            if (GUILayout.Button("Remove text and shadow", style))
            {
                script.RemoveTextInEditor();
            }
        }

        private Texture2D MakeBackgroundTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D backgroundTexture = new Texture2D(width, height);

            backgroundTexture.SetPixels(pixels);
            backgroundTexture.Apply();

            return backgroundTexture;
        }
    }
}