using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraMAP)), CanEditMultipleObjects]
    public class CameraMAPEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetController,
           smoothTime,
           smoothOffset
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetController = serializedObject.FindProperty("TargetController");
            smoothTime = serializedObject.FindProperty("smoothTime");
            smoothOffset = serializedObject.FindProperty("smoothOffset");
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
                EditorGUILayout.PropertyField(smoothTime, true);
                EditorGUILayout.PropertyField(smoothOffset, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}