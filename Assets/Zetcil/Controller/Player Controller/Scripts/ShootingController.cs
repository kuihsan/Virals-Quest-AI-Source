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

    public class ShootingController : MonoBehaviour
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


        // Use this for initialization
        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
                {
                    InvokeShootingController();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    if (usingDelay)
                    {
                        Invoke("InvokeShootingController", Delay);
                    }
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
                {
                    if (usingInterval)
                    {
                        InvokeRepeating("InvokeShootingController", 1, Interval);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteShootingController()
        {
            InvokeShootingController();
        }

        public void InvokeShootingController()
        {
            if (usingParent)
            {
                GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation, TargetParent);
                if (AfterShooting == CEnumAfterShooting.DetachFromParent)
                {
                    temp.transform.parent = null;
                }
                if (temp == null) Debug.Log("Shooting Failed.");
            }
            else
            {
                GameObject temp = Instantiate(TargetPrefab, TargetPosition.position, TargetPosition.rotation);
                if (temp == null) Debug.Log("Shooting Failed.");
            }
        }

    }
}
