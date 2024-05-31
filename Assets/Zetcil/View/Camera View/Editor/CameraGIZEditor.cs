using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraGIZ)), CanEditMultipleObjects]
    public class CameraGIZEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           IndexMouseButton,
           usingMouseStatus,
           MouseType,
           space,
           type,
           TagObjects,
           showReadOnly,
           debugMouseX,
           debugMouseY,
           debugCoordX,
           debugCoordY,
           debugCoordZ,
           IndexSelection
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            IndexMouseButton = serializedObject.FindProperty("IndexMouseButton");
            usingMouseStatus = serializedObject.FindProperty("usingMouseStatus");
            MouseType = serializedObject.FindProperty("MouseType");
            space = serializedObject.FindProperty("space");
            type = serializedObject.FindProperty("type");
            TagObjects = serializedObject.FindProperty("TagObjects");
            showReadOnly = serializedObject.FindProperty("showReadOnly");
            debugMouseX = serializedObject.FindProperty("debugMouseX");
            debugMouseY = serializedObject.FindProperty("debugMouseY");
            debugCoordX = serializedObject.FindProperty("debugCoordX");
            debugCoordY = serializedObject.FindProperty("debugCoordY");
            debugCoordZ = serializedObject.FindProperty("debugCoordZ");
            IndexSelection = serializedObject.FindProperty("IndexSelection");
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
                EditorGUILayout.PropertyField(IndexMouseButton, true);
                EditorGUILayout.PropertyField(usingMouseStatus, true);
                EditorGUILayout.PropertyField(MouseType, true);
                EditorGUILayout.PropertyField(space, true);
                EditorGUILayout.PropertyField(type, true);
                EditorGUILayout.PropertyField(TagObjects, true);
                EditorGUILayout.PropertyField(showReadOnly, true);
                EditorGUILayout.PropertyField(debugMouseX, true);
                EditorGUILayout.PropertyField(debugMouseY, true);
                EditorGUILayout.PropertyField(debugCoordX, true);
                EditorGUILayout.PropertyField(debugCoordY, true);
                EditorGUILayout.PropertyField(debugCoordZ, true);
                EditorGUILayout.PropertyField(IndexSelection, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}