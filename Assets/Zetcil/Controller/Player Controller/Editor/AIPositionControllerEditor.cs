using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(AIPositionController)), CanEditMultipleObjects]
    public class AIPositionControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            AIType,
            AIController,
            MovementSetting,
            usingMeleeAttack,
            MeleeAttack,
            usingRangedAttack,
            RangedAttack,
            usingTargetPatrol,
            PatrolSettings,
            usingTargetFollow,
            FollowSettings,
            usingTargetRetreat,
            RetreatSettings,
            isMeleeAttack,
            isRangedAttack,
            isChasing,
            isFollow,
            isRetreat,
            isPatrol,
            isGrounded,
            VectorDistance
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetHealth = serializedObject.FindProperty("TargetHealth");
            AIType = serializedObject.FindProperty("AIType");
            AIController = serializedObject.FindProperty("AIController");
            MovementSetting = serializedObject.FindProperty("MovementSetting");

            usingMeleeAttack = serializedObject.FindProperty("usingMeleeAttack");
            MeleeAttack = serializedObject.FindProperty("MeleeAttack");
            usingRangedAttack = serializedObject.FindProperty("usingRangedAttack");
            RangedAttack = serializedObject.FindProperty("RangedAttack");
            usingTargetPatrol = serializedObject.FindProperty("usingTargetPatrol");
            PatrolSettings = serializedObject.FindProperty("PatrolSettings");
            usingTargetFollow = serializedObject.FindProperty("usingTargetFollow");
            FollowSettings = serializedObject.FindProperty("FollowSettings");
            usingTargetRetreat = serializedObject.FindProperty("usingTargetRetreat");
            RetreatSettings = serializedObject.FindProperty("RetreatSettings");

            isMeleeAttack = serializedObject.FindProperty("isMeleeAttack");
            isRangedAttack = serializedObject.FindProperty("isRangedAttack");
            isChasing = serializedObject.FindProperty("isChasing");
            isFollow  = serializedObject.FindProperty("isFollow");
            isRetreat = serializedObject.FindProperty("isRetreat");
            isPatrol = serializedObject.FindProperty("isPatrol");
            isGrounded = serializedObject.FindProperty("isGrounded");
            VectorDistance = serializedObject.FindProperty("VectorDistance");
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            bool check = isEnabled.boolValue;

            if (check)
            {

                EditorGUILayout.PropertyField(TargetHealth, true);
                if (TargetHealth.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }


                EditorGUILayout.PropertyField(AIType, true);
                EditorGUILayout.PropertyField(AIController, true);
                if (AIController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(usingMeleeAttack, true);
                if (usingMeleeAttack.boolValue) {
                    EditorGUILayout.PropertyField(MeleeAttack, true);
                }
                EditorGUILayout.PropertyField(usingRangedAttack, true);
                if (usingRangedAttack.boolValue)
                {
                    EditorGUILayout.PropertyField(RangedAttack, true);
                }
                EditorGUILayout.PropertyField(usingTargetPatrol, true);
                if (usingTargetPatrol.boolValue)
                {
                    EditorGUILayout.PropertyField(PatrolSettings, true);
                }

                EditorGUILayout.PropertyField(usingTargetFollow, true);
                if (usingTargetFollow.boolValue)
                {
                    EditorGUILayout.PropertyField(FollowSettings, true);
                }
                EditorGUILayout.PropertyField(usingTargetRetreat, true);
                if (usingTargetRetreat.boolValue)
                {
                    EditorGUILayout.PropertyField(RetreatSettings, true);
                }

                EditorGUILayout.PropertyField(MovementSetting, true);

                EditorGUILayout.PropertyField(isMeleeAttack, true);
                EditorGUILayout.PropertyField(isRangedAttack, true);
                EditorGUILayout.PropertyField(isChasing, true);
                EditorGUILayout.PropertyField(isFollow, true);
                EditorGUILayout.PropertyField(isRetreat, true);
                EditorGUILayout.PropertyField(isPatrol, true);
                EditorGUILayout.PropertyField(isGrounded, true);
                EditorGUILayout.PropertyField(VectorDistance, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Function Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}