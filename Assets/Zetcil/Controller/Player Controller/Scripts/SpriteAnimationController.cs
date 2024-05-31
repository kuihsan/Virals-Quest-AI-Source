using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;
using UnityEngine.Events;

namespace Zetcil
{
    public class SpriteAnimationController : MonoBehaviour
    {
        public enum CForceTrigger { TriggerByIndex, TriggerByKey }
        public enum CInputType { None, Keyboard, Mouse, Vector, Command }
        public enum CParameterType { Int, Float, Bool, Trigger }
        public enum CMouseType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CBulletOrientation { VectorForward, VectorLeftRight, VectorUpDown }

        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Animation Type")]
        public CInputType InputType;

        [System.Serializable]
        public class CParameterAnimation
        {
            public CParameterType Type;
            public string Name;
        }

        [Header("Animation Settings")]
        public Rigidbody2D TargetController;
        public Animator TargetAnimator;
        public SpritePositionController MousePositionController;

        [System.Serializable]
        public class CMovingKeyboardState2D
        {
            [Header("Keyboard Settings")]
            public KeyboardController KeyboardKey;

            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool ForceAnimation;
            public bool usingHoldTriggerKey;
            [SearchableEnum] public KeyCode HoldTriggerKey;

            [Header("Sound Settings")]
            public bool usingSound;
            public AudioSource animaAudioSource;
            public AudioClip animaAudioClip;
        }

        [System.Serializable]
        public class CKeyboardBullet2D
        {
            public bool isEnabled;
            public CForceTrigger TriggerMode;
            [Header("Bullet Settings")]
            public GameObject BulletObject;
            public GameObject BulletPosition;
            public int ExecuteDelay;
            public int DestroyDelay;

            [Header("Bullet Orientation")]
            public CBulletOrientation BulletOrientation;

            [HideInInspector]
            public bool isCooldown = false;
        }

        [System.Serializable]
        public class CActionKeyboardState2D
        {
            [Header("Keyboard Settings")]
            [SearchableEnum] public KeyCode[] TriggerKey;

            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool ForceAnimation;
            public bool FlipAnimation;
            public bool usingHoldTriggerKey;
            [SearchableEnum] public KeyCode HoldTriggerKey;

            [Header("Ranged Attack Settings")]
            public bool usingRangedAttack;
            public int ActiveBulletIndex = 0;
            public CKeyboardBullet2D[] Bullet2D;

            [Header("Autolook Settings")]
            public bool usingAutolook;
            [Tag]
            public string LookTargetTag;
            public float LookRange;

            [Header("Sound Settings")]
            public bool usingSound;
            public AudioSource animaAudioSource;
            public AudioClip animaAudioClip;
        }

        [System.Serializable]
        public class CShutdownKeyboardState2D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [SearchableEnum] public KeyCode[] ShutdownTestkey;

            [Header("Sound Settings")]
            public bool usingSound;
            public AudioSource animaAudioSource;
            public AudioClip animaAudioClip;
        }

        [Header("Moving Settings")]
        public bool usingMovingKeyboardState2D;
        public CMovingKeyboardState2D[] MovingKeyboardState2D;

        [Header("Action Settings")]
        public bool usingActionKeyboardState2D;
        public CActionKeyboardState2D[] ActionKeyboardState2D;

        [Header("Shutdown Settings")]
        public bool usingShutdownKeyboardState2D;
        public CShutdownKeyboardState2D ShutdownKeyboardState2D;

        [System.Serializable]
        public class CMovingMouseState2D
        {
            [Header("Mouse Settings")]
            public CMouseType TriggerKey;

            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            public KeyCode GetTriggerKey()
            {
                KeyCode Result = KeyCode.None;
                if (TriggerKey == CMouseType.LeftMouse) Result = KeyCode.Mouse0;
                if (TriggerKey == CMouseType.RightMouse) Result = KeyCode.Mouse1;
                if (TriggerKey == CMouseType.MiddleMouse) Result = KeyCode.Mouse2;
                if (TriggerKey == CMouseType.Touch) Result = KeyCode.Mouse0;
                return Result;
            }

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CActionMouseState2D
        {
            [Header("Mouse Settings")]
            [SearchableEnum] public KeyCode[] TriggerKey;

            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Action Status Settings")]
            public bool usingActionStatus;
            public VarBoolean ActionStatus;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CShutdownMouseState2D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Shutdown Settings")]
            [SearchableEnum] public KeyCode[] ShutdownTestkey;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [Header("Moving Settings")]
        public bool usingMovingMouseState2D;
        public CMovingMouseState2D[] MovingMouseState2D;

        [Header("Action Settings")]
        public bool usingActionMouseState2D;
        public CActionMouseState2D[] ActionMouseState2D;

        [Header("Shutdown Settings")]
        public bool usingShutdownMouseState2D;
        public CShutdownMouseState2D ShutdownMouseState2D;

        [System.Serializable]
        public class CMovingVectorState2D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }


        [System.Serializable]
        public class CActionVectorState2D
        {
            [Header("Vector Settings")]
            public VarBoolean[] TriggerKey;
            
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CShutdownVectorState2D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterAnimation Parameter;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Shutdown Settings")]
            [SearchableEnum] public KeyCode[] ShutdownTestkey;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [Header("Vector Settings")]
        public bool isVectorActive;
        public VarVector3 TargetVector;
        private Vector3 TargetVectorInput;

        [Header("Moving Settings")]
        public bool usingMovingVectorState2D;
        public CMovingVectorState2D[] MovingVectorState2D;

        [Header("Action Settings")]
        public bool usingActionVectorState2D;
        public CActionVectorState2D[] ActionVectorState2D;

        [Header("Shutdown Settings")]
        public bool usingShutdownVectorState2D;
        public CShutdownVectorState2D ShutdownVectorState2D;

        [System.Serializable]
        public class CColliderState
        {
            [Header("Delay Collider Settings")]
            public float DelayCollider;

            [Header("Box Collider Settings")]
            public bool enabledBoxCollider;
            public BoxCollider2D[] BoxColliders;
            [Header("Sphere Collider Settings")]
            public bool enabledCircleCollider;
            public CircleCollider2D[] CircleColliders;
            [Header("Capsule Collider Settings")]
            public bool enabledCapsuleCollider;
            public CapsuleCollider2D[] CapsuleColliders;
        }


        [Header("Collider Settings")]
        public bool usingColliderState;
        public CColliderState ColliderState;

        bool MovementEnabled = true;
        bool ShutdownStatus = false;
        bool isMoving;
        bool isAttack;

        // Use this for initialization
        void Awake()
        {
            DisabledColliders();
            if (isEnabled)
            {
                TargetAnimator.enabled = isEnabled;
            }
        }

        void DisabledColliders()
        {
            if (usingColliderState)
            {
                if (ColliderState.enabledBoxCollider)
                {
                    for (int i = 0; i < ColliderState.BoxColliders.Length; i++)
                    {
                        ColliderState.BoxColliders[i].enabled = false;
                    }
                    for (int i = 0; i < ColliderState.CircleColliders.Length; i++)
                    {
                        ColliderState.CircleColliders[i].enabled = false;
                    }
                    for (int i = 0; i < ColliderState.CapsuleColliders.Length; i++)
                    {
                        ColliderState.CapsuleColliders[i].enabled = false;
                    }
                }
            }
        }

        void EnabledColliders()
        {
            if (usingColliderState)
            {
                if (ColliderState.enabledBoxCollider)
                {
                    for (int i = 0; i < ColliderState.BoxColliders.Length; i++)
                    {
                        ColliderState.BoxColliders[i].enabled = true;
                    }
                    for (int i = 0; i < ColliderState.CircleColliders.Length; i++)
                    {
                        ColliderState.CircleColliders[i].enabled = true;
                    }
                    for (int i = 0; i < ColliderState.CapsuleColliders.Length; i++)
                    {
                        ColliderState.CapsuleColliders[i].enabled = true;
                    }
                }
                Invoke("DisabledColliders", ColliderState.DelayCollider);
            }
        }


        // Update is called once per frame
        void FixedUpdate()
        {

            if (isEnabled && TargetHealth.CurrentValue > 0)
            {

                //=============================================================================================== KEYBOARD: MODE
                if (InputType == CInputType.Keyboard)
                {
                    if (isEnabled && usingMovingKeyboardState2D)
                    {
                        for (int i = 0; i < MovingKeyboardState2D.Length; i++)
                        {
                            if (MovingKeyboardState2D[i].KeyboardKey.TriggerKeyPress())
                            {
                                if (MovingKeyboardState2D[i].usingHoldTriggerKey && Input.GetKey(MovingKeyboardState2D[i].HoldTriggerKey))
                                {
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        MovingKeyboardState2D[i].PositiveValue = MovingKeyboardState2D[i].TransitionValue;
                                        float dummyvalue = float.Parse(MovingKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        MovingKeyboardState2D[i].PositiveValue = MovingKeyboardState2D[i].TransitionValue;
                                        int dummyvalue = int.Parse(MovingKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        bool dummyvalue = bool.Parse(MovingKeyboardState2D[i].PositiveValue);
                                        TargetAnimator.SetBool(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(MovingKeyboardState2D[i].Parameter.Name);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                }
                                else if (!MovingKeyboardState2D[i].usingHoldTriggerKey)
                                {
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        MovingKeyboardState2D[i].PositiveValue = MovingKeyboardState2D[i].TransitionValue;
                                        float dummyvalue = float.Parse(MovingKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }

                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        MovingKeyboardState2D[i].PositiveValue = MovingKeyboardState2D[i].TransitionValue;
                                        int dummyvalue = int.Parse(MovingKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        bool dummyvalue = bool.Parse(MovingKeyboardState2D[i].PositiveValue);
                                        TargetAnimator.SetBool(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                    if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(MovingKeyboardState2D[i].Parameter.Name);
                                        if (MovingKeyboardState2D[i].ForceAnimation)
                                        {
                                            if (!TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(MovingKeyboardState2D[i].AnimationStateName))
                                            {
                                                TargetAnimator.Play(MovingKeyboardState2D[i].AnimationStateName);
                                            }
                                        }
                                        ExecuteMovingSound(i);
                                    }
                                }
                            }
                        }
                    }
                    if (isEnabled && usingActionKeyboardState2D)
                    {
                        for (int i = 0; i < ActionKeyboardState2D.Length; i++)
                        {
                            for (int j = 0; j < ActionKeyboardState2D[i].TriggerKey.Length; j++)
                            {
                                if (Input.GetKeyDown(ActionKeyboardState2D[i].TriggerKey[j]))
                                {
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        float dummyvalue = float.Parse(ActionKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i);
                                        ExecuteActionKeyboardAttack(i);
                                    }
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        int dummyvalue = int.Parse(ActionKeyboardState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i);
                                        ExecuteActionKeyboardAttack(i);
                                    }
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        bool dummyvalue = bool.Parse(ActionKeyboardState2D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i);
                                        ExecuteActionKeyboardAttack(i);
                                    }
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionKeyboardState2D[i].Parameter.Name);
                                        ExecuteActionSound(i);
                                        ExecuteActionKeyboardAttack(i);
                                    }
                                    if (ActionKeyboardState2D[i].usingAutolook)
                                    {
                                        GameObject[] tempLook = GameObject.FindGameObjectsWithTag(ActionKeyboardState2D[i].LookTargetTag);
                                        float tempNearest = 1000;
                                        int tempClosest = 0;
                                        for (int x = 0; x < tempLook.Length; x++)
                                        {
                                            float tempdist = Vector3.Distance(TargetAnimator.gameObject.transform.position, tempLook[x].transform.position);
                                            if (tempdist < ActionKeyboardState2D[i].LookRange)
                                            {
                                                if (tempdist < tempNearest)
                                                {
                                                    tempClosest = x;
                                                    tempNearest = tempdist;
                                                }
                                            }
                                        }
                                        Vector3 tempcontroller = new Vector3(tempLook[tempClosest].transform.position.x,
                                                                             TargetController.transform.position.y,
                                                                             tempLook[tempClosest].transform.position.z);
                                        TargetController.transform.LookAt(tempcontroller);
                                        //TargetAnimator.gameObject.transform.LookAt(tempLook[tempClosest].transform);

                                    }
                                    EnabledColliders();
                                }
                            }
                        }
                    }
                    if (isEnabled && usingShutdownKeyboardState2D)
                    {
                        for (int i = 0; i < ShutdownKeyboardState2D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownKeyboardState2D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }
                }

                //=============================================================================================== MOUSE: MODE
                if (InputType == CInputType.Mouse)
                {
                    if (isEnabled && usingMovingMouseState2D && MovementEnabled)
                    {
                        isMoving = MousePositionController.GetIsMoving();

                        for (int i = 0; i < MovingMouseState2D.Length; i++)
                        {
                            if (Input.GetKey(MovingMouseState2D[i].GetTriggerKey()))
                            {
                                isMoving = MousePositionController.GetIsMoving();
                            }

                            if (isMoving)
                            {
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingMouseState2D[i].PositiveValue = MovingMouseState2D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingMouseState2D[i].PositiveValue) + 1;
                                    TargetAnimator.SetFloat(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }

                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingMouseState2D[i].PositiveValue = MovingMouseState2D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingMouseState2D[i].PositiveValue) + 1;
                                    TargetAnimator.SetInteger(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingMouseState2D[i].PositiveValue = MovingMouseState2D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingMouseState2D[i].PositiveValue);
                                    TargetAnimator.SetBool(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingMouseState2D[i].Parameter.Name);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionMouseState2D)
                    {
                        isAttack = MousePositionController.GetIsAttack();

                        for (int i = 0; i < ActionMouseState2D.Length; i++)
                        {

                            if (Input.GetKey(MovingMouseState2D[i].GetTriggerKey()))
                            {
                                isAttack = MousePositionController.GetIsAttack();
                            }

                            for (int j = 0; j < ActionMouseState2D[i].TriggerKey.Length; j++)
                            {

                                bool ActionStatus = false;
                                if (ActionMouseState2D[i].usingActionStatus)
                                {
                                    ActionMouseState2D[i].ActionStatus.CurrentValue = isAttack;
                                    ActionStatus = ActionMouseState2D[i].ActionStatus.CurrentValue;
                                }
                                else
                                {
                                    ActionStatus = true;
                                }

                                //if ((Input.GetKeyUp(ActionMouseState2D[i].TriggerKey[j]) || isAttack) && IsActionReady())
                                if (isValidatedMouseAction(ActionMouseState2D[i].TriggerKey[j]) && ActionStatus && IsActionReady())
                                {
                                    if (ActionMouseState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionMouseState2D[i].PositiveValue = ActionMouseState2D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionMouseState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionMouseState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState2D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionMouseState2D[i].PositiveValue = ActionMouseState2D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionMouseState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionMouseState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState2D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionMouseState2D[i].PositiveValue = ActionMouseState2D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionMouseState2D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionMouseState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState2D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState2D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionMouseState2D[i].Parameter.Name);
                                        if (ActionMouseState2D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    EnabledColliders();
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownMouseState2D)
                    {
                        for (int i = 0; i < ShutdownMouseState2D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownMouseState2D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }

                }

                //=============================================================================================== VECTOR: MODE
                if (InputType == CInputType.Vector)
                {
                    if (isEnabled && usingMovingVectorState2D)
                    {
                        for (int i = 0; i < MovingVectorState2D.Length; i++)
                        {
                            if (TargetVector.CurrentValue != Vector3.zero)
                            {
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingVectorState2D[i].PositiveValue = MovingVectorState2D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingVectorState2D[i].PositiveValue) + 1;
                                    TargetAnimator.SetFloat(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingVectorState2D[i].PositiveValue = MovingVectorState2D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingVectorState2D[i].PositiveValue) + 1;
                                    TargetAnimator.SetInteger(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingVectorState2D[i].PositiveValue = MovingVectorState2D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingVectorState2D[i].PositiveValue);
                                    TargetAnimator.SetBool(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingVectorState2D[i].Parameter.Name);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }

                        }
                    }

                    if (isEnabled && usingActionVectorState2D)
                    {
                        for (int i = 0; i < ActionVectorState2D.Length; i++)
                        {
                            for (int j = 0; j < ActionVectorState2D[i].TriggerKey.Length; j++)
                            {
                                if (ActionVectorState2D[i].TriggerKey[j].CurrentValue)
                                {
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionVectorState2D[i].PositiveValue = ActionVectorState2D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionVectorState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionVectorState2D[i].PositiveValue = ActionVectorState2D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionVectorState2D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionVectorState2D[i].PositiveValue = ActionVectorState2D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionVectorState2D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionVectorState2D[i].Parameter.Name);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownVectorState2D)
                    {
                        for (int i = 0; i < ShutdownVectorState2D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownVectorState2D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }

                }
            }

            if (isEnabled && TargetHealth.CurrentValue <= 0)
            {
                Shutdown(true);
            }

        }

        void LateUpdate()
        {
            if (isEnabled && TargetHealth.CurrentValue > 0)
            {
                if (InputType == CInputType.Keyboard)
                {
                    if (isEnabled && usingMovingKeyboardState2D)
                    {
                        for (int i = 0; i < MovingKeyboardState2D.Length; i++)
                        {
                            if (MovingKeyboardState2D[i].KeyboardKey.TriggerKeyUp())
                            {
                                if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingKeyboardState2D[i].NegativeValue = MovingKeyboardState2D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingKeyboardState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                    ExecuteMovingSound(i, false);
                                }
                                if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingKeyboardState2D[i].NegativeValue = MovingKeyboardState2D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingKeyboardState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                    ExecuteMovingSound(i, false);
                                }
                                if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingKeyboardState2D[i].NegativeValue = MovingKeyboardState2D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingKeyboardState2D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingKeyboardState2D[i].Parameter.Name, dummyvalue);
                                    ExecuteMovingSound(i, false);
                                }
                                if (MovingKeyboardState2D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingKeyboardState2D[i].Parameter.Name);
                                    ExecuteMovingSound(i, false);
                                }
                            }
                        }
                    }
                    if (isEnabled && usingActionKeyboardState2D)
                    {
                        for (int i = 0; i < ActionKeyboardState2D.Length; i++)
                        {
                            for (int j = 0; j < ActionKeyboardState2D[i].TriggerKey.Length; j++)
                            {
                                if (Input.GetKeyUp(ActionKeyboardState2D[i].TriggerKey[j]))
                                {
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        float dummyvalue = float.Parse(ActionKeyboardState2D[i].NegativeValue);
                                        TargetAnimator.SetFloat(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i, false);
                                    }
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        int dummyvalue = int.Parse(ActionKeyboardState2D[i].NegativeValue);
                                        TargetAnimator.SetInteger(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i, false);
                                    }
                                    if (ActionKeyboardState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        bool dummyvalue = bool.Parse(ActionKeyboardState2D[i].NegativeValue);
                                        TargetAnimator.SetBool(ActionKeyboardState2D[i].Parameter.Name, dummyvalue);
                                        ExecuteActionSound(i, false);
                                    }
                                }
                            }
                        }
                    }
                }

                if (InputType == CInputType.Mouse)
                {
                    if (isEnabled && usingMovingMouseState2D)
                    {
                        for (int i = 0; i < MovingMouseState2D.Length; i++)
                        {
                            if (!isMoving)
                            {
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingMouseState2D[i].NegativeValue = MovingMouseState2D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingMouseState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingMouseState2D[i].NegativeValue = MovingMouseState2D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingMouseState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingMouseState2D[i].NegativeValue = MovingMouseState2D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingMouseState2D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingMouseState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState2D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingMouseState2D[i].Parameter.Name);
                                    if (MovingMouseState2D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState2D[i].AdditionalEvent.Invoke();
                                    }
                                }

                            }
                        }
                    }
                }

                if (InputType == CInputType.Vector)
                {
                    if (isEnabled && usingMovingVectorState2D)
                    {
                        for (int i = 0; i < MovingVectorState2D.Length; i++)
                        {
                            if (TargetVector.CurrentValue == Vector3.zero)
                            {
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingVectorState2D[i].NegativeValue = MovingVectorState2D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingVectorState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingVectorState2D[i].NegativeValue = MovingVectorState2D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingVectorState2D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingVectorState2D[i].NegativeValue = MovingVectorState2D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingVectorState2D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingVectorState2D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState2D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingVectorState2D[i].Parameter.Name);
                                    if (MovingVectorState2D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState2D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }

                        }
                    }

                    if (isEnabled && usingActionVectorState2D)
                    {
                        for (int i = 0; i < ActionVectorState2D.Length; i++)
                        {
                            for (int j = 0; j < ActionVectorState2D[i].TriggerKey.Length; j++)
                            {
                                if (!ActionVectorState2D[i].TriggerKey[j].CurrentValue)
                                {
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionVectorState2D[i].NegativeValue = ActionVectorState2D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionVectorState2D[i].NegativeValue) - 1;
                                        TargetAnimator.SetFloat(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionVectorState2D[i].NegativeValue = ActionVectorState2D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionVectorState2D[i].NegativeValue) - 1;
                                        TargetAnimator.SetInteger(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState2D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionVectorState2D[i].NegativeValue = ActionVectorState2D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionVectorState2D[i].NegativeValue);
                                        TargetAnimator.SetBool(ActionVectorState2D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState2D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState2D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownVectorState2D)
                    {
                        for (int i = 0; i < ShutdownVectorState2D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownVectorState2D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }

                }

            }
        }

        void ExecuteMovingSound(int index, bool plays = true)
        {
            if (index <= MovingKeyboardState2D.Length - 1)
            {

                if (MovingKeyboardState2D[index].usingSound)
                {
                    if (!MovingKeyboardState2D[index].animaAudioSource.isPlaying && plays)
                    {
                        MovingKeyboardState2D[index].animaAudioSource.clip = MovingKeyboardState2D[index].animaAudioClip;
                        MovingKeyboardState2D[index].animaAudioSource.Play();
                    }
                    else if (!plays)
                    {
                        MovingKeyboardState2D[index].animaAudioSource.clip = MovingKeyboardState2D[index].animaAudioClip;
                        MovingKeyboardState2D[index].animaAudioSource.Stop();
                    }
                }
            }
        }

        void ExecuteActionSound(int index, bool plays = true)
        {
            if (index <= ActionKeyboardState2D.Length - 1)
            {
                if (ActionKeyboardState2D[index].usingSound)
                {
                    if (!ActionKeyboardState2D[index].animaAudioSource.isPlaying && plays)
                    {
                        ActionKeyboardState2D[index].animaAudioSource.clip = ActionKeyboardState2D[index].animaAudioClip;
                        ActionKeyboardState2D[index].animaAudioSource.Play();
                    }
                    else if (!plays)
                    {
                        ActionKeyboardState2D[index].animaAudioSource.clip = ActionKeyboardState2D[index].animaAudioClip;
                        ActionKeyboardState2D[index].animaAudioSource.Stop();
                    }
                }
            }
        }

        int keyboard_index;
        int mouse_index;
        int Vector_index;

        void ExecuteActionKeyboardAttack(int index)
        {
            if (index < ActionKeyboardState2D.Length && ActionKeyboardState2D[index].usingRangedAttack)
            {
                if (ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].TriggerMode == CForceTrigger.TriggerByIndex &&
                    !ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isCooldown)
                {
                    if (ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isEnabled &&
                        Input.GetKey(ActionKeyboardState2D[index].TriggerKey[0]) &&
                        !ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isCooldown)
                    {
                        keyboard_index = index;

                        ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isCooldown = true;
                        Invoke("KeyboardCooldown", ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].ExecuteDelay - 1);
                        Invoke("KeyboardExecuteShooter", ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].ExecuteDelay);
                    }
                }
                if (ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].TriggerMode == CForceTrigger.TriggerByKey)
                {
                    for (int i = 0; i < ActionKeyboardState2D[index].Bullet2D.Length; i++)
                    {
                        if (ActionKeyboardState2D[index].Bullet2D[i].isEnabled && Input.GetKey(ActionKeyboardState2D[index].TriggerKey[0]) &&
                            !ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isCooldown)
                        {
                            ActionKeyboardState2D[index].Bullet2D[ActionKeyboardState2D[index].ActiveBulletIndex].isCooldown = true;
                            ActionKeyboardState2D[index].ActiveBulletIndex = i;

                            keyboard_index = index;

                            Invoke("KeyboardExecuteShooter", ActionKeyboardState2D[index].Bullet2D[i].ExecuteDelay);
                            Invoke("KeyboardCooldown", 1f);
                        }
                    }
                }
            }
        }

        void KeyboardExecuteShooter()
        {
            if (ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletOrientation == CBulletOrientation.VectorForward)
            {
                GameObject temp = GameObject.Instantiate(ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletObject,
                                                         ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletPosition.transform.position,
                                                         ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletPosition.transform.rotation);
                Destroy(temp.gameObject, ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].DestroyDelay);
            }
            if (ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletOrientation == CBulletOrientation.VectorLeftRight)
            {
                float Direction = 0;
                if (TargetAnimator.gameObject.transform.localScale.x >= 0)
                {
                    Direction = 0;
                }
                else
                {
                    Direction = 180;
                }
                GameObject temp = GameObject.Instantiate(ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletObject,
                                                         ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletPosition.transform.position,
                                                         Quaternion.Euler(0, Direction, 0));
                Destroy(temp.gameObject, ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].DestroyDelay);
            }
            if (ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletOrientation == CBulletOrientation.VectorUpDown)
            {
                float Direction = 0;
                if (TargetAnimator.gameObject.transform.localEulerAngles.z <= 90)
                {
                    Direction = 90;
                }
                else
                {
                    Direction = 270;
                }
                GameObject temp = GameObject.Instantiate(ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletObject,
                                                         ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].BulletPosition.transform.position,
                                                         Quaternion.Euler(0, 0, Direction));
                Destroy(temp.gameObject, ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].DestroyDelay);
            }
        }

        void KeyboardCooldown()
        {
            ActionKeyboardState2D[keyboard_index].Bullet2D[ActionKeyboardState2D[keyboard_index].ActiveBulletIndex].isCooldown = false;
        }

        public void SetActive(bool aValue)
        {
            isEnabled = aValue;
        }

        void Shutdown(bool aValue)
        {
            if (isEnabled && !ShutdownStatus)
            {
                if (isEnabled && usingShutdownKeyboardState2D)
                {
                    if (ShutdownKeyboardState2D.Parameter.Type == CParameterType.Float)
                    {
                        float dummyvalue = float.Parse(ShutdownKeyboardState2D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownKeyboardState2D.Parameter.Name, dummyvalue);
                        ExecuteShutdownSound();
                    }
                    if (ShutdownKeyboardState2D.Parameter.Type == CParameterType.Int)
                    {
                        int dummyvalue = int.Parse(ShutdownKeyboardState2D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownKeyboardState2D.Parameter.Name, dummyvalue);
                        ExecuteShutdownSound();
                    }
                    if (ShutdownKeyboardState2D.Parameter.Type == CParameterType.Bool)
                    {
                        bool dummyvalue = bool.Parse(ShutdownKeyboardState2D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownKeyboardState2D.Parameter.Name, dummyvalue);
                        ExecuteShutdownSound();
                    }
                    if (ShutdownKeyboardState2D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownKeyboardState2D.Parameter.Name);
                        ExecuteShutdownSound();
                    }
                    DisabledColliders();
                }

                if (isEnabled && usingShutdownMouseState2D)
                {
                    if (ShutdownMouseState2D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownMouseState2D.PositiveValue = ShutdownMouseState2D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownMouseState2D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownMouseState2D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState2D.usingAdditionalSettings)
                        {
                            ShutdownMouseState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState2D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownMouseState2D.PositiveValue = ShutdownMouseState2D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownMouseState2D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownMouseState2D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState2D.usingAdditionalSettings)
                        {
                            ShutdownMouseState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState2D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownMouseState2D.PositiveValue = ShutdownMouseState2D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownMouseState2D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownMouseState2D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState2D.usingAdditionalSettings)
                        {
                            ShutdownMouseState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState2D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownMouseState2D.Parameter.Name);
                        if (ShutdownMouseState2D.usingAdditionalSettings)
                        {
                            ShutdownMouseState2D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                if (isEnabled && usingShutdownVectorState2D)
                {
                    if (ShutdownVectorState2D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownVectorState2D.PositiveValue = ShutdownVectorState2D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownVectorState2D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownVectorState2D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState2D.usingAdditionalSettings)
                        {
                            ShutdownVectorState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState2D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownVectorState2D.PositiveValue = ShutdownVectorState2D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownVectorState2D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownVectorState2D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState2D.usingAdditionalSettings)
                        {
                            ShutdownVectorState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState2D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownVectorState2D.PositiveValue = ShutdownVectorState2D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownVectorState2D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownVectorState2D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState2D.usingAdditionalSettings)
                        {
                            ShutdownVectorState2D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState2D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownVectorState2D.Parameter.Name);
                        if (ShutdownVectorState2D.usingAdditionalSettings)
                        {
                            ShutdownVectorState2D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                ShutdownStatus = true;
                Debug.Log("Receiving: Shutdown Animation Message");
            }
        }

        void ExecuteShutdownSound(bool plays = true)
        {
            if (ShutdownKeyboardState2D.usingSound)
            {
                if (!ShutdownKeyboardState2D.animaAudioSource.isPlaying && plays)
                {
                    ShutdownKeyboardState2D.animaAudioSource.clip = ShutdownKeyboardState2D.animaAudioClip;
                    ShutdownKeyboardState2D.animaAudioSource.Play();
                }
                else if (!plays)
                {
                    ShutdownKeyboardState2D.animaAudioSource.clip = ShutdownKeyboardState2D.animaAudioClip;
                    ShutdownKeyboardState2D.animaAudioSource.Stop();
                }
            }
        }

        bool isValidatedMouseAction(KeyCode keyInput)
        {
            bool result = false;

            if (Input.GetKeyUp(keyInput))
            {
                if (keyInput == KeyCode.Mouse0 || keyInput == KeyCode.Mouse1 || keyInput == KeyCode.Mouse2)
                {
                    if (isAttack)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            else if (isAttack)
            {
                result = true;
            }

            return result;
        }

        public bool IsActionReady()
        {
            bool result = true;

            if (InputType == CInputType.Keyboard)
            {
                for (int i = 0; i < ActionKeyboardState2D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionKeyboardState2D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }
            if (InputType == CInputType.Mouse)
            {
                for (int i = 0; i < ActionMouseState2D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionMouseState2D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }
            if (InputType == CInputType.Vector)
            {
                for (int i = 0; i < ActionVectorState2D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionVectorState2D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}