using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutAudioView)), CanEditMultipleObjects]
    public class LayoutAudioViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           OperationType,
           SoundVolume,
           SoundSlider
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            OperationType = serializedObject.FindProperty("OperationType");
            SoundVolume = serializedObject.FindProperty("SoundVolume");
            SoundSlider = serializedObject.FindProperty("SoundSlider");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(OperationType, true);
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Initialize)
                {
                    EditorGUILayout.HelpBox("Save Mode: ON", MessageType.Warning);
                }
                if ((VarAudio.COperationType)OperationType.enumValueIndex == VarAudio.COperationType.Runtime)
                {
                    EditorGUILayout.HelpBox("Publish Mode: ON", MessageType.Info);
                }
                EditorGUILayout.PropertyField(SoundVolume, true);
                EditorGUILayout.PropertyField(SoundSlider, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}