/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk perubahan posisi karakter menggunakan Keyboard (Versi 2D)
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class SpritePositionController : MonoBehaviour
    {

        public enum CInputType { None, Keyboard, Mouse, Vector }

        public enum CKeyboardStyle { None, StyleSSP, StyleFLB, StyleELR, StyleSTW, StyleTDS, StyleJRPG }
        public enum CVectorStyle { None, StyleSSP, StyleFLB, StyleELR, StyleSTW, StyleTDS, StyleJRPG }
        public enum CMouseStyle { None, StyleTDS }

        public enum CFilterSelection { Everything, ByName, ByTag }

        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CMovementQuadran { None, UpLeft, UpRight, DownLeft, DownRight }

        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Movement Type")]
        public CInputType InputType;
        public KeyboardController TargetKeyboardController;

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
        public Vector2 ScreenPosition;
        public Vector3 SpritePosition;

        [Header("Character Controller")]
        public Rigidbody2D TargetController;
        public SpriteRenderer TargetSprite;
        public Camera TargetCamera;
        [Tag] public string MainCameraTag;

        [Header("Movement Style")]
        public CKeyboardStyle KeyboardStyle;

        [Header("Movement Style")]
        public CMouseStyle MouseStyle;

        [Header("Movement Style")]
        public CVectorStyle VectorStyle;
        public VarVector3 TargetVector;

        [Header("Movement Settings")]
        public float CrouchSpeed = 1;
        public float WalkSpeed = 2;
        public float RunSpeed = 3;
        public float MoveSpeed = 3;
        public float ConstantSpeed = 0;
        public float StopDistance = 0.2f;
        public bool MouseFaceDirection;
        public bool FlipX;
        public bool FlipY;
        [ReadOnly] public bool isAttack;
        [ReadOnly] public bool isMoving;


        [Header("Jump Settings")]
        public float JumpSpeed = 8.0F;
        public int MaxJump = 1;
        [ReadOnly] public int CurrentJump;

        [System.Serializable]
        public class CSpriteCollider
        {
            public SpriteColliderController SpriteController;

            [Header("Collision Settings")]
            public bool usingCollisionTag;
            public VarString VarCollisionTag;

            [Header("Trigger Settings")]
            public bool usingTriggerTag;
            public VarString VarTriggerTag;

            [Header("FlipX Settings")]
            public bool usingFlipX;
            public VarBoolean VarFlipX;

            [Header("FlipY Settings")]
            public bool usingFlipY;
            public VarBoolean VarFlipY;
        }

        [Header("Sprite Settings")]
        public CSpriteCollider SpriteCollider;

        [Header("Platform Settings")]
        [Tag] public List<string> GroundTag;
        public LayerMask GroundLayer;
        public Transform GroundCheck;
        public Transform CeilingCheck;
        public float GroundedRadius = 1f;
        public float GroundedFilter = 1f;
        public bool GroundedStatus;

        [Header("Readonly Mouse Value")]
        [ReadOnly] public string RaycastTag;
        [ReadOnly] public string RaycastName;
        [ReadOnly] public GameObject RaycastObject;
        [ReadOnly] public int DownStatus;
        [ReadOnly] public float CurrentDistance;

        private Vector3 moveDirection = Vector3.zero;
        bool initialValue = true; 
        float lastDistance;
        float MultipleValue = 10f;

        Vector3 Destination;
        [HideInInspector]
        public CMovementQuadran MovementQuadran;

        void Awake()
        {
            //-- freeze scale
            transform.localScale = new Vector3(1, 1, 1);
            TargetController.transform.localScale = new Vector3(1, 1, 1);
        }

        // Start is called before the first frame update
        void Start()
        {
            GroundedStatus = false;

            if (KeyboardStyle == CKeyboardStyle.StyleTDS || KeyboardStyle == CKeyboardStyle.StyleSTW || 
                KeyboardStyle == CKeyboardStyle.StyleJRPG)
            {
                TargetController.GetComponent<Rigidbody2D>().gravityScale = 0;
                TargetController.GetComponent<Rigidbody2D>().freezeRotation = true;
            } 
            else if (VectorStyle == CVectorStyle.StyleTDS || VectorStyle == CVectorStyle.StyleSTW ||
                VectorStyle == CVectorStyle.StyleJRPG)
            {
                TargetController.GetComponent<Rigidbody2D>().gravityScale = 0;
                TargetController.GetComponent<Rigidbody2D>().freezeRotation = true;
            }

            if (isEnabled && InputType == CInputType.Mouse)
            {
                isMoving = false;
                Destination = TargetController.transform.position;
                MovementQuadran = CMovementQuadran.None;
            }

            InputSyncronize();
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

        public void SetToCrouchSpeed()
        {
            MoveSpeed = CrouchSpeed;
        }

        public void SetToWalkSpeed()
        {
            MoveSpeed = WalkSpeed;
        }

        public void SetToRunSpeed()
        {
            MoveSpeed = RunSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled && TargetHealth.CurrentValue > 0)
            {

                if (SpriteCollider.usingFlipX)
                {
                    SpriteCollider.VarFlipX.CurrentValue = TargetSprite.flipX;
                }
                if (SpriteCollider.usingFlipY)
                {
                    SpriteCollider.VarFlipY.CurrentValue = TargetSprite.flipY;
                }

                //=============================================================================================== KEYBOARD: MODE
                if (InputType == CInputType.Keyboard)
                {
                    if (KeyboardStyle == CKeyboardStyle.StyleSSP)
                    {
                        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
                        // This can be done using layers instead but Sample Assets will not overwrite your project settings.

                        string groundTag = SpriteCollider.SpriteController.CollisionTag;
                        for (int i = 0; i < GroundTag.Count; i++)
                        {
                            if (groundTag == GroundTag[i])
                            {
                                GroundedStatus = true;
                                CurrentJump = 0;
                            }
                        }

                        if (SpriteCollider.usingCollisionTag)
                        {
                            SpriteCollider.VarCollisionTag.CurrentValue = SpriteCollider.SpriteController.CollisionTag;
                            
                        }

                        if (Input.GetKey(PrimaryKeyboardKey.LeftKey) || Input.GetKey(AltKeyboardKey.LeftKey))
                        {
                            TargetController.velocity = new Vector2(-MoveSpeed, TargetController.velocity.y);
                            TargetSprite.flipX = FlipX;
                        }
                        if (Input.GetKey(PrimaryKeyboardKey.RightKey) || Input.GetKey(AltKeyboardKey.RightKey))
                        {
                            TargetController.velocity = new Vector2(MoveSpeed, TargetController.velocity.y);
                            TargetSprite.flipX = false;
                        }
                        if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                        {
                            if (CurrentJump < MaxJump)
                            {
                                TargetController.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
                                GroundedStatus = false;
                                CurrentJump++;
                            }
                        }
                    }

                    else if (KeyboardStyle == CKeyboardStyle.StyleELR)
                    {
                        string groundTag = SpriteCollider.SpriteController.CollisionTag;
                        for (int i = 0; i < GroundTag.Count; i++)
                        {
                            if (groundTag == GroundTag[i])
                            {
                                GroundedStatus = true;
                                CurrentJump = 0;
                            }
                        }

                        if (SpriteCollider.usingCollisionTag)
                        {
                            SpriteCollider.VarCollisionTag.CurrentValue = SpriteCollider.SpriteController.CollisionTag;

                        }

                        if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                        {
                            TargetController.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
                            GroundedStatus = false;
                        }

                        TargetController.velocity = new Vector2(MultipleValue * ConstantSpeed * Time.deltaTime, TargetController.velocity.y);
                        TargetSprite.flipX = false;
                    }

                    else if (KeyboardStyle == CKeyboardStyle.StyleFLB)
                    {
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayer);
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            if (colliders[i].gameObject != gameObject)
                                GroundedStatus = true;
                        }
                        if (Input.GetKeyDown(PrimaryKeyboardKey.JumpKey) || Input.GetKeyDown(AltKeyboardKey.JumpKey))
                        {
                            TargetController.velocity = new Vector2(TargetController.velocity.x, JumpSpeed);
                        }
                        TargetController.velocity = new Vector2(MultipleValue * ConstantSpeed * Time.deltaTime, TargetController.velocity.y);
                        TargetSprite.flipX = false;
                    }

                    else if (KeyboardStyle == CKeyboardStyle.StyleSTW)
                    {
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayer);
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            if (colliders[i].gameObject != gameObject)
                                GroundedStatus = true;
                        }
                        if (GetAxisHorizontal() != 0)
                        {
                            if (GetAxisHorizontal() > 0)
                            {
                                Vector2 movement = new Vector2(GetAxisHorizontal(), 0);
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipX = false;
                            }
                            else if (GetAxisHorizontal() < 0)
                            {
                                Vector2 movement = new Vector2(GetAxisHorizontal(), 0);
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipX = FlipX;
                            }
                        }
                        if (GetAxisVertical() != 0)
                        {
                            if (GetAxisVertical() > 0)
                            {
                                Vector2 movement = new Vector2(0, GetAxisVertical());
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipY = FlipY;
                            }
                            else if (GetAxisVertical() < 0)
                            {
                                Vector2 movement = new Vector2(0, GetAxisVertical());
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipY = false;
                            }
                        }

                        //-- Constant Speed
                        TargetController.velocity = new Vector2(TargetController.velocity.x * Time.deltaTime, MultipleValue * ConstantSpeed * Time.deltaTime);
                        TargetSprite.flipX = false;
                    }

                    else if (KeyboardStyle == CKeyboardStyle.StyleTDS)
                    {
                        Vector2 movement = new Vector2(GetAxisHorizontal(), GetAxisVertical());
                        TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);

                        if (GetAxisHorizontal() > 0)
                        {
                            TargetSprite.flipX = false;
                        }
                        else if (GetAxisHorizontal() < 0)
                        {
                            TargetSprite.flipX = FlipX;
                        }

                        if (GetAxisVertical() > 0)
                        {
                             TargetSprite.flipY = FlipY;
                        }
                        else if (GetAxisVertical() < 0)
                        {
                             TargetSprite.flipY = false;
                        }

                        if (MouseFaceDirection)
                        {
                            var dir = Input.mousePosition - TargetCamera.WorldToScreenPoint(TargetSprite.transform.position);
                            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                            TargetSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        }
                    }
                    else if (KeyboardStyle == CKeyboardStyle.StyleJRPG)
                    {
                        if (GetAxisHorizontal() != 0)
                        {
                            if (GetAxisHorizontal() > 0)
                            {
                                Vector2 movement = new Vector2(GetAxisHorizontal(), 0);
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipX = false;
                            } else if (GetAxisHorizontal() < 0)
                            {
                                Vector2 movement = new Vector2(GetAxisHorizontal(), 0);
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipX = FlipX;
                            }
                        }
                        if (GetAxisVertical() != 0)
                        {
                            if (GetAxisVertical() > 0)
                            {
                                Vector2 movement = new Vector2(0, GetAxisVertical());
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipY = FlipY; 
                            }
                            else if (GetAxisVertical() < 0)
                            {
                                Vector2 movement = new Vector2(0, GetAxisVertical());
                                TargetController.MovePosition(TargetController.position + movement * MoveSpeed * Time.deltaTime);
                                TargetSprite.flipY = false;
                            }
                        }
                    }
                }

                //=============================================================================================== MOUSE: MODE
                if (InputType == CInputType.Mouse)
                {
                    if (MouseStyle == CMouseStyle.StyleTDS)
                    {
                        if (MouseFaceDirection)
                        {
                            var dir = Input.mousePosition - TargetCamera.WorldToScreenPoint(TargetSprite.transform.position);
                            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                            TargetSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        }

                        if (Input.GetKeyDown(GetTriggerKey()))
                        {
                            ScreenPosition.x = Input.mousePosition.x;
                            ScreenPosition.y = Input.mousePosition.y;
                            SpritePosition = TargetCamera.ScreenToWorldPoint(ScreenPosition);
                            Destination = SpritePosition;
                        }

                        if (Destination.x < TargetSprite.transform.position.x && FlipX)
                        {
                            TargetSprite.flipX = FlipX;
                        }
                        else if (Destination.x >= TargetSprite.transform.position.x)
                        {
                            TargetSprite.flipX = false;
                        }

                        if (Destination.y < TargetSprite.transform.position.y && FlipY)
                        {
                            TargetSprite.flipY = FlipY;
                        }
                        else if (Destination.y >= TargetSprite.transform.position.y)
                        {
                            TargetSprite.flipY = false;
                        }

                        CurrentDistance = Vector2.Distance(TargetController.transform.position, SpritePosition);
                        if (CurrentDistance > StopDistance)
                        {
                            TargetController.transform.position = Vector2.MoveTowards(TargetController.transform.position, SpritePosition, MoveSpeed * Time.deltaTime);
                            isMoving = true;
                            isAttack = false;
                        } else
                        {
                            isMoving = false;
                        }
                    }
                }

                //=============================================================================================== VECTOR: MODE
                if (InputType == CInputType.Vector)
                {
                    if (VectorStyle == CVectorStyle.StyleSSP)
                    {
                        if (TargetVector.CurrentValue.x < 0)
                        {
                            TargetSprite.flipX = FlipX;
                            TargetController.velocity = new Vector2(TargetVector.CurrentValue.x * MultipleValue * MoveSpeed * Time.deltaTime, TargetController.velocity.y);
                        }
                        if (TargetVector.CurrentValue.x > 0)
                        {
                            TargetSprite.flipX = false;
                            TargetController.velocity = new Vector2(TargetVector.CurrentValue.x * MultipleValue * MoveSpeed * Time.deltaTime, TargetController.velocity.y);
                        }
                        if (TargetVector.CurrentValue.x == 0)
                        {
                            TargetController.velocity = new Vector2(0, TargetController.velocity.y);
                        }


                        if (TargetVector.CurrentValue.y != 0) {
                            
                            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayer);
                            for (int i = 0; i < colliders.Length; i++)
                            {
                                if (colliders[i].gameObject != gameObject)
                                    GroundedStatus = true;
                            }
                            RaycastHit2D hit2D = Physics2D.Raycast(TargetController.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, GroundLayer);

                            if (hit2D)
                            {
                                if (CurrentJump < MaxJump)
                                {
                                    TargetController.velocity = new Vector2(TargetController.velocity.x, JumpSpeed);
                                    GroundedStatus = false;
                                    CurrentJump++;
                                }

                                for (int i = 0; i < GroundTag.Count; i++)
                                {
                                    if (hit2D.collider.tag == GroundTag[i])
                                    {
                                        GroundedStatus = true;
                                        CurrentJump = 0;
                                    }
                                }

                                if (hit2D.distance < lastDistance)
                                {
                                    // Update the last distance if the object below is less than the last known distance
                                    lastDistance = hit2D.distance;
                                }
                            }

                            TargetVector.CurrentValue.y = 0;
                        }
                        
                    }
                    else if (VectorStyle == CVectorStyle.StyleJRPG)
                    {
                        if (TargetVector.CurrentValue.x < 0)
                        {
                            TargetSprite.flipX = FlipX;
                            TargetController.velocity = new Vector2(TargetVector.CurrentValue.x * MultipleValue * MoveSpeed * Time.deltaTime, TargetController.velocity.y);
                        }
                        if (TargetVector.CurrentValue.x > 0)
                        {
                            TargetSprite.flipX = false;
                            TargetController.velocity = new Vector2(TargetVector.CurrentValue.x * MultipleValue * MoveSpeed * Time.deltaTime, TargetController.velocity.y);
                        }
                        if (TargetVector.CurrentValue.x == 0)
                        {
                            TargetController.velocity = new Vector2(0, TargetController.velocity.y);
                        }
                        if (TargetVector.CurrentValue.y < 0)
                        {
                            TargetSprite.flipY = FlipY;
                            TargetController.velocity = new Vector2(TargetController.velocity.x, TargetVector.CurrentValue.y * MultipleValue * MoveSpeed * Time.deltaTime);
                        }
                        if (TargetVector.CurrentValue.y > 0)
                        {
                            TargetSprite.flipY = false;
                            TargetController.velocity = new Vector2(TargetController.velocity.x, TargetVector.CurrentValue.y * MultipleValue * MoveSpeed * Time.deltaTime);
                        }
                        if (TargetVector.CurrentValue.y == 0)
                        {
                            TargetController.velocity = new Vector2(TargetController.velocity.x, 0);
                        }
                    }
                }
            }
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
    }
}
