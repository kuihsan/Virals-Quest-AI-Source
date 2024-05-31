using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(AIAnimationController)), CanEditMultipleObjects]
    public class AIAnimationControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            AIController,
            AIPosition,
            AIAnimator,
            usingMovingState,
            MovingState3D,
            usingActionState,
            ActionState3D,
            usingShutdownState,
            ShutdownState3D,
            usingColliderState,
            ColliderState,
            DisabledController
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetHealth = serializedObject.FindProperty("TargetHealth");
            AIController = serializedObject.FindProperty("AIController");
            AIPosition = serializedObject.FindProperty("AIPosition");
            AIAnimator = serializedObject.FindProperty("AIAnimator");

            usingMovingState = serializedObject.FindProperty("usingMovingState");
            MovingState3D = serializedObject.FindProperty("MovingState3D");
            usingActionState = serializedObject.FindProperty("usingActionState");
            ActionState3D = serializedObject.FindProperty("ActionState3D");
            usingShutdownState = serializedObject.FindProperty("usingShutdownState");
            ShutdownState3D = serializedObject.FindProperty("ShutdownState3D");
            usingColliderState = serializedObject.FindProperty("usingColliderState");
            ColliderState = serializedObject.FindProperty("ColliderState");
            DisabledController = serializedObject.FindProperty("DisabledController");

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

                EditorGUILayout.PropertyField(AIController, true);
                if (AIController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(AIPosition, true);
                if (AIPosition.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(AIAnimator, true);
                if (AIAnimator.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }


                EditorGUILayout.PropertyField(usingMovingState, true);
                bool check1 = usingMovingState.boolValue;
                if (check1)
                {
                    EditorGUILayout.PropertyField(MovingState3D, true);
                }

                EditorGUILayout.PropertyField(usingActionState, true);
                bool check2 = usingActionState.boolValue;
                if (check2)
                {
                    EditorGUILayout.PropertyField(ActionState3D, true);
                }

                EditorGUILayout.PropertyField(usingShutdownState, true);
                bool check3 = usingShutdownState.boolValue;
                if (check3)
                {
                    EditorGUILayout.PropertyField(ShutdownState3D, true);
                }

                EditorGUILayout.PropertyField(usingColliderState, true);
                bool check4 = usingColliderState.boolValue;
                if (check4)
                {
                    EditorGUILayout.PropertyField(ColliderState, true);
                }

                EditorGUILayout.PropertyField(DisabledController, true);
                if (DisabledController.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                }

            }
            else
            {
                EditorGUILayout.HelpBox("Function Status: Ignored", MessageType.Warning);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}