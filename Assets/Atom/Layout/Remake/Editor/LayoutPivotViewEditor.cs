using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutPivotView)), CanEditMultipleObjects]
    public class LayoutPivotViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           CanvasPivot,
           StartPosition,
           FinishPosition,
           finishPos,
           InvokeTime,
           EaseSpeed,
           Delay,
           Interval,
           anchor,
           HideMarker
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            CanvasPivot = serializedObject.FindProperty("CanvasPivot");
            StartPosition = serializedObject.FindProperty("StartPosition");
            FinishPosition = serializedObject.FindProperty("FinishPosition");
            finishPos = serializedObject.FindProperty("finishPos");
            InvokeTime = serializedObject.FindProperty("InvokeTime");
            EaseSpeed = serializedObject.FindProperty("EaseSpeed");
            Delay = serializedObject.FindProperty("Delay");
            Interval = serializedObject.FindProperty("Interval");
            anchor = serializedObject.FindProperty("anchor");
            HideMarker = serializedObject.FindProperty("HideMarker");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(CanvasPivot, true);
                EditorGUILayout.PropertyField(StartPosition, true);
                EditorGUILayout.PropertyField(FinishPosition, true);
                EditorGUILayout.PropertyField(finishPos, true);
                EditorGUILayout.PropertyField(InvokeTime, true);
                EditorGUILayout.PropertyField(EaseSpeed, true);
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnDelay)
                {
                    EditorGUILayout.PropertyField(Delay, true);
                }
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnInterval)
                {
                    EditorGUILayout.PropertyField(Interval, true);
                }
                EditorGUILayout.PropertyField(anchor, true);
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