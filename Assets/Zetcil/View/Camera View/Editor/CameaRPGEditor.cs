using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraRPG)), CanEditMultipleObjects]
    public class CameraRPGEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetController,
           usingFollowSettings,
           FollowSettings,
           usingMouseController,
           MouseController
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetController = serializedObject.FindProperty("TargetController");
            usingFollowSettings = serializedObject.FindProperty("usingFollowSettings");
            FollowSettings = serializedObject.FindProperty("FollowSettings");
            usingMouseController = serializedObject.FindProperty("usingMouseController");
            MouseController = serializedObject.FindProperty("MouseController");
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
                EditorGUILayout.PropertyField(usingFollowSettings, true);
                EditorGUILayout.PropertyField(FollowSettings, true);
                EditorGUILayout.PropertyField(usingMouseController, true);
                if (usingMouseController.boolValue)
                {
                    EditorGUILayout.PropertyField(MouseController, true);
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