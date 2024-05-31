using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SpriteAnimationController)), CanEditMultipleObjects]
    public class SpriteAnimationControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            InputType,
            TargetController,
            TargetAnimator,
            TargetVector,
            MousePositionController,

            usingMovingKeyboardState2D,
            MovingKeyboardState2D,
            usingActionKeyboardState2D,
            ActionKeyboardState2D,
            usingShutdownKeyboardState2D,
            ShutdownKeyboardState2D,

            usingMovingMouseState2D,
            MovingMouseState2D,
            usingActionMouseState2D,
            ActionMouseState2D,
            usingShutdownMouseState2D,
            ShutdownMouseState2D,

            usingMovingVectorState2D,
            MovingVectorState2D,
            usingActionVectorState2D,
            ActionVectorState2D,
            usingShutdownVectorState2D,
            ShutdownVectorState2D,

            usingMovingCommandState2D,
            MovingCommandState2D,
            usingActionCommandState2D,
            ActionCommandState2D,
            usingShutdownCommandState2D,
            ShutdownCommandState2D,

            isVectorActive,
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
            TargetVector = serializedObject.FindProperty("TargetVector");
            MousePositionController = serializedObject.FindProperty("MousePositionController");

            usingMovingKeyboardState2D = serializedObject.FindProperty("usingMovingKeyboardState2D");
            MovingKeyboardState2D = serializedObject.FindProperty("MovingKeyboardState2D");
            usingMovingMouseState2D = serializedObject.FindProperty("usingMovingMouseState2D");
            MovingMouseState2D = serializedObject.FindProperty("MovingMouseState2D");
            isVectorActive = serializedObject.FindProperty("isVectorActive");
            usingMovingVectorState2D = serializedObject.FindProperty("usingMovingVectorState2D");
            MovingVectorState2D = serializedObject.FindProperty("MovingVectorState2D");

            usingActionKeyboardState2D = serializedObject.FindProperty("usingActionKeyboardState2D");
            ActionKeyboardState2D = serializedObject.FindProperty("ActionKeyboardState2D");
            usingActionMouseState2D = serializedObject.FindProperty("usingActionMouseState2D");
            ActionMouseState2D = serializedObject.FindProperty("ActionMouseState2D");
            usingActionVectorState2D = serializedObject.FindProperty("usingActionVectorState2D");
            ActionVectorState2D = serializedObject.FindProperty("ActionVectorState2D");

            usingShutdownKeyboardState2D = serializedObject.FindProperty("usingShutdownKeyboardState2D");
            ShutdownKeyboardState2D = serializedObject.FindProperty("ShutdownKeyboardState2D");
            usingShutdownMouseState2D = serializedObject.FindProperty("usingShutdownMouseState2D");
            ShutdownMouseState2D = serializedObject.FindProperty("ShutdownMouseState2D");
            usingShutdownVectorState2D = serializedObject.FindProperty("usingShutdownVectorState2D");
            ShutdownVectorState2D = serializedObject.FindProperty("ShutdownVectorState2D");

            usingMovingCommandState2D = serializedObject.FindProperty("usingMovingCommandState2D");
            MovingCommandState2D = serializedObject.FindProperty("MovingCommandState2D");
            usingActionCommandState2D = serializedObject.FindProperty("usingActionCommandState2D");
            ActionCommandState2D = serializedObject.FindProperty("ActionCommandState2D");
            usingShutdownCommandState2D = serializedObject.FindProperty("usingShutdownCommandState2D");
            ShutdownCommandState2D = serializedObject.FindProperty("ShutdownCommandState2D");

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

                SpriteAnimationController.CInputType animtype = (SpriteAnimationController.CInputType)InputType.enumValueIndex;

                switch (animtype)
                {
                    case SpriteAnimationController.CInputType.None:

                        break;
                    case SpriteAnimationController.CInputType.Keyboard:
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

                        EditorGUILayout.PropertyField(usingMovingKeyboardState2D, true);
                        bool check1 = usingMovingKeyboardState2D.boolValue;
                        if (check1)
                        {
                            EditorGUILayout.PropertyField(MovingKeyboardState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionKeyboardState2D, true);
                        bool check2 = usingActionKeyboardState2D.boolValue;
                        if (check2)
                        {
                            EditorGUILayout.PropertyField(ActionKeyboardState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownKeyboardState2D, true);
                        bool check3 = usingShutdownKeyboardState2D.boolValue;
                        if (check3)
                        {
                            EditorGUILayout.PropertyField(ShutdownKeyboardState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool check4 = usingColliderState.boolValue;
                        if (check4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }
                        break;

                    case SpriteAnimationController.CInputType.Mouse:
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

                        EditorGUILayout.PropertyField(usingMovingMouseState2D, true);
                        bool mcheck1 = usingMovingMouseState2D.boolValue;
                        if (mcheck1)
                        {
                            EditorGUILayout.PropertyField(MovingMouseState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionMouseState2D, true);
                        bool mcheck2 = usingActionMouseState2D.boolValue;
                        if (mcheck2)
                        {
                            EditorGUILayout.PropertyField(ActionMouseState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownMouseState2D, true);
                        bool mcheck3 = usingShutdownMouseState2D.boolValue;
                        if (mcheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownMouseState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool mcheck4 = usingColliderState.boolValue;
                        if (mcheck4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }
                        break;

                    case SpriteAnimationController.CInputType.Vector:
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

                        EditorGUILayout.PropertyField(usingMovingVectorState2D, true);
                        bool tcheck1 = usingMovingVectorState2D.boolValue;
                        if (tcheck1)
                        {
                            EditorGUILayout.PropertyField(MovingVectorState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionVectorState2D, true);
                        bool tcheck2 = usingActionVectorState2D.boolValue;
                        if (tcheck2)
                        {
                            EditorGUILayout.PropertyField(ActionVectorState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownVectorState2D, true);
                        bool tcheck3 = usingShutdownVectorState2D.boolValue;
                        if (tcheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownVectorState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingColliderState, true);
                        bool tcheck4 = usingColliderState.boolValue;
                        if (tcheck4)
                        {
                            EditorGUILayout.PropertyField(ColliderState, true);
                        }

                        break;
                    case SpriteAnimationController.CInputType.Command:
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

                        EditorGUILayout.PropertyField(usingMovingCommandState2D, true);
                        bool com1 = usingMovingCommandState2D.boolValue;
                        if (com1)
                        {
                            EditorGUILayout.PropertyField(MovingCommandState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingMovingKeyboardState2D, true);
                        bool ccheck1 = usingMovingKeyboardState2D.boolValue;
                        if (ccheck1)
                        {
                            EditorGUILayout.PropertyField(MovingKeyboardState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionCommandState2D, true);
                        bool com2 = usingActionCommandState2D.boolValue;
                        if (com2)
                        {
                            EditorGUILayout.PropertyField(ActionCommandState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingActionKeyboardState2D, true);
                        bool ccheck2 = usingActionKeyboardState2D.boolValue;
                        if (ccheck2)
                        {
                            EditorGUILayout.PropertyField(ActionKeyboardState2D, true);
                        }

                        EditorGUILayout.PropertyField(usingShutdownCommandState2D, true);
                        bool com3 = usingShutdownCommandState2D.boolValue;
                        if (com3)
                        {
                            EditorGUILayout.PropertyField(ShutdownCommandState2D, true);
                        }


                        EditorGUILayout.PropertyField(usingShutdownKeyboardState2D, true);
                        bool ccheck3 = usingShutdownKeyboardState2D.boolValue;
                        if (ccheck3)
                        {
                            EditorGUILayout.PropertyField(ShutdownKeyboardState2D, true);
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