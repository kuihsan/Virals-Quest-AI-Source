using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraAVI)), CanEditMultipleObjects]
    public class CameraAVIEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           usingStraight,
           cameraType,
           StraightSpeed,
           usingRotation,
           RotateDirection,
           TimerDelay,
           TimerStopAfter,
           usingLookAt,
           rotationType,
           TargetObject,
           RotationSpeed
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            usingStraight = serializedObject.FindProperty("usingStraight");
            cameraType = serializedObject.FindProperty("cameraType");
            StraightSpeed = serializedObject.FindProperty("StraightSpeed");
            usingRotation = serializedObject.FindProperty("usingRotation");
            RotateDirection = serializedObject.FindProperty("RotateDirection");
            TimerDelay = serializedObject.FindProperty("TimerDelay");
            TimerStopAfter = serializedObject.FindProperty("TimerStopAfter");
            usingLookAt = serializedObject.FindProperty("usingLookAt");
            rotationType = serializedObject.FindProperty("rotationType");
            TargetObject = serializedObject.FindProperty("TargetObject");
            RotationSpeed = serializedObject.FindProperty("RotationSpeed");
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
                EditorGUILayout.PropertyField(usingStraight);
                EditorGUILayout.PropertyField(cameraType);
                EditorGUILayout.PropertyField(StraightSpeed);
                EditorGUILayout.PropertyField(usingRotation);
                EditorGUILayout.PropertyField(RotateDirection);
                EditorGUILayout.PropertyField(TimerDelay);
                EditorGUILayout.PropertyField(TimerStopAfter);
                EditorGUILayout.PropertyField(usingLookAt);
                EditorGUILayout.PropertyField(rotationType);
                EditorGUILayout.PropertyField(TargetObject);
                EditorGUILayout.PropertyField(RotationSpeed);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}