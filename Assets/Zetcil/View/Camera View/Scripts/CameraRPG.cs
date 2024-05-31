/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera ala RPG third person, dengan mouse bisa ngelilingin si player
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraRPG : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetController;

        [System.Serializable]
        public class CMouseController
        {
            public bool isEnabled;
            public bool isAutoRotate;
            [Header("Primary Button")]
            [SearchableEnum] public KeyCode MouseLookButton = KeyCode.Mouse1;
            [SearchableEnum] public KeyCode MouseScrollButton = KeyCode.Mouse2;

            [Header("Special Settings")]
            public float Sensitivity = 15f;
            public float PitchMin = 0;
            public float PitchMax = 60;
            [HideInInspector] public float Pitch = 0;
            [HideInInspector] public float Yaw = 0;
            [HideInInspector] public float Roll = 0;
            

        }

        [System.Serializable]
        public class CFollowSettings
        {
            public float heightDamping = 2.0f;
            public float rotationDamping = 3.0f;
            public Vector3 OffsetDistance;

            [Header("Distance Settings")]
            public float UpDistance = 1f;
            public float FarDistance = 4f;
            public float FarDistanceMin = 0;
            public float FarDistanceMax = 50;
            public float FarDelta = 0.1f;
        }

        [Header("Follow Settings")]
        public bool usingFollowSettings;
        [ConditionalField("usingFollowSettings")] public CFollowSettings FollowSettings;

        [Header("Mouse Settings")]
        public bool usingMouseController;
        public CMouseController MouseController;

        float currentHeight;

        Vector3 lastObjectPosition; 

        // Use this for initialization
        void Start()
        {
            lastObjectPosition = Vector3.zero;
        }

        public bool isTargetControllerMoving()
        {
            bool result = false;
            if (lastObjectPosition.x != TargetController.transform.position.x ||
                lastObjectPosition.z != TargetController.transform.position.z)
            {
                lastObjectPosition = TargetController.transform.position;
                result = true;
            }
            return result;
        }

        public void SetTargetController(VarObject aValue)
        {
            if (aValue.CurrentValue != null)
            {
                TargetController = aValue.CurrentValue;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (isEnabled && TargetController != null)
            {

                if (usingFollowSettings)
                {
                        float wantedRotationAngle = TargetController.transform.eulerAngles.y;
                        float wantedHeight = TargetController.transform.position.y + FollowSettings.UpDistance;

                        float currentRotationAngle = TargetCamera.transform.eulerAngles.y;
                        currentHeight = TargetCamera.transform.position.y;

                        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, FollowSettings.rotationDamping * Time.deltaTime);
                        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, FollowSettings.heightDamping * Time.deltaTime);

                        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                        TargetCamera.transform.position = TargetController.transform.position;
                        TargetCamera.transform.position -= currentRotation * Vector3.forward * FollowSettings.FarDistance;

                        TargetCamera.transform.position = new Vector3(TargetCamera.transform.position.x, currentHeight, TargetCamera.transform.position.z);

                        Vector3 OffsetPosition = TargetController.transform.position + FollowSettings.OffsetDistance;
                        TargetCamera.transform.LookAt(OffsetPosition);
                }

                if (usingMouseController) {
                    MouseController.Pitch -= MouseController.Sensitivity * Input.GetAxis("Mouse Y");
                    MouseController.Yaw += MouseController.Sensitivity * Input.GetAxis("Mouse X");

                    MouseController.Pitch = Mathf.Clamp(MouseController.Pitch, MouseController.PitchMin, MouseController.PitchMax);

                    if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        FollowSettings.FarDistance += FollowSettings.FarDelta;
                        FollowSettings.FarDistance = Mathf.Clamp(FollowSettings.FarDistance, FollowSettings.FarDistanceMin, FollowSettings.FarDistanceMax);
                    }
                    if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        FollowSettings.FarDistance -= FollowSettings.FarDelta;
                        FollowSettings.FarDistance = Mathf.Clamp(FollowSettings.FarDistance, FollowSettings.FarDistanceMin, FollowSettings.FarDistanceMax);
                    }

                    if (MouseController.isAutoRotate)
                    {
                        TargetCamera.transform.eulerAngles = new Vector3(MouseController.Pitch, MouseController.Yaw, MouseController.Roll);
                    }
                    else if (Input.GetKey(MouseController.MouseLookButton))
                    {
                        TargetCamera.transform.eulerAngles = new Vector3(MouseController.Pitch, MouseController.Yaw, MouseController.Roll);
                    }

                    TargetCamera.transform.position = TargetController.transform.position - FollowSettings.FarDistance * TargetCamera.transform.forward + Vector3.up * FollowSettings.UpDistance;
                    TargetCamera.transform.position = new Vector3(TargetCamera.transform.position.x, currentHeight, TargetCamera.transform.position.z);
                }

            }
        }
    }
}
