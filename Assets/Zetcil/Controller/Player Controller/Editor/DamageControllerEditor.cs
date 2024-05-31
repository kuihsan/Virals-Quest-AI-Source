using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(DamageController)), CanEditMultipleObjects]
    public class DamageControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            DamagerType,
            CurrentPositionController,
            CurrentAnimationController,
            CurrentDamageController,
            DamagerRigidbody,
            DamageSender,
            DamageReceiver,
            usingAction,
            ActionType,
            ActionAnimator,
            ActionAnimationName,
            ActionEventGroup,
            usingShutdown,
            TriggerOperator,
            TriggerReceived,
            TriggerTag
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            DamagerType = serializedObject.FindProperty("DamagerType");
            CurrentPositionController = serializedObject.FindProperty("CurrentPositionController");
            CurrentAnimationController = serializedObject.FindProperty("CurrentAnimationController");
            CurrentDamageController = serializedObject.FindProperty("CurrentDamageController");
            DamagerRigidbody = serializedObject.FindProperty("DamagerRigidbody");
            DamageSender = serializedObject.FindProperty("DamageSender");
            DamageReceiver = serializedObject.FindProperty("DamageReceiver");

            usingAction = serializedObject.FindProperty("usingAction");
            ActionType = serializedObject.FindProperty("ActionType");
            ActionAnimator = serializedObject.FindProperty("ActionAnimator");
            ActionAnimationName = serializedObject.FindProperty("ActionAnimationName");
            ActionEventGroup = serializedObject.FindProperty("ActionEventGroup");

            TriggerOperator = serializedObject.FindProperty("TriggerOperator");
            TriggerReceived = serializedObject.FindProperty("TriggerReceived");
            TriggerTag = serializedObject.FindProperty("TriggerTag");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            bool check = isEnabled.boolValue;

            if (check)
            {

                EditorGUILayout.PropertyField(DamagerType, true);

                DamageController.CDamageType st = (DamageController.CDamageType) DamagerType.enumValueIndex;

                switch (st)
                {
                    case DamageController.CDamageType.DamageSender:

                        EditorGUILayout.PropertyField(CurrentDamageController, new GUIContent("Damage Controller"));
                        EditorGUILayout.PropertyField(DamagerRigidbody, true);
                        EditorGUILayout.PropertyField(DamageSender, true);
                        break;

                    case DamageController.CDamageType.DamageReceiver:

                        EditorGUILayout.PropertyField(CurrentDamageController, new GUIContent("Damage Controller"));
                        EditorGUILayout.PropertyField(DamagerRigidbody, true);
                        EditorGUILayout.PropertyField(DamageReceiver, true);

                        EditorGUILayout.PropertyField(usingAction, true);
                        if (usingAction.boolValue)
                        {
                            EditorGUILayout.PropertyField(ActionType, true);
                            DamageController.CActionType actionType = (DamageController.CActionType) ActionType.enumValueIndex;
                            switch (actionType)
                            {
                                case DamageController.CActionType.ActionAnimation:
                                    EditorGUILayout.PropertyField(ActionAnimator, true);
                                    EditorGUILayout.PropertyField(ActionAnimationName, true);
                                    break;
                                case DamageController.CActionType.ActionEvent:
                                    EditorGUILayout.PropertyField(ActionAnimator, true);
                                    EditorGUILayout.PropertyField(ActionEventGroup, true);
                                    break;
                            }
                        }


                        break;

                    case DamageController.CDamageType.AIDamageReceiver:

                        EditorGUILayout.PropertyField(CurrentDamageController, new GUIContent("Damage Controller"));
                        EditorGUILayout.PropertyField(DamagerRigidbody, true);
                        EditorGUILayout.PropertyField(DamageReceiver, true);

                        EditorGUILayout.PropertyField(usingAction, true);
                        if (usingAction.boolValue)
                        {
                            EditorGUILayout.PropertyField(ActionType, true);
                            DamageController.CActionType actionType = (DamageController.CActionType)ActionType.enumValueIndex;
                            switch (actionType)
                            {
                                case DamageController.CActionType.ActionAnimation:
                                    EditorGUILayout.PropertyField(ActionAnimator, true);
                                    EditorGUILayout.PropertyField(ActionAnimationName, true);
                                    break;
                                case DamageController.CActionType.ActionEvent:
                                    EditorGUILayout.PropertyField(ActionAnimator, true);
                                    EditorGUILayout.PropertyField(ActionEventGroup, true);
                                    break;
                            }
                        }


                        break;
                }

                EditorGUILayout.PropertyField(TriggerTag, true);
                EditorGUILayout.PropertyField(TriggerOperator, true);
                EditorGUILayout.PropertyField(TriggerReceived, true);

            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}