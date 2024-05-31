/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk var manager
 **************************************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SQLRequest)), CanEditMultipleObjects]
    public class SQLRequestEditor : Editor
    {
        public SerializedProperty
            isEnabled,
            MainURL,

            RequestType,
            SQLDataType,
            SQLHost,
            SQLUsername,
            SQLPassword,
            SQLDatabase,
            SQLCommand,
            SQLStatement,
            SQLTableName,
            SQLFieldName,
            usingSQLCondition,
            SQLCondition,
            usingSQLOrderBy,
            SQLOrderBy,
            SQLOrderType,

            JSONString,
            PrintDebugConsole
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            MainURL = serializedObject.FindProperty("MainURL");
            RequestType = serializedObject.FindProperty("RequestType");

            SQLDataType = serializedObject.FindProperty("SQLDataType");
            SQLHost = serializedObject.FindProperty("SQLHost");
            SQLUsername = serializedObject.FindProperty("SQLUsername");
            SQLPassword = serializedObject.FindProperty("SQLPassword");
            SQLDatabase = serializedObject.FindProperty("SQLDatabase");
            SQLCommand = serializedObject.FindProperty("SQLCommand");
            SQLStatement = serializedObject.FindProperty("SQLStatement");
            SQLTableName = serializedObject.FindProperty("SQLTableName");
            SQLFieldName = serializedObject.FindProperty("SQLFieldName");
            usingSQLCondition = serializedObject.FindProperty("usingSQLCondition");
            SQLCondition = serializedObject.FindProperty("SQLCondition");
            usingSQLOrderBy = serializedObject.FindProperty("usingSQLOrderBy");
            SQLOrderBy = serializedObject.FindProperty("SQLOrderBy");
            SQLOrderType = serializedObject.FindProperty("SQLOrderType");

            JSONString = serializedObject.FindProperty("JSONString");
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

                EditorGUILayout.PropertyField(SQLDataType, true);

                SQLRequest.CSQLDataType dt = (SQLRequest.CSQLDataType) SQLDataType.enumValueIndex;

                if (dt == SQLRequest.CSQLDataType.Native)
                {
                    EditorGUILayout.PropertyField(SQLHost, true);
                    EditorGUILayout.PropertyField(SQLUsername, true);
                    EditorGUILayout.PropertyField(SQLPassword, true);
                    EditorGUILayout.PropertyField(SQLDatabase, true);
                }
                EditorGUILayout.PropertyField(SQLCommand, true);

                SQLRequest.CSQLCommand st = (SQLRequest.CSQLCommand)SQLCommand.enumValueIndex;

                if (st == SQLRequest.CSQLCommand.ByStatement)
                {
                    EditorGUILayout.PropertyField(SQLStatement, true);
                }
                if (st == SQLRequest.CSQLCommand.ByParameter)
                {
                    EditorGUILayout.PropertyField(SQLTableName, true);
                    EditorGUILayout.PropertyField(SQLFieldName, true);
                    EditorGUILayout.PropertyField(usingSQLCondition, true);
                    if (usingSQLCondition.boolValue)
                    {
                        EditorGUILayout.PropertyField(SQLCondition, true);
                    }
                    EditorGUILayout.PropertyField(usingSQLOrderBy, true);
                    if (usingSQLOrderBy.boolValue)
                    {
                        EditorGUILayout.PropertyField(SQLOrderBy, true);
                        EditorGUILayout.PropertyField(SQLOrderType, true);
                    }
                }

                EditorGUILayout.PropertyField(JSONString, true);
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
