using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(QuizController)), CanEditMultipleObjects]
    public class QuizControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            CurrentIndex,
            QuizSettings,
            usingResultSettings,
            QuizResults
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");

            QuizSettings = serializedObject.FindProperty("QuizSettings");
            usingResultSettings = serializedObject.FindProperty("usingResultSettings");
            QuizResults = serializedObject.FindProperty("QuizResults");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            bool check = isEnabled.boolValue;

            if (check)
            {
                EditorGUILayout.PropertyField(CurrentIndex, true);
                EditorGUILayout.PropertyField(QuizSettings, true);

                EditorGUILayout.PropertyField(usingResultSettings, true);
                if (usingResultSettings.boolValue)
                {
                    EditorGUILayout.PropertyField(QuizResults, true);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}