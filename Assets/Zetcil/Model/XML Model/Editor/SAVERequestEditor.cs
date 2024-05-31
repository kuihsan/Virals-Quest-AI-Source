using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SAVERequest)), CanEditMultipleObjects]
    public class SAVERequestEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           DirectoryName,
           FileName,
           DataValue,
           usingTagName,
           TagName,
           usingDATAObject
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            DirectoryName = serializedObject.FindProperty("DirectoryName");
            FileName = serializedObject.FindProperty("FileName");
            DataValue = serializedObject.FindProperty("DataValue");
            usingTagName = serializedObject.FindProperty("usingTagName");
            TagName = serializedObject.FindProperty("TagName");
            usingDATAObject = serializedObject.FindProperty("usingDATAObject");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(DirectoryName, true);
                EditorGUILayout.PropertyField(FileName, true);
                EditorGUILayout.PropertyField(DataValue, true);
                EditorGUILayout.PropertyField(usingTagName, true);
                EditorGUILayout.PropertyField(TagName, true);
                EditorGUILayout.PropertyField(usingDATAObject, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}