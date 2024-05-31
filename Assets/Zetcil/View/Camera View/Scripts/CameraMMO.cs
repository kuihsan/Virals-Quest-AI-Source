/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera ala game MOBA/DOTA multi karakter
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;


namespace Zetcil
{
    public class CameraMMO : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetController;

        [System.Serializable]
        public class CTargetObjectGroup
        {
            public CharacterController[] TargetObject;
        }

        [Header("Target Object Settings")]
        public bool usingTargetObject;
        [ConditionalField("usingTargetObject")] public CTargetObjectGroup TargetObjectGroup;

        [Header("Syncronize Settings")]
        public bool usingSyncronize;
        [ConditionalField("usingSyncronize")] public float Syncronize = 0.1f;

        Vector3 targetDistance;
        Vector3 firstPosition;

        // Use this for initialization
        void Awake()
        {
            firstPosition = TargetCamera.transform.position;
        }

        void Start()
        {
            if (isEnabled)
            {
                if (usingSyncronize)
                {
                    Invoke("SetFirstPosition", Syncronize);
                }
                else {
                    Invoke("SetFirstPosition", 1f);
                }
            }
        }

        void SetFirstPosition()
        {
            TargetCamera.transform.position = firstPosition;
            if (usingTargetObject)
            {
                for (int i = 0; i < TargetObjectGroup.TargetObject.Length; i++)
                {
                    if (TargetObjectGroup.TargetObject[i].gameObject.activeSelf)
                    {
                        targetDistance = TargetCamera.transform.position - TargetObjectGroup.TargetObject[i].transform.position;
                    }
                }
            } else
            {
                targetDistance = TargetCamera.transform.position - TargetController.transform.position;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (isEnabled)
            {
                if (usingTargetObject)
                {
                    for (int i = 0; i < TargetObjectGroup.TargetObject.Length; i++)
                    {
                        if (TargetObjectGroup.TargetObject[i].gameObject.activeSelf)
                        {
                            TargetCamera.transform.position = TargetObjectGroup.TargetObject[i].transform.position + targetDistance;
                        }
                    }
                } else
                {
                    TargetCamera.transform.position = TargetController.transform.position + targetDistance;
                }
            }
        }
    }
}
