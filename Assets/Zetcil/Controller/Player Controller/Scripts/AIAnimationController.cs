/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin perubahan pose musuh. 
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class AIAnimationController : MonoBehaviour
    {
        bool isCooldown = false;

        public enum CCompareType { Greater, Equal, Less }
        public enum CParameterType { Int, Float, Bool, Trigger }

        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Animation Settings")]
        public CharacterController AIController;
        public AIPositionController AIPosition;
        public Animator AIAnimator;

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

        [System.Serializable]
        public class CMovingState3D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterType ParameterType;
            public string ParameterName;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;
        }

        [System.Serializable]
        public class CActionState3D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterType ParameterType;
            public string ParameterName;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;

            [Header("Delay Settings")]
            public float ActionDelay;

            [Header("Autolook Settings")]
            public bool usingAutolook;
            [Tag]
            public string LookTargetTag;
            public float LookRange;
        }

        [System.Serializable]
        public class CShutdownState3D
        {
            [Header("Animation State Settings")]
            public string AnimationStateName;

            [Header("Parameter Settings")]
            public CParameterType ParameterType;
            public string ParameterName;

            [Header("Transition Settings")]
            public string TransitionValue;
            [HideInInspector] public string PositiveValue;
            [HideInInspector] public string NegativeValue;

            [Header("Additional Settings")]
            public bool usingAdditionalSettings;
            public UnityEvent AdditionalEvent;

            [Header("Shutdown Settings")]
            public KeyCode[] ShutdownTestkey;

            [Header("Destroy Object")]
            public int DestroyObjectDelay;
        }

        [Header("Moving Settings")]
        public bool usingMovingState;
        public CMovingState3D[] MovingState3D;

        [Header("Action Settings")]
        public bool usingActionState;
        public CActionState3D[] ActionState3D;

        [Header("Shutdown Settings")]
        public bool usingShutdownState;
        public CShutdownState3D ShutdownState3D;

        [Header("Collider Settings")]
        public bool usingColliderState;
        public CColliderState ColliderState;

        [Header("Disabled Movement Settings")]
        public AIPositionController DisabledController;

        string animationName;
        string paramName;
        float floatValue;
        int intValue;
        bool boolValue;
        int indexSound;
        bool isShutdown = false;

        // Use this for initialization
        void Awake()
        {
            DisabledColliders();
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

        // Update is called once per frame
        void Update()
        {
            if (isEnabled && TargetHealth.CurrentValue <= 0)
            {
                Shutdown(true);
            }

            if (isEnabled && usingMovingState && TargetHealth.CurrentValue > 0)
            {
                for (int i = 0; i < MovingState3D.Length; i++)
                {
                    if (!isShutdown && AIPosition.GetMovingStatus())
                    {
                        if (MovingState3D[i].ParameterType == CParameterType.Float)
                        {
                            MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                            float dummyvalue = float.Parse(MovingState3D[i].PositiveValue) + 1;
                            AIAnimator.SetFloat(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (MovingState3D[i].ParameterType == CParameterType.Int)
                        {
                            MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                            int dummyvalue = int.Parse(MovingState3D[i].PositiveValue) + 1;
                            AIAnimator.SetInteger(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (MovingState3D[i].ParameterType == CParameterType.Bool)
                        {
                            MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                            bool dummyvalue = bool.Parse(MovingState3D[i].PositiveValue);
                            AIAnimator.SetBool(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (MovingState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            AIAnimator.SetTrigger(MovingState3D[i].ParameterName);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                    }
                }
            }

            if (isEnabled && usingActionState && TargetHealth.CurrentValue > 0)
            {
                for (int i = 0; i < ActionState3D.Length; i++)
                {
                    if (!isShutdown && AIPosition.GetAttackStatus())
                    {
                        if (ActionState3D[i].ParameterType == CParameterType.Float)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                                floatValue = float.Parse(ActionState3D[i].PositiveValue) + 1;
                                paramName = ActionState3D[i].ParameterName;
                                animationName = ActionState3D[i].AnimationStateName;
                                Invoke("ExecuteActionFloat", ActionState3D[i].ActionDelay);
                                if (ActionState3D[i].usingAdditionalSettings)
                                {
                                    ActionState3D[i].AdditionalEvent.Invoke();
                                }

                            }
                        }
                        if (ActionState3D[i].ParameterType == CParameterType.Int)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                                intValue = int.Parse(ActionState3D[i].PositiveValue) + 1;
                                paramName = ActionState3D[i].ParameterName;
                                animationName = ActionState3D[i].AnimationStateName;
                                Invoke("ExecuteActionInt", ActionState3D[i].ActionDelay);
                                if (ActionState3D[i].usingAdditionalSettings)
                                {
                                    ActionState3D[i].AdditionalEvent.Invoke();
                                }
                            }
                        }
                        if (ActionState3D[i].ParameterType == CParameterType.Bool)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                MovingState3D[i].PositiveValue = MovingState3D[i].TransitionValue;
                                boolValue = bool.Parse(ActionState3D[i].PositiveValue);
                                paramName = ActionState3D[i].ParameterName;
                                animationName = ActionState3D[i].AnimationStateName;
                                Invoke("ExecuteActionBool", ActionState3D[i].ActionDelay);
                                if (ActionState3D[i].usingAdditionalSettings)
                                {
                                    ActionState3D[i].AdditionalEvent.Invoke();
                                }
                            }
                        }
                        if (ActionState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                paramName = ActionState3D[i].ParameterName;
                                animationName = ActionState3D[i].AnimationStateName;
                                Invoke("ExecuteActionTrigger", ActionState3D[i].ActionDelay);
                                if (ActionState3D[i].usingAdditionalSettings)
                                {
                                    ActionState3D[i].AdditionalEvent.Invoke();
                                }
                            }
                        }
                        if (ActionState3D[i].usingAutolook)
                        {
                            GameObject[] tempLook = GameObject.FindGameObjectsWithTag(ActionState3D[i].LookTargetTag);
                            float tempNearest = 1000;
                            int tempClosest = 0;
                            for (int x = 0; x < tempLook.Length; x++)
                            {
                                float tempdist = Vector3.Distance(AIAnimator.gameObject.transform.position, tempLook[x].transform.position);
                                if (tempdist < ActionState3D[i].LookRange)
                                {
                                    if (tempdist < tempNearest)
                                    {
                                        tempClosest = x;
                                        tempNearest = tempdist;
                                    }
                                }
                            }
                            Vector3 tempcontroller = new Vector3(tempLook[tempClosest].transform.position.x,
                                                                 AIController.transform.position.y,
                                                                 tempLook[tempClosest].transform.position.z);
                            AIController.transform.LookAt(tempcontroller);
                            //TargetAnimator.gameObject.transform.LookAt(tempLook[tempClosest].transform);

                        }
                        EnabledColliders();
                    }
                }
            }

            if (isEnabled && usingShutdownState)
            {
                for (int i = 0; i < ShutdownState3D.ShutdownTestkey.Length; i++)
                {
                    if (Input.GetKey(ShutdownState3D.ShutdownTestkey[i]))
                    {
                        Shutdown(true);
                    }
                }
            }
        }

        void ExecuteActionFloat()
        {
            isCooldown = false;
            AIAnimator.SetFloat(paramName, floatValue);
        }

        void ExecuteActionInt()
        {
            isCooldown = false;
            AIAnimator.SetInteger(paramName, intValue);
        }

        void ExecuteActionBool()
        {
            isCooldown = false;
            AIAnimator.SetBool(paramName, boolValue);
        }

        void ExecuteActionTrigger()
        {
            isCooldown = false;
            if (!AIAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                AIAnimator.SetTrigger(paramName);
            }
        }

        void LateUpdate()
        {
            if (isEnabled && TargetHealth.CurrentValue <= 0)
            {
                Shutdown(true);
            }

            if (isEnabled && TargetHealth.CurrentValue > 0)
            {

                for (int i = 0; i < MovingState3D.Length; i++)
                {
                    if (!AIPosition.GetMovingStatus())
                    {
                        if (!isShutdown && MovingState3D[i].ParameterType == CParameterType.Float)
                        {
                            MovingState3D[i].NegativeValue = MovingState3D[i].TransitionValue;
                            float dummyvalue = float.Parse(MovingState3D[i].NegativeValue) - 1;
                            AIAnimator.SetFloat(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (!isShutdown && MovingState3D[i].ParameterType == CParameterType.Int)
                        {
                            MovingState3D[i].NegativeValue = MovingState3D[i].TransitionValue;
                            int dummyvalue = int.Parse(MovingState3D[i].NegativeValue) - 1;
                            AIAnimator.SetInteger(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (!isShutdown && MovingState3D[i].ParameterType == CParameterType.Bool)
                        {
                            MovingState3D[i].NegativeValue = MovingState3D[i].TransitionValue;
                            bool dummyvalue = bool.Parse(MovingState3D[i].NegativeValue);
                            AIAnimator.SetBool(MovingState3D[i].ParameterName, dummyvalue);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (!isShutdown && MovingState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            AIAnimator.SetTrigger(MovingState3D[i].ParameterName);
                            if (MovingState3D[i].usingAdditionalSettings)
                            {
                                MovingState3D[i].AdditionalEvent.Invoke();
                            }
                        }

                    }

                }

                for (int i = 0; i < ActionState3D.Length; i++)
                {
                    if (!AIPosition.GetAttackStatus())
                    {
                        if (!isShutdown && ActionState3D[i].ParameterType == CParameterType.Float)
                        {
                            ActionState3D[i].NegativeValue = ActionState3D[i].TransitionValue;
                            float dummyvalue = float.Parse(ActionState3D[i].NegativeValue) - 1;
                            AIAnimator.SetFloat(ActionState3D[i].ParameterName, dummyvalue);
                            if (ActionState3D[i].usingAdditionalSettings)
                            {
                                ActionState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (!isShutdown && ActionState3D[i].ParameterType == CParameterType.Int)
                        {
                            ActionState3D[i].NegativeValue = ActionState3D[i].TransitionValue;
                            int dummyvalue = int.Parse(ActionState3D[i].NegativeValue) - 1;
                            AIAnimator.SetInteger(ActionState3D[i].ParameterName, dummyvalue);
                            if (ActionState3D[i].usingAdditionalSettings)
                            {
                                ActionState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                        if (!isShutdown && ActionState3D[i].ParameterType == CParameterType.Bool)
                        {
                            ActionState3D[i].NegativeValue = ActionState3D[i].TransitionValue;
                            bool dummyvalue = bool.Parse(ActionState3D[i].NegativeValue);
                            AIAnimator.SetBool(ActionState3D[i].ParameterName, dummyvalue);
                            if (ActionState3D[i].usingAdditionalSettings)
                            {
                                ActionState3D[i].AdditionalEvent.Invoke();
                            }
                        }
                    }

                }
            }
        }

        void Shutdown(bool aValue)
        {
            if (!isShutdown)
            {
                isShutdown = true;

                if (DisabledController != null) DisabledController.enabled = false;

                if (ShutdownState3D.ParameterType == CParameterType.Float)
                {
                    ShutdownState3D.PositiveValue = ShutdownState3D.TransitionValue;
                    float dummyvalue = float.Parse(ShutdownState3D.PositiveValue) + 1;
                    AIAnimator.SetFloat(ShutdownState3D.ParameterName, dummyvalue);
                    if (ShutdownState3D.usingAdditionalSettings)
                    {
                        ShutdownState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownState3D.ParameterType == CParameterType.Int)
                {
                    ShutdownState3D.PositiveValue = ShutdownState3D.TransitionValue;
                    int dummyvalue = int.Parse(ShutdownState3D.PositiveValue) + 1;
                    AIAnimator.SetInteger(ShutdownState3D.ParameterName, dummyvalue);
                    if (ShutdownState3D.usingAdditionalSettings)
                    {
                        ShutdownState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownState3D.ParameterType == CParameterType.Bool)
                {
                    ShutdownState3D.PositiveValue = ShutdownState3D.TransitionValue;
                    bool dummyvalue = bool.Parse(ShutdownState3D.PositiveValue);
                    AIAnimator.SetBool(ShutdownState3D.ParameterName, dummyvalue);
                    if (ShutdownState3D.usingAdditionalSettings)
                    {
                        ShutdownState3D.AdditionalEvent.Invoke();
                    }
                }
                if (ShutdownState3D.ParameterType == CParameterType.Trigger)
                {
                    AIAnimator.SetTrigger(ShutdownState3D.ParameterName);
                    if (ShutdownState3D.usingAdditionalSettings)
                    {
                        ShutdownState3D.AdditionalEvent.Invoke();
                    }
                }

                Invoke("DestroyObject", ShutdownState3D.DestroyObjectDelay);
            }
        }

        void DestroyObject()
        {
            Destroy(this.gameObject);
        }

        public void SetActive(bool aValue)
        {
            isEnabled = aValue;
        }

    }

}