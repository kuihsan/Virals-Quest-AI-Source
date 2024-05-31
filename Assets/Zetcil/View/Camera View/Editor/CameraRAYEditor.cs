using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraRAY)), CanEditMultipleObjects]
    public class CameraRAYEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           usingSelectedObject,
           SelectedObject,
           ObjectSelection,
           ObjectName,
           ObjectTag,
           usingOnHitEvent,
           OnHitEvent,
           usingOffHitEvent,
           OffHitEvent,
           SelectedObjectType,
           SelectedObjectTag,
           SelectedObjectName
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            usingSelectedObject = serializedObject.FindProperty("usingSelectedObject");
            SelectedObject = serializedObject.FindProperty("SelectedObject");
            ObjectSelection = serializedObject.FindProperty("ObjectSelection");
            ObjectName = serializedObject.FindProperty("ObjectName");
            ObjectTag = serializedObject.FindProperty("ObjectTag");
            usingOnHitEvent = serializedObject.FindProperty("usingOnHitEvent");
            OnHitEvent = serializedObject.FindProperty("OnHitEvent");
            usingOffHitEvent = serializedObject.FindProperty("usingOffHitEvent");
            OffHitEvent = serializedObject.FindProperty("OffHitEvent");
            SelectedObjectType = serializedObject.FindProperty("SelectedObjectType");
            SelectedObjectTag = serializedObject.FindProperty("SelectedObjectTag");
            SelectedObjectName = serializedObject.FindProperty("SelectedObjectName");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetCamera, true);
                if (TargetCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingSelectedObject, true);
                EditorGUILayout.PropertyField(SelectedObject, true);
                EditorGUILayout.PropertyField(ObjectSelection, true);
                EditorGUILayout.PropertyField(ObjectName, true);
                EditorGUILayout.PropertyField(ObjectTag, true);
                EditorGUILayout.PropertyField(usingOnHitEvent, true);
                EditorGUILayout.PropertyField(OnHitEvent, true);
                EditorGUILayout.PropertyField(usingOffHitEvent, true);
                EditorGUILayout.PropertyField(OffHitEvent, true);
                EditorGUILayout.PropertyField(SelectedObjectType, true);
                EditorGUILayout.PropertyField(SelectedObjectTag, true);
                EditorGUILayout.PropertyField(SelectedObjectName, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}