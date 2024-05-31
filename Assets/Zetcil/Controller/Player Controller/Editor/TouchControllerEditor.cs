/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script editor untuk var manager
 **************************************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(TouchController)), CanEditMultipleObjects]
    public class TouchControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            usingMainCamera,
            MainCamera,
            usingCameraPan,
            PanSpeed,
            usingCameraScale,
            perspectiveZoomSpeed,
            orthoZoomSpeed,
            usingCameraRotate,
            usingSelectedObject,
            SelectedObject,
            ObjectSelection,
            ObjectName,
            ObjectTag,
            usingTapSelection,
            usingTapScale,
            TapScaleSpeed,
            usingTapRotate,
            TapRotationSpeed,
            usingTapDrag,
            TapDragSpeed,
            usingSwipe,
            swipeStatus,
            usingSwipeUpEvent,
            SwipeUpEvent,
            usingSwipeUpRightEvent,
            SwipeUpRightEvent,
            usingSwipeRightEvent,
            SwipeRightEvent,
            usingSwipeDownRightEvent,
            SwipeDownRightEvent,
            usingSwipeDownEvent,
            SwipeDownEvent,
            usingSwipeDownLeftEvent,
            SwipeDownLeftEvent,
            usingSwipeLeftEvent,
            SwipeLeftEvent,
            usingSwipeUpLeftEvent,
            SwipeUpLeftEvent,
            SelectedObjectType,
            SelectedObjectTag,
            SelectedObjectName,
            FingerTouchCount,
            BeginTouch,
            MovedTouch,
            EndedTouch
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            usingMainCamera = serializedObject.FindProperty("usingMainCamera");
            MainCamera = serializedObject.FindProperty("MainCamera");
            usingCameraPan = serializedObject.FindProperty("usingCameraPan");
            PanSpeed = serializedObject.FindProperty("PanSpeed");
            usingCameraScale = serializedObject.FindProperty("usingCameraScale");
            perspectiveZoomSpeed = serializedObject.FindProperty("perspectiveZoomSpeed");
            orthoZoomSpeed = serializedObject.FindProperty("orthoZoomSpeed");
            usingCameraRotate = serializedObject.FindProperty("usingCameraRotate");
            usingSelectedObject = serializedObject.FindProperty("usingSelectedObject");
            SelectedObject = serializedObject.FindProperty("SelectedObject");
            ObjectSelection = serializedObject.FindProperty("ObjectSelection");
            ObjectName = serializedObject.FindProperty("ObjectName");
            ObjectTag = serializedObject.FindProperty("ObjectTag");
            usingTapSelection = serializedObject.FindProperty("usingTapSelection");
            usingTapScale = serializedObject.FindProperty("usingTapScale");
            TapScaleSpeed = serializedObject.FindProperty("TapScaleSpeed");
            usingTapRotate = serializedObject.FindProperty("usingTapRotate");
            TapRotationSpeed = serializedObject.FindProperty("TapRotationSpeed");
            usingTapDrag = serializedObject.FindProperty("usingTapDrag");
            TapDragSpeed = serializedObject.FindProperty("TapDragSpeed");
            usingSwipe = serializedObject.FindProperty("usingSwipe");
            swipeStatus = serializedObject.FindProperty("swipeStatus");

            usingSwipeUpEvent = serializedObject.FindProperty("usingSwipeUpEvent");
            SwipeUpEvent = serializedObject.FindProperty("SwipeUpEvent");
            usingSwipeUpRightEvent = serializedObject.FindProperty("usingSwipeUpRightEvent");
            SwipeUpRightEvent = serializedObject.FindProperty("SwipeUpRightEvent");
            usingSwipeRightEvent = serializedObject.FindProperty("usingSwipeRightEvent");
            SwipeRightEvent = serializedObject.FindProperty("SwipeRightEvent");
            usingSwipeDownRightEvent = serializedObject.FindProperty("usingSwipeDownRightEvent");
            SwipeDownRightEvent = serializedObject.FindProperty("SwipeDownRightEvent");
            usingSwipeDownEvent = serializedObject.FindProperty("usingSwipeDownEvent");
            SwipeDownEvent = serializedObject.FindProperty("SwipeDownEvent");
            usingSwipeDownLeftEvent = serializedObject.FindProperty("usingSwipeDownLeftEvent");
            SwipeDownLeftEvent = serializedObject.FindProperty("SwipeDownLeftEvent");
            usingSwipeLeftEvent = serializedObject.FindProperty("usingSwipeLeftEvent");
            SwipeLeftEvent = serializedObject.FindProperty("SwipeLeftEvent");
            usingSwipeUpLeftEvent = serializedObject.FindProperty("usingSwipeUpLeftEvent");
            SwipeUpLeftEvent = serializedObject.FindProperty("SwipeUpLeftEvent");

            SelectedObjectType = serializedObject.FindProperty("SelectedObjectType");
            SelectedObjectTag = serializedObject.FindProperty("SelectedObjectTag");
            SelectedObjectName = serializedObject.FindProperty("SelectedObjectName");
            FingerTouchCount = serializedObject.FindProperty("FingerTouchCount");
            BeginTouch = serializedObject.FindProperty("BeginTouch");
            MovedTouch = serializedObject.FindProperty("MovedTouch");
            EndedTouch = serializedObject.FindProperty("EndedTouch");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled, true);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(usingMainCamera, true);
                usingMainCamera.boolValue = true;
                if (usingMainCamera.boolValue)
                {
                    EditorGUILayout.PropertyField(MainCamera, true);
                    if (MainCamera.objectReferenceValue == null)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }

                EditorGUILayout.PropertyField(usingSelectedObject, true);
                usingSelectedObject.boolValue = true;
                if (usingSelectedObject.boolValue)
                {
                    EditorGUILayout.PropertyField(SelectedObject, true);
                    if (SelectedObject.objectReferenceValue == null)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }

                    EditorGUILayout.PropertyField(ObjectSelection, true);
                }

                EditorGUILayout.PropertyField(usingCameraPan, true);
                if (usingCameraPan.boolValue)
                {
                    EditorGUILayout.PropertyField(PanSpeed, true);
                }
                EditorGUILayout.PropertyField(usingCameraScale, true);
                if (usingCameraScale.boolValue)
                {
                    EditorGUILayout.PropertyField(perspectiveZoomSpeed, true);
                    EditorGUILayout.PropertyField(orthoZoomSpeed, true);
                }
                EditorGUILayout.PropertyField(usingCameraRotate, true);

                TouchController.CFilterSelection st = (TouchController.CFilterSelection)ObjectSelection.enumValueIndex;

                if (st == TouchController.CFilterSelection.ByName)
                {
                    EditorGUILayout.PropertyField(ObjectName, true);
                }
                if (st == TouchController.CFilterSelection.ByTag)
                {
                    EditorGUILayout.PropertyField(ObjectTag, true);
                }

                EditorGUILayout.PropertyField(usingTapSelection, true);

                if (usingTapSelection.boolValue)
                {
                    EditorGUILayout.PropertyField(usingTapScale, true);
                    if (usingTapScale.boolValue)
                    {
                        EditorGUILayout.PropertyField(TapScaleSpeed, true);
                    }
                    EditorGUILayout.PropertyField(usingTapRotate, true);
                    if (usingTapRotate.boolValue)
                    {
                        EditorGUILayout.PropertyField(TapRotationSpeed, true);
                    }
                    EditorGUILayout.PropertyField(usingTapDrag, true);
                    if (usingTapDrag.boolValue)
                    {
                        EditorGUILayout.PropertyField(TapDragSpeed, true);
                    }
                }

                EditorGUILayout.PropertyField(usingSwipe, true);

                if (usingSwipe.boolValue)
                {
                    EditorGUILayout.PropertyField(swipeStatus, true);

                    EditorGUILayout.PropertyField(usingSwipeUpEvent, true);
                    if (usingSwipeUpEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeUpEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeUpRightEvent, true);
                    if (usingSwipeUpRightEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeUpRightEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeRightEvent, true);
                    if (usingSwipeRightEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeRightEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeDownRightEvent, true);
                    if (usingSwipeDownRightEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeDownRightEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeDownEvent, true);
                    if (usingSwipeDownEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeDownEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeDownLeftEvent, true);
                    if (usingSwipeDownLeftEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeDownLeftEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeLeftEvent, true);
                    if (usingSwipeLeftEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeLeftEvent, true);
                    }

                    EditorGUILayout.PropertyField(usingSwipeUpLeftEvent, true);
                    if (usingSwipeUpLeftEvent.boolValue)
                    {
                        EditorGUILayout.PropertyField(SwipeUpLeftEvent, true);
                    }
                }

                EditorGUILayout.PropertyField(SelectedObjectType, true);
                EditorGUILayout.PropertyField(SelectedObjectTag, true);
                EditorGUILayout.PropertyField(SelectedObjectName, true);
                EditorGUILayout.PropertyField(FingerTouchCount, true);
                EditorGUILayout.PropertyField(BeginTouch, true);
                EditorGUILayout.PropertyField(MovedTouch, true);
                EditorGUILayout.PropertyField(EndedTouch, true);

            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
