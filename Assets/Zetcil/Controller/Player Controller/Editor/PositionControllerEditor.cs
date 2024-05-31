using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(PositionController)), CanEditMultipleObjects]
    public class PositionControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            TargetHealth,
            TargetController,
            TargetCamera,
            MainCameraTag,
            KeyboardStyle,
            InputType,
            MouseStyle,

            ObjectSelection,
            ObjectName,
            ObjectTag,
            SelectionStatus,
            ClearSelection,
            ClickSelection,
            ClickTrigger,

            EnemyTag,
            usingAutoAttack,
            SearchingRange,
            AttackRange,
            TargetEnemy,
            TargetDistance,

            RaycastTag,
            RaycastName,
            RaycastObject,

            DownStatus,
            CurrentDistance,
            VectorStyle,
            TargetKeyboardController,
            TargetVector,
            PrimaryKeyboardKey,
            AltKeyboardKey,
            SecondaryKeyboardKey,
            AltSecondaryKeyboardKey,
            touchSensitivity,
            moveSensitivity,
            rotateSensitivity,
            usingRotateCameraRight,
            isFlipDirection,
            HorizontalCommand,
            VerticalCommand,
            SpecialCommand,
            MoveSpeed,
            AlterSpeed,
            RotateSpeed,
            TurnSpeed,
            jumpSpeed,
            maxJump,
            gravity,
            StopDistance,
            currentJump,
            followDirection,
            usingSyncronizeController,
            SyncronizePosition,
            SyncronizeRotation,
            usingAdditionalSettings,
            AdditionalSettings,
            isAttack,
            isMoving

            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isAttack = serializedObject.FindProperty("isAttack");
            isMoving = serializedObject.FindProperty("isMoving");

            isEnabled = serializedObject.FindProperty("isEnabled");
            TargetHealth = serializedObject.FindProperty("TargetHealth");

            usingSyncronizeController = serializedObject.FindProperty("usingSyncronizeController");
            SyncronizePosition = serializedObject.FindProperty("SyncronizePosition");
            SyncronizeRotation = serializedObject.FindProperty("SyncronizeRotation");

            usingAdditionalSettings = serializedObject.FindProperty("usingAdditionalSettings");
            AdditionalSettings = serializedObject.FindProperty("AdditionalSettings");

            TargetKeyboardController = serializedObject.FindProperty("TargetKeyboardController");
            TargetController = serializedObject.FindProperty("TargetController");
            TargetCamera = serializedObject.FindProperty("TargetCamera");
            MainCameraTag = serializedObject.FindProperty("MainCameraTag");
            InputType = serializedObject.FindProperty("InputType");

            KeyboardStyle = serializedObject.FindProperty("KeyboardStyle");
            MouseStyle = serializedObject.FindProperty("MouseStyle");

            ObjectSelection = serializedObject.FindProperty("ObjectSelection");
            ObjectName = serializedObject.FindProperty("ObjectName");
            ObjectTag = serializedObject.FindProperty("ObjectTag");

            EnemyTag = serializedObject.FindProperty("EnemyTag");
            TargetEnemy = serializedObject.FindProperty("TargetEnemy");
            TargetDistance = serializedObject.FindProperty("TargetDistance");

            usingAutoAttack = serializedObject.FindProperty("usingAutoAttack");
            SearchingRange = serializedObject.FindProperty("SearchingRange");
            AttackRange = serializedObject.FindProperty("AttackRange");

            SelectionStatus = serializedObject.FindProperty("SelectionStatus");
            ClearSelection = serializedObject.FindProperty("ClearSelection");
            ClickSelection = serializedObject.FindProperty("ClickSelection");
            ClickTrigger = serializedObject.FindProperty("ClickTrigger");

            PrimaryKeyboardKey = serializedObject.FindProperty("PrimaryKeyboardKey");
            AltKeyboardKey = serializedObject.FindProperty("AltKeyboardKey");
            SecondaryKeyboardKey = serializedObject.FindProperty("SecondaryKeyboardKey");
            AltSecondaryKeyboardKey = serializedObject.FindProperty("AltSecondaryKeyboardKey");

            VectorStyle = serializedObject.FindProperty("VectorStyle");
            TargetVector = serializedObject.FindProperty("TargetVector");

            HorizontalCommand = serializedObject.FindProperty("HorizontalCommand");
            VerticalCommand = serializedObject.FindProperty("VerticalCommand");
            SpecialCommand = serializedObject.FindProperty("SpecialCommand");

            RaycastTag = serializedObject.FindProperty("RaycastTag");
            RaycastName = serializedObject.FindProperty("RaycastName");
            RaycastObject = serializedObject.FindProperty("RaycastObject");

            DownStatus = serializedObject.FindProperty("DownStatus");
            CurrentDistance = serializedObject.FindProperty("CurrentDistance");

            touchSensitivity = serializedObject.FindProperty("touchSensitivity");
            moveSensitivity = serializedObject.FindProperty("moveSensitivity");
            rotateSensitivity = serializedObject.FindProperty("rotateSensitivity");
            isFlipDirection = serializedObject.FindProperty("isFlipDirection");
            usingRotateCameraRight = serializedObject.FindProperty("usingRotateCameraRight");
            

            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            AlterSpeed = serializedObject.FindProperty("AlterSpeed");
            RotateSpeed = serializedObject.FindProperty("RotateSpeed");
            TurnSpeed = serializedObject.FindProperty("TurnSpeed");
            jumpSpeed = serializedObject.FindProperty("jumpSpeed");
            maxJump = serializedObject.FindProperty("maxJump");
            currentJump = serializedObject.FindProperty("currentJump");
            gravity = serializedObject.FindProperty("gravity");
            StopDistance = serializedObject.FindProperty("StopDistance");
            followDirection = serializedObject.FindProperty("followDirection");
        }

        void MovementEvent() {

            EditorGUILayout.PropertyField(MoveSpeed, true);
            EditorGUILayout.PropertyField(AlterSpeed, true);
            EditorGUILayout.PropertyField(RotateSpeed, true);
            EditorGUILayout.PropertyField(TurnSpeed, true);
            EditorGUILayout.PropertyField(StopDistance, true);
            EditorGUILayout.PropertyField(isAttack, true);
            EditorGUILayout.PropertyField(isMoving, true);

            EditorGUILayout.PropertyField(jumpSpeed, true);
            EditorGUILayout.PropertyField(maxJump, true);
            EditorGUILayout.PropertyField(gravity, true);
            EditorGUILayout.PropertyField(currentJump, true);
            EditorGUILayout.PropertyField(followDirection, true);
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
                PositionController.CInputType st = (PositionController.CInputType)InputType.enumValueIndex;

                switch (st)
                {
                    case PositionController.CInputType.None:

                    break;

                    case PositionController.CInputType.Keyboard:
                        PositionController.CKeyboardStyle ms = (PositionController.CKeyboardStyle)KeyboardStyle.enumValueIndex;
                        EditorGUILayout.PropertyField(TargetKeyboardController);
                        if (TargetKeyboardController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }


                        switch (ms)
                        {
                            case PositionController.CKeyboardStyle.StyleMMO:
                                //EditorGUILayout.PropertyField(TargetCamera, true);
                                break;
                            case PositionController.CKeyboardStyle.StyleFPS:
                                //EditorGUILayout.PropertyField(TargetCamera, true);
                                break;
                        }

                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(MainCameraTag, true);
                        EditorGUILayout.PropertyField(KeyboardStyle);
                        switch (ms)
                        {
                            case PositionController.CKeyboardStyle.StyleADV:
                                EditorGUILayout.HelpBox("Action/Adventure", MessageType.Info);
                                break;
                            case PositionController.CKeyboardStyle.StyleFPS:
                                EditorGUILayout.HelpBox("First Person Shooter", MessageType.Info);
                                break;
                            case PositionController.CKeyboardStyle.StyleMMO:
                                EditorGUILayout.HelpBox("Multiplayer Moba Online", MessageType.Info);
                                break;
                            case PositionController.CKeyboardStyle.StyleSSP:
                                EditorGUILayout.HelpBox("Side Scrolling/Platformer", MessageType.Info);
                                break;
                            case PositionController.CKeyboardStyle.StyleRPG:
                                EditorGUILayout.HelpBox("Role-Playing Game", MessageType.Info);
                                break;
                            case PositionController.CKeyboardStyle.StyleJRPG:
                                EditorGUILayout.HelpBox("Japan Role-Playing Game", MessageType.Info);
                                break;
                        }

                        MovementEvent();

                        break;

                    case PositionController.CInputType.Mouse:
                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(ObjectSelection);

                        PositionController.CFilterSelection cfilter = (PositionController.CFilterSelection) ObjectSelection.enumValueIndex;
                        if (cfilter == PositionController.CFilterSelection.Everything)
                        {

                        }
                        if (cfilter == PositionController.CFilterSelection.ByName)
                        {
                            EditorGUILayout.PropertyField(ObjectName, true);
                        }
                        if (cfilter == PositionController.CFilterSelection.ByTag)
                        {
                            EditorGUILayout.PropertyField(ObjectTag, true);
                        }

                        EditorGUILayout.PropertyField(SelectionStatus, true);
                        if (SelectionStatus.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }


                        EditorGUILayout.PropertyField(MouseStyle, true);
                        EditorGUILayout.PropertyField(MainCameraTag, true);

                        EditorGUILayout.PropertyField(ClearSelection, true);
                        EditorGUILayout.PropertyField(ClickSelection, true);
                        EditorGUILayout.PropertyField(ClickTrigger, true);

                        PositionController.CMouseStyle msy = (PositionController.CMouseStyle)MouseStyle.enumValueIndex;

                        switch (msy)
                        {
                            case PositionController.CMouseStyle.StyleRTS:
                                EditorGUILayout.PropertyField(SelectionStatus, true);
                                break;
                        }


                        MovementEvent();

                        EditorGUILayout.PropertyField(TargetEnemy, true);
                        if (TargetEnemy.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }


                        EditorGUILayout.PropertyField(TargetDistance, true);
                        EditorGUILayout.PropertyField(AttackRange, true);
                        EditorGUILayout.PropertyField(EnemyTag, true);

                        EditorGUILayout.PropertyField(usingAutoAttack, true);
                        EditorGUILayout.PropertyField(SearchingRange, true);

                        EditorGUILayout.PropertyField(RaycastTag, true);
                        EditorGUILayout.PropertyField(RaycastName, true);
                        EditorGUILayout.PropertyField(RaycastObject, true);

                        EditorGUILayout.PropertyField(DownStatus, true);
                        EditorGUILayout.PropertyField(CurrentDistance, true);

                        break;

                    case PositionController.CInputType.Vector:
                        EditorGUILayout.PropertyField(TargetVector);
                        if (TargetVector.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(VectorStyle);
                        EditorGUILayout.PropertyField(MainCameraTag, true);

                        //New Vector Configuration
                        //EditorGUILayout.PropertyField(touchSensitivity);
                        //EditorGUILayout.PropertyField(moveSensitivity);
                        //EditorGUILayout.PropertyField(rotateSensitivity);
                        //EditorGUILayout.PropertyField(isFlipDirection);
                        //EditorGUILayout.PropertyField(usingRotateCameraRight);

                        MovementEvent();

                        break;

                    case PositionController.CInputType.Command:
                        EditorGUILayout.PropertyField(HorizontalCommand, true);
                        if (HorizontalCommand.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(VerticalCommand, true);
                        if (VerticalCommand.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        EditorGUILayout.PropertyField(SpecialCommand, true);
                        if (SpecialCommand.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        EditorGUILayout.PropertyField(TargetController);
                        if (TargetController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }
                        PositionController.CKeyboardStyle cms = (PositionController.CKeyboardStyle)KeyboardStyle.enumValueIndex;
                        EditorGUILayout.PropertyField(TargetKeyboardController);
                        if (TargetKeyboardController.objectReferenceValue == null)
                        {
                            EditorGUILayout.HelpBox("Required Field(s) Null/None", MessageType.Error);
                        }

                        switch (cms)
                        {
                            case PositionController.CKeyboardStyle.StyleMMO:
                                //EditorGUILayout.PropertyField(TargetCamera, true);
                                break;
                            case PositionController.CKeyboardStyle.StyleFPS:
                                //EditorGUILayout.PropertyField(TargetCamera, true);
                                break;
                        }

                        EditorGUILayout.PropertyField(KeyboardStyle);
                        EditorGUILayout.PropertyField(MainCameraTag, true);

                        MovementEvent();

                        break;

                }

                EditorGUILayout.PropertyField(usingSyncronizeController, true);
                EditorGUILayout.PropertyField(SyncronizePosition, true);
                EditorGUILayout.PropertyField(SyncronizeRotation, true);

                EditorGUILayout.PropertyField(usingAdditionalSettings, true);
                EditorGUILayout.PropertyField(AdditionalSettings, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Function Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}