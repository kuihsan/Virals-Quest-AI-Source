using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(KeyboardController)), CanEditMultipleObjects]
    public class KeyboardControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           usingKeyboardSettings,
           PrimaryKeyboardKey,
           AltKeyboardKey,
           SecondaryKeyboardKey,
           AltSecondaryKeyboardKey,
           usingShiftSettings,
           ShiftKeyboardArray,
           usingAdditionalSettings,
           KeyboardArray
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            usingKeyboardSettings = serializedObject.FindProperty("usingKeyboardSettings");
            PrimaryKeyboardKey = serializedObject.FindProperty("PrimaryKeyboardKey");
            AltKeyboardKey = serializedObject.FindProperty("AltKeyboardKey");
            SecondaryKeyboardKey = serializedObject.FindProperty("SecondaryKeyboardKey");
            AltSecondaryKeyboardKey = serializedObject.FindProperty("AltSecondaryKeyboardKey");
            usingShiftSettings = serializedObject.FindProperty("usingShiftSettings");
            ShiftKeyboardArray = serializedObject.FindProperty("ShiftKeyboardArray");
            usingAdditionalSettings = serializedObject.FindProperty("usingAdditionalSettings");
            KeyboardArray = serializedObject.FindProperty("KeyboardArray");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(usingKeyboardSettings, true);
                EditorGUILayout.PropertyField(PrimaryKeyboardKey, true);
                EditorGUILayout.PropertyField(AltKeyboardKey, true);
                EditorGUILayout.PropertyField(SecondaryKeyboardKey, true);
                EditorGUILayout.PropertyField(AltSecondaryKeyboardKey, true);
                EditorGUILayout.PropertyField(usingShiftSettings, true);
                EditorGUILayout.PropertyField(ShiftKeyboardArray, true);
                EditorGUILayout.PropertyField(usingAdditionalSettings, true);
                EditorGUILayout.PropertyField(KeyboardArray, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}