using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutTaskView)), CanEditMultipleObjects]
    public class LayoutTaskViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TaskCount,
           TaskView
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TaskCount = serializedObject.FindProperty("TaskCount");
            TaskView = serializedObject.FindProperty("TaskView");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TaskCount, true);
                if (TaskCount.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TaskView, true);
                if (TaskView.arraySize == 0)
                {
                    EditorGUILayout.HelpBox("Array Field(s) Null / None", MessageType.Error);
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