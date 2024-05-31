using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SessionVarExp)), CanEditMultipleObjects]
    public class SessionVarExpEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           SessionConfig,
           SessionName,
           SessionKey,
           SessionValue,
           LoadOnStart,
           LoadDelay
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            SessionConfig = serializedObject.FindProperty("SessionConfig");
            SessionName = serializedObject.FindProperty("SessionName");
            SessionKey = serializedObject.FindProperty("SessionKey");
            SessionValue = serializedObject.FindProperty("SessionValue");
            LoadOnStart = serializedObject.FindProperty("LoadOnStart");
            LoadDelay = serializedObject.FindProperty("LoadDelay");
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
                EditorGUILayout.PropertyField(SessionName, true);
                if (SessionName.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(SessionKey, true);
                if (SessionKey.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(SessionValue, true);
                if (SessionValue.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(LoadOnStart, true);
                EditorGUILayout.PropertyField(LoadDelay, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}