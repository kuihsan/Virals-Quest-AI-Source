/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 // * Desc   : Script untuk mengatur Shooting objek
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class SpriteShootingController : MonoBehaviour
    {
        public enum CEnumAfterShooting { StillWithParent, DetachFromParent }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Prefab Settings")]
        public GameObject TargetPrefab;

        [Header("Position Settings")]
        public Transform TargetPosition;
        public SpriteRenderer TargetSprite;

        [Header("Parent Settings")]
        public bool usingParent;
        public Transform TargetParent;
        public CEnumAfterShooting AfterShooting;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        void SetBulletDirection(int aDirection)
        {
            if (TargetPrefab.GetComponent<SpriteBulletController>())
            {
                TargetPrefab.GetComponent<SpriteBulletController>().SetAngle(aDirection);
            }
        }

        // Use this for initialization
        void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                if (!TargetSprite.flipX)
                {
                    SetBulletDirection(0);
                    Invoke("ExecuteSpriteShootingObject", Delay);
                }
                if (TargetSprite.flipX)
                {
                    SetBulletDirection(180);
                    Invoke("ExecuteSpriteShootingObject", Delay);
                }
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                if (usingDelay)
                {
                    if (!TargetSprite.flipX)
                    {
                        SetBulletDirection(0);
                        Invoke("ExecuteSpriteShootingObject", Delay);
                    }
                    if (TargetSprite.flipX)
                    {
                        SetBulletDirection(180);
                        Invoke("ExecuteSpriteShootingObject", Delay);
                    }
                }
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
            {
                if (usingInterval)
                {
                    if (!TargetSprite.flipX)
                    {
                        SetBulletDirection(0);
                        InvokeRepeating("ExecuteSpriteShootingObject", 0, Interval);
                    }
                    if (TargetSprite.flipX)
                    {
                        SetBulletDirection(180);
                        InvokeRepeating("ExecuteSpriteShootingObject", 0, Interval);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteSpriteShootingObjectWithDelay()
        {
            if (usingDelay)
            {
                if (!TargetSprite.flipX)
                {
                    SetBulletDirection(0);
                    Invoke("ExecuteSpriteShootingObject", Delay);
                }
                if (TargetSprite.flipX)
                {
                    SetBulletDirection(180);
                    Invoke("ExecuteSpriteShootingObject", Delay);
                }
            }
        }

        public void ExecuteSpriteShootingObject()
        {
            if (usingParent)
            {
                if (!TargetSprite.flipX)
                {
                    GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation, TargetParent);
                    if (AfterShooting == CEnumAfterShooting.DetachFromParent)
                    {
                        temp.transform.parent = null;
                    }
                    if (temp == null) Debug.Log("Shooting Failed.");
                }
                if (TargetSprite.flipX)
                {
                    GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation, TargetParent);
                    if (AfterShooting == CEnumAfterShooting.DetachFromParent)
                    {
                        temp.transform.parent = null;
                    }
                    if (temp == null) Debug.Log("Shooting Failed.");
                }
            }
            else
            {
                if (!TargetSprite.flipX)
                {
                    GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation);
                    if (temp == null) Debug.Log("Shooting Failed.");
                }
                if (TargetSprite.flipX)
                {
                    GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation);
                    if (temp == null) Debug.Log("Shooting Failed.");
                }
            }
        }

    }
}
