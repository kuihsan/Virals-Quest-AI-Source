using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraTPV)), CanEditMultipleObjects]
    public class CameraTPVEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           TargetPivot,
           TargetController,
           mouseControl,
           rotationSpeed,
           pitchMax,
           pitchMin,
           rotationSmoothing,
           translationSmoothing,
           turnRate,
           verticalTurnInfluence,
           mouseX,
           mouseY
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            TargetPivot = serializedObject.FindProperty("TargetPivot");
            TargetController = serializedObject.FindProperty("TargetController");
            mouseControl = serializedObject.FindProperty("mouseControl");
            rotationSpeed = serializedObject.FindProperty("rotationSpeed");
            pitchMax = serializedObject.FindProperty("pitchMax");
            pitchMin = serializedObject.FindProperty("pitchMin");
            rotationSmoothing = serializedObject.FindProperty("rotationSmoothing");
            translationSmoothing = serializedObject.FindProperty("translationSmoothing");
            turnRate = serializedObject.FindProperty("turnRate");
            verticalTurnInfluence = serializedObject.FindProperty("verticalTurnInfluence");
            mouseX = serializedObject.FindProperty("mouseX");
            mouseY = serializedObject.FindProperty("mouseY");
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
                EditorGUILayout.PropertyField(TargetPivot, true);
                if (TargetPivot.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TargetController, true);
                if (TargetController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(mouseControl, true);
                if (mouseControl.boolValue)
                {
                    EditorGUILayout.PropertyField(rotationSpeed, true);
                    EditorGUILayout.PropertyField(pitchMax, true);
                    EditorGUILayout.PropertyField(pitchMin, true);
                    EditorGUILayout.PropertyField(rotationSmoothing, true);
                    EditorGUILayout.PropertyField(translationSmoothing, true);
                    EditorGUILayout.PropertyField(turnRate, true);
                    EditorGUILayout.PropertyField(verticalTurnInfluence, true);
                    EditorGUILayout.PropertyField(mouseX, true);
                    EditorGUILayout.PropertyField(mouseY, true);
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