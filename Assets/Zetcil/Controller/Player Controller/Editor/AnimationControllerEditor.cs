using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(AnimationController)), CanEditMultipleObjects]
    public class AnimationControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            InputType,
            TargetController,
            TargetAnimator,
            MousePositionController,
            
            usingMovingKeyboardState3D,
            MovingKeyboardState3D,

            usingMovingMouseState3D,
            MovingMouseState3D,

            isVectorActive,
            TargetVector,
            usingMovingVectorState3D,
            MovingVectorState3D,

            usingMovingCommandState3D,
            MovingCommandState3D,
            usingActionCommandState3D,
            ActionCommandState3D,
            usingShutdownCommandState3D,
            ShutdownCommandState3D,

            usingActionKeyboardState3D,
            ActionKeyboardState3D,
            usingActionMouseState3D,
            ActionMouseState3D,
            usingActionVectorState3D,
            ActionVectorState3D,

            usingShutdownKeyboardState3D,
            ShutdownKeyboardState3D,
            usingShutdownMouseState3D,
            ShutdownMouseState3D,
            usingShutdownVectorState3D,
            ShutdownVectorState3D,

            usingColliderState,
            ColliderState
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetHealth = serializedObject.FindProperty("TargetHealth");

            InputType = serializedObject.FindProperty("InputType");
            TargetController = serializedObject.FindProperty("TargetController");
            TargetAnimator = serializedObject.FindProperty("TargetAnimator");
            MousePositionController = serializedObject.FindProperty("MousePositionController");

            usingMovingKeyboardState3D = serializedObject.FindProperty("usingMovingKeyboardState3D");
            MovingKeyboardState3D = serializedObject.FindProperty("MovingKeyboardState3D");
            usingMovingMouseState3D = serializedObject.FindProperty("usingMovingMouseState3D");
            MovingMouseState3D = serializedObject.FindProperty("MovingMouseState3D");
            isVectorActive = serializedObject.FindProperty("isVectorActive");
            TargetVector = serializedObject.FindProperty("TargetVector");
            usingMovingVectorState3D = serializedObject.FindProperty("usingMovingVectorState3D");
            MovingVectorState3D = serializedObject.FindProperty("MovingVectorState3D");

            usingActionKeyboardState3D  = serializedObject.FindProperty("usingActionKeyboardState3D");
            ActionKeyboardState3D = serializedObject.FindProperty("ActionKeyboardState3D");
            usingActionMouseState3D = serializedObject.FindProperty("usingActionMouseState3D");
            ActionMouseState3D = serializedObject.FindProperty("ActionMouseState3D");
            usingActionVectorState3D = serializedObject.FindProperty("usingActionVectorState3D");
            ActionVectorState3D = serializedObject.FindProperty("ActionVectorState3D");

            usingShutdownKeyboardState3D = serializedObject.FindProperty("usingShutdownKeyboardState3D");
            ShutdownKeyboardState3D = serializedObject.FindProperty("ShutdownKeyboardState3D");
            usingShutdownMouseState3D = serializedObject.FindProperty("usingShutdownMouseState3D");
            ShutdownMouseState3D = serializedObject.FindProperty("ShutdownMouseState3D");
            usingShutdownVectorState3D = serializedObject.FindProperty("usingShutdownVectorState3D");
            ShutdownVectorState3D = serializedObject.FindProperty("ShutdownVectorState3D");

            usingMovingCommandState3D = serializedObject.FindProperty("usingMovingCommandState3D");
            MovingCommandState3D = serializedObject.FindProperty("MovingCommandState3D");
            usingActionCommandState3D = serializedObject.FindProperty("usingActionCommandState3D");
            ActionCommandState3D = serializedObject.FindProperty("ActionCommandState3D");
            usingShutdownCommandState3D = serializedObject.FindProperty("usingShutdownCommandState3D");
            ShutdownCommandState3D = serializedObject.FindProperty("ShutdownCommandState3D");

            usingColliderState = serializedObject.FindProperty("usingColliderState");
            ColliderState = serializedObject.FindProperty("ColliderState");

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

                EditorGUILayout.PropertyField(InputType);

                AnimationController.CInputType animtype = (AnimationController.CInputType) InputType.enumValueIndex;

                switch (animtype) {
                    case AnimationController.CInputType.None:

                    break;
                    case AnimationController.CInputType.Keyboard:
                        EditorGUILayout.PropertyField(TargetController, true);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetAnimator, true);
                        if (TargetAnimator.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(usingMovingKeyboardState3D, true);
                        bool check1 = usingMovingKeyboardState3D.boolValue;
                        if (check1)
                        {
                            EditorGUILayout.PropertyField(MovingKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionKeyboardState3D , true);
                        bool check2 = usingActionKeyboardState3D .boolValue;
                        if (check2)
                        {
                            EditorGUILayout.PropertyField(ActionKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownKeyboardState3D, true);
                        bool check3 = usingShutdownKeyboardState3D.boolValue;
                        if (check3)
                        {
                            EditorGUILayout.PropertyField(ShutdownKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool check4 = usingColliderState.boolValue;
                        if (check4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }
                    break;

                    case AnimationController.CInputType.Mouse:
                        EditorGUILayout.PropertyField(TargetController, true);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(TargetAnimator, true);
                        if (TargetAnimator.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(MousePositionController, true);
                        if (MousePositionController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(usingMovingMouseState3D, true);
                        bool mcheck1 = usingMovingMouseState3D.boolValue;
                        if (mcheck1)
                        {
                            EditorGUILayout.PropertyField(MovingMouseState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionMouseState3D, true);
                        bool mcheck2 = usingActionMouseState3D.boolValue;
                        if (mcheck2)
                        {
                            EditorGUILayout.PropertyField(ActionMouseState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownMouseState3D, true);
                        bool mcheck3 = usingShutdownMouseState3D.boolValue;
                        if (mcheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownMouseState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool mcheck4 = usingColliderState.boolValue;
                        if (mcheck4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }
                        break;

                    case AnimationController.CInputType.Vector:
                        EditorGUILayout.PropertyField(TargetController, true);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(TargetAnimator, true);
                        if (TargetAnimator.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetVector, true);
                        if (TargetVector.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(usingMovingVectorState3D, true);
                        bool tcheck1 = usingMovingVectorState3D.boolValue;
                        if (tcheck1) {
                            EditorGUILayout.PropertyField(MovingVectorState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionVectorState3D, true);
                        bool tcheck2 = usingActionVectorState3D.boolValue;
                        if (tcheck2)
                        {
                            EditorGUILayout.PropertyField(ActionVectorState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownVectorState3D, true);
                        bool tcheck3 = usingShutdownVectorState3D.boolValue;
                        if (tcheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownVectorState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool tcheck4 = usingColliderState.boolValue;
                        if (tcheck4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }

                        break;
                    case AnimationController.CInputType.Command:
                        EditorGUILayout.PropertyField(TargetController, true);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(TargetAnimator, true);
                        if (TargetAnimator.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(usingMovingCommandState3D, true);
                        bool com1 = usingMovingCommandState3D.boolValue;
                        if (com1)
                        {
                            EditorGUILayout.PropertyField(MovingCommandState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingMovingKeyboardState3D, true);
                        bool ccheck1 = usingMovingKeyboardState3D.boolValue;
                        if (ccheck1)
                        {
                            EditorGUILayout.PropertyField(MovingKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionCommandState3D, true);
                        bool com2 = usingActionCommandState3D.boolValue;
                        if (com2)
                        {
                            EditorGUILayout.PropertyField(ActionCommandState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionKeyboardState3D, true);
                        bool ccheck2 = usingActionKeyboardState3D.boolValue;
                        if (ccheck2)
                        {
                            EditorGUILayout.PropertyField(ActionKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownCommandState3D, true);
                        bool com3 = usingShutdownCommandState3D.boolValue;
                        if (com3)
                        {
                            EditorGUILayout.PropertyField(ShutdownCommandState3D, true);
                        }


                        EditorGUILayout.PropertyField(usingShutdownKeyboardState3D, true);
                        bool ccheck3 = usingShutdownKeyboardState3D.boolValue;
                        if (ccheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownKeyboardState3D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool ccheck4 = usingColliderState.boolValue;
                        if (ccheck4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }

                        break;
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