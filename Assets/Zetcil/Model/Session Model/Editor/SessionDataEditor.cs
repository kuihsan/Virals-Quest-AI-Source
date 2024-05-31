using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SessionData)), CanEditMultipleObjects]
    public class SessionDataEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           LoadOnStart,
           SessionConfig,
           SessionName,
           SessionList,
           CharacterSession,
           LevelSession,
           CurrentIndex,
           CurrentLevel,
           CurrentTask
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            LoadOnStart = serializedObject.FindProperty("LoadOnStart");
            SessionConfig = serializedObject.FindProperty("SessionConfig");
            SessionName = serializedObject.FindProperty("SessionName");
            SessionList = serializedObject.FindProperty("SessionList");
            CharacterSession = serializedObject.FindProperty("CharacterSession");
            LevelSession = serializedObject.FindProperty("LevelSession");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");
            CurrentLevel = serializedObject.FindProperty("CurrentLevel");
            CurrentTask = serializedObject.FindProperty("CurrentTask");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(LoadOnStart, true);
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
                EditorGUILayout.PropertyField(SessionList, true);
                EditorGUILayout.PropertyField(CharacterSession, true);
                EditorGUILayout.PropertyField(LevelSession, true);
                EditorGUILayout.PropertyField(CurrentIndex, true);
                EditorGUILayout.PropertyField(CurrentLevel, true);
                EditorGUILayout.PropertyField(CurrentTask, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}