using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LangGameObjectController)), CanEditMultipleObjects]
    public class LangGameObjectControllerEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           LanguageType,
           IndonesianGameObject,
           EnglishGameObject,
           ArabicGameObject,
           KoreanGameObject,
           JapaneseGameObject,
           ChineseGameObject,
           usingLanguageEvent,
           LanguageEvent, 
		   usingDelay, 
		   Delay, 
		   usingInterval, 
	       Interval 
		 ; 

		void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            LanguageType = serializedObject.FindProperty("LanguageType");
            IndonesianGameObject = serializedObject.FindProperty("IndonesianGameObject");
            EnglishGameObject = serializedObject.FindProperty("EnglishGameObject");
            ArabicGameObject = serializedObject.FindProperty("ArabicGameObject");
            KoreanGameObject = serializedObject.FindProperty("KoreanGameObject");
            JapaneseGameObject = serializedObject.FindProperty("JapaneseGameObject");
            ChineseGameObject = serializedObject.FindProperty("ChineseGameObject");
            usingLanguageEvent = serializedObject.FindProperty("usingLanguageEvent");
            LanguageEvent = serializedObject.FindProperty("LanguageEvent");
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
                EditorGUILayout.PropertyField(LanguageType, true);

                EditorGUILayout.PropertyField(IndonesianGameObject, true);
                if (IndonesianGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(EnglishGameObject, true);
                if (EnglishGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ArabicGameObject, true);
                if (ArabicGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(KoreanGameObject, true);
                if (KoreanGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(JapaneseGameObject, true);
                if (JapaneseGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ChineseGameObject, true);
                if (ChineseGameObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingLanguageEvent, true);
                if (usingLanguageEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(LanguageEvent, true);
                }

                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnDelay)
                {
                    EditorGUILayout.PropertyField(usingDelay, true);
                    if (usingDelay.boolValue)
                    {
                        EditorGUILayout.PropertyField(Delay, true);
                    }
                }

                if ((GlobalVariable.CInvokeType)InvokeType.enumValueIndex == GlobalVariable.CInvokeType.OnInterval)
                {
                    EditorGUILayout.PropertyField(usingInterval, true);
                    if (usingInterval.boolValue)
                    {
                        EditorGUILayout.PropertyField(Interval, true);
                    }
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