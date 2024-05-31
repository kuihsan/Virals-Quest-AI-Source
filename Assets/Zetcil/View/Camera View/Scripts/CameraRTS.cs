/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera ala game jadul Age of Empires, yaitu kamera isometric
 *          kurang lebih 45 derajat ke bawah
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraRTS : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned]
        public Camera TargetCamera;

        [System.Serializable]
        public class CKeyboardController
        {
            public bool isEnabled;
            [Header("Primary Keys")]
            [SearchableEnum] public KeyCode  ForwardKey = KeyCode.UpArrow;
            [SearchableEnum] public KeyCode  LeftKey = KeyCode.LeftArrow;
            [SearchableEnum] public KeyCode  BackwardKey = KeyCode.DownArrow;
            [SearchableEnum] public KeyCode  RightKey = KeyCode.RightArrow;
            [SearchableEnum] public KeyCode  UpKey = KeyCode.PageUp;
            [SearchableEnum] public KeyCode  DownKey = KeyCode.PageDown;

            [Header("Alternative Keys")]
            [SearchableEnum] public KeyCode  AltForwardKey = KeyCode.W;
            [SearchableEnum] public KeyCode  AltLeftKey = KeyCode.A;
            [SearchableEnum] public KeyCode  AltBackwardKey = KeyCode.S;
            [SearchableEnum] public KeyCode  AltRightKey = KeyCode.D;
            [SearchableEnum] public KeyCode  AltUpKey = KeyCode.None;
            [SearchableEnum] public KeyCode  AltDownKey = KeyCode.None;
        }

        [System.Serializable]
        public class CMouseController
        {
            public bool isEnabled;
            public bool isAutoRotate;
            [Header("Primary Button")]
            public KeyCode MouseLookButton = KeyCode.Mouse1;
            public KeyCode MousePanButton = KeyCode.Mouse2;
            public KeyCode MouseScrollButton = KeyCode.Mouse2;
        }

        public enum CTouchStatus { isNone, isTouchPan, isTouchPinch, isTouchRotate, isTouchMultiple }

        [System.Serializable]
        public class CTouchController
        {
            public bool isEnabled;
            public CTouchStatus TouchStatus;
            [Header("Finger Status")]
            public string PinchStatus;
            public int FingerTouchCount;
            public float Finger1TouchX;
            public float Finger1TouchY;
            public float Finger2TouchX;
            public float Finger2TouchY;
            public float FingerDelta;
            public float prevFingerDelta;
        }

        [Header("Keyboard Settings")]
        public bool usingKeyboardController;
        [ConditionalField("usingKeyboardController")] public CKeyboardController KeyboardController;
        [Header("Mouse Settings")]
        public bool usingMouseController;
        [ConditionalField("usingMouseController")] public CMouseController MouseController;
        [Header("Touch Settings")]
        public bool usingTouchController;
        [ConditionalField("usingTouchController")] public CTouchController TouchController;

        [Header("Special Settings")]
        [SearchableEnum] public KeyCode  ResetKey = KeyCode.F5;
        public float MoveSpeed = 100;
        public float PitchSpeed = 1;
        public float YawSpeed = 1;
        public float RollSpeed = 1;
        public float ScrollSpeed = 0.1f;
        public float LookSpeed = 4f;
        public float PanSpeed = 1.0f;

        int mousePanButton;
        Vector3 startPosition;
        Quaternion startRotation;
        Vector3 lastPosition;

        Vector3 targetPosition;
        Quaternion targetRotation;
        float targetRotationY;
        float targetRotationX;

        public void SetAsMinimap()
        {
            TargetCamera.rect = new Rect(0.7f, 0.5f, 0.29f, 0.49f);
            TargetCamera.depth = 1;
        }

        public void ResetCameraPosition()
        {
            TargetCamera.transform.position = startPosition;
            TargetCamera.transform.rotation = startRotation;
        }

        public void SetAsMainmap()
        {
            TargetCamera.rect = new Rect(0, 0, 1, 1);
            TargetCamera.depth = 0;
        }

        public void SetKeyboardStatus(bool aValue)
        {
            usingKeyboardController = aValue;
            KeyboardController.isEnabled = aValue;
        }

        public void SetMouseStatus(bool aValue)
        {
            usingMouseController = aValue;
            MouseController.isEnabled = aValue;
        }

        public void SetTouchStatus(bool aValue)
        {
            usingTouchController = aValue;
            TouchController.isEnabled = aValue;
        }

        void SyncronizeInput()
        {
            targetPosition = TargetCamera.transform.position;
            targetRotation = TargetCamera.transform.rotation;
            targetRotationY = TargetCamera.transform.localRotation.eulerAngles.y;
            targetRotationX = TargetCamera.transform.localRotation.eulerAngles.x;
        }

        // Use this for initialization
        void Start()
        {
            startPosition = TargetCamera.transform.position;
            startRotation = TargetCamera.transform.rotation;

            if (MouseController.MousePanButton == KeyCode.Mouse0) mousePanButton = 0;
            if (MouseController.MousePanButton == KeyCode.Mouse1) mousePanButton = 1;
            if (MouseController.MousePanButton == KeyCode.Mouse2) mousePanButton = 2;

            SyncronizeInput();
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (Input.GetKey(ResetKey))
                {
                    TargetCamera.transform.position = startPosition;
                    TargetCamera.transform.rotation = startRotation;
                }

                //-- Keyboard Controller
                if (usingKeyboardController && KeyboardController.isEnabled)
                {
                    if (Input.GetKey(KeyboardController.ForwardKey) || Input.GetKey(KeyboardController.AltForwardKey))
                    {
                        TargetCamera.transform.position += (Vector3.forward * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.BackwardKey) || Input.GetKey(KeyboardController.AltBackwardKey))
                    {
                        TargetCamera.transform.position += (Vector3.back * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.LeftKey) || Input.GetKey(KeyboardController.AltLeftKey))
                    {
                        TargetCamera.transform.position += (Vector3.left * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.RightKey) || Input.GetKey(KeyboardController.AltRightKey))
                    {
                        TargetCamera.transform.position += (Vector3.right * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.UpKey) || Input.GetKey(KeyboardController.AltUpKey))
                    {
                        TargetCamera.transform.position += (Vector3.up * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.DownKey) || Input.GetKey(KeyboardController.DownKey))
                    {
                        TargetCamera.transform.position += (Vector3.down * MoveSpeed * Time.deltaTime);
                    }
                }

                //-- Mouse Controller
                if (usingMouseController && MouseController.isEnabled)
                {
                    if (MouseController.isAutoRotate || Input.GetKey(MouseController.MouseLookButton))
                    {
                        targetRotationY += Input.GetAxis("Mouse X") * LookSpeed;
                        targetRotationX -= Input.GetAxis("Mouse Y") * LookSpeed;
                        targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
                        TargetCamera.transform.rotation = targetRotation;
                    }

                    if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        if (TargetCamera.orthographic)
                        {
                            if (TargetCamera.orthographicSize > 10)
                            {
                                TargetCamera.orthographicSize -= 1 * ScrollSpeed;
                            }
                        }
                        else
                        {
                            TargetCamera.transform.Translate(Vector3.forward * ScrollSpeed * Time.deltaTime);
                        }
                    }
                    if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        if (TargetCamera.orthographic)
                        {
                            TargetCamera.orthographicSize += 1 * ScrollSpeed;
                        }
                        else
                        {
                            TargetCamera.transform.Translate(Vector3.back * ScrollSpeed * Time.deltaTime);
                        }
                    }

                    if (Input.GetMouseButtonDown(mousePanButton))
                    {
                        lastPosition = Input.mousePosition;
                    }
                    if (Input.GetMouseButton(mousePanButton))
                    {
                        Vector3 delta = Input.mousePosition - lastPosition;
                        TargetCamera.transform.Translate(-delta.x * PanSpeed, -delta.y * PanSpeed, 0);
                        lastPosition = Input.mousePosition;
                        targetPosition = TargetCamera.transform.position;
                    }
                }

                //-- Touch Controller
                if (usingTouchController && TouchController.isEnabled)
                {
                    if (Input.touchCount > 0)
                    {
                        if (TouchController.TouchStatus == CTouchStatus.isTouchPan || TouchController.TouchStatus == CTouchStatus.isTouchMultiple)
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Began)
                            {
                                lastPosition = Input.GetTouch(0).position;
                            }
                            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                            {
                                Vector2 lastPost2D = new Vector2(lastPosition.x, lastPosition.y);
                                Vector2 delta = Input.GetTouch(0).position - lastPost2D;
                                TargetCamera.transform.Translate(-delta.x * PanSpeed, -delta.y * PanSpeed, 0);
                                lastPosition = Input.GetTouch(0).position;
                                targetPosition = TargetCamera.transform.position;
                            }
                        }
                        if (TouchController.TouchStatus == CTouchStatus.isTouchPinch || TouchController.TouchStatus == CTouchStatus.isTouchMultiple)
                        {
                            TouchController.FingerTouchCount = Input.touchCount;

                            if (Input.touchCount == 2)
                            {
                                Touch finger1Touch = Input.GetTouch(0);
                                Touch finger2Touch = Input.GetTouch(1);

                                if (finger1Touch.phase == TouchPhase.Moved && finger2Touch.phase == TouchPhase.Moved)
                                {
                                    TouchController.Finger1TouchX = finger1Touch.position.x;
                                    TouchController.Finger1TouchY = finger1Touch.position.y;
                                    TouchController.Finger2TouchX = finger2Touch.position.x;
                                    TouchController.Finger2TouchY = finger2Touch.position.y;

                                    Vector2 currDist = finger1Touch.position - finger2Touch.position;
                                    Vector2 prevDist = ((finger1Touch.position - finger1Touch.position) - (finger2Touch.position - finger2Touch.deltaPosition));
                                    TouchController.FingerDelta = currDist.magnitude - prevDist.magnitude;

                                    if (TouchController.FingerDelta > TouchController.prevFingerDelta)
                                    {
                                        TouchController.PinchStatus = "ZoomIn";

                                        if (TargetCamera.orthographic)
                                        {
                                            if (TargetCamera.orthographicSize > 10)
                                            {
                                                TargetCamera.orthographicSize -= 1 * ScrollSpeed;
                                            }
                                        }
                                        else
                                        {
                                            TargetCamera.transform.Translate(Vector3.forward * ScrollSpeed * Time.deltaTime);
                                        }

                                    }
                                    else
                                    {
                                        TouchController.PinchStatus = "ZoomOut";

                                        if (TargetCamera.orthographic)
                                        {
                                            TargetCamera.orthographicSize += 1 * ScrollSpeed;
                                        }
                                        else
                                        {
                                            TargetCamera.transform.Translate(Vector3.back * ScrollSpeed * Time.deltaTime);
                                        }

                                    }

                                    TouchController.prevFingerDelta = TouchController.FingerDelta;

                                }
                            }
                        }
                    }
                }
            }
        }

        void LateUpdate()
        {
            SyncronizeInput();
        }

        public void SetTouchStatus(int aStatus)
        {
            TouchController.TouchStatus = CTouchStatus.isNone;
            if (aStatus == 0) TouchController.TouchStatus = CTouchStatus.isNone;
            if (aStatus == 1) TouchController.TouchStatus = CTouchStatus.isTouchPan;
            if (aStatus == 2) TouchController.TouchStatus = CTouchStatus.isTouchPinch;
            if (aStatus == 3) TouchController.TouchStatus = CTouchStatus.isTouchRotate;
            if (aStatus == 4) TouchController.TouchStatus = CTouchStatus.isTouchMultiple;
        }

        public void ChangeCameraRotation(float aValue)
        {
            if (isEnabled)
                TargetCamera.transform.localEulerAngles = new Vector3(TargetCamera.transform.localEulerAngles.x,
                                                                      TargetCamera.transform.localEulerAngles.y + aValue,
                                                                      TargetCamera.transform.localEulerAngles.z);
        }

    }
}

