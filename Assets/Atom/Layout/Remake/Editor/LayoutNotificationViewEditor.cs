using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutNotificationView)), CanEditMultipleObjects]
    public class LayoutNotificationViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           Notification1,
           Notification2,
           Notification3,
           Notification4,
           Notification5,
           usingXML,
           NotificationDelay,
           usingDelay,
           Delay,
           usingInterval,
           Interval
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            Notification1 = serializedObject.FindProperty("Notification1");
            Notification2 = serializedObject.FindProperty("Notification2");
            Notification3 = serializedObject.FindProperty("Notification3");
            Notification4 = serializedObject.FindProperty("Notification4");
            Notification5 = serializedObject.FindProperty("Notification5");

            usingXML = serializedObject.FindProperty("usingXML");

            NotificationDelay = serializedObject.FindProperty("NotificationDelay");
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
                EditorGUILayout.PropertyField(Notification1, true);
                EditorGUILayout.PropertyField(Notification2, true);
                EditorGUILayout.PropertyField(Notification3, true);
                EditorGUILayout.PropertyField(Notification4, true);
                EditorGUILayout.PropertyField(Notification5, true);

                EditorGUILayout.PropertyField(usingXML, true);

                EditorGUILayout.PropertyField(NotificationDelay, true);
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