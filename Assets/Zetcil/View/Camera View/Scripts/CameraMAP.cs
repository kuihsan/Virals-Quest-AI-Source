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
    public class CameraMAP : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetController;

        [Header("Follow Settings")]
        Vector3 velocity = Vector3.zero;
        public float smoothTime = .15f;
        public Vector3 smoothOffset;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (TargetController)
                {
                    Vector3 targetPos = TargetController.transform.position + smoothOffset;
                    //align the camera and the target z position
                    targetPos.z = TargetCamera.transform.position.z + smoothOffset.z;
                    TargetCamera.transform.position = Vector3.SmoothDamp(TargetCamera.transform.position, targetPos, ref velocity, smoothTime);
                }
            }
        }
    }
}
