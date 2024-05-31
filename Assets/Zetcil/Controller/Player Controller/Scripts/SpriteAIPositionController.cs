/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin perubahan posisi musuh/teman. Idenya cukup sederhana yaitu dengan 
 *          membandingkan posisi variabel karakter utama, teman dan musuh dengan urutan Ranged, Melee dan Follow.
 *          Jadi fungsi yang duluan di eksekusi adalah fungsi pencarian untuk menyerang, baru follow 
 *          dan patroli. Semua fungsi ditandai dengan variabel debug biar gampang ngecek masing2 statusnya.
 **************************************************************************************************************/

using UnityEngine;
using System.Collections;
using TechnomediaLabs;
using UnityEngine.Events;

namespace Zetcil
{

    public class SpriteAIPositionController : MonoBehaviour
    {
        public enum CAIType { Friend, Foe }

        [System.Serializable]
        public class CRangedAttack
        {
            [Tag]
            public string RangedTargetTag;
            public float RangedSearchingRange = 100;
            public float RangedAttackRange = 20;
            public int RangedAttackInterval;
            [Header("Ranged Attack Settings")]
            public GameObject Bullet3D;
            public GameObject BulletPosition3D;
            public int DestroyDelay = 5;
            public int ExecuteDelay = 1;
            [Space(10)]
            public bool usingShootingTestkey;
            public KeyCode[] ShootingTestkey;
            [Space(10)]
            public bool isShowDebug;
            [HideInInspector]
            public bool isCooldown = false;

            [HideInInspector]
            public bool isInvokeAttack = false;
        }

        [System.Serializable]
        public class CMeleeAttack
        {
            [Tag]
            public string MeleeTargetTag;
            public float MeleeSearchingRange = 100;
            public float MeleeAttackRange = 20;
        }

        [System.Serializable]
        public class CFollowSettings
        {
            [Tag]
            public string TargetToFollowTag;
            public float FollowSearchingRange = 100;
            public float FollowRange = 20;
        }


        [System.Serializable]
        public class CRetreatSettings
        {
            [Tag]
            public string TargetToRetreatTag;
            public float RetreatSearchingRange = 100;
            public float RetreatRange = 20;
        }

        [System.Serializable]
        public class CPatrolSettings
        {
            public GameObject[] PatrolPoints;
            public float PatrolCheckPoint = 10;
            [HideInInspector]
            public int PatrolIndex;
        }

        [System.Serializable]
        public class CMovementSettings
        {
            public float MoveSpeed = 1;
            public float RotateSpeed = 5;
            public float jumpSpeed = 8.0F;
            public float LookAtSpeed = 2.0f;
            public float gravity = 20.0F;
            [HideInInspector]
            public Vector3 moveDirection = Vector3.zero;
            public bool freezeX;
            public bool forceIsGrounded = true;

            [Header("Quick Destroy Setting")]
            public bool usingQuickDestroySetting;
            public UnityEvent QuickDestroyEvent;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Health Settings")]
        public VarHealth TargetHealth;

        [Header("Character Controller")]
        public CAIType AIType;
        public Rigidbody2D AIController;

        [Header("Movement Settings")]
        public CMovementSettings MovementSetting;

        [Header("Patrol Settings")]
        public bool usingTargetPatrol;
        public CPatrolSettings PatrolSettings;
        [Header("Follow Settings")]
        public bool usingTargetFollow;
        public CFollowSettings FollowSettings;
        [Header("Retreat Settings")]
        public bool usingTargetRetreat;
        public CRetreatSettings RetreatSettings;

        [Header("Melee Attack Settings")]
        public bool usingMeleeAttack;
        public CMeleeAttack MeleeAttack;
        [Header("Ranged Attack Settings")]
        public bool usingRangedAttack;
        public CRangedAttack RangedAttack;
        [Space(10)]

        [Header("Readonly Value")]
        [ReadOnly] public bool isMeleeAttack = false;
        [ReadOnly] public bool isRangedAttack = false;
        [ReadOnly] public bool isChasing = false;
        [ReadOnly] public bool isFollow = false;
        [ReadOnly] public bool isRetreat = false;
        [ReadOnly] public bool isPatrol = false;
        [ReadOnly] public bool isGrounded = false;
        [ReadOnly] public float VectorDistance;

        GameObject RetreatTargetObject;
        GameObject FollowedTargetObject;
        GameObject PlayerTargetObject;
        bool statusPatrolStart = true;

        void Start()
        {
            if (usingTargetPatrol) isPatrol = true;
        }

        void Update()
        {
            if (isEnabled && TargetHealth.CurrentValue > 0)
            {
                if (MovementSetting.forceIsGrounded)
                {
                    isGrounded = MovementSetting.forceIsGrounded;
                }

                if (isGrounded)
                {

                    //== RANGED ATTACK ==================================================================================== 
                    if (usingRangedAttack && (AIType == CAIType.Friend || AIType == CAIType.Foe))
                    {

                        PlayerTargetObject = GameObject.FindGameObjectWithTag(RangedAttack.RangedTargetTag);
                        if (PlayerTargetObject != null)
                        {
                            if (IsOnRangedAttackSearchingRange())
                            {
                                isChasing = true;
                                isPatrol = false;

                                //AIController.transform.LookAt(PlayerTargetObject.transform);
                                Vector3 relativePos = PlayerTargetObject.transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                MovementSetting.moveDirection = AIController.transform.forward;
                                MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                                MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                                //-1 
                                if (usingRangedAttack)
                                    AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                            }
                            else
                            {
                                isChasing = false;
                                if (usingTargetPatrol) isPatrol = true;
                            }

                            if (IsOnRangedAttackRange())
                            {
                                isRangedAttack = true;

                                //AIController.transform.LookAt(PlayerTargetObject.transform);
                                Vector3 relativePos = PlayerTargetObject.transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                if (!RangedAttack.isInvokeAttack)
                                {
                                    RangedAttack.isInvokeAttack = true;
                                    InvokeRepeating("AIRangedAttackShooting", 1, RangedAttack.RangedAttackInterval);
                                }
                                isPatrol = false;
                            }
                            else
                            {
                                isRangedAttack = false;
                                RangedAttack.isInvokeAttack = false;
                                CancelInvoke();
                                if (usingTargetPatrol) isPatrol = true;
                            }
                        }
                    }

                    //== RANGED ATTACK PATROL ==================================================================================== 
                    if (!isChasing && !isRangedAttack && usingRangedAttack)
                    {
                        if (usingTargetPatrol)
                        {
                            if (statusPatrolStart && isPatrol)
                            {
                                PatrolSettings.PatrolIndex = 0;

                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    statusPatrolStart = false;
                                    PatrolSettings.PatrolIndex++;
                                }
                            }
                            else
                            if (!statusPatrolStart && isPatrol && PatrolSettings.PatrolIndex < PatrolSettings.PatrolPoints.Length)
                            {
                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    PatrolSettings.PatrolIndex++;
                                }
                            }
                            if (!statusPatrolStart && isPatrol && PatrolSettings.PatrolIndex >= PatrolSettings.PatrolPoints.Length)
                            {
                                PatrolSettings.PatrolIndex = 0;

                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    statusPatrolStart = true;
                                }
                            }

                            MovementSetting.moveDirection = AIController.transform.forward;
                            MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                            MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                            if (!IsOnRangedAttackRange())
                                AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                        }
                    }

                    //== MELEE ATTACK ==================================================================================== 
                    if (usingMeleeAttack && (AIType == CAIType.Friend || AIType == CAIType.Foe))
                    {

                        PlayerTargetObject = GameObject.FindGameObjectWithTag(MeleeAttack.MeleeTargetTag);
                        if (PlayerTargetObject != null)
                        {
                            if (IsOnMeleeAttackSearchingRange())
                            {
                                isChasing = true;
                                isPatrol = false;

                                //AIController.transform.LookAt(PlayerTargetObject.transform);
                                Vector3 relativePos = PlayerTargetObject.transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                MovementSetting.moveDirection = AIController.transform.forward;
                                MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                                MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                                //-3 
                                if (usingMeleeAttack)
                                    AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                            }
                            else
                            {
                                isChasing = false;
                                if (usingTargetPatrol) isPatrol = true;
                            }

                            if (IsOnMeleeAttackRange())
                            {
                                isMeleeAttack = true;
                                isPatrol = false;
                            }
                            else
                            {
                                isMeleeAttack = false;
                                RangedAttack.isInvokeAttack = false;
                                CancelInvoke();
                                if (usingTargetPatrol) isPatrol = true;
                            }
                        }
                    }

                    //== MELEE ATTACK PATROL ==================================================================================== 
                    if (!isChasing && !isMeleeAttack && usingMeleeAttack)
                    {
                        if (usingTargetPatrol)
                        {
                            if (statusPatrolStart && isPatrol)
                            {
                                PatrolSettings.PatrolIndex = 0;
                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    statusPatrolStart = false;
                                    PatrolSettings.PatrolIndex++;
                                }
                            }
                            else
                            if (!statusPatrolStart && isPatrol && PatrolSettings.PatrolIndex < PatrolSettings.PatrolPoints.Length)
                            {
                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    PatrolSettings.PatrolIndex++;
                                }
                            }
                            if (!statusPatrolStart && isPatrol && PatrolSettings.PatrolIndex >= PatrolSettings.PatrolPoints.Length)
                            {
                                PatrolSettings.PatrolIndex = 0;

                                //AIController.transform.LookAt(PatrolPoints[PatrolIndex].transform);
                                Vector3 relativePos = PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                VectorDistance = Vector3.Distance(AIController.transform.position, PatrolSettings.PatrolPoints[PatrolSettings.PatrolIndex].transform.position);
                                if (VectorDistance < PatrolSettings.PatrolCheckPoint)
                                {
                                    statusPatrolStart = true;
                                }
                            }

                            MovementSetting.moveDirection = AIController.transform.forward;
                            MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                            MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                            //-4 
                            if (!IsOnMeleeAttackRange())
                                AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                        }

                    }

                    //== ALLY RETREAT  ==================================================================================== 
                    if (usingTargetRetreat && (AIType == CAIType.Friend || AIType == CAIType.Foe) && !isChasing && !isMeleeAttack && !isRangedAttack)
                    {
                        RetreatTargetObject = GameObject.FindGameObjectWithTag(RetreatSettings.TargetToRetreatTag);
                        if (RetreatTargetObject != null)
                        {
                            if (IsOnRetreatSearchingRange())
                            {
                                isRetreat = true;

                                AIController.transform.LookAt(RetreatTargetObject.transform);
                                //Vector3 relativePos = RetreatTargetObject.transform.position - AIController.transform.position;
                                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                //AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, LookAtSpeed * Time.deltaTime);

                                AIController.transform.localEulerAngles = new Vector3(AIController.transform.localEulerAngles.x,
                                                                                      AIController.transform.localEulerAngles.y + 180,
                                                                                      AIController.transform.localEulerAngles.z);

                                MovementSetting.moveDirection = AIController.transform.forward;
                                MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                                MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                                //-5 
                                AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                            }
                            else
                            {
                                AIController.transform.LookAt(RetreatTargetObject.transform);
                                //Vector3 relativePos = RetreatTargetObject.transform.position - AIController.transform.position;
                                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                //AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                isRetreat = false;
                            }
                        }
                    }

                    //== ENEMEY/ALLY FOLLOW  ==================================================================================== 
                    if (usingTargetFollow && (AIType == CAIType.Friend || AIType == CAIType.Foe) && !isChasing && !isMeleeAttack && !isRangedAttack)
                    {
                        FollowedTargetObject = GameObject.FindGameObjectWithTag(FollowSettings.TargetToFollowTag);
                        if (FollowedTargetObject != null)
                        {
                            if (IsOnFollowSearchingRange())
                            {
                                isFollow = true;

                                //AIController.transform.LookAt(FollowedTargetObject.transform);
                                Vector3 relativePos = FollowedTargetObject.transform.position - AIController.transform.position;
                                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                                AIController.transform.rotation = Quaternion.Slerp(AIController.transform.rotation, rotation, MovementSetting.LookAtSpeed * Time.deltaTime);

                                MovementSetting.moveDirection = AIController.transform.forward;
                                MovementSetting.moveDirection *= MovementSetting.MoveSpeed;
                                MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                                //-6 
                                AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                            }
                            else
                            {
                                isFollow = false;
                            }
                        }
                    }

                    //MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                    //AIController.Move(MovementSetting.moveDirection * Time.deltaTime);

                }
                else
                {
                    if (IsOnMeleeAttackSearchingRange() || IsOnRangedAttackSearchingRange() ||
                        IsOnFollowSearchingRange() || IsOnRetreatSearchingRange())
                    {
                        if (!IsOnMeleeAttackRange() || !IsOnRangedAttackRange())
                        {
                            MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                            //-7 
                            AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);
                        }
                    }
                    else
                    {
                        //MovementSetting.moveDirection = Vector3.zero;
                        //MovementSetting.moveDirection.y -= MovementSetting.gravity * Time.deltaTime;
                        //-8 
                        AIController.MovePosition(MovementSetting.moveDirection * Time.deltaTime);

                    }
                }

                if (MovementSetting.freezeX)
                {
                    AIController.transform.localEulerAngles = new Vector3(0, AIController.transform.localEulerAngles.y, AIController.transform.localEulerAngles.z);
                }

                if (usingRangedAttack && RangedAttack.usingShootingTestkey)
                {
                    for (int i = 0; i < RangedAttack.ShootingTestkey.Length; i++)
                    {
                        if (Input.GetKeyDown(RangedAttack.ShootingTestkey[i]))
                        {
                            AIRangedAttackShooting();
                        }
                    }

                }
            }
            else
            {
                if (MovementSetting.usingQuickDestroySetting)
                {
                    MovementSetting.QuickDestroyEvent.Invoke();
                    Destroy(this.gameObject);
                }
            }
        }

        public void AIRangedAttackShooting()
        {
            if (isEnabled && usingRangedAttack)
            {
                RangedAttack.isCooldown = true;
                Invoke("RangedAttackCooldown", 1);
                Invoke("RangedAttackExecuteShooter", RangedAttack.ExecuteDelay);
            }
        }

        void RangedAttackCooldown()
        {
            RangedAttack.isCooldown = false;
        }

        void RangedAttackExecuteShooter()
        {
            GameObject temp = GameObject.Instantiate(RangedAttack.Bullet3D, RangedAttack.BulletPosition3D.transform.position, RangedAttack.BulletPosition3D.transform.rotation);
            Destroy(temp.gameObject, RangedAttack.DestroyDelay);

            if (RangedAttack.isShowDebug)
            {
                Debug.Log(RangedAttack.BulletPosition3D.transform.position);
            }
        }

        bool IsOnMeleeAttackSearchingRange()
        {
            bool result = false;

            if (PlayerTargetObject != null)
            {
                VectorDistance = Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position);
                result = (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < MeleeAttack.MeleeSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) > MeleeAttack.MeleeAttackRange);

            }

            return result;
        }

        bool IsOnMeleeAttackRange()
        {
            bool result = false;

            if (PlayerTargetObject != null)
            {
                result = (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < MeleeAttack.MeleeSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < MeleeAttack.MeleeAttackRange);
            }

            return result;
        }

        bool IsOnRangedAttackSearchingRange()
        {
            bool result = false;

            if (PlayerTargetObject != null)
            {
                VectorDistance = Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position);
                result = (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < RangedAttack.RangedSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) > RangedAttack.RangedAttackRange);

            }

            return result;
        }

        bool IsOnRangedAttackRange()
        {
            bool result = false;

            if (PlayerTargetObject != null)
            {
                result = (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < RangedAttack.RangedSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, PlayerTargetObject.transform.position) < RangedAttack.RangedAttackRange);
            }

            return result;
        }


        bool IsOnFollowSearchingRange()
        {
            bool result = false;

            if (FollowedTargetObject != null)
            {
                VectorDistance = Vector3.Distance(AIController.transform.position, FollowedTargetObject.transform.position);
                result = (Vector3.Distance(AIController.transform.position, FollowedTargetObject.transform.position) < FollowSettings.FollowSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, FollowedTargetObject.transform.position) > FollowSettings.FollowRange);

            }

            return result;
        }

        bool IsOnRetreatSearchingRange()
        {
            bool result = false;

            if (RetreatTargetObject != null)
            {
                VectorDistance = Vector3.Distance(AIController.transform.position, RetreatTargetObject.transform.position);
                result = (Vector3.Distance(AIController.transform.position, RetreatTargetObject.transform.position) < RetreatSettings.RetreatSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, RetreatTargetObject.transform.position) > RetreatSettings.RetreatRange);

            }

            return result;
        }

        bool IsOnFollowRange()
        {
            bool result = false;

            if (FollowedTargetObject != null)
            {
                result = (Vector3.Distance(AIController.transform.position, FollowedTargetObject.transform.position) < FollowSettings.FollowSearchingRange) &&
                         (Vector3.Distance(AIController.transform.position, FollowedTargetObject.transform.position) < FollowSettings.FollowRange);
            }

            return result;
        }


        public bool GetMovingStatus()
        {
            return isChasing || isPatrol || isFollow || isRetreat;
        }

        public bool GetAttackStatus()
        {
            return isMeleeAttack || isRangedAttack;
        }

        void Shutdown(bool aValue)
        {
            isEnabled = false;
        }

        public void SetActive(bool aValue)
        {
            isEnabled = aValue;
        }
    }
}