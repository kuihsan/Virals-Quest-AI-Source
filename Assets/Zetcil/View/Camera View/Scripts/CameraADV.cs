/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera ala Action RPG third person, dengan mouse bisa ngelilingin si player versi 2
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class CameraADV : MonoBehaviour
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

            [Header("Distance Settings")]
            public float UpDistance = 1f;
            public float FarDistance = 4f;
            public float FarDistanceMin = 0;
            public float FarDistanceMax = 50;
            public float FarDelta = 0.1f;
            public float currentHeight;

            [Header("Special Settings")]
            public float Sensitivity = 15f;
            public float PitchMin = 0;
            public float PitchMax = 60;
            [ReadOnly] public float Pitch = 0;
            [ReadOnly] public float Yaw = 0;
            [ReadOnly] public float Roll = 0;
        }

        [Header("Mouse Settings")]
        public bool usingMouseController;
        [ConditionalField("usingMouseController")] public CMouseController MouseController;

        Vector3 lastObjectPosition;


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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (usingMouseController)
            {
                MouseController.Pitch -= MouseController.Sensitivity * Input.GetAxis("Mouse Y");
                MouseController.Yaw += MouseController.Sensitivity * Input.GetAxis("Mouse X");

                MouseController.Pitch = Mathf.Clamp(MouseController.Pitch, MouseController.PitchMin, MouseController.PitchMax);

                if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    MouseController.FarDistance += MouseController.FarDelta;
                    MouseController.FarDistance = Mathf.Clamp(MouseController.FarDistance, MouseController.FarDistanceMin, MouseController.FarDistanceMax);
                }
                if (MouseController.MouseScrollButton == KeyCode.Mouse2 && Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    MouseController.FarDistance -= MouseController.FarDelta;
                    MouseController.FarDistance = Mathf.Clamp(MouseController.FarDistance, MouseController.FarDistanceMin, MouseController.FarDistanceMax);
                }

                if (MouseController.isAutoRotate)
                {
                    TargetCamera.transform.eulerAngles = new Vector3(MouseController.Pitch, MouseController.Yaw, MouseController.Roll);
                }
                else if (Input.GetKey(MouseController.MouseLookButton))
                {
                    TargetCamera.transform.eulerAngles = new Vector3(MouseController.Pitch, MouseController.Yaw, MouseController.Roll);
                }

                TargetCamera.transform.position = TargetController.transform.position - MouseController.FarDistance * TargetCamera.transform.forward + Vector3.up * MouseController.UpDistance;
                //TargetCamera.transform.position = new Vector3(TargetCamera.transform.position.x, MouseController.currentHeight, TargetCamera.transform.position.z);
            }

        }
    }
}
