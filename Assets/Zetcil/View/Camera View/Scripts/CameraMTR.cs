/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin pergerakan camera untuk pembuatan game fps, khususnya pada pergerakan 
 *          rotasi mouse dengan menjadikan senjata player sebagai childnya.  
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraMTR : MonoBehaviour
    {
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned]
        public GameObject TargetObject;

        [System.Serializable]
        public class CMouseController
        {
            public bool isEnabled;
            public bool isAutoRotate;
            public bool isCursorVisible;
            [SearchableEnum]
            public KeyCode MouseLookButton = KeyCode.Mouse1;
            public float LookSpeed = 4f;
        }

        [Header("Mouse Settings")]
        public bool usingMouseController;
        [ConditionalField("usingMouseController")] public CMouseController MouseController;

        int mousePanButton;
        Vector3 lastPosition;
        Vector3 deltaPosition;

        Quaternion targetRotation;
        float targetRotationY;
        float targetRotationX;

        void SyncronizeInput()
        {
            TargetCamera.transform.position = TargetObject.transform.position - deltaPosition;

            TargetObject.transform.rotation = TargetCamera.transform.rotation;

            targetRotation = TargetCamera.transform.rotation;
            targetRotationY = TargetCamera.transform.localRotation.eulerAngles.y;
            targetRotationX = TargetCamera.transform.localRotation.eulerAngles.x;

            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.visible = !Cursor.visible;
            }
        }

        // Use this for initialization
        void Start()
        {
            deltaPosition = TargetObject.transform.position - TargetCamera.transform.position;

            Cursor.visible = MouseController.isCursorVisible;

            SyncronizeInput();
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {

                //-- Mouse Controller
                if (MouseController.isEnabled)
                {
                    if (MouseController.isAutoRotate || Input.GetKey(MouseController.MouseLookButton))
                    {
                        targetRotationY += Input.GetAxis("Mouse X") * MouseController.LookSpeed;
                        targetRotationX -= Input.GetAxis("Mouse Y") * MouseController.LookSpeed;
                        targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);
                        TargetCamera.transform.rotation = targetRotation;
                    }

                }
            }
        }

        void LateUpdate()
        {
            SyncronizeInput();
        }
    }
}

