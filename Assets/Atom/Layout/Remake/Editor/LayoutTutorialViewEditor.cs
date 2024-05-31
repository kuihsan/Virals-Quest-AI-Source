using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutTutorialView)), CanEditMultipleObjects]
    public class LayoutTutorialViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           ButtonPrev,
           ButtonNext,
           TutorialIndicator,
           TutorialContent
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            ButtonPrev = serializedObject.FindProperty("ButtonPrev");
            ButtonNext = serializedObject.FindProperty("ButtonNext");
            TutorialIndicator = serializedObject.FindProperty("TutorialIndicator");
            TutorialContent = serializedObject.FindProperty("TutorialContent");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(ButtonPrev, true);
                if (ButtonPrev.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ButtonNext, true);
                if (ButtonNext.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TutorialIndicator, true);
                if (TutorialIndicator.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TutorialContent, true);
                if (TutorialContent.arraySize == 0)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
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