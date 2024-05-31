using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraMMO)), CanEditMultipleObjects]
    public class CameraMMOEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetController,
           usingTargetObject,
           TargetObjectGroup,
           usingSyncronize,
           Syncronize
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetController = serializedObject.FindProperty("TargetController");
            usingTargetObject = serializedObject.FindProperty("usingTargetObject");
            TargetObjectGroup = serializedObject.FindProperty("TargetObjectGroup");
            usingSyncronize = serializedObject.FindProperty("usingSyncronize");
            Syncronize = serializedObject.FindProperty("Syncronize");
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
                EditorGUILayout.PropertyField(TargetController, true);
                if (TargetController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingTargetObject, true);
                EditorGUILayout.PropertyField(TargetObjectGroup, true);
                EditorGUILayout.PropertyField(usingSyncronize, true);
                EditorGUILayout.PropertyField(Syncronize, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}