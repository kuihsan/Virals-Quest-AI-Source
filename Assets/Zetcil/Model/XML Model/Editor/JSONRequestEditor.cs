/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk var manager
 **************************************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(JSONRequest)), CanEditMultipleObjects]
    public class JSONRequestEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            StringInput,
            JSONParsing,
            ParsingDelay,
            ParsingTriggerKey,
            JSONList
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            StringInput = serializedObject.FindProperty("StringInput");

            JSONParsing = serializedObject.FindProperty("JSONParsing");
            ParsingDelay = serializedObject.FindProperty("ParsingDelay");
            ParsingTriggerKey = serializedObject.FindProperty("ParsingTriggerKey");


            JSONList = serializedObject.FindProperty("JSONList");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled, true);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(StringInput, true);
                EditorGUILayout.PropertyField(JSONParsing, true);

                JSONRequest.CJSONParsing st = (JSONRequest.CJSONParsing) JSONParsing.enumValueIndex;
                if (st == JSONRequest.CJSONParsing.ByDelay)
                {
                    EditorGUILayout.PropertyField(ParsingDelay, true);
                }
                if (st == JSONRequest.CJSONParsing.ByInputKey)
                {
                    EditorGUILayout.PropertyField(ParsingTriggerKey, true);
                }

                EditorGUILayout.PropertyField(JSONList, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
