/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan minimap
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class CameraMIN : MonoBehaviour
    {
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetController;

        [System.Serializable]
        public class CTargetObjectGroup
        {
            public CharacterController[] TargetObject;
        }

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
                else
                {
                    Invoke("SetFirstPosition", 1f);
                }
            }
        }

        void SetFirstPosition()
        {
            TargetCamera.transform.position = firstPosition;
            targetDistance = TargetCamera.transform.position - TargetController.transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (isEnabled)
            {
                TargetCamera.transform.position = TargetController.transform.position + targetDistance;
            }
        }
    }
}

