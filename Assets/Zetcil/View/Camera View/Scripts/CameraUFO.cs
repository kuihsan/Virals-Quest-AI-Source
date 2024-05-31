/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera secara bebas, lepas, tanpa beban yang ada seperti burung-burung
 *          terbang di udara, oh syaaap! Dibuat untuk mendeteksi keyboard, joystick, dan mouse. 
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraUFO : MonoBehaviour
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
            [SearchableEnum] public KeyCode ForwardKey = KeyCode.UpArrow;
            [SearchableEnum] public KeyCode LeftKey = KeyCode.LeftArrow;
            [SearchableEnum] public KeyCode BackwardKey = KeyCode.DownArrow;
            [SearchableEnum] public KeyCode RightKey = KeyCode.RightArrow;
            [SearchableEnum] public KeyCode UpKey = KeyCode.PageUp;
            [SearchableEnum] public KeyCode DownKey = KeyCode.PageDown;
            [SearchableEnum] public KeyCode PitchUp = KeyCode.Insert;
            [SearchableEnum] public KeyCode PitchDown = KeyCode.Delete;
            [SearchableEnum] public KeyCode YawLeft = KeyCode.Home;
            [SearchableEnum] public KeyCode YawRight = KeyCode.End;
            [SearchableEnum] public KeyCode RollLeft = KeyCode.Home;
            [SearchableEnum] public KeyCode RollRight = KeyCode.End;
            [SearchableEnum] public KeyCode JumpKey = KeyCode.None;
            [SearchableEnum] public KeyCode ShiftKey = KeyCode.None;

            [Header("Alternative Keys")]
            [SearchableEnum] public KeyCode AltForwardKey = KeyCode.W;
            [SearchableEnum] public KeyCode AltLeftKey = KeyCode.A;
            [SearchableEnum] public KeyCode AltBackwardKey = KeyCode.S;
            [SearchableEnum] public KeyCode AltRightKey = KeyCode.D;
            [SearchableEnum] public KeyCode AltUpKey = KeyCode.None;
            [SearchableEnum] public KeyCode AltDownKey = KeyCode.None;
            [SearchableEnum] public KeyCode AltPitchUp = KeyCode.None;
            [SearchableEnum] public KeyCode AltPitchDown = KeyCode.None;
            [SearchableEnum] public KeyCode AltYawLeft = KeyCode.None;
            [SearchableEnum] public KeyCode AltYawRight = KeyCode.None;
            [SearchableEnum] public KeyCode AltRollLeft = KeyCode.None;
            [SearchableEnum] public KeyCode AltRollRight = KeyCode.None;
            [SearchableEnum] public KeyCode AltJumpKey = KeyCode.None;
            [SearchableEnum] public KeyCode AltShiftKey = KeyCode.None;
        }

        [System.Serializable]
        public class CJoystickController
        {
            public bool isEnabled;
            [Header("Primary Pad")]
            [SearchableEnum] public KeyCode ForwardKey = KeyCode.Joystick1Button0;
            [SearchableEnum] public KeyCode LeftKey = KeyCode.Joystick1Button1;
            [SearchableEnum] public KeyCode BackwardKey = KeyCode.Joystick1Button2;
            [SearchableEnum] public KeyCode RightKey = KeyCode.Joystick1Button3;
            [SearchableEnum] public KeyCode UpKey = KeyCode.Joystick1Button4;
            [SearchableEnum] public KeyCode DownKey = KeyCode.Joystick1Button5;
            [SearchableEnum] public KeyCode YawLeft = KeyCode.Joystick1Button6;
            [SearchableEnum] public KeyCode YawRight = KeyCode.Joystick1Button7;
            [SearchableEnum] public KeyCode PitchUp = KeyCode.Joystick1Button8;
            [SearchableEnum] public KeyCode PitchDown = KeyCode.Joystick1Button9;
            [SearchableEnum] public KeyCode RollLeft = KeyCode.Joystick1Button10;
            [SearchableEnum] public KeyCode RollRight = KeyCode.Joystick1Button11;
            [SearchableEnum] public KeyCode JumpKey = KeyCode.None;
            [SearchableEnum] public KeyCode ShiftKey = KeyCode.None;
        }

        [System.Serializable]
        public class CMouseController
        {
            public bool isEnabled;
            public bool isAutoRotate;
            [Header("Primary Button")]
            [SearchableEnum] public KeyCode MouseLookButton = KeyCode.Mouse1;
            [SearchableEnum] public KeyCode MousePanButton = KeyCode.Mouse2;
            [SearchableEnum] public KeyCode MouseScrollButton = KeyCode.Mouse2;
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
        [Header("Joystick Settings")]
        public bool usingJoystickController;
        [ConditionalField("usingJoystickController")] public CJoystickController JoystickController;
        [Header("Mouse Settings")]
        public bool usingMouseController;
        [ConditionalField("usingMouseController")] public CMouseController MouseController;
        [Header("Touch Settings")]
        public bool usingTouchController;
        public CTouchController TouchController;

        [Header("Special Settings")]
        [SearchableEnum] public KeyCode ResetKey = KeyCode.F5;
        public float MoveSpeed = 100;
        public float ShiftSpeed = 10;
        public float PitchSpeed = 1;
        public float YawSpeed = 1;
        public float RollSpeed = 1;
        public float ScrollSpeed = 0.1f;
        public float LookSpeed = 4f;
        public float PanSpeed = 1.0f;
        float NormalSpeed;

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
            NormalSpeed = MoveSpeed;

            startPosition = TargetCamera.transform.position;
            startRotation = TargetCamera.transform.rotation;

            if (MouseController.MousePanButton == KeyCode.Mouse0) mousePanButton = 0;
            if (MouseController.MousePanButton == KeyCode.Mouse1) mousePanButton = 1;
            if (MouseController.MousePanButton == KeyCode.Mouse2) mousePanButton = 2;

            SyncronizeInput();
        }
		
		public void SetMousePan()
        {
            mousePanButton = 2; 
        }

        public void SetTouchPan()
        {
            mousePanButton = 0;
        }

        public void ResetCameraPosition()
        {
            TargetCamera.transform.position = startPosition;
            TargetCamera.transform.rotation = startRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (Input.GetKey(ResetKey))
                {
                    ResetCameraPosition();
                }

                //-- Keyboard Controller
                if (KeyboardController.isEnabled)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        MoveSpeed = ShiftSpeed;
                    }
                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        MoveSpeed = NormalSpeed;
                    }

                    if (Input.GetKey(KeyboardController.ForwardKey) || Input.GetKey(KeyboardController.AltForwardKey))
                    {
                        TargetCamera.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.BackwardKey) || Input.GetKey(KeyboardController.AltBackwardKey))
                    {
                        TargetCamera.transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.LeftKey) || Input.GetKey(KeyboardController.AltLeftKey))
                    {
                        TargetCamera.transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.RightKey) || Input.GetKey(KeyboardController.AltRightKey))
                    {
                        TargetCamera.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.UpKey) || Input.GetKey(KeyboardController.AltUpKey))
                    {
                        TargetCamera.transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.DownKey) || Input.GetKey(KeyboardController.DownKey))
                    {
                        TargetCamera.transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(KeyboardController.YawLeft) || Input.GetKey(KeyboardController.AltYawLeft))
                    {
                        TargetCamera.transform.Rotate(0, -YawSpeed, 0);
                    }
                    if (Input.GetKey(KeyboardController.YawRight) || Input.GetKey(KeyboardController.AltYawRight))
                    {
                        TargetCamera.transform.Rotate(0, YawSpeed, 0);
                    }
                    if (Input.GetKey(KeyboardController.PitchUp) || Input.GetKey(KeyboardController.AltPitchUp))
                    {
                        TargetCamera.transform.Rotate(-PitchSpeed, 0, 0);
                    }
                    if (Input.GetKey(KeyboardController.PitchDown) || Input.GetKey(KeyboardController.AltPitchDown))
                    {
                        TargetCamera.transform.Rotate(PitchSpeed, 0, 0);
                    }
                    if (Input.GetKey(KeyboardController.RollLeft) || Input.GetKey(KeyboardController.AltRollLeft))
                    {
                        TargetCamera.transform.Rotate(0, 0, RollSpeed);
                    }
                    if (Input.GetKey(KeyboardController.RollRight) || Input.GetKey(KeyboardController.AltRollRight))
                    {
                        TargetCamera.transform.Rotate(0, 0, -RollSpeed);
                    }
                }


                //-- Joystick Controller
                if (JoystickController.isEnabled)
                {
                    if (Input.GetKey(JoystickController.ForwardKey))
                    {
                        TargetCamera.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.BackwardKey))
                    {
                        TargetCamera.transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.LeftKey))
                    {
                        TargetCamera.transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.RightKey))
                    {
                        TargetCamera.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.UpKey))
                    {
                        TargetCamera.transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.DownKey))
                    {
                        TargetCamera.transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
                    }
                    if (Input.GetKey(JoystickController.YawLeft))
                    {
                        TargetCamera.transform.Rotate(0, YawSpeed, 0);
                    }
                    if (Input.GetKey(JoystickController.YawRight))
                    {
                        TargetCamera.transform.Rotate(0, -YawSpeed, 0);
                    }
                    if (Input.GetKey(JoystickController.PitchUp))
                    {
                        TargetCamera.transform.Rotate(-PitchSpeed, 0, 0);
                    }
                    if (Input.GetKey(JoystickController.PitchDown))
                    {
                        TargetCamera.transform.Rotate(PitchSpeed, 0, 0);
                    }
                    if (Input.GetKey(JoystickController.RollLeft))
                    {
                        TargetCamera.transform.Rotate(0, 0, RollSpeed);
                    }
                    if (Input.GetKey(JoystickController.RollRight))
                    {
                        TargetCamera.transform.Rotate(0, 0, -RollSpeed);
                    }
                }

                //-- Mouse Controller
                if (MouseController.isEnabled)
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
                        TargetCamera.transform.Translate(Vector3.forward * ScrollSpeed * Time.deltaTime);
                    }
                    if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        TargetCamera.transform.Translate(Vector3.back * ScrollSpeed * Time.deltaTime);
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
                if (TouchController.isEnabled)
                {
                    if (Input.touchCount > 0)
                    {
                        if (TouchController.TouchStatus == CTouchStatus.isTouchPan || TouchController.TouchStatus == CTouchStatus.isTouchMultiple)
                        {
                            if (isValidTouchPan(TouchController.TouchStatus))
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

                                    TouchController.Finger1TouchX = delta.x;
                                    TouchController.Finger1TouchY = delta.y;
                                }
                            }
                        }
                        if (TouchController.TouchStatus == CTouchStatus.isTouchRotate || TouchController.TouchStatus == CTouchStatus.isTouchMultiple)
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Began)
                            {
                                lastPosition = Input.GetTouch(0).position;
                            }
                            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                            {
                                Vector2 lastPost2D = new Vector2(lastPosition.x, lastPosition.y);
                                Vector2 delta = Input.GetTouch(0).position - lastPost2D;

                                float resultX = 0;
                                float resultY = 0;

                                if (delta.x < 0) resultX = -1;
                                if (delta.x > 0) resultX = 1;
                                if (delta.y < 0) resultY = -1;
                                if (delta.y > 0) resultY = 1;

                                targetRotationY += resultX * LookSpeed * 0.1f;
                                targetRotationX -= resultY * LookSpeed * 0.1f;
                                targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
                                TargetCamera.transform.rotation = targetRotation;

                                lastPosition = Input.GetTouch(0).position;
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

        bool isValidTouchPan(CTouchStatus aStatus)
        {
            bool result = false;

            if (aStatus == CTouchStatus.isTouchPan)
            {
                result = true;
            }
            else if (aStatus == CTouchStatus.isTouchMultiple)
            {
                if (Input.GetTouch(0).position.x < Screen.width * 2 / 3)
                {
                    result = true;
                }
            }

            return result;
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

        public void ActivateCamera(Toggle ToggleMap)
        {
            ToggleMap.isOn = true;
            KeyboardController.isEnabled = ToggleMap.isOn;
            MouseController.isEnabled = ToggleMap.isOn;
        }

        public void ActivateCamera(bool aValue)
        {
            KeyboardController.isEnabled = aValue;
            MouseController.isEnabled = aValue;
        }

        public void InActivateCamera(Toggle ToggleMap)
        {
            ToggleMap.isOn = false;
            KeyboardController.isEnabled = ToggleMap.isOn;
            MouseController.isEnabled = ToggleMap.isOn;
        }

        public void ChangeCameraStatus(Toggle ToggleMap)
        {
            KeyboardController.isEnabled = ToggleMap.isOn;
            MouseController.isEnabled = ToggleMap.isOn;
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

