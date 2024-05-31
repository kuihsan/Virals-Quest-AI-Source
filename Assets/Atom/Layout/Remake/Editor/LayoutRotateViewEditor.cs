using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutRotateView)), CanEditMultipleObjects]
    public class LayoutRotateViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           ease,
           RotateObject,
           Angle,
           InvokeTime,
           EaseSpeed,
           Delay,
           Interval
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            ease = serializedObject.FindProperty("ease");
            RotateObject = serializedObject.FindProperty("RotateObject");
            Angle = serializedObject.FindProperty("Angle");
            InvokeTime = serializedObject.FindProperty("InvokeTime");
            EaseSpeed = serializedObject.FindProperty("EaseSpeed");
            Delay = serializedObject.FindProperty("Delay");
            Interval = serializedObject.FindProperty("Interval");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(ease, true);
                EditorGUILayout.PropertyField(RotateObject, true);
                if (RotateObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(Angle, true);
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
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}