using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(DATARequest)), CanEditMultipleObjects]
    public class DATARequestEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           DATAObjectCollection
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            DATAObjectCollection = serializedObject.FindProperty("DATAObjectCollection");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(DATAObjectCollection, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}