using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SessionApp)), CanEditMultipleObjects]
    public class SessionAppEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           SessionConfig,
           Name,
           Machine,
           License,
           Token
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            SessionConfig = serializedObject.FindProperty("SessionConfig");
            Name = serializedObject.FindProperty("Name");
            Machine = serializedObject.FindProperty("Machine");
            License = serializedObject.FindProperty("License");
            Token = serializedObject.FindProperty("Token");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(SessionConfig, true);
                if (SessionConfig.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(Name, true);
                EditorGUILayout.PropertyField(Machine, true);
                EditorGUILayout.PropertyField(License, true);
                EditorGUILayout.PropertyField(Token, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}