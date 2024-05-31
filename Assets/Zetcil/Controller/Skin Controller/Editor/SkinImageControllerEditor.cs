using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SkinImageController)), CanEditMultipleObjects]
    public class SkinImageControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           TargetTag,
           SkinImage,
           SkinColor,
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
            SkinColor = serializedObject.FindProperty("SkinColor");
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
                EditorGUILayout.PropertyField(SkinColor, true);
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