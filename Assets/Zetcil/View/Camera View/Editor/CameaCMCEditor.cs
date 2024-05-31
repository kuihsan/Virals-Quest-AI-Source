using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraCMC)), CanEditMultipleObjects]
    public class CameraCMCEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           usingCheckpoint,
           CameraGroup
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            usingCheckpoint = serializedObject.FindProperty("usingCheckpoint");
            CameraGroup = serializedObject.FindProperty("CameraGroup");
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
                EditorGUILayout.PropertyField(usingCheckpoint);
                EditorGUILayout.PropertyField(CameraGroup);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}