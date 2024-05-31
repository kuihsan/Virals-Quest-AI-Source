using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraCUT)), CanEditMultipleObjects]
    public class CameraCUTEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           usingCameraCut,
           CameraGroup,
           CurrentIndex,
           Duration
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            usingCameraCut = serializedObject.FindProperty("usingCameraCut");
            CameraGroup = serializedObject.FindProperty("CameraGroup");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");
            Duration = serializedObject.FindProperty("Duration");
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
                EditorGUILayout.PropertyField(usingCameraCut);
                EditorGUILayout.PropertyField(CameraGroup);
                EditorGUILayout.PropertyField(CurrentIndex);
                EditorGUILayout.PropertyField(Duration, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}