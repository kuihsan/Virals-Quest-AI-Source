using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CameraUFO)), CanEditMultipleObjects]
    public class CameraUFOEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TargetCamera,
           usingKeyboardController,
           KeyboardController,
           usingJoystickController,
           JoystickController,
           usingMouseController,
           MouseController,
           usingTouchController,
           TouchController,
           ResetKey,
           MoveSpeed,
           ShiftSpeed,
           PitchSpeed,
           YawSpeed,
           RollSpeed,
           ScrollSpeed,
           LookSpeed,
           PanSpeed
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            usingKeyboardController = serializedObject.FindProperty("usingKeyboardController");
            KeyboardController = serializedObject.FindProperty("KeyboardController");
            usingJoystickController = serializedObject.FindProperty("usingJoystickController");
            JoystickController = serializedObject.FindProperty("JoystickController");
            usingMouseController = serializedObject.FindProperty("usingMouseController");
            MouseController = serializedObject.FindProperty("MouseController");
            usingTouchController = serializedObject.FindProperty("usingTouchController");
            TouchController = serializedObject.FindProperty("TouchController");
            ResetKey = serializedObject.FindProperty("ResetKey");
            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            ShiftSpeed = serializedObject.FindProperty("ShiftSpeed");
            PitchSpeed = serializedObject.FindProperty("PitchSpeed");
            YawSpeed = serializedObject.FindProperty("YawSpeed");
            RollSpeed = serializedObject.FindProperty("RollSpeed");
            ScrollSpeed = serializedObject.FindProperty("ScrollSpeed");
            LookSpeed = serializedObject.FindProperty("LookSpeed");
            PanSpeed = serializedObject.FindProperty("PanSpeed");
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
                EditorGUILayout.PropertyField(usingKeyboardController, true);
                EditorGUILayout.PropertyField(KeyboardController, true);
                EditorGUILayout.PropertyField(usingJoystickController, true);
                EditorGUILayout.PropertyField(JoystickController, true);
                EditorGUILayout.PropertyField(usingMouseController, true);
                EditorGUILayout.PropertyField(MouseController, true);
                EditorGUILayout.PropertyField(usingTouchController, true);
                EditorGUILayout.PropertyField(TouchController, true);
                EditorGUILayout.PropertyField(ResetKey, true);
                EditorGUILayout.PropertyField(MoveSpeed, true);
                EditorGUILayout.PropertyField(ShiftSpeed, true);
                EditorGUILayout.PropertyField(PitchSpeed, true);
                EditorGUILayout.PropertyField(YawSpeed, true);
                EditorGUILayout.PropertyField(RollSpeed, true);
                EditorGUILayout.PropertyField(ScrollSpeed, true);
                EditorGUILayout.PropertyField(LookSpeed, true);
                EditorGUILayout.PropertyField(PanSpeed, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}