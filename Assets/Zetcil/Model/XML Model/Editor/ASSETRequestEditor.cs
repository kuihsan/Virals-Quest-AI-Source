using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(ASSETRequest)), CanEditMultipleObjects]
    public class ASSETRequestEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           AssetBundleDirectory,
           AssetBundleFile,
           AssetsBundleLoadType,
           AssetBundleStatus,
           AssetBundleController,
           usingDataRequest,
           DataRequest,
           usingLoadSettings,
           DirectoryName,
           FileName,
           DataValue,
           usingInstantiateSettings,
           InstantiateType,
           ActivePrefab,
           ActivePointer,
           ActiveOffset
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            AssetBundleDirectory = serializedObject.FindProperty("AssetBundleDirectory");
            AssetBundleFile = serializedObject.FindProperty("AssetBundleFile");
            AssetsBundleLoadType = serializedObject.FindProperty("AssetsBundleLoadType");
            AssetBundleStatus = serializedObject.FindProperty("AssetBundleStatus");
            AssetBundleController = serializedObject.FindProperty("AssetBundleController");
            usingDataRequest = serializedObject.FindProperty("usingDataRequest");
            DataRequest = serializedObject.FindProperty("DataRequest");
            usingLoadSettings = serializedObject.FindProperty("usingLoadSettings");
            DirectoryName = serializedObject.FindProperty("DirectoryName");
            FileName = serializedObject.FindProperty("FileName");
            DataValue = serializedObject.FindProperty("DataValue");
            usingInstantiateSettings = serializedObject.FindProperty("usingInstantiateSettings");
            InstantiateType = serializedObject.FindProperty("InstantiateType");
            ActivePrefab = serializedObject.FindProperty("ActivePrefab");
            ActivePointer = serializedObject.FindProperty("ActivePointer");
            ActiveOffset = serializedObject.FindProperty("ActiveOffset");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(AssetBundleDirectory, true);
                EditorGUILayout.PropertyField(AssetBundleFile, true);
                EditorGUILayout.PropertyField(AssetsBundleLoadType, true);
                EditorGUILayout.PropertyField(AssetBundleStatus, true);
                EditorGUILayout.PropertyField(AssetBundleController, true);
                EditorGUILayout.PropertyField(usingDataRequest, true);
                EditorGUILayout.PropertyField(DataRequest, true);
                EditorGUILayout.PropertyField(usingLoadSettings, true);
                EditorGUILayout.PropertyField(DirectoryName, true);
                EditorGUILayout.PropertyField(FileName, true);
                EditorGUILayout.PropertyField(DataValue, true);
                EditorGUILayout.PropertyField(usingInstantiateSettings, true);
                EditorGUILayout.PropertyField(InstantiateType, true);
                EditorGUILayout.PropertyField(ActivePrefab, true);
                EditorGUILayout.PropertyField(ActivePointer, true);
                EditorGUILayout.PropertyField(ActiveOffset, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}