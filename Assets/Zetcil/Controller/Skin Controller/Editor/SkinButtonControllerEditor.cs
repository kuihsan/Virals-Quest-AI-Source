using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SkinButtonController)), CanEditMultipleObjects]
    public class SkinButtonControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           TargetTag,
           SkinImage,
           NormalColor,
           HighlightColor,
           PressedColor,
           SelectedColor,
           DisabledColor,
           FontName,
           FontSize,
           FontColor,
           usingDelay,
           Delay,
           usingInterval,
           Interval
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            TargetTag = serializedObject.FindProperty("TargetTag");
            SkinImage = serializedObject.FindProperty("SkinImage");
            NormalColor = serializedObject.FindProperty("NormalColor");
            HighlightColor = serializedObject.FindProperty("HighlightColor");
            PressedColor = serializedObject.FindProperty("PressedColor");
            SelectedColor = serializedObject.FindProperty("SelectedColor");
            DisabledColor = serializedObject.FindProperty("DisabledColor");
            FontName = serializedObject.FindProperty("FontName");
            FontSize = serializedObject.FindProperty("FontSize");
            FontColor = serializedObject.FindProperty("FontColor");
            usingDelay = serializedObject.FindProperty("usingDelay");
            Delay = serializedObject.FindProperty("Delay");
            usingInterval = serializedObject.FindProperty("usingInterval");
            Interval = serializedObject.FindProperty("Interval");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(TargetTag, true);
                EditorGUILayout.PropertyField(SkinImage, true);
                if (SkinImage.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(NormalColor, true);
                EditorGUILayout.PropertyField(HighlightColor, true);
                EditorGUILayout.PropertyField(PressedColor, true);
                EditorGUILayout.PropertyField(SelectedColor, true);
                EditorGUILayout.PropertyField(DisabledColor, true);
                EditorGUILayout.PropertyField(FontName, true);
                EditorGUILayout.PropertyField(FontSize, true);
                EditorGUILayout.PropertyField(FontColor, true);
                EditorGUILayout.PropertyField(usingDelay, true);
                if (usingDelay.boolValue)
                {
                    EditorGUILayout.PropertyField(Delay, true);
                }
                EditorGUILayout.PropertyField(usingInterval, true);
                if (usingInterval.boolValue)
                {
                    EditorGUILayout.PropertyField(Interval, true);
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