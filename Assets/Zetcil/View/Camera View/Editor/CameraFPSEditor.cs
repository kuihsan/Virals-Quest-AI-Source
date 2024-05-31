using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraFPS)), CanEditMultipleObjects]
    public class CameraFPSEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetController,
           TargetPivot,
           usingMouseController,
           MouseController,
           usingHeadbobber,
           bobbingSpeed,
           bobbingAmount,
           midpoint
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetController = serializedObject.FindProperty("TargetController");
            TargetPivot = serializedObject.FindProperty("TargetPivot");
            usingMouseController = serializedObject.FindProperty("usingMouseController");
            MouseController = serializedObject.FindProperty("MouseController");
            usingHeadbobber = serializedObject.FindProperty("usingHeadbobber");
            bobbingSpeed = serializedObject.FindProperty("bobbingSpeed");
            bobbingAmount = serializedObject.FindProperty("bobbingAmount");
            midpoint = serializedObject.FindProperty("midpoint");
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
                EditorGUILayout.PropertyField(TargetPivot);
                if (TargetPivot.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingMouseController);
                EditorGUILayout.PropertyField(MouseController);
                EditorGUILayout.PropertyField(usingHeadbobber);
                EditorGUILayout.PropertyField(bobbingSpeed);
                EditorGUILayout.PropertyField(bobbingAmount);
                EditorGUILayout.PropertyField(midpoint, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}