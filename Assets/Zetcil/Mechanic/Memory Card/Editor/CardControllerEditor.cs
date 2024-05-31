using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(CardController)), CanEditMultipleObjects]
    public class CardControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetCamera,
            CardCollection,
            usingTrueCompareEvent,
            TrueCompareEvent,
            usingFalseCompareEvent,
            FalseCompareEvent,
            usingFinishCompareEvent,
            FinishCompareEvent,
            FirstCard,
            SecondCard,
            CompareStatus,
            SelectedObjectType,
            SelectedObjectTag,
            SelectedObjectName,
            CurrentIndex
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            CardCollection = serializedObject.FindProperty("CardCollection");
            usingTrueCompareEvent = serializedObject.FindProperty("usingTrueCompareEvent");
            TrueCompareEvent = serializedObject.FindProperty("TrueCompareEvent");
            usingFalseCompareEvent = serializedObject.FindProperty("usingFalseCompareEvent");
            FalseCompareEvent = serializedObject.FindProperty("FalseCompareEvent");
            usingFinishCompareEvent = serializedObject.FindProperty("usingFinishCompareEvent");
            FinishCompareEvent = serializedObject.FindProperty("FinishCompareEvent");
            FirstCard = serializedObject.FindProperty("FirstCard");
            SecondCard = serializedObject.FindProperty("SecondCard");
            CompareStatus = serializedObject.FindProperty("CompareStatus");
            SelectedObjectType = serializedObject.FindProperty("SelectedObjectType");
            SelectedObjectTag = serializedObject.FindProperty("SelectedObjectTag");
            SelectedObjectName = serializedObject.FindProperty("SelectedObjectName");
            CurrentIndex = serializedObject.FindProperty("CurrentIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            bool check = isEnabled.boolValue;

            if (check)
            {
                EditorGUILayout.PropertyField(TargetCamera, true);
                EditorGUILayout.PropertyField(CardCollection, true);

                EditorGUILayout.PropertyField(usingTrueCompareEvent, true);
                if (usingTrueCompareEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(TrueCompareEvent, true);
                }

                EditorGUILayout.PropertyField(usingFalseCompareEvent, true);
                if (usingFalseCompareEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(FalseCompareEvent, true);
                }

                EditorGUILayout.PropertyField(usingFinishCompareEvent, true);
                if (usingFinishCompareEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(FinishCompareEvent, true);
                }

                EditorGUILayout.PropertyField(FirstCard, true);
                EditorGUILayout.PropertyField(SecondCard, true);
                EditorGUILayout.PropertyField(CompareStatus, true);
                EditorGUILayout.PropertyField(SelectedObjectType, true);
                EditorGUILayout.PropertyField(SelectedObjectTag, true);
                EditorGUILayout.PropertyField(SelectedObjectName, true);
                EditorGUILayout.PropertyField(CurrentIndex, true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}