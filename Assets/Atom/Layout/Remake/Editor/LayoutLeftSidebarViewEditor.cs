using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutLeftSidebarView)), CanEditMultipleObjects]
    public class LayoutLeftSidebarViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           SearchText,
           ButtonMenu,
           usingKeyEvent,
           KeyCodeEvent,
           KeyEvent
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            SearchText = serializedObject.FindProperty("SearchText");
            ButtonMenu = serializedObject.FindProperty("ButtonMenu");
            usingKeyEvent = serializedObject.FindProperty("usingKeyEvent");
            KeyCodeEvent = serializedObject.FindProperty("KeyCodeEvent");
            KeyEvent = serializedObject.FindProperty("KeyEvent");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(SearchText, true);
                EditorGUILayout.PropertyField(ButtonMenu, true);
                EditorGUILayout.PropertyField(usingKeyEvent, true);
                if (usingKeyEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(KeyCodeEvent, true);
                    EditorGUILayout.PropertyField(KeyEvent, true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}