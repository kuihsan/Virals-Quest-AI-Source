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

    public class CameraFPS : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetController;
        [MustBeAssigned] public GameObject TargetPivot;

        [System.Serializable]
        public class CMouseController
        {
            public bool isEnabled;
            public bool isAutoRotate;
            public bool isCursorVisible;
            [SearchableEnum]
            public KeyCode MouseLookButton = KeyCode.Mouse1;
            public float LookSpeed = 4f;
            public Vector3 Offset;
        }

        [Header("Mouse Settings")]
        public bool usingMouseController;
        [ConditionalField("usingMouseController")] public CMouseController MouseController;

        [Header("Headbobber Settings")]
        public bool usingHeadbobber;
        public float bobbingSpeed = 0.18f;
        public float bobbingAmount = 0.2f;
        public float midpoint = 2.0f;
        private float timer = 0.0f;

        int mousePanButton;
        Vector3 lastPosition;
        Vector3 deltaPosition;

        Quaternion targetRotation;
        float targetRotationY;
        float targetRotationX;

        Vector3 PositionOffset;

        void SyncronizeInput()
        {
            targetRotation = TargetCamera.transform.rotation;
            targetRotationY = TargetCamera.transform.localRotation.eulerAngles.y;
            targetRotationX = TargetCamera.transform.localRotation.eulerAngles.x;

            TargetController.transform.rotation = Quaternion.Euler(TargetController.transform.localRotation.eulerAngles.x,
                                                                   TargetCamera.transform.localRotation.eulerAngles.y,
                                                                   TargetController.transform.localRotation.eulerAngles.z);

            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.visible = !Cursor.visible;
            }
        }

        // Use this for initialization
        void Start()
        {

            Cursor.visible = MouseController.isCursorVisible;
            PositionOffset = TargetCamera.transform.position - TargetController.transform.position;
            SyncronizeInput();
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                TargetCamera.transform.position = TargetPivot.transform.position;

                //-- Mouse Controller
                if (MouseController.isEnabled)
                {
                    if (MouseController.isAutoRotate || Input.GetKey(MouseController.MouseLookButton))
                    {
                        targetRotationY += Input.GetAxis("Mouse X") * MouseController.LookSpeed;
                        targetRotationX -= Input.GetAxis("Mouse Y") * MouseController.LookSpeed;
                        targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0.0f);

                        TargetCamera.transform.rotation = targetRotation;
                        TargetController.transform.rotation = Quaternion.Euler(TargetController.transform.localRotation.eulerAngles.x,
                                                                               TargetCamera.transform.localRotation.eulerAngles.y,
                                                                               TargetController.transform.localRotation.eulerAngles.z);
                    }

                }

                if (usingHeadbobber)
                {
                    float waveslice = 0.0f;
                    float horizontal = Input.GetAxis("Horizontal");
                    float vertical = Input.GetAxis("Vertical");
                    if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
                    {
                        timer = 0.0f;
                    }
                    else
                    {
                        waveslice = Mathf.Sin(timer);
                        timer = timer + bobbingSpeed;
                        if (timer > Mathf.PI * 2)
                        {
                            timer = timer - (Mathf.PI * 2);
                        }
                    }

                    Vector3 v3T = TargetPivot.transform.localPosition;
                    if (waveslice != 0)
                    {
                        float translateChange = waveslice * bobbingAmount;
                        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                        totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                        translateChange = totalAxes * translateChange;

                        v3T.x = TargetPivot.transform.position.x;
                        v3T.y = TargetPivot.transform.position.y + midpoint + translateChange;
                        v3T.z = TargetPivot.transform.position.z;


                    }
                    else
                    {
                        v3T.x = TargetPivot.transform.position.x;
                        v3T.y = TargetPivot.transform.position.y + midpoint;
                        v3T.z = TargetPivot.transform.position.z;
                    }

                    TargetCamera.transform.localPosition = v3T;
                }
            }
        }

        void LateUpdate()
        {
            //SyncronizeInput();
        }
    }
}

