using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutCornerView)), CanEditMultipleObjects]
    public class LayoutCornerViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           DialogPanel,
           DialogText,
           TextDelay,
           FadeDelay,
           
           usingXML,
           XMLFilename,

           usingLeftCornerName,
           LeftCornerCharacterName,
           LeftCornerCharacterPanel,
           usingLeftCornerPortrait,
           LeftCornerCharacterPortrait,

           usingRightCornerName,
           RightCornerCharacterName,
           RightCornerCharacterPanel,
           usingRightCornerPortrait,
           RightCornerCharacterPortrait,

           usingCornerDialog,
           CornerDialog,
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
            DialogText = serializedObject.FindProperty("DialogText");
            TextDelay = serializedObject.FindProperty("TextDelay");
            FadeDelay = serializedObject.FindProperty("FadeDelay");

            usingXML = serializedObject.FindProperty("usingXML");
            XMLFilename = serializedObject.FindProperty("XMLFilename");

            usingLeftCornerName = serializedObject.FindProperty("usingLeftCornerName");
            LeftCornerCharacterName = serializedObject.FindProperty("LeftCornerCharacterName");
            LeftCornerCharacterPanel = serializedObject.FindProperty("LeftCornerCharacterPanel");
            usingLeftCornerPortrait = serializedObject.FindProperty("usingLeftCornerPortrait");
            LeftCornerCharacterPortrait = serializedObject.FindProperty("LeftCornerCharacterPortrait");

            usingRightCornerName = serializedObject.FindProperty("usingRightCornerName");
            RightCornerCharacterName = serializedObject.FindProperty("RightCornerCharacterName");
            RightCornerCharacterPanel = serializedObject.FindProperty("RightCornerCharacterPanel");
            usingRightCornerPortrait = serializedObject.FindProperty("usingRightCornerPortrait");
            RightCornerCharacterPortrait = serializedObject.FindProperty("RightCornerCharacterPortrait");

            usingCornerDialog = serializedObject.FindProperty("usingCornerDialog");
            CornerDialog = serializedObject.FindProperty("CornerDialog");
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
                EditorGUILayout.PropertyField(DialogText, true);
                if (DialogText.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(TextDelay, true);
                EditorGUILayout.PropertyField(FadeDelay, true);

                EditorGUILayout.PropertyField(usingXML, true);
                if (usingXML.boolValue)
                {
                    EditorGUILayout.PropertyField(XMLFilename, true);
                }

                EditorGUILayout.PropertyField(usingLeftCornerName, true);
                EditorGUILayout.PropertyField(LeftCornerCharacterPanel, true);
                EditorGUILayout.PropertyField(LeftCornerCharacterName, true);
                if (LeftCornerCharacterName.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingLeftCornerPortrait, true);
                EditorGUILayout.PropertyField(LeftCornerCharacterPortrait, true);
                if (LeftCornerCharacterPortrait.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(usingRightCornerName, true);
                EditorGUILayout.PropertyField(RightCornerCharacterPanel, true);
                EditorGUILayout.PropertyField(RightCornerCharacterName, true);
                if (RightCornerCharacterName.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingRightCornerPortrait, true);
                EditorGUILayout.PropertyField(RightCornerCharacterPortrait, true);
                if (LeftCornerCharacterPortrait.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(usingCornerDialog, true);
                if (usingCornerDialog.boolValue)
                {
                    EditorGUILayout.PropertyField(CornerDialog, true);
                    EditorGUILayout.PropertyField(CurrentDialog, true);
                    EditorGUILayout.PropertyField(CurrentStorytellingIndex, true);
                }

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