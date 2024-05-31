using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraADV)), CanEditMultipleObjects]
    public class CameraADVEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetController,
           usingMouseController,
           MouseController
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetController = serializedObject.FindProperty("TargetController");
            usingMouseController = serializedObject.FindProperty("usingMouseController");
            MouseController = serializedObject.FindProperty("MouseController");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TargetCamera);
                if (TargetCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TargetController);
                if (TargetController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingMouseController);
                EditorGUILayout.PropertyField(MouseController);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}