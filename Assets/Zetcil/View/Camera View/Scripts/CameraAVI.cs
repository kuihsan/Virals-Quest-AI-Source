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

    public class CameraAVI : MonoBehaviour
    {
        public enum CameraType { ZoomIn, ZoomOut, MoveLeft, MoveRight, MoveUp, MoveDown }
        public enum RotationType { RotateLeft, RotateRight }

        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;

        [Header("Straight Settings")]
        public bool usingStraight;
        [ConditionalField("usingStraight")] public CameraType cameraType = CameraType.ZoomIn;
        [ConditionalField("usingStraight")] public float StraightSpeed = 0.5f;

        [Header("Rotation Settings")]
        public bool usingRotation;
        [ConditionalField("usingRotation")] public Vector3 RotateDirection;
        [ConditionalField("usingRotation")] public float TimerDelay;
        [ConditionalField("usingRotation")] public float TimerStopAfter;
        bool beginRotate;

        [Header("LookAt Settings")]
        public bool usingLookAt;
        [ConditionalField("usingRotation")] public RotationType rotationType;
        [ConditionalField("usingRotation")] public GameObject TargetObject;
        [ConditionalField("usingRotation")] public float RotationSpeed = 0.5f;

        private Quaternion lookRotation;
        private Vector3 direction;

        // Use this for initialization
        void Start()
        {
            beginRotate = false;
            if (usingRotation)
            {
                Invoke("EnabledRotation", TimerDelay);
                if (TimerStopAfter > 0)
                {
                    Invoke("EnabledStopAfter", TimerStopAfter);
                }
            }
        }

        void EnabledRotation()
        {
            beginRotate = true;
        }

        void EnabledStopAfter()
        {
            beginRotate = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingStraight)
                {
                    if (cameraType == CameraType.ZoomIn)
                    {
                        TargetCamera.transform.Translate(Vector3.forward * StraightSpeed * Time.deltaTime);
                    }
                    if (cameraType == CameraType.ZoomOut)
                    {
                        TargetCamera.transform.Translate(Vector3.back * StraightSpeed * Time.deltaTime);
                    }
                    if (cameraType == CameraType.MoveLeft)
                    {
                        TargetCamera.transform.Translate(Vector3.left * StraightSpeed * Time.deltaTime);
                    }
                    if (cameraType == CameraType.MoveRight)
                    {
                        TargetCamera.transform.Translate(Vector3.right * StraightSpeed * Time.deltaTime);
                    }
                    if (cameraType == CameraType.MoveUp)
                    {
                        TargetCamera.transform.Translate(Vector3.up * StraightSpeed * Time.deltaTime);
                    }
                    if (cameraType == CameraType.MoveDown)
                    {
                        TargetCamera.transform.Translate(Vector3.down * StraightSpeed * Time.deltaTime);
                    }
                }
                if (usingRotation && beginRotate)
                {
                    TargetCamera.transform.Rotate(RotateDirection);
                }
                if (usingLookAt)
                {
                    if (rotationType == RotationType.RotateLeft)
                    {
                        TargetObject.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
                        TargetCamera.transform.parent = TargetObject.transform;
                        TargetCamera.transform.LookAt(TargetObject.transform);
                    }
                    if (rotationType == RotationType.RotateRight)
                    {
                        TargetObject.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
                        TargetCamera.transform.parent = TargetObject.transform;
                        TargetCamera.transform.LookAt(TargetObject.transform);
                    }
                }
            }
        }
    }
}
