using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(BulletController)), CanEditMultipleObjects]
    public class BulletControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           TranslateType,
           Speed,
           usingDestroy,
           DestroyDelay
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            TranslateType = serializedObject.FindProperty("TranslateType");
            Speed = serializedObject.FindProperty("Speed");
            usingDestroy = serializedObject.FindProperty("usingDestroy");
            DestroyDelay = serializedObject.FindProperty("DestroyDelay");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(TranslateType, true);
                EditorGUILayout.PropertyField(Speed, true);

                EditorGUILayout.PropertyField(usingDestroy, true);
                if (usingDestroy.boolValue)
                {
                    EditorGUILayout.PropertyField(DestroyDelay, true);
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