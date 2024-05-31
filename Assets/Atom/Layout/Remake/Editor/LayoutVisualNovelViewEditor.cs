using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutVisualNovelView)), CanEditMultipleObjects]
    public class LayoutVisualNovelViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           DialogPanel,
           DialogNext,
           DialogSound,
           DialogText,
           TextDelay,
           usingXML,
           XMLFilename,
           LeftCharacterPortrait,
           LeftCharacterName,
           LeftCharacterSound,
           CenterCharacterPortrait,
           CenterCharacterName,
           CenterCharacterSound,
           RightCharacterPortrait,
           RightCharacterName,
           RightCharacterSound,
           usingLeftCharacter,
           LeftCharacter,
           usingCenterCharacter,
           CenterCharacter,
           usingRightCharacter,
           RightCharacter,
           usingStorytelling,
           StorytellingTrigger,
           Storytelling,
           FinalDialogEvent,
           CurrentDialog,
           CurrentStorytellingIndex,
           usingDelay,
           Delay,
           usingInterval,
           Interval
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            DialogPanel = serializedObject.FindProperty("DialogPanel");
            DialogNext = serializedObject.FindProperty("DialogNext");
            DialogSound = serializedObject.FindProperty("DialogSound");
            DialogText = serializedObject.FindProperty("DialogText");
            TextDelay = serializedObject.FindProperty("TextDelay");

            usingXML = serializedObject.FindProperty("usingXML");
            XMLFilename = serializedObject.FindProperty("XMLFilename");

            LeftCharacterPortrait = serializedObject.FindProperty("LeftCharacterPortrait");
            LeftCharacterName = serializedObject.FindProperty("LeftCharacterName");
            LeftCharacterSound = serializedObject.FindProperty("LeftCharacterSound");
            CenterCharacterPortrait = serializedObject.FindProperty("CenterCharacterPortrait");
            CenterCharacterName = serializedObject.FindProperty("CenterCharacterName");
            CenterCharacterSound = serializedObject.FindProperty("CenterCharacterSound");
            RightCharacterPortrait = serializedObject.FindProperty("RightCharacterPortrait");
            RightCharacterName = serializedObject.FindProperty("RightCharacterName");
            RightCharacterSound = serializedObject.FindProperty("RightCharacterSound");
            usingLeftCharacter = serializedObject.FindProperty("usingLeftCharacter");
            LeftCharacter = serializedObject.FindProperty("LeftCharacter");
            usingCenterCharacter = serializedObject.FindProperty("usingCenterCharacter");
            CenterCharacter = serializedObject.FindProperty("CenterCharacter");
            usingRightCharacter = serializedObject.FindProperty("usingRightCharacter");
            RightCharacter = serializedObject.FindProperty("RightCharacter");
            usingStorytelling = serializedObject.FindProperty("usingStorytelling");
            StorytellingTrigger = serializedObject.FindProperty("StorytellingTrigger");
            Storytelling = serializedObject.FindProperty("Storytelling");
            FinalDialogEvent = serializedObject.FindProperty("FinalDialogEvent");
            CurrentDialog = serializedObject.FindProperty("CurrentDialog");
            CurrentStorytellingIndex = serializedObject.FindProperty("CurrentStorytellingIndex");
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
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(DialogPanel, true);
                if (DialogPanel.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(DialogNext, true);
                if (DialogNext.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(DialogSound, true);
                if (DialogSound.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(DialogText, true);
                if (DialogText.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TextDelay, true);

                EditorGUILayout.PropertyField(usingXML, true);
                if (usingXML.boolValue)
                {
                    EditorGUILayout.PropertyField(XMLFilename, true);
                }

                EditorGUILayout.PropertyField(LeftCharacterPortrait, true);
                if (LeftCharacterPortrait.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(LeftCharacterName, true);
                EditorGUILayout.PropertyField(LeftCharacterSound, true);
                if (LeftCharacterSound.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(CenterCharacterPortrait, true);
                if (CenterCharacterPortrait.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(CenterCharacterName, true);
                EditorGUILayout.PropertyField(CenterCharacterSound, true);
                if (CenterCharacterSound.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(RightCharacterPortrait, true);
                if (RightCharacterPortrait.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(RightCharacterName, true);
                EditorGUILayout.PropertyField(RightCharacterSound, true);
                if (RightCharacterSound.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingLeftCharacter, true);
                if (usingLeftCharacter.boolValue)
                {
                    EditorGUILayout.PropertyField(LeftCharacter, true);
                    if (LeftCharacter.arraySize <= 0)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }
                EditorGUILayout.PropertyField(usingCenterCharacter, true);
                if (usingCenterCharacter.boolValue)
                {
                    EditorGUILayout.PropertyField(CenterCharacter, true);
                    if (CenterCharacter.arraySize <= 0)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }
                EditorGUILayout.PropertyField(usingRightCharacter, true);
                if (usingRightCharacter.boolValue)
                {
                    EditorGUILayout.PropertyField(RightCharacter, true);
                    if (RightCharacter.arraySize <= 0)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }
                EditorGUILayout.PropertyField(usingStorytelling, true);
                if (usingStorytelling.boolValue)
                {
                    EditorGUILayout.PropertyField(StorytellingTrigger, true);
                    EditorGUILayout.PropertyField(Storytelling, true);
                    if (Storytelling.arraySize <= 0)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }
                EditorGUILayout.PropertyField(FinalDialogEvent, true);
                EditorGUILayout.PropertyField(CurrentDialog, true);
                EditorGUILayout.PropertyField(CurrentStorytellingIndex, true);

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