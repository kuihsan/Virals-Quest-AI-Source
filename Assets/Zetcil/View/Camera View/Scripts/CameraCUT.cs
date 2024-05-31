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
    public class CameraCUT : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;

        [System.Serializable]
        public class CCameraCut
        {
            public Camera TargetCamera;
            public float Duration;
        }

        [System.Serializable]
        public class CCameraGroup
        {
            public CCameraCut[] CameraCut;
        }

        [Header("Camera Cut Settings")]
        public bool usingCameraCut;
        [ConditionalField("usingCameraCut")] public CCameraGroup CameraGroup;

        [Header("Debug Value")]
        [ReadOnly] public int CurrentIndex = 0;
        [ReadOnly] public int Duration = 0;

        // Use this for initialization
        void Awake()
        {
            TargetCamera.transform.position = CameraGroup.CameraCut[0].TargetCamera.transform.position;
            TargetCamera.transform.rotation = CameraGroup.CameraCut[0].TargetCamera.transform.rotation;
        }

        void Start()
        {
            if (isEnabled && usingCameraCut)
            {
                //TargetCamera.GetComponent<Camera>().enabled = false;

                for (int i = 0; i < CameraGroup.CameraCut.Length; i++)
                {
                    CameraGroup.CameraCut[i].TargetCamera.gameObject.SetActive(false);
                }
                InvokeRepeating("ExecuteCamera", 1, 1);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecuteCamera()
        {
            Duration++;
            if (CurrentIndex < CameraGroup.CameraCut.Length)
            {
                if (Duration < CameraGroup.CameraCut[CurrentIndex].Duration)
                {
                    CameraGroup.CameraCut[CurrentIndex].TargetCamera.gameObject.SetActive(true);
                }
                else
                {
                    if (CurrentIndex + 1 < CameraGroup.CameraCut.Length)
                    {
                        CameraGroup.CameraCut[CurrentIndex + 1].TargetCamera.gameObject.SetActive(true);
                        CameraGroup.CameraCut[CurrentIndex].TargetCamera.gameObject.SetActive(false);
                    }
                    Duration = 0;
                    CurrentIndex++;
                }
            }
        }
    }
}
