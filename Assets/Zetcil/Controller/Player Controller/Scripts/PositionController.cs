/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk perubahan posisi karakter menggunakan Keyboard
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class PositionController : MonoBehaviour
    {
        public enum CInputType { None, Keyboard, Mouse, Vector, Command }
        public enum CKeyboardStyle { None, StyleFPS, StyleADV, StyleMMO, StyleSSP, StyleRPG, StyleJRPG }
        public enum CVectorStyle { None, StyleRPG }
        public enum CMouseStyle { None, StyleRPG, StyleRTS }
        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CMovementQuadran { None, UpLeft, UpRight, DownLeft, DownRight }
        public enum CFilterSelection { Everything, ByName, ByTag }

        [System.Serializable]
        public class CAdditionalSettings
        {
            [SearchableEnum] public KeyCode AdditionalKey;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CKeyboardKey
        {
            [SearchableEnum] public KeyCode UpKey = KeyCode.UpArrow;
            [SearchableEnum] public KeyCode LeftKey = KeyCode.LeftArrow;
            [SearchableEnum] public KeyCode DownKey = KeyCode.DownArrow;
            [SearchableEnum] public KeyCode RightKey = KeyCode.RightArrow;
            [SearchableEnum] public KeyCode JumpKey;
            [SearchableEnum] public KeyCode ShiftKey;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Movement Type")]
        public CInputType InputType;
        public KeyboardController TargetKeyboardController;

        [Header("Movement Style")]
        public CKeyboardStyle KeyboardStyle;

        [Header("Movement Style")]
        public CMouseStyle MouseStyle;

        [Header("Movement Style")]
        public CVectorStyle VectorStyle;
        public VarVector3 TargetVector; // the game object containing the TargetVector script

        [Header("Character Controller")]
        public CharacterController TargetController;
        [HideInInspector] public Camera TargetCamera;
        [Tag]public string MainCameraTag;

        [Header("Keyboard Settings")]
        public KeyboardController.CKeyboardKey PrimaryKeyboardKey;
        public KeyboardController.CKeyboardKey AltKeyboardKey;
        public KeyboardController.CKeyboardKey SecondaryKeyboardKey;
        public KeyboardController.CKeyboardKey AltSecondaryKeyboardKey;

        [Header("Object Selection Settings")]
        public CFilterSelection ObjectSelection;
        public string ObjectName;
        [Tag] public string[] ObjectTag;
        public VarBoolean SelectionStatus;

        [Header("Mouse Action Settings")]
        [SearchableEnum] public KeyCode ClearSelection;
        public CClickType ClickSelection;
        public CClickType ClickTrigger;

        [HideInInspector]
        public CMovementQuadran MovementQuadran;

        [Header("Direction Pad Settings")]
        public float touchSensitivity = 1;
        public float moveSensitivity = 1;
        public float rotateSensitivity = 1;
        public bool usingRotateCameraRight;
        public bool isFlipDirection;

        [Header("Command Settings")]
        public VarString HorizontalCommand;
        public VarString VerticalCommand;
        public VarString SpecialCommand;

        [Header("Movement Settings")]
        public float MoveSpeed = 200;
        public float AlterSpeed = 200;
        public float RotateSpeed = 5;
        public float TurnSpeed = 0.01F;
        public bool followDirection;
        public float StopDistance = 0.2f;
        [ReadOnly] public bool isAttack;
        [ReadOnly] public bool isMoving;
        float staticSpeed;

        [Header("Jump Settings")]
        public float jumpSpeed = 8.0F;
        public int maxJump = 1;
        public float gravity = 20.0F;
        [ReadOnly] public int currentJump;

        [Header("Enemy Settings")]
        public VarObject TargetEnemy;
        [ReadOnly] public float TargetDistance;
        public float AttackRange;
        [Tag] public string[] EnemyTag;

        [Header("AutoAttack Settings")]
        public bool usingAutoAttack;
        public float SearchingRange;

        [Header("Syncronize Settings")]
        public bool usingSyncronizeController;
        public bool SyncronizePosition;
        public bool SyncronizeRotation;

        [Header("Readonly Mouse Value")]
        [ReadOnly] public string RaycastTag;
        [ReadOnly] public string RaycastName;
        [ReadOnly] public GameObject RaycastObject;
        [ReadOnly] public int DownStatus;
        [ReadOnly] public float CurrentDistance;

        [Header("Visual Settings")]
        public bool usingVisualObject;
        public GameObject VisualObject;

        float myAngle = 0f;
        private Vector3 moveDirection = Vector3.zero;

        bool initialValue = true;
        RaycastHit raycastHit;
        Ray ray;
        Vector3 Destination, FirstTouch, SecondTouch, Direction;
        public Vector3 RotationStatus;
        Vector3 delta, lastpos;

        private Vector3 TargetVectorInput; // holds the input of the Left Joystick
        Quaternion targetRotation;

        float targetRotationX, targetRotationY;
        private Vector2 lastPosition;

        Vector3 FirstPoint;
        Vector3 SecondPoint;
        float xAngle;
        float yAngle;
        float xAngleTemp;
        float yAngleTemp;

        [Header("Additional Settings")]
        public bool usingAdditionalSettings;
        public CAdditionalSettings[] AdditionalSettings;

        private Transform follow = null;
        private Vector3 originalLocalPosition;
        private Quaternion originalLocalRotation;

        [HideInInspector]
        public bool MovementEnabled;

        void Awake()
        {
            if (isEnabled)
            {
                staticSpeed = MoveSpeed;
                follow = TargetController.transform;
                originalLocalPosition = follow.localPosition;
                originalLocalRotation = follow.localRotation;
                //-- freeze scale
                transform.localScale = new Vector3(1, 1, 1);
                TargetController.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public void SetMovementEnabled(bool aEnabled)
        {
            MovementEnabled = aEnabled;
        }

        void Start()
        {

            if (isEnabled)
            {

                MovementEnabled = true;

                if (!GameObject.FindGameObjectWithTag(MainCameraTag))
                {
                    Debug.Log("MainCamera Tag Not Found");
                } else
                {
                    TargetCamera = GameObject.FindGameObjectWithTag(MainCameraTag).GetComponent<Camera>();
                }
            }

            if (isEnabled && InputType == CInputType.Mouse)
            {
                isMoving = false;
                Destination = TargetController.transform.position;
                MovementQuadran = CMovementQuadran.None;
            }

            InputSyncronize();

            /* Update: Progress penambahan style baru untuk Direction-Pad
            if (isEnabled && InputType == CInputType.Vector && VectorStyle == CVectorStyle.StyleMMO)
            {
                xAngle = 0;
                yAngle = 0;
                TargetCamera.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
            }
            */
        }

        void SyncronizeController()
        {
            if (usingSyncronizeController)
            {
                if (SyncronizePosition) transform.position = follow.position;

                if (SyncronizeRotation)
                {
                    follow.RotateAround(follow.position, follow.forward, -originalLocalRotation.eulerAngles.z);
                    follow.RotateAround(follow.position, follow.right, -originalLocalRotation.eulerAngles.x);
                    follow.RotateAround(follow.position, follow.up, -originalLocalRotation.eulerAngles.y);
                    transform.rotation = follow.rotation;
                }

                if (SyncronizePosition)
                {
                    transform.position += -transform.right * originalLocalPosition.x;
                    transform.position += -transform.up * originalLocalPosition.y;
                    transform.position += -transform.forward * originalLocalPosition.z;
                }
                if (SyncronizeRotation) follow.localRotation = originalLocalRotation;
                if (SyncronizePosition) follow.localPosition = originalLocalPosition;
            }
        }

        void InputSyncronize()
        {
            if (isEnabled && InputType == CInputType.Keyboard)
            {
                PrimaryKeyboardKey = TargetKeyboardController.PrimaryKeyboardKey;
                AltKeyboardKey = TargetKeyboardController.AltKeyboardKey;
                SecondaryKeyboardKey = TargetKeyboardController.SecondaryKeyboardKey;
                AltSecondaryKeyboardKey = TargetKeyboardController.AltSecondaryKeyboardKey;
            }
        }

        void FixedUpdate()
        {
            if (isEnabled && TargetHealth.CurrentValue > 0)
            {
                
                if (usingAdditionalSettings)
                {
                    for (int x=0; x<AdditionalSettings.Length; x++)
                    {
                        if (Input.GetKey(AdditionalSettings[x].AdditionalKey))
                        {
                            AdditionalSettings[x].AdditionalEvent.Invoke();
                        }
                    }
                }

                //=============================================================================================== KEYBOARD: MODE
                if (InputType == CInputType.Keyboard || InputType == CInputType.Command)
                {

                    if (Input.GetKey(PrimaryKeyboardKey.ShiftKey) || Input.GetKey(SecondaryKeyboardKey.ShiftKey) || Input.GetKey(AltKeyboardKey.ShiftKey) || Input.GetKey(SecondaryKeyboardKey.ShiftKey))
                    {
                        MoveSpeed = AlterSpeed;
                    }
                    else
                    {
                        MoveSpeed = staticSpeed;
                    }

                    if (KeyboardStyle == CKeyboardStyle.StyleFPS)
                    {

                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = new Vector3(GetAxisHorizontal(), 0, GetAxisVertical());
                            moveDirection = TargetController.transform.TransformDirection(moveDirection);
                            moveDirection *= MoveSpeed;
                            if (Input.GetKey(PrimaryKeyboardKey.JumpKey))
                                moveDirection.y = jumpSpeed;
                        }

                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);

                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleSSP)
                    {

                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = Vector3.zero;
                            currentJump = 0;

                            if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Euler(temp);

                                moveDirection = Vector3.left;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.LeftKey) || Input.GetKeyUp(AltKeyboardKey.LeftKey))
                            {
                                moveDirection = Vector3.zero;
                            }


                            if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Euler(temp);

                                moveDirection = Vector3.right;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;

                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.RightKey) || Input.GetKeyUp(AltKeyboardKey.RightKey))
                            {
                                moveDirection = Vector3.zero;
                            }


                            if (Input.GetKey(PrimaryKeyboardKey.JumpKey))
                                moveDirection.y = jumpSpeed;

                        }
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleJRPG)
                    {
                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        moveDirection = Vector3.zero;
                        if (GetAxisHorizontal() != 0)
                        {
                            if (GetAxisHorizontal() > 0)
                            {
                                moveDirection = new Vector3(GetAxisHorizontal(), 0, 0);
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                                if (followDirection && usingVisualObject) VisualObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                            }
                            else
                            if (GetAxisHorizontal() < 0)
                            {
                                moveDirection = new Vector3(GetAxisHorizontal(), 0, 0);
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                                if (followDirection && usingVisualObject) VisualObject.transform.localEulerAngles = new Vector3(0, 180, 0);
                            }
                        }
                        else if (GetAxisVertical() != 0)
                        {
                            if (GetAxisVertical() > 0)
                            {
                                moveDirection = new Vector3(0, GetAxisVertical(), 0);
                                moveDirection *= MoveSpeed;
                                if (followDirection && usingVisualObject) VisualObject.transform.localEulerAngles = new Vector3(0, 270, 0);
                            }
                            else
                            if (GetAxisVertical() < 0)
                            {
                                moveDirection = new Vector3(0, GetAxisVertical(), 0);
                                moveDirection *= MoveSpeed;
                                if (followDirection && usingVisualObject) VisualObject.transform.localEulerAngles = new Vector3(0, 270, 0);
                            }
                        }

                        TargetController.Move(moveDirection * Time.deltaTime);
                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleMMO)
                    {
                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = Vector3.zero;
                            currentJump = 0;

                            if (Input.GetKey(PrimaryKeyboardKey.UpKey) || Input.GetKey(AltKeyboardKey.UpKey))
                            {

                                Vector3 temp = Vector3.forward;
                                temp.x = 0;
                                temp.z = 0;
                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.UpKey) || Input.GetKeyUp(AltKeyboardKey.UpKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.DownKey) || Input.GetKey(AltKeyboardKey.DownKey))
                            {

                                Vector3 temp = Vector3.forward;

                                temp.x = 0;
                                temp.y += 180;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.DownKey) || Input.GetKeyUp(AltKeyboardKey.DownKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                            {
                                Vector3 temp = Vector3.forward;

                                temp.x = 0;
                                temp.y += 270;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.LeftKey) || Input.GetKeyUp(AltKeyboardKey.LeftKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                            {
                                Vector3 temp = Vector3.forward;

                                temp.x = 0;
                                temp.y += 90;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.RightKey) || Input.GetKeyUp(AltKeyboardKey.RightKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.JumpKey) || Input.GetKey(AltKeyboardKey.JumpKey))
                                moveDirection.y = jumpSpeed;

                        }
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleADV)
                    {
                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = Vector3.zero;
                            currentJump = 0;

                            if (Input.GetKey(PrimaryKeyboardKey.UpKey) || Input.GetKey(AltKeyboardKey.UpKey))
                            {

                                Quaternion temp = TargetCamera.transform.rotation;
                                temp.x = 0;
                                temp.z = 0;
                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, temp, Time.time * TurnSpeed);
                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.UpKey) || Input.GetKeyUp(AltKeyboardKey.UpKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.DownKey) || Input.GetKey(AltKeyboardKey.DownKey))
                            {

                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 180;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.DownKey) || Input.GetKeyUp(AltKeyboardKey.DownKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 270;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.LeftKey) || Input.GetKeyUp(AltKeyboardKey.LeftKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 90;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (Input.GetKeyUp(PrimaryKeyboardKey.RightKey) || Input.GetKeyUp(AltKeyboardKey.RightKey))
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.JumpKey) || Input.GetKey(AltKeyboardKey.JumpKey))
                                moveDirection.y = jumpSpeed;

                        }
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleRPG)
                    {
                        if (currentJump < maxJump - 1)
                        {
                            if (Input.GetKeyUp(PrimaryKeyboardKey.JumpKey) || Input.GetKeyUp(AltKeyboardKey.JumpKey))
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = Vector3.zero;
                            currentJump = 0;

                            if (GetAxisHorizontal() > 0 || GetAxisHorizontal() < 0)
                            {
                                moveDirection = new Vector3(GetAxisHorizontal(), 0, 0);
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }

                            if (GetAxisVertical() > 0 || GetAxisVertical() < 0)
                            {
                                moveDirection = new Vector3(GetAxisHorizontal(), 0, GetAxisVertical());
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                            {
                                TargetController.transform.Rotate(0, -RotateSpeed, 0);
                            }
                            if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                            {
                                TargetController.transform.Rotate(0, RotateSpeed, 0);
                            }

                            if (Input.GetKeyUp(PrimaryKeyboardKey.JumpKey) || Input.GetKeyUp(AltKeyboardKey.JumpKey))
                                moveDirection.y = jumpSpeed;

                        }
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                    }

                    SyncronizeController();
                }

                //=============================================================================================== MOUSE: MODE
                if (InputType == CInputType.Mouse)
                {

                    if (MouseStyle == CMouseStyle.StyleRTS && MovementEnabled)
                    {

                        moveDirection = Vector3.zero;

                        if (Input.GetKeyDown(ClearSelection))
                        {
                            SelectionStatus.CurrentValue = false;
                            TargetEnemy.CurrentValue = null;
                        }

                        //if (TargetController.isGrounded)
                        //{
                        if (!SelectionStatus.CurrentValue)
                        {
                            if (Input.GetKeyUp(GetSelectionKey()))
                            {
                                ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                                if (Physics.Raycast(ray, out raycastHit))
                                {
                                    RaycastTag = raycastHit.collider.tag;
                                    RaycastName = raycastHit.collider.gameObject.name;

                                    if (ObjectSelection == CFilterSelection.ByName)
                                    {
                                        if (RaycastName == ObjectName)
                                        {
                                            SelectionStatus.CurrentValue = true;
                                        }
                                    }
                                    if (ObjectSelection == CFilterSelection.ByTag)
                                    {
                                        for (int i=0; i< ObjectTag.Length; i++)
                                        {
                                            if (RaycastTag == ObjectTag[i])
                                            {
                                                SelectionStatus.CurrentValue = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else 
                        {
                            if (Input.GetKeyDown(GetTriggerKey()))
                            {
                                lastpos = Input.mousePosition;
                            }
                            if (Input.GetKey(GetTriggerKey()))
                            {
                                delta = Input.mousePosition - lastpos;
                                RotationStatus = delta;
                                lastpos = Input.mousePosition;
                                Vector3 resultRotation = new Vector3(0, -RotationStatus.y, 0);
                                TargetController.transform.Rotate(resultRotation);
                            }
                            if (Input.GetKeyUp(GetTriggerKey()))
                            {
                                RotationStatus = Vector3.zero;
                            }
                            if (Input.GetKeyUp(GetTriggerKey()))
                            {
                                ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                                if (Physics.Raycast(ray, out raycastHit))
                                {
                                    Destination = raycastHit.point;
                                    Destination.y = TargetController.transform.position.y;
                                    TargetController.transform.LookAt(Destination);

                                    RaycastTag = raycastHit.collider.tag;
                                    RaycastName = raycastHit.collider.gameObject.name;
                                    RaycastObject = raycastHit.collider.gameObject;
                                    TargetEnemy.CurrentValue = null;

                                    for (int i = 0; i < EnemyTag.Length; i++)
                                    {
                                         if (RaycastTag == EnemyTag[i])
                                         {
                                            TargetEnemy.CurrentValue = RaycastObject;
                                         }
                                    }

                                }
                                initialValue = false;
                            }
                        }

                        if (!initialValue)
                        {
                            CurrentDistance = Vector3.Distance(Destination, TargetController.transform.position);
                            if (CurrentDistance > StopDistance)
                            {
                                moveDirection = TargetController.transform.forward * MoveSpeed * Time.deltaTime;
                                TargetController.Move(moveDirection);
                                isMoving = true;
                                isAttack = false;
                            } else
                            {
                                isMoving = false;
                            }
                        }

                        if (usingAutoAttack)
                        {
                            if (TargetEnemy.CurrentValue != null)
                            {
                                TargetDistance = Vector3.Distance(TargetController.transform.position, TargetEnemy.CurrentValue.transform.position);
                                if (TargetDistance < SearchingRange)
                                {
                                    Destination = TargetEnemy.CurrentValue.transform.position;
                                    Destination.y = TargetController.transform.position.y;
                                    TargetController.transform.LookAt(Destination);

                                    initialValue = false;
                                }
                                if (TargetDistance < AttackRange)
                                {
                                    isAttack = true;
                                    isMoving = false;
                                }
                            }
                        } else
                        {
                            if (TargetEnemy.CurrentValue != null)
                            {
                                TargetDistance = Vector3.Distance(TargetController.transform.position, TargetEnemy.CurrentValue.transform.position);
                                if (TargetDistance < AttackRange)
                                {
                                    //Destination = TargetEnemy.CurrentValue.transform.position;
                                    //Destination.y = TargetController.transform.position.y;
                                    //TargetController.transform.LookAt(Destination);

                                    isAttack = true;
                                    isMoving = false;
                                }
                            }
                        }

                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                        //}

                    }

                    if (MouseStyle == CMouseStyle.StyleRPG && MovementEnabled)
                    {

                        moveDirection = Vector3.zero;

                        if (Input.GetKeyDown(ClearSelection))
                        {
                            SelectionStatus.CurrentValue = false;
                            TargetEnemy.CurrentValue = null;
                        }

                        //if (TargetController.isGrounded)
                        //{

                        if (Input.GetKeyDown(GetTriggerKey()))
                        {
                            lastpos = Input.mousePosition;
                        }
                        if (Input.GetKey(GetTriggerKey()))
                        {
                            delta = Input.mousePosition - lastpos;
                            RotationStatus = delta;
                            lastpos = Input.mousePosition;
                            Vector3 resultRotation = new Vector3(0, -RotationStatus.y, 0);
                            TargetController.transform.Rotate(resultRotation);
                        }
                        if (Input.GetKeyUp(GetTriggerKey()))
                        {
                            RotationStatus = Vector3.zero;
                        }

                        if (Input.GetKeyUp(GetTriggerKey()))
                        {
                            ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out raycastHit))
                            {
                                Destination = raycastHit.point;
                                Destination.y = TargetController.transform.position.y;
                                TargetController.transform.LookAt(Destination);

                                RaycastTag = raycastHit.collider.tag;
                                RaycastName = raycastHit.collider.gameObject.name;
                                RaycastObject = raycastHit.collider.gameObject;

                                if (ObjectSelection == CFilterSelection.ByTag)
                                {
                                    for (int i = 0; i < EnemyTag.Length; i++)
                                    {
                                        if (RaycastTag == EnemyTag[i])
                                        {
                                            RaycastObject.SetActive(false);
                                        }
                                    }
                                }

                            }
                            initialValue = false;
                        }

                        if (!initialValue)
                        {
                            CurrentDistance = Vector3.Distance(Destination, TargetController.transform.position);
                            if (CurrentDistance > StopDistance)
                            {
                                moveDirection = TargetController.transform.forward * MoveSpeed * Time.deltaTime;
                                TargetController.Move(moveDirection);
                                isMoving = true;
                            }
                            else
                            {
                                isMoving = false;

                            }
                        }

                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                        //}
                    }

                    SyncronizeController();
                }

                //=============================================================================================== VECTOR: MODE
                if (InputType == CInputType.Vector)
                {

                    // get input touch count
                    if (Input.touchCount > 0 && usingRotateCameraRight)
                    {
                        if (Input.touchCount == 1 && Input.GetTouch(0).position.x > Screen.width / 2)
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Began)
                            {
                                FirstPoint = Input.GetTouch(0).position;
                                xAngleTemp = xAngle;
                                yAngleTemp = yAngle;
                            }
                            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                            {
                                SecondPoint = Input.GetTouch(0).position;
                                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                                TargetCamera.transform.rotation = Quaternion.Euler(-yAngle, xAngle, 0.0f);
                            }
                        }
                        else if (Input.touchCount == 2 && Input.GetTouch(1).position.x > Screen.width / 2)
                        {
                            if (Input.GetTouch(1).phase == TouchPhase.Began)
                            {
                                FirstPoint = Input.GetTouch(1).position;
                                xAngleTemp = xAngle;
                                yAngleTemp = yAngle;
                            }
                            if (Input.GetTouch(1).phase == TouchPhase.Moved)
                            {
                                SecondPoint = Input.GetTouch(1).position;
                                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                                TargetCamera.transform.rotation = Quaternion.Euler(-yAngle, xAngle, 0.0f);
                            }
                        }
                    }

                    // get input from both joysticks
                    if (isFlipDirection)
                    {
                        TargetVectorInput = -1 * TargetVector.CurrentValue;
                    }
                    else
                    {
                        TargetVectorInput = TargetVector.CurrentValue;
                    }

                    float xMovementTargetVector = 0; // The horizontal movement from joystick 01
                    float zMovementTargetVector = 0; // The vertical movement from joystick 01	

                    xMovementTargetVector = TargetVectorInput.x; // The horizontal movement from joystick 01
                    zMovementTargetVector = TargetVectorInput.y; // The vertical movement from joystick 01	

                    // if there is no input on the left joystick
                    if (TargetVectorInput == Vector3.zero)
                    {

                    }
                    //****************************************************************************** (1) if there is only input from the left joystick
                    if (TargetVectorInput != Vector3.zero)
                    {

                        //== LEFT MOVEMENT BEGIN
                        /*
                        if (VectorStyle == CVectorStyle.StyleMMO)
                        {
                            float tempAngle = Mathf.Atan2(zMovementTargetVector, xMovementTargetVector);
                            xMovementTargetVector *= Mathf.Abs(Mathf.Cos(tempAngle));
                            zMovementTargetVector *= Mathf.Abs(Mathf.Sin(tempAngle));

                            xMovementTargetVector *= touchSensitivity;
                            zMovementTargetVector *= touchSensitivity;

                            // calculate the player's direction based on angle
                            TargetVectorInput = new Vector3(xMovementTargetVector, 0, zMovementTargetVector);
                            TargetVectorInput = TargetController.transform.TransformDirection(TargetVectorInput);
                            TargetVectorInput *= MoveSpeed;

                            // rotate the player to face the direction of input
                            if (Mathf.Abs(xMovementTargetVector) > Mathf.Abs(zMovementTargetVector))
                            {
                                Vector3 temp = TargetController.transform.position;
                                temp.x += xMovementTargetVector;
                                temp.z += zMovementTargetVector;
                                Vector3 lookDirection = temp - TargetController.transform.position;
                                if (lookDirection != Vector3.zero)
                                {
                                    TargetController.transform.Rotate(0, xMovementTargetVector * rotateSensitivity, 0);
                                }
                            }

                            moveDirection = TargetController.transform.forward * MoveSpeed * moveSensitivity * 100 * Time.deltaTime;
                            moveDirection.y -= gravity * Time.deltaTime;
                            TargetController.Move(moveDirection * Time.deltaTime);

                        }
                        */
                        if (VectorStyle == CVectorStyle.StyleRPG)
                        {

                            float tempAngle = Mathf.Atan2(zMovementTargetVector, xMovementTargetVector);
                            xMovementTargetVector *= Mathf.Abs(Mathf.Cos(tempAngle));
                            zMovementTargetVector *= Mathf.Abs(Mathf.Sin(tempAngle));

                            TargetVectorInput = new Vector3(xMovementTargetVector, 0, zMovementTargetVector);
                            TargetVectorInput = TargetController.transform.TransformDirection(TargetVectorInput);
                            TargetVectorInput *= MoveSpeed;

                            // rotate the player to face the direction of input
                            Vector3 temp = TargetController.transform.position;
                            temp.x += xMovementTargetVector;
                            temp.z += zMovementTargetVector;
                            Vector3 lookDirection = temp - TargetController.transform.position;
                            if (lookDirection != Vector3.zero)
                            {
                                TargetController.transform.localRotation = Quaternion.Slerp(TargetController.transform.localRotation, Quaternion.LookRotation(lookDirection), RotateSpeed * Time.deltaTime);
                            }

                            moveDirection = TargetController.transform.forward * MoveSpeed * 100 * Time.deltaTime;
                            moveDirection.y -= gravity * Time.deltaTime;
                            TargetController.Move(moveDirection * Time.deltaTime);
                        }
                        //== LEFT MOVEMENT END
                    }
                    else
                    {
                        moveDirection = Vector3.zero;
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);
                    }

                    SyncronizeController();

                }

                //=============================================================================================== COMMAND: MODE
                if (InputType == CInputType.Command)
                {

                        if (currentJump < maxJump - 1)
                        {
                            if (VerticalCommand.CurrentValue == VarConstant.JUMP)
                            {
                                moveDirection.y = jumpSpeed;
                                currentJump++;
                            }
                        }

                        if (TargetController.isGrounded && MovementEnabled)
                        {
                            moveDirection = Vector3.zero;
                            currentJump = 0;

                            if (VerticalCommand.CurrentValue == VarConstant.FORWARD)
                            {

                                Quaternion temp = TargetCamera.transform.rotation;
                                temp.x = 0;
                                temp.z = 0;
                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, temp, Time.time * TurnSpeed);
                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (VerticalCommand.CurrentValue == VarConstant.STOP)
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (VerticalCommand.CurrentValue == VarConstant.BACKWARD)
                            {

                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 180;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (VerticalCommand.CurrentValue == VarConstant.STOP)
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (HorizontalCommand.CurrentValue == VarConstant.LEFT)
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 270;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (HorizontalCommand.CurrentValue == VarConstant.STOP)
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (HorizontalCommand.CurrentValue == VarConstant.RIGHT)
                            {
                                Vector3 temp = TargetCamera.transform.eulerAngles;

                                temp.x = 0;
                                temp.y += 90;
                                temp.z = 0;

                                TargetController.transform.rotation = Quaternion.Lerp(TargetController.transform.rotation, Quaternion.Euler(temp), Time.time * TurnSpeed);

                                moveDirection = Vector3.forward;
                                moveDirection = TargetController.transform.TransformDirection(moveDirection);
                                moveDirection *= MoveSpeed;
                            }
                            else if (HorizontalCommand.CurrentValue == VarConstant.STOP)
                            {
                                moveDirection = Vector3.zero;
                            }

                            if (SpecialCommand.CurrentValue == VarConstant.JUMP)
                                moveDirection.y = jumpSpeed;

                        }
                        moveDirection.y -= gravity * Time.deltaTime;
                        TargetController.Move(moveDirection * Time.deltaTime);

                    SyncronizeController();
                }

            }
        }

        KeyCode GetSelectionKey()
        {
            KeyCode Result = KeyCode.None;
            if (ClickSelection == CClickType.LeftMouse) Result = KeyCode.Mouse0;
            if (ClickSelection == CClickType.RightMouse) Result = KeyCode.Mouse1;
            if (ClickSelection == CClickType.MiddleMouse) Result = KeyCode.Mouse2;
            if (ClickSelection == CClickType.Touch) Result = KeyCode.Mouse0;
            return Result;
        }

        KeyCode GetTriggerKey()
        {
            KeyCode Result = KeyCode.None;
            if (ClickTrigger == CClickType.LeftMouse) Result = KeyCode.Mouse0;
            if (ClickTrigger == CClickType.RightMouse) Result = KeyCode.Mouse1;
            if (ClickTrigger == CClickType.MiddleMouse) Result = KeyCode.Mouse2;
            if (ClickTrigger == CClickType.Touch) Result = KeyCode.Mouse0;
            return Result;
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {

            myAngle = Vector3.Angle(Vector3.up, hit.normal);
            Debug.Log("Angle: " + myAngle);
        }

        float GetAxisHorizontal()
        {
            float result = 0;
            if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
            {
                result = -1;
            }
            if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
            {
                result = 1;
            }
            return result;
        }

        float GetAxisVertical()
        {
            float result = 0;
            if (Input.GetKey(PrimaryKeyboardKey.UpKey) || Input.GetKey(AltKeyboardKey.UpKey))
            {
                result = 1;
            }
            if (Input.GetKey(PrimaryKeyboardKey.DownKey) || Input.GetKey(AltKeyboardKey.DownKey))
            {
                result = -1;
            }
            return result;
        }

        public void SetActive(bool aValue)
        {
            isEnabled = aValue;
        }

        CMovementQuadran SwipeValue()
        {
            CMovementQuadran result = CMovementQuadran.None;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        FirstTouch = touch.position;
                        break;

                    case TouchPhase.Moved:
                        SecondTouch = touch.position;
                        Direction = SecondTouch - FirstTouch;
                        break;

                    case TouchPhase.Ended:
                        if (Direction.x >= 0 && Direction.y >= 0)
                        {
                            result = CMovementQuadran.UpRight;
                        }
                        if (Direction.x >= 0 && Direction.y < 0)
                        {
                            result = CMovementQuadran.DownRight;
                        }
                        if (Direction.x < 0 && Direction.y >= 0)
                        {
                            result = CMovementQuadran.UpLeft;
                        }
                        if (Direction.x < 0 && Direction.y < 0)
                        {
                            result = CMovementQuadran.DownLeft;
                        }

                        break;
                }
            }

            return result;
        }

        public bool GetIsAttack()
        {
            return isAttack;
        }

        public bool GetIsMoving()
        {
            return isMoving;
        }

        public void ForceStopMechanim()
        {
            isMoving = false;
            initialValue = true;
            Destination = TargetController.transform.position;
        }

        public string GetRaycastTag()
        {
            return RaycastTag;
        }

        public string GetRaycastName()
        {
            return RaycastName;
        }

        public void SetMoveSpeed(float aValue)
        {
            staticSpeed = aValue;
        }

        public void SetAlterSpeed(float aValue)
        {
            AlterSpeed = aValue;
        }

        public void SetRotateSpeed(float aValue)
        {
            RotateSpeed = aValue;
        }

        public void SetTurnSpeed(float aValue)
        {
            TurnSpeed = aValue;
        }

        public void SetjumpSpeed(float aValue)
        {
            jumpSpeed = aValue;
        }

        public void SetmaxJump(int aValue)
        {
            maxJump = aValue;
        }

    }
}
