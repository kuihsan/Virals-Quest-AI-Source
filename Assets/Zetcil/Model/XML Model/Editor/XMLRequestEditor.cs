using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(XMLRequest)), CanEditMultipleObjects]
    public class XMLRequestEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           RequestType,
           XMLPath,
           XMLDirectory,
           XMLFile,
           DataType,
           TagName,
           TagCount,
           TagList,
           XMLString,
           PrintDebugConsole
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            RequestType = serializedObject.FindProperty("RequestType");
            XMLPath = serializedObject.FindProperty("XMLPath");
            XMLDirectory = serializedObject.FindProperty("XMLDirectory");
            XMLFile = serializedObject.FindProperty("XMLFile");
            DataType = serializedObject.FindProperty("DataType");
            TagName = serializedObject.FindProperty("TagName");
            TagCount = serializedObject.FindProperty("TagCount");
            TagList = serializedObject.FindProperty("TagList");
            XMLString = serializedObject.FindProperty("XMLString");
            PrintDebugConsole = serializedObject.FindProperty("PrintDebugConsole");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(RequestType, true);
                EditorGUILayout.PropertyField(XMLPath, true);
                EditorGUILayout.PropertyField(XMLDirectory, true);
                EditorGUILayout.PropertyField(XMLFile, true);
                EditorGUILayout.PropertyField(DataType, true);
                EditorGUILayout.PropertyField(TagName, true);
                EditorGUILayout.PropertyField(TagCount, true);
                EditorGUILayout.PropertyField(TagList, true);
                EditorGUILayout.PropertyField(XMLString, true);
                EditorGUILayout.PropertyField(PrintDebugConsole, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}