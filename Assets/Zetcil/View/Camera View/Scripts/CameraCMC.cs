/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera untuk pembuatan cutscene. 
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraCMC : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;

        [System.Serializable]
        public class CCameraCheckpoint
        {
            public Camera TargetCamera;
            public float TrackingSpeed;
            public float RotationSpeed;
        }

        [System.Serializable]
        public class CCameraGroup
        {
            public CCameraCheckpoint[] CameraCheckPoint;
        }

        [Header("Camera Checkpoint Settings")]
        public bool usingCheckpoint;
        [ConditionalField("usingCheckpoint")] public CCameraGroup CameraGroup;
        int CurrentIndex = 0;

        Vector3 startPosition;
        Quaternion startRotation;

        // Use this for initialization
        void Awake()
        {
            if (isEnabled)
            {
                for (int i = 0; i < CameraGroup.CameraCheckPoint.Length; i++)
                {
                    CameraGroup.CameraCheckPoint[i].TargetCamera.gameObject.SetActive(false);
                }

                startPosition = CameraGroup.CameraCheckPoint[0].TargetCamera.transform.position;
                startRotation = CameraGroup.CameraCheckPoint[0].TargetCamera.transform.rotation;

                TargetCamera.transform.position = startPosition;
                TargetCamera.transform.rotation = startRotation;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled && usingCheckpoint)
            {
                if (CurrentIndex < CameraGroup.CameraCheckPoint.Length)
                {
                    if (Vector3.Distance(TargetCamera.transform.position, CameraGroup.CameraCheckPoint[CurrentIndex].TargetCamera.transform.position) > 1)
                    {
                        TargetCamera.transform.position = Vector3.MoveTowards(TargetCamera.transform.position, CameraGroup.CameraCheckPoint[CurrentIndex].TargetCamera.transform.position, CameraGroup.CameraCheckPoint[CurrentIndex].TrackingSpeed * Time.deltaTime);
                        TargetCamera.transform.rotation = Quaternion.RotateTowards(TargetCamera.transform.rotation, CameraGroup.CameraCheckPoint[CurrentIndex].TargetCamera.transform.rotation, CameraGroup.CameraCheckPoint[CurrentIndex].RotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        CurrentIndex++;
                        if (CurrentIndex < CameraGroup.CameraCheckPoint.Length)
                        {
                            startPosition = CameraGroup.CameraCheckPoint[CurrentIndex].TargetCamera.transform.position;
                            startRotation = CameraGroup.CameraCheckPoint[CurrentIndex].TargetCamera.transform.rotation;
                        }
                    }
                }
            }
        }
    }
}
