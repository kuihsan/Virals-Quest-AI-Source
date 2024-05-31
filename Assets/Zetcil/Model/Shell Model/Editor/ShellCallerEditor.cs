using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(ShellCaller)), CanEditMultipleObjects]
    public class ShellCallerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           ApplicationName,
           TargetShell
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            ApplicationName = serializedObject.FindProperty("ApplicationName");
            TargetShell = serializedObject.FindProperty("TargetShell");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(ApplicationName, true);
                EditorGUILayout.PropertyField(TargetShell, true);
                ApplicationName.stringValue = Application.productName;
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}