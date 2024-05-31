/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk var manager
 **************************************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(PHPRequest)), CanEditMultipleObjects]
    public class PHPRequestEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            MainURL,
            RequestType,
            RequestOutput,
            PrintDebugConsole
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            MainURL = serializedObject.FindProperty("MainURL");
            RequestType = serializedObject.FindProperty("RequestType");
            RequestOutput = serializedObject.FindProperty("RequestOutput");
            PrintDebugConsole = serializedObject.FindProperty("PrintDebugConsole");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled, true);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(RequestType, true);
                EditorGUILayout.PropertyField(MainURL, true);

                EditorGUILayout.PropertyField(RequestOutput, true);
                EditorGUILayout.PropertyField(PrintDebugConsole, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
