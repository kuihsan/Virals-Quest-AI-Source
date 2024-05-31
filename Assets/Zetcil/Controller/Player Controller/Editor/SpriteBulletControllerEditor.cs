using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SpriteBulletController)), CanEditMultipleObjects]
    public class SpriteBulletControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           BulletAngle,
           TranslateType,
           Speed,
           usingDestroy,
           DestroyDelay
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            BulletAngle = serializedObject.FindProperty("BulletAngle");
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
                EditorGUILayout.PropertyField(BulletAngle, true);
                EditorGUILayout.PropertyField(TranslateType, true);
                EditorGUILayout.PropertyField(Speed, true);
                EditorGUILayout.PropertyField(usingDestroy, true);
                EditorGUILayout.PropertyField(DestroyDelay, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}