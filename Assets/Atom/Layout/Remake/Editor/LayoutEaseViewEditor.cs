using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutEaseView)), CanEditMultipleObjects]
    public class LayoutEaseViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           ease,
           InvokeTime,
           EaseSpeed,
           Delay,
           Interval,
           StartPosition,
           FinishPosition,
           HideMarker
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            ease = serializedObject.FindProperty("ease");
            InvokeTime = serializedObject.FindProperty("InvokeTime");
            EaseSpeed = serializedObject.FindProperty("EaseSpeed");
            Delay = serializedObject.FindProperty("Delay");
            Interval = serializedObject.FindProperty("Interval");
            StartPosition = serializedObject.FindProperty("StartPosition");
            FinishPosition = serializedObject.FindProperty("FinishPosition");
            HideMarker = serializedObject.FindProperty("HideMarker");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(ease, true);
                EditorGUILayout.PropertyField(InvokeTime, true);
                EditorGUILayout.PropertyField(EaseSpeed, true);
                if ((GlobalVariable.CInvokeType) InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnDelay)
                {
                    EditorGUILayout.PropertyField(Delay, true);
                }
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnInterval)
                {
                    EditorGUILayout.PropertyField(Interval, true);
                }
                EditorGUILayout.PropertyField(StartPosition, true);
                EditorGUILayout.PropertyField(FinishPosition, true);
                EditorGUILayout.PropertyField(HideMarker, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}