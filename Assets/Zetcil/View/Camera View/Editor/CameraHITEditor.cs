using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraHIT)), CanEditMultipleObjects]
    public class CameraHITEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           power,
           duration,
           slowDownAmount,
           startShake
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            power = serializedObject.FindProperty("power");
            duration = serializedObject.FindProperty("duration");
            slowDownAmount = serializedObject.FindProperty("slowDownAmount");
            startShake = serializedObject.FindProperty("startShake");
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
                EditorGUILayout.PropertyField(power, true);
                EditorGUILayout.PropertyField(duration, true);
                EditorGUILayout.PropertyField(slowDownAmount, true);
                EditorGUILayout.PropertyField(startShake, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}