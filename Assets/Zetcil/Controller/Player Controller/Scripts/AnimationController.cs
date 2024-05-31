/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk perubahan animasi karakter menggunakan Keyboard
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class AnimationController : MonoBehaviour
    {
        public enum CForceTrigger { TriggerByIndex, TriggerByKey }
        public enum CCompareType { Greater, Equal, Less }
        public enum CParameterType { None, Int, Float, Bool, Trigger }
        public enum CInputType { None, Keyboard, Mouse, Vector, Command }
        public enum CMouseType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CBulletOrientation { VectorForward, VectorLeftRight, VectorUpDown }


        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Animation Type")]
        public CInputType InputType;

        [Header("Animation Settings")]
        public CharacterController TargetController;
        public Animator TargetAnimator;
        public PositionController MousePositionController;
        bool isMoving;
        bool isAttack;

        [System.Serializable]
        public class CParameterAnimation
        {
            public CParameterType Type;
            public string Name;
        }

        [System.Serializable]
        public class CMovingCommandState3D
        {
            [Header("Command Settings")]
            public VarString CommandString;
            public string CompareString;

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

        [Header("Moving Settings")]
        public bool usingMovingCommandState3D;
        public CMovingCommandState3D[] MovingCommandState3D;

        [System.Serializable]
        public class CMovingKeyboardState3D
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
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CMovingMouseState3D
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

        [Header("Vector Settings")]
        public bool isVectorActive;
        public VarVector3 TargetVector;
        private Vector3 TargetVectorInput;

        [System.Serializable]
        public class CMovingVectorState3D
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

        [Header("Moving Settings")]
        public bool usingMovingVectorState3D;
        public CMovingVectorState3D[] MovingVectorState3D;

        [System.Serializable]
        public class CActionCommandState3D
        {
            [Header("Command Settings")]
            public VarString CommandString;
            public string CompareString;

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
        public class CActionKeyboardState3D
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

            [Header("Action Status Settings")]
            public bool usingActionStatus;
            public VarBoolean ActionStatus;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CActionMouseState3D
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
        public class CActionVectorState3D
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
        public class CShutdownCommandState3D
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

        [System.Serializable]
        public class CShutdownKeyboardState3D
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

        [System.Serializable]
        public class CShutdownMouseState3D
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

        [System.Serializable]
        public class CShutdownVectorState3D
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

        [System.Serializable]
        public class CColliderState
        {
            [Header("Delay Collider Settings")]
            public float DelayCollider;

            [Header("Box Collider Settings")]
            public bool enabledBoxCollider;
            public BoxCollider[] BoxColliders;
            [Header("Sphere Collider Settings")]
            public bool enabledSphereCollider;
            public SphereCollider[] SphereColliders;
            [Header("Capsule Collider Settings")]
            public bool enabledCapsuleCollider;
            public CapsuleCollider[] CapsuleColliders;
        }

        [Header("Moving Settings")]
        public bool usingMovingKeyboardState3D;
        public CMovingKeyboardState3D[] MovingKeyboardState3D;

        [Header("Moving Settings")]
        public bool usingMovingMouseState3D;
        public CMovingMouseState3D[] MovingMouseState3D;

        [Header("Action Settings")]
        public bool usingActionKeyboardState3D;
        public CActionKeyboardState3D[] ActionKeyboardState3D;

        [Header("Action Settings")]
        public bool usingActionCommandState3D;
        public CActionCommandState3D[] ActionCommandState3D;

        [Header("Action Settings")]
        public bool usingActionMouseState3D;
        public CActionMouseState3D[] ActionMouseState3D;

        [Header("Action Settings")]
        public bool usingActionVectorState3D;
        public CActionVectorState3D[] ActionVectorState3D;

        [Header("Shutdown Settings")]
        public bool usingShutdownCommandState3D;
        public CShutdownKeyboardState3D ShutdownCommandState3D;

        [Header("Shutdown Settings")]
        public bool usingShutdownKeyboardState3D;
        public CShutdownKeyboardState3D ShutdownKeyboardState3D;

        [Header("Shutdown Settings")]
        public bool usingShutdownMouseState3D;
        public CShutdownMouseState3D ShutdownMouseState3D;

        [Header("Shutdown Settings")]
        public bool usingShutdownVectorState3D;
        public CShutdownVectorState3D ShutdownVectorState3D;

        [Header("Collider Settings")]
        public bool usingColliderState;
        public CColliderState ColliderState;

        bool ShutdownStatus = false;
        bool MovementEnabled = true;
        PositionController ThisPositionController;

        // Use this for initialization
        void Awake()
        {
            DisabledColliders();
            ThisPositionController = GetComponent<PositionController>();
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
                    for (int i = 0; i < ColliderState.SphereColliders.Length; i++)
                    {
                        ColliderState.SphereColliders[i].enabled = false;
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
                    for (int i = 0; i < ColliderState.SphereColliders.Length; i++)
                    {
                        ColliderState.SphereColliders[i].enabled = true;
                    }
                    for (int i = 0; i < ColliderState.CapsuleColliders.Length; i++)
                    {
                        ColliderState.CapsuleColliders[i].enabled = true;
                    }
                }
                Invoke("DisabledColliders", ColliderState.DelayCollider);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        public void SimulateActionIndex(int aValue)
        {
            if (isEnabled)
            {
                if (ActionKeyboardState3D[aValue].Parameter.Type == CParameterType.Float)
                 {
                     ActionKeyboardState3D[aValue].PositiveValue = ActionKeyboardState3D[aValue].TransitionValue;
                     float dummyvalue = float.Parse(ActionKeyboardState3D[aValue].PositiveValue) + 1;
                     TargetAnimator.SetFloat(ActionKeyboardState3D[aValue].Parameter.Name, dummyvalue);
                     if (ActionKeyboardState3D[aValue].usingAdditionalSettings)
                     { 
                         ActionKeyboardState3D[aValue].AdditionalEvent.Invoke();
                     }
                 }
                 if (ActionKeyboardState3D[aValue].Parameter.Type == CParameterType.Int)
                 {
                     ActionKeyboardState3D[aValue].PositiveValue = ActionKeyboardState3D[aValue].TransitionValue;
                     int dummyvalue = int.Parse(ActionKeyboardState3D[aValue].PositiveValue) + 1;
                     TargetAnimator.SetInteger(ActionKeyboardState3D[aValue].Parameter.Name, dummyvalue);
                     if (ActionKeyboardState3D[aValue].usingAdditionalSettings)
                     {
                         ActionKeyboardState3D[aValue].AdditionalEvent.Invoke();
                     }
                }
                if (ActionKeyboardState3D[aValue].Parameter.Type == CParameterType.Bool)
                 {
                     bool dummyvalue = bool.Parse(ActionKeyboardState3D[aValue].PositiveValue);
                     TargetAnimator.SetBool(ActionKeyboardState3D[aValue].Parameter.Name, dummyvalue);
                    if (ActionKeyboardState3D[aValue].usingAdditionalSettings)
                    {
                        ActionKeyboardState3D[aValue].AdditionalEvent.Invoke();
                    }
                }

                if (ActionKeyboardState3D[aValue].Parameter.Type == CParameterType.Trigger)
                 {
                    TargetAnimator.SetTrigger(ActionKeyboardState3D[aValue].Parameter.Name);
                    if (ActionKeyboardState3D[aValue].usingAdditionalSettings)
                    {
                        ActionKeyboardState3D[aValue].AdditionalEvent.Invoke();
                    }
                }
                EnabledColliders();
            }
        }

        public void SimulateShutdownIndex(int aValue)
        {
            if (isEnabled)
            {
                if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Float)
                {
                    ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                    float dummyvalue = float.Parse(ShutdownKeyboardState3D.PositiveValue) + 1;
                    TargetAnimator.SetFloat(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                    if (ShutdownKeyboardState3D.usingAdditionalSettings)
                    {
                        ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Int)
                {
                    ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                    int dummyvalue = int.Parse(ShutdownKeyboardState3D.PositiveValue) + 1;
                    TargetAnimator.SetInteger(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                    if (ShutdownKeyboardState3D.usingAdditionalSettings)
                    {
                        ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Bool)
                {
                    ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                    bool dummyvalue = bool.Parse(ShutdownKeyboardState3D.PositiveValue);
                    TargetAnimator.SetBool(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                    if (ShutdownKeyboardState3D.usingAdditionalSettings)
                    {
                        ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Trigger)
                {
                    TargetAnimator.SetTrigger(ShutdownKeyboardState3D.Parameter.Name);
                    if (ShutdownKeyboardState3D.usingAdditionalSettings)
                    {
                        ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                    }
                }
                DisabledColliders();
            }
        }

        void CheckActionAnimation()
        {
            MovementEnabled = true;

            if (isEnabled)
            {

                if (InputType == CInputType.Command)
                {
                    for (int i = 0; i < ActionCommandState3D.Length; i++)
                    {
                        if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionCommandState3D[i].AnimationStateName))
                        {
                            MovementEnabled = false;
                        }
                    }
                }
                if (InputType == CInputType.Keyboard)
                {
                    for (int i = 0; i < ActionKeyboardState3D.Length; i++)
                    {
                        if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionKeyboardState3D[i].AnimationStateName))
                        {
                            MovementEnabled = false;
                        }
                    }
                }
                if (InputType == CInputType.Mouse)
                {
                    for (int i = 0; i < ActionMouseState3D.Length; i++)
                    {
                        if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionMouseState3D[i].AnimationStateName))
                        {
                            MovementEnabled = false;
                        }
                    }
                }
                if (InputType == CInputType.Vector)
                {
                    for (int i = 0; i < ActionVectorState3D.Length; i++)
                    {
                        if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionVectorState3D[i].AnimationStateName))
                        {
                            MovementEnabled = false;
                        }
                    }
                }

            }

            ThisPositionController.MovementEnabled = MovementEnabled;

        }

        public bool IsActionReady()
        {
            bool result = true;

            if (InputType == CInputType.Command)
            {
                for (int i = 0; i < ActionCommandState3D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionCommandState3D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }
            if (InputType == CInputType.Keyboard)
            {
                for (int i = 0; i < ActionKeyboardState3D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionKeyboardState3D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }
            if (InputType == CInputType.Mouse)
            {
                for (int i = 0; i < ActionMouseState3D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionMouseState3D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }
            if (InputType == CInputType.Vector)
            {
                for (int i = 0; i < ActionVectorState3D.Length; i++)
                {
                    if (TargetAnimator.GetCurrentAnimatorStateInfo(0).IsName(ActionVectorState3D[i].AnimationStateName))
                    {
                        result = false;
                    }
                }
            }

            return result;
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
                } else
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
        // Update is called once per frame
        void FixedUpdate()
        {
            CheckActionAnimation();

            if (isEnabled && TargetHealth.CurrentValue > 0)
            {

                //=============================================================================================== COMMAND: MODE
                if (InputType == CInputType.Command)
                {
                    if (isEnabled && usingMovingCommandState3D && MovementEnabled)
                    {
                        for (int i = 0; i < MovingCommandState3D.Length; i++)
                        {

                            if (MovingCommandState3D[i].CommandString.CurrentValue == VarConstant.FORWARD)
                            {
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingCommandState3D[i].PositiveValue = MovingCommandState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingCommandState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetFloat(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingCommandState3D[i].PositiveValue = MovingCommandState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingCommandState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetInteger(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingCommandState3D[i].PositiveValue = MovingCommandState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingCommandState3D[i].PositiveValue);
                                    TargetAnimator.SetBool(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingCommandState3D[i].TransitionValue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionCommandState3D)
                    {
                        for (int i = 0; i < ActionCommandState3D.Length; i++)
                        {
                                if (ActionCommandState3D[i].CommandString.CurrentValue == ActionCommandState3D[i].CompareString)
                                {
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                    ActionCommandState3D[i].PositiveValue = ActionCommandState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionCommandState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                        ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                    ActionCommandState3D[i].PositiveValue = ActionCommandState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionCommandState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                        ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                    ActionCommandState3D[i].PositiveValue = ActionCommandState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionCommandState3D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                        ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionCommandState3D[i].Parameter.Name);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                        ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    EnabledColliders();
                                }
                        }
                    }

                    if (isEnabled && usingShutdownKeyboardState3D)
                    {
                        for (int i = 0; i < ShutdownKeyboardState3D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownKeyboardState3D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }
                }

                //=============================================================================================== KEYBOARD: MODE
                if (InputType == CInputType.Keyboard || InputType == CInputType.Command)
                {
                    if (isEnabled && usingMovingKeyboardState3D && MovementEnabled)
                    {
                        for (int i = 0; i < MovingKeyboardState3D.Length; i++)
                        {

                            if (MovingKeyboardState3D[i].KeyboardKey.TriggerKeyPress())
                            {
                                    if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        MovingKeyboardState3D[i].PositiveValue = MovingKeyboardState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(MovingKeyboardState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        MovingKeyboardState3D[i].PositiveValue = MovingKeyboardState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(MovingKeyboardState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        MovingKeyboardState3D[i].PositiveValue = MovingKeyboardState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(MovingKeyboardState3D[i].PositiveValue);
                                        TargetAnimator.SetBool(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(MovingKeyboardState3D[i].TransitionValue);
                                        if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                            }
                        }
                    }

                    if (isEnabled && usingActionKeyboardState3D)
                    {
                        for (int i = 0; i < ActionKeyboardState3D.Length; i++)
                        {
                            for (int j = 0; j < ActionKeyboardState3D[i].TriggerKey.Length; j++)
                            {
                                bool ActionStatus = false;
                                if (ActionKeyboardState3D[i].usingActionStatus)
                                {
                                    ActionStatus = ActionKeyboardState3D[i].ActionStatus.CurrentValue;
                                } else
                                {
                                    ActionStatus = true;
                                }
                                if (Input.GetKeyUp(ActionKeyboardState3D[i].TriggerKey[j]) && ActionStatus && IsActionReady())
                                {
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionKeyboardState3D[i].PositiveValue = ActionKeyboardState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionKeyboardState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionKeyboardState3D[i].PositiveValue = ActionKeyboardState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionKeyboardState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionKeyboardState3D[i].PositiveValue = ActionKeyboardState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionKeyboardState3D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionKeyboardState3D[i].Parameter.Name);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    EnabledColliders();
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownKeyboardState3D)
                    {
                        for (int i = 0; i < ShutdownKeyboardState3D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownKeyboardState3D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }
                }

                //=============================================================================================== MOUSE: MODE
                if (InputType == CInputType.Mouse)
                {
                    if (isEnabled && usingMovingMouseState3D && MovementEnabled)
                    {
                        isMoving = MousePositionController.GetIsMoving();

                        for (int i = 0; i < MovingMouseState3D.Length; i++)
                        {
                            if (Input.GetKey(MovingMouseState3D[i].GetTriggerKey()))
                            {
                                isMoving = MousePositionController.GetIsMoving();
                            }

                            if (isMoving)
                            {
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingMouseState3D[i].PositiveValue = MovingMouseState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingMouseState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetFloat(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }

                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingMouseState3D[i].PositiveValue = MovingMouseState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingMouseState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetInteger(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingMouseState3D[i].PositiveValue = MovingMouseState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingMouseState3D[i].PositiveValue);
                                    TargetAnimator.SetBool(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingMouseState3D[i].Parameter.Name);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionMouseState3D)
                    {
                        isAttack = MousePositionController.GetIsAttack();

                        for (int i = 0; i < ActionMouseState3D.Length; i++)
                        {

                            if (Input.GetKey(MovingMouseState3D[i].GetTriggerKey()))
                            {
                                isAttack = MousePositionController.GetIsAttack();
                            }

                            for (int j = 0; j < ActionMouseState3D[i].TriggerKey.Length; j++)
                            {

                                bool ActionStatus = false;
                                if (ActionMouseState3D[i].usingActionStatus)
                                {
                                    ActionMouseState3D[i].ActionStatus.CurrentValue = isAttack;
                                    ActionStatus = ActionMouseState3D[i].ActionStatus.CurrentValue;
                                }
                                else
                                {
                                    ActionStatus = true;
                                }

                                //if ((Input.GetKeyUp(ActionMouseState3D[i].TriggerKey[j]) || isAttack) && IsActionReady())
                                if (isValidatedMouseAction(ActionMouseState3D[i].TriggerKey[j]) && ActionStatus && IsActionReady())
                                {
                                    if (ActionMouseState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionMouseState3D[i].PositiveValue = ActionMouseState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionMouseState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionMouseState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState3D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionMouseState3D[i].PositiveValue = ActionMouseState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionMouseState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionMouseState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState3D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionMouseState3D[i].PositiveValue = ActionMouseState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionMouseState3D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionMouseState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionMouseState3D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionMouseState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionMouseState3D[i].Parameter.Name);
                                        if (ActionMouseState3D[i].usingAdditionalSettings)
                                        {
                                            ActionMouseState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    EnabledColliders();
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownMouseState3D)
                    {
                        for (int i = 0; i < ShutdownMouseState3D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownMouseState3D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }

                }

                //=============================================================================================== Vector: MODE
                if (InputType == CInputType.Vector)
                {
                    if (isEnabled && usingMovingVectorState3D && MovementEnabled)
                    {
                        
                        TargetVectorInput = TargetVector.CurrentValue;
                        for (int i = 0; i < MovingVectorState3D.Length; i++)
                        {
                            if (TargetVectorInput != Vector3.zero)
                            {
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingVectorState3D[i].PositiveValue = MovingVectorState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingVectorState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetFloat(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingVectorState3D[i].PositiveValue = MovingVectorState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingVectorState3D[i].PositiveValue) + 1;
                                    TargetAnimator.SetInteger(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingVectorState3D[i].PositiveValue = MovingVectorState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingVectorState3D[i].PositiveValue);
                                    TargetAnimator.SetBool(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingVectorState3D[i].Parameter.Name);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionVectorState3D)
                    {
                        for (int i = 0; i < ActionVectorState3D.Length; i++)
                        {
                            for (int j = 0; j < ActionVectorState3D[i].TriggerKey.Length; j++)
                            {
                                if (ActionVectorState3D[i].TriggerKey[j].CurrentValue)
                                {
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionVectorState3D[i].PositiveValue = ActionVectorState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionVectorState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetFloat(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionVectorState3D[i].PositiveValue = ActionVectorState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionVectorState3D[i].PositiveValue) + 1;
                                        TargetAnimator.SetInteger(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionVectorState3D[i].PositiveValue = ActionVectorState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionVectorState3D[i].PositiveValue);
                                        TargetAnimator.SetBool(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        TargetAnimator.SetTrigger(ActionVectorState3D[i].Parameter.Name);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    EnabledColliders();
                                }
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
                if (InputType == CInputType.Keyboard || InputType == CInputType.Command)
                {
                    if (isEnabled && usingMovingKeyboardState3D)
                    {
                        for (int i = 0; i < MovingKeyboardState3D.Length; i++)
                        {
                            if (MovingKeyboardState3D[i].KeyboardKey.TriggerKeyUp())
                            {
                                if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingKeyboardState3D[i].NegativeValue = MovingKeyboardState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingKeyboardState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                    {
                                        MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingKeyboardState3D[i].NegativeValue = MovingKeyboardState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingKeyboardState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                    {
                                        MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingKeyboardState3D[i].NegativeValue = MovingKeyboardState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingKeyboardState3D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingKeyboardState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                    {
                                        MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingKeyboardState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    MovingKeyboardState3D[i].NegativeValue = MovingKeyboardState3D[i].TransitionValue;
                                    TargetAnimator.SetTrigger(MovingKeyboardState3D[i].Parameter.Name);
                                    if (MovingKeyboardState3D[i].usingAdditionalSettings)
                                    {
                                        MovingKeyboardState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionKeyboardState3D)
                    {
                        for (int i = 0; i < ActionKeyboardState3D.Length; i++)
                        {
                            for (int j = 0; j < ActionKeyboardState3D[i].TriggerKey.Length; j++)
                            {
                                if (Input.GetKeyUp(ActionKeyboardState3D[i].TriggerKey[j]))
                                {
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionKeyboardState3D[i].NegativeValue = ActionKeyboardState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionKeyboardState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetFloat(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionKeyboardState3D[i].NegativeValue = ActionKeyboardState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionKeyboardState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetInteger(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionKeyboardState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionKeyboardState3D[i].NegativeValue = ActionKeyboardState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionKeyboardState3D[i].NegativeValue);
                                        TargetAnimator.SetBool(ActionKeyboardState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionKeyboardState3D[i].usingAdditionalSettings)
                                        {
                                            ActionKeyboardState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (InputType == CInputType.Command)
                {
                    if (isEnabled && usingMovingCommandState3D)
                    {
                        for (int i = 0; i < MovingCommandState3D.Length; i++)
                        {
                            if (MovingCommandState3D[i].CommandString.CurrentValue.Length == 0 || MovingCommandState3D[i].CommandString.CurrentValue == VarConstant.STOP)
                            {
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingCommandState3D[i].NegativeValue = MovingCommandState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingCommandState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingCommandState3D[i].NegativeValue = MovingCommandState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingCommandState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingCommandState3D[i].NegativeValue = MovingCommandState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingCommandState3D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingCommandState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingCommandState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    MovingCommandState3D[i].NegativeValue = MovingCommandState3D[i].TransitionValue;
                                    TargetAnimator.SetTrigger(MovingCommandState3D[i].Parameter.Name);
                                    if (MovingCommandState3D[i].usingAdditionalSettings)
                                    {
                                        MovingCommandState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingActionCommandState3D)
                    {
                        for (int i = 0; i < ActionCommandState3D.Length; i++)
                        {
                                if (ActionCommandState3D[i].CommandString.CurrentValue.Length == 0 || ActionCommandState3D[i].CommandString.CurrentValue == VarConstant.STOP)
                                {
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionCommandState3D[i].NegativeValue = ActionCommandState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionCommandState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetFloat(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                            ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionCommandState3D[i].NegativeValue = ActionCommandState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionCommandState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetInteger(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                            ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionCommandState3D[i].NegativeValue = ActionCommandState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionCommandState3D[i].NegativeValue);
                                        TargetAnimator.SetBool(ActionCommandState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                            ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionCommandState3D[i].Parameter.Type == CParameterType.Trigger)
                                    {
                                        if (ActionCommandState3D[i].usingAdditionalSettings)
                                        {
                                            ActionCommandState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                }
                        }
                    }
                }

                if (InputType == CInputType.Mouse)
                {
                    if (isEnabled && usingMovingMouseState3D)
                    {
                        for (int i = 0; i < MovingMouseState3D.Length; i++)
                        {
                            if (!isMoving)
                            {
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingMouseState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingMouseState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingMouseState3D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingMouseState3D[i].Parameter.Name);
                                    if (MovingMouseState3D[i].usingAdditionalSettings)
                                    {
                                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                                    }
                                }

                            }
                        }
                    }
                }

                if (InputType == CInputType.Vector)
                {
                    if (isEnabled && usingMovingVectorState3D)
                    {
                        for (int i = 0; i < MovingVectorState3D.Length; i++)
                        {
                            if (TargetVectorInput == Vector3.zero)
                            {
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Float)
                                {
                                    MovingVectorState3D[i].NegativeValue = MovingVectorState3D[i].TransitionValue;
                                    float dummyvalue = float.Parse(MovingVectorState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetFloat(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Int)
                                {
                                    MovingVectorState3D[i].NegativeValue = MovingVectorState3D[i].TransitionValue;
                                    int dummyvalue = int.Parse(MovingVectorState3D[i].NegativeValue) - 1;
                                    TargetAnimator.SetInteger(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Bool)
                                {
                                    MovingVectorState3D[i].NegativeValue = MovingVectorState3D[i].TransitionValue;
                                    bool dummyvalue = bool.Parse(MovingVectorState3D[i].NegativeValue);
                                    TargetAnimator.SetBool(MovingVectorState3D[i].Parameter.Name, dummyvalue);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                                if (MovingVectorState3D[i].Parameter.Type == CParameterType.Trigger)
                                {
                                    TargetAnimator.SetTrigger(MovingVectorState3D[i].Parameter.Name);
                                    if (MovingVectorState3D[i].usingAdditionalSettings)
                                    {
                                        MovingVectorState3D[i].AdditionalEvent.Invoke();
                                    }
                                }
                            }

                        }
                    }

                    if (isEnabled && usingActionVectorState3D)
                    {
                        for (int i = 0; i < ActionVectorState3D.Length; i++)
                        {
                            for (int j = 0; j < ActionVectorState3D[i].TriggerKey.Length; j++)
                            {
                                if (!ActionVectorState3D[i].TriggerKey[j].CurrentValue)
                                {
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Float)
                                    {
                                        ActionVectorState3D[i].NegativeValue = ActionVectorState3D[i].TransitionValue;
                                        float dummyvalue = float.Parse(ActionVectorState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetFloat(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Int)
                                    {
                                        ActionVectorState3D[i].NegativeValue = ActionVectorState3D[i].TransitionValue;
                                        int dummyvalue = int.Parse(ActionVectorState3D[i].NegativeValue) - 1;
                                        TargetAnimator.SetInteger(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                    if (ActionVectorState3D[i].Parameter.Type == CParameterType.Bool)
                                    {
                                        ActionVectorState3D[i].NegativeValue = ActionVectorState3D[i].TransitionValue;
                                        bool dummyvalue = bool.Parse(ActionVectorState3D[i].NegativeValue);
                                        TargetAnimator.SetBool(ActionVectorState3D[i].Parameter.Name, dummyvalue);
                                        if (ActionVectorState3D[i].usingAdditionalSettings)
                                        {
                                            ActionVectorState3D[i].AdditionalEvent.Invoke();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (isEnabled && usingShutdownVectorState3D)
                    {
                        for (int i = 0; i < ShutdownVectorState3D.ShutdownTestkey.Length; i++)
                        {
                            if (Input.GetKey(ShutdownVectorState3D.ShutdownTestkey[i]))
                            {
                                Shutdown(true);
                            }
                        }
                    }

                }
            }
        }

        public void ForceStopAnima()
        {
            isMoving = false;
            for (int i = 0; i < MovingMouseState3D.Length; i++)
            {
                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Float)
                {
                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                    float dummyvalue = float.Parse(MovingMouseState3D[i].NegativeValue) - 1;
                    TargetAnimator.SetFloat(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                    if (MovingMouseState3D[i].usingAdditionalSettings)
                    {
                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                    }
                }
                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Int)
                {
                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                    int dummyvalue = int.Parse(MovingMouseState3D[i].NegativeValue) - 1;
                    TargetAnimator.SetInteger(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                    if (MovingMouseState3D[i].usingAdditionalSettings)
                    {
                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                    }
                }
                if (MovingMouseState3D[i].Parameter.Type == CParameterType.Bool)
                {
                    MovingMouseState3D[i].NegativeValue = MovingMouseState3D[i].TransitionValue;
                    bool dummyvalue = bool.Parse(MovingMouseState3D[i].NegativeValue);
                    TargetAnimator.SetBool(MovingMouseState3D[i].Parameter.Name, dummyvalue);
                    if (MovingMouseState3D[i].usingAdditionalSettings)
                    {
                        MovingMouseState3D[i].AdditionalEvent.Invoke();
                    }
                }
            }
        }

        void Shutdown(bool aValue)
        {
            if (isEnabled && !ShutdownStatus)
            {
                if (isEnabled && usingShutdownKeyboardState3D)
                {
                    if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownKeyboardState3D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                        if (ShutdownKeyboardState3D.usingAdditionalSettings)
                        {
                            ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownKeyboardState3D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                        if (ShutdownKeyboardState3D.usingAdditionalSettings)
                        {
                            ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownKeyboardState3D.PositiveValue = ShutdownKeyboardState3D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownKeyboardState3D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownKeyboardState3D.Parameter.Name, dummyvalue);
                        if (ShutdownKeyboardState3D.usingAdditionalSettings)
                        {
                            ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownKeyboardState3D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownKeyboardState3D.Parameter.Name);
                        if (ShutdownKeyboardState3D.usingAdditionalSettings)
                        {
                            ShutdownKeyboardState3D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                if (isEnabled && usingShutdownMouseState3D)
                {
                    if (ShutdownMouseState3D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownMouseState3D.PositiveValue = ShutdownMouseState3D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownMouseState3D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownMouseState3D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState3D.usingAdditionalSettings)
                        {
                            ShutdownMouseState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState3D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownMouseState3D.PositiveValue = ShutdownMouseState3D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownMouseState3D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownMouseState3D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState3D.usingAdditionalSettings)
                        {
                            ShutdownMouseState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState3D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownMouseState3D.PositiveValue = ShutdownMouseState3D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownMouseState3D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownMouseState3D.Parameter.Name, dummyvalue);
                        if (ShutdownMouseState3D.usingAdditionalSettings)
                        {
                            ShutdownMouseState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownMouseState3D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownMouseState3D.Parameter.Name);
                        if (ShutdownMouseState3D.usingAdditionalSettings)
                        {
                            ShutdownMouseState3D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                if (isEnabled && usingShutdownVectorState3D)
                {
                    if (ShutdownVectorState3D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownVectorState3D.PositiveValue = ShutdownVectorState3D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownVectorState3D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownVectorState3D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState3D.usingAdditionalSettings)
                        {
                            ShutdownVectorState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState3D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownVectorState3D.PositiveValue = ShutdownVectorState3D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownVectorState3D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownVectorState3D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState3D.usingAdditionalSettings)
                        {
                            ShutdownVectorState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState3D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownVectorState3D.PositiveValue = ShutdownVectorState3D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownVectorState3D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownVectorState3D.Parameter.Name, dummyvalue);
                        if (ShutdownVectorState3D.usingAdditionalSettings)
                        {
                            ShutdownVectorState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownVectorState3D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownVectorState3D.Parameter.Name);
                        if (ShutdownVectorState3D.usingAdditionalSettings)
                        {
                            ShutdownVectorState3D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                if (isEnabled && usingShutdownCommandState3D)
                {
                    if (ShutdownCommandState3D.Parameter.Type == CParameterType.Float)
                    {
                        ShutdownCommandState3D.PositiveValue = ShutdownCommandState3D.TransitionValue;
                        float dummyvalue = float.Parse(ShutdownCommandState3D.PositiveValue);
                        TargetAnimator.SetFloat(ShutdownCommandState3D.Parameter.Name, dummyvalue);
                        if (ShutdownCommandState3D.usingAdditionalSettings)
                        {
                            ShutdownCommandState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownCommandState3D.Parameter.Type == CParameterType.Int)
                    {
                        ShutdownCommandState3D.PositiveValue = ShutdownCommandState3D.TransitionValue;
                        int dummyvalue = int.Parse(ShutdownCommandState3D.PositiveValue);
                        TargetAnimator.SetInteger(ShutdownCommandState3D.Parameter.Name, dummyvalue);
                        if (ShutdownCommandState3D.usingAdditionalSettings)
                        {
                            ShutdownCommandState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownCommandState3D.Parameter.Type == CParameterType.Bool)
                    {
                        ShutdownCommandState3D.PositiveValue = ShutdownCommandState3D.TransitionValue;
                        bool dummyvalue = bool.Parse(ShutdownCommandState3D.PositiveValue);
                        TargetAnimator.SetBool(ShutdownCommandState3D.Parameter.Name, dummyvalue);
                        if (ShutdownCommandState3D.usingAdditionalSettings)
                        {
                            ShutdownCommandState3D.AdditionalEvent.Invoke();
                        }
                    }
                    if (ShutdownCommandState3D.Parameter.Type == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(ShutdownCommandState3D.Parameter.Name);
                        if (ShutdownCommandState3D.usingAdditionalSettings)
                        {
                            ShutdownCommandState3D.AdditionalEvent.Invoke();
                        }
                    }
                    DisabledColliders();
                }

                ShutdownStatus = true;
                Debug.Log("Receiving: Shutdown Animation Message");
            }
        }

        public void SetUsingMovingKeyboardState3D(bool aValue)
        {
            usingMovingKeyboardState3D = aValue;
        }
        public void SetUsingActionKeyboardState3D(bool aValue)
        {
            usingActionKeyboardState3D = aValue;
        }
        public void SetUsingShutdownKeyboardState3D(bool aValue)
        {
            usingShutdownKeyboardState3D = aValue;
        }

        public void SetUsingMovingCommandState3D(bool aValue)
        {
            usingMovingCommandState3D = aValue;
        }
        public void SetUsingActionCommandState3D(bool aValue)
        {
            usingActionCommandState3D = aValue;
        }
        public void SetUsingShutdownCommandState3D(bool aValue)
        {
            usingShutdownCommandState3D = aValue;
        }

        public void SetUsingMovingMouseState3D(bool aValue)
        {
            usingMovingMouseState3D = aValue;
        }
        public void SetUsingActionMouseState3D(bool aValue)
        {
            usingActionMouseState3D = aValue;
        }
        public void SetUsingShutdownMouseState3D(bool aValue)
        {
            usingShutdownMouseState3D = aValue;
        }
    }
}
