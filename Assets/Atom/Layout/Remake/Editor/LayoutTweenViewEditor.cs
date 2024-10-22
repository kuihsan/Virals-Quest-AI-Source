﻿using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutTweenView)), CanEditMultipleObjects]
    public class LayoutTweenViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           ease,
           InvokeTime,
           EaseSpeed,
           Delay,
           Interval,
           InPosition,
           OutPosition,
           CurrentPosition,
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
            InPosition = serializedObject.FindProperty("InPosition");
            OutPosition = serializedObject.FindProperty("OutPosition");
            CurrentPosition = serializedObject.FindProperty("CurrentPosition");
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
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnDelay)
                {
                    EditorGUILayout.PropertyField(Delay, true);
                }
                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnInterval)
                {
                    EditorGUILayout.PropertyField(Interval, true);
                }
                EditorGUILayout.PropertyField(InPosition, true);
                EditorGUILayout.PropertyField(OutPosition, true);
                EditorGUILayout.PropertyField(CurrentPosition, true);
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