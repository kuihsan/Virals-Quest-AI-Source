using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(SpritePositionController)), CanEditMultipleObjects]
    public class SpritePositionControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            InputType,
            TargetKeyboardController,
            PrimaryKeyboardKey,
            AltKeyboardKey,
            SecondaryKeyboardKey,
            AltSecondaryKeyboardKey,
            ObjectSelection,
            ObjectName,
            ObjectTag,
            SelectionStatus,
            ClearSelection,
            ClickSelection,
            ClickTrigger,
            ScreenPosition,
            SpritePosition,
            TargetController,
            TargetSprite,
            KeyboardStyle,
            MouseStyle,
            VectorStyle,
            TargetVector,
            CrouchSpeed,
            WalkSpeed,
            RunSpeed,
            MoveSpeed,
            ConstantSpeed,
            StopDistance,
            MouseFaceDirection,
            FlipX,
            FlipY,
            JumpSpeed,
            MaxJump,
            CurrentJump,
            SpriteCollider,
            GroundTag,
            GroundLayer,
            GroundCheck,
            CeilingCheck,
            GroundedRadius,
            GroundedFilter,
            GroundedStatus,
            TargetCamera,
            MainCameraTag,
            RaycastTag,
            RaycastName,
            RaycastObject,
            DownStatus,
            CurrentDistance
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetHealth = serializedObject.FindProperty("TargetHealth");

            InputType = serializedObject.FindProperty("InputType");

            TargetKeyboardController = serializedObject.FindProperty("TargetKeyboardController");
            PrimaryKeyboardKey = serializedObject.FindProperty("PrimaryKeyboardKey");
            AltKeyboardKey = serializedObject.FindProperty("AltKeyboardKey");
            SecondaryKeyboardKey = serializedObject.FindProperty("SecondaryKeyboardKey");
            AltSecondaryKeyboardKey = serializedObject.FindProperty("AltSecondaryKeyboardKey");

            StopDistance = serializedObject.FindProperty("StopDistance");

            ObjectSelection = serializedObject.FindProperty("ObjectSelection");
            ObjectName = serializedObject.FindProperty("ObjectName");
            ObjectTag = serializedObject.FindProperty("ObjectTag");
            SelectionStatus = serializedObject.FindProperty("SelectionStatus");
            ClearSelection = serializedObject.FindProperty("ClearSelection");
            ClickSelection = serializedObject.FindProperty("ClickSelection");
            ClickTrigger = serializedObject.FindProperty("ClickTrigger");
            ScreenPosition = serializedObject.FindProperty("ScreenPosition");
            SpritePosition = serializedObject.FindProperty("SpritePosition");

            TargetController = serializedObject.FindProperty("TargetController");
            TargetSprite = serializedObject.FindProperty("TargetSprite");

            KeyboardStyle = serializedObject.FindProperty("KeyboardStyle");
            MouseStyle = serializedObject.FindProperty("MouseStyle");
            VectorStyle = serializedObject.FindProperty("VectorStyle");
            TargetVector = serializedObject.FindProperty("TargetVector");

            CrouchSpeed = serializedObject.FindProperty("CrouchSpeed");
            WalkSpeed = serializedObject.FindProperty("WalkSpeed");
            RunSpeed = serializedObject.FindProperty("RunSpeed");
            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            ConstantSpeed = serializedObject.FindProperty("ConstantSpeed");
            MouseFaceDirection = serializedObject.FindProperty("MouseFaceDirection");

            FlipX = serializedObject.FindProperty("FlipX");
            FlipY = serializedObject.FindProperty("FlipY");

            JumpSpeed = serializedObject.FindProperty("JumpSpeed");
            MaxJump = serializedObject.FindProperty("MaxJump");
            CurrentJump = serializedObject.FindProperty("CurrentJump");

            SpriteCollider = serializedObject.FindProperty("SpriteCollider");

            GroundTag = serializedObject.FindProperty("GroundTag");
            GroundLayer = serializedObject.FindProperty("GroundLayer");
            GroundCheck = serializedObject.FindProperty("GroundCheck");
            CeilingCheck = serializedObject.FindProperty("CeilingCheck");
            GroundedRadius = serializedObject.FindProperty("GroundedRadius");
            GroundedStatus = serializedObject.FindProperty("GroundedStatus");
            GroundedFilter = serializedObject.FindProperty("GroundedFilter");

            TargetCamera = serializedObject.FindProperty("TargetCamera");
            MainCameraTag = serializedObject.FindProperty("MainCameraTag");

            RaycastTag = serializedObject.FindProperty("RaycastTag");
            RaycastName = serializedObject.FindProperty("RaycastName");
            RaycastObject = serializedObject.FindProperty("RaycastObject");
            DownStatus = serializedObject.FindProperty("DownStatus");
            CurrentDistance = serializedObject.FindProperty("CurrentDistance");
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

                SpritePositionController.CInputType st = (SpritePositionController.CInputType)InputType.enumValueIndex;

                switch (st)
                {
                    case SpritePositionController.CInputType.None:

                        break;

                    case SpritePositionController.CInputType.Keyboard:
                        SpritePositionController.CKeyboardStyle ms = (SpritePositionController.CKeyboardStyle)KeyboardStyle.enumValueIndex;
                        EditorGUILayout.PropertyField(TargetKeyboardController);
                        if (TargetKeyboardController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }


                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetSprite);
                        if (TargetSprite.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(MainCameraTag, true);
                        EditorGUILayout.PropertyField(KeyboardStyle);
                        switch (ms)
                        {
                            case SpritePositionController.CKeyboardStyle.StyleSSP:
                                EditorGUILayout.HelpBox("Side Scrolling/Platformer", MessageType.Info);
                                break;
                            case SpritePositionController.CKeyboardStyle.StyleFLB:
                                EditorGUILayout.HelpBox("Flappy Bird", MessageType.Info);
                                break;
                            case SpritePositionController.CKeyboardStyle.StyleELR:
                                EditorGUILayout.HelpBox("Endless Runner", MessageType.Info);
                                break;
                            case SpritePositionController.CKeyboardStyle.StyleSTW:
                                EditorGUILayout.HelpBox("Star Wars", MessageType.Info);
                                break;
                            case SpritePositionController.CKeyboardStyle.StyleTDS:
                                EditorGUILayout.HelpBox("Top Down Shooter", MessageType.Info);
                                break;
                            case SpritePositionController.CKeyboardStyle.StyleJRPG:
                                EditorGUILayout.HelpBox("Japan Role-Playing Game", MessageType.Info);
                                break;
                        }

                        EditorGUILayout.PropertyField(ScreenPosition, true);
                        EditorGUILayout.PropertyField(SpritePosition, true);

                        EditorGUILayout.PropertyField(CrouchSpeed, true);
                        EditorGUILayout.PropertyField(WalkSpeed, true);
                        EditorGUILayout.PropertyField(RunSpeed, true);
                        EditorGUILayout.PropertyField(MoveSpeed, true);
                        EditorGUILayout.PropertyField(ConstantSpeed, true);
                        EditorGUILayout.PropertyField(StopDistance, true);

                        switch (ms)
                        {
                            case SpritePositionController.CKeyboardStyle.StyleTDS:
                                EditorGUILayout.PropertyField(MouseFaceDirection, true);
                                break;
                        }

                        EditorGUILayout.PropertyField(FlipX, true);
                        EditorGUILayout.PropertyField(FlipY, true);

                        EditorGUILayout.PropertyField(JumpSpeed, true);
                        EditorGUILayout.PropertyField(MaxJump, true);
                        EditorGUILayout.PropertyField(CurrentJump, true);

                        EditorGUILayout.PropertyField(SpriteCollider, true);

                        EditorGUILayout.PropertyField(GroundTag, true);
                        EditorGUILayout.PropertyField(GroundLayer, true);
                        EditorGUILayout.PropertyField(GroundCheck, true);
                        EditorGUILayout.PropertyField(CeilingCheck, true);
                        EditorGUILayout.PropertyField(GroundedRadius, true);
                        EditorGUILayout.PropertyField(GroundedFilter, true);
                        EditorGUILayout.PropertyField(GroundedStatus, true);


                        break;

                    case SpritePositionController.CInputType.Mouse:
                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetSprite);
                        if (TargetSprite.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetCamera, true);
                        if (TargetCamera.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(MainCameraTag, true);

                        EditorGUILayout.PropertyField(ObjectSelection);

                        SpritePositionController.CFilterSelection cfilter = (SpritePositionController.CFilterSelection)ObjectSelection.enumValueIndex;
                        if (cfilter == SpritePositionController.CFilterSelection.Everything)
                        {

                        }
                        if (cfilter == SpritePositionController.CFilterSelection.ByName)
                        {
                            EditorGUILayout.PropertyField(ObjectName, true);
                        }
                        if (cfilter == SpritePositionController.CFilterSelection.ByTag)
                        {
                            EditorGUILayout.PropertyField(ObjectTag, true);
                        }

                        EditorGUILayout.PropertyField(SelectionStatus, true);
                        if (SelectionStatus.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(ClearSelection, true);
                        EditorGUILayout.PropertyField(ClickSelection, true);
                        EditorGUILayout.PropertyField(ClickTrigger, true);
                        
                        EditorGUILayout.PropertyField(ScreenPosition, true);
                        EditorGUILayout.PropertyField(SpritePosition, true);

                        EditorGUILayout.PropertyField(MouseStyle, true);

                        EditorGUILayout.PropertyField(CrouchSpeed, true);
                        EditorGUILayout.PropertyField(WalkSpeed, true);
                        EditorGUILayout.PropertyField(RunSpeed, true);
                        EditorGUILayout.PropertyField(MoveSpeed, true);
                        EditorGUILayout.PropertyField(ConstantSpeed, true);
                        EditorGUILayout.PropertyField(StopDistance, true);
                        EditorGUILayout.PropertyField(MouseFaceDirection, true); 
                        
                        EditorGUILayout.PropertyField(FlipX, true);
                        EditorGUILayout.PropertyField(FlipY, true);

                        EditorGUILayout.PropertyField(JumpSpeed, true);
                        EditorGUILayout.PropertyField(MaxJump, true);
                        EditorGUILayout.PropertyField(CurrentJump, true);

                        EditorGUILayout.PropertyField(SpriteCollider, true);

                        EditorGUILayout.PropertyField(GroundTag, true);
                        EditorGUILayout.PropertyField(GroundLayer, true);
                        EditorGUILayout.PropertyField(GroundCheck, true);
                        EditorGUILayout.PropertyField(CeilingCheck, true);
                        EditorGUILayout.PropertyField(GroundedRadius, true);
                        EditorGUILayout.PropertyField(GroundedFilter, true);
                        EditorGUILayout.PropertyField(GroundedStatus, true);

                        EditorGUILayout.PropertyField(RaycastTag, true);
                        EditorGUILayout.PropertyField(RaycastName, true);
                        EditorGUILayout.PropertyField(RaycastObject, true);
                        EditorGUILayout.PropertyField(DownStatus, true);
                        EditorGUILayout.PropertyField(CurrentDistance, true);


                        break;

                    case SpritePositionController.CInputType.Vector:
                        EditorGUILayout.PropertyField(TargetController);
                        EditorGUILayout.PropertyField(TargetSprite);
                        EditorGUILayout.PropertyField(TargetCamera, true);
                        EditorGUILayout.PropertyField(MainCameraTag, true);

                        EditorGUILayout.PropertyField(VectorStyle);
                        EditorGUILayout.PropertyField(TargetVector);

                        EditorGUILayout.PropertyField(CrouchSpeed, true);
                        EditorGUILayout.PropertyField(WalkSpeed, true);
                        EditorGUILayout.PropertyField(RunSpeed, true);
                        EditorGUILayout.PropertyField(MoveSpeed, true);
                        EditorGUILayout.PropertyField(ConstantSpeed, true);
                        EditorGUILayout.PropertyField(MouseFaceDirection, true);

                        EditorGUILayout.PropertyField(FlipX, true);
                        EditorGUILayout.PropertyField(FlipY, true);

                        EditorGUILayout.PropertyField(JumpSpeed, true);
                        EditorGUILayout.PropertyField(MaxJump, true);
                        EditorGUILayout.PropertyField(CurrentJump, true);

                        EditorGUILayout.PropertyField(SpriteCollider, true);

                        EditorGUILayout.PropertyField(GroundTag, true);
                        EditorGUILayout.PropertyField(GroundLayer, true);
                        EditorGUILayout.PropertyField(GroundCheck, true);
                        EditorGUILayout.PropertyField(CeilingCheck, true);
                        EditorGUILayout.PropertyField(GroundedRadius, true);
                        EditorGUILayout.PropertyField(GroundedFilter, true);
                        EditorGUILayout.PropertyField(GroundedStatus, true);

                        EditorGUILayout.PropertyField(RaycastTag, true);
                        EditorGUILayout.PropertyField(RaycastName, true);
                        EditorGUILayout.PropertyField(RaycastObject, true);
                        EditorGUILayout.PropertyField(DownStatus, true);
                        EditorGUILayout.PropertyField(CurrentDistance, true);

                        break;

                }
            }
            else
            {
                EditorGUILayout.HelpBox("Function Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}