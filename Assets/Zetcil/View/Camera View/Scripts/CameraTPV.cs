using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class CameraTPV : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;
        [MustBeAssigned] public GameObject TargetPivot;
        [MustBeAssigned] public GameObject TargetController;

        [Header("Mouse Settings")]
        public bool mouseControl = true;  // If mouse causes rotation
        public float rotationSpeed = 120.0f;  // Speed said rotation happens at
        public float pitchMax = 80.0f;  // Max angle cam can be at
        public float pitchMin = -45.0f;  // Smallest angle cam can be at
        public float rotationSmoothing = 18f;
        public float translationSmoothing = 10f;
        public float turnRate = 120f; // Rate at which horizontal axis causes LAU turning
        public float verticalTurnInfluence = 60f; // Vertical axis influence on said turning
        public string mouseX = "Mouse X";
        public string mouseY = "Mouse Y";

        private float yaw = 0.0f;
        private float pitch = 0.0f;
        private float lastMouseMove = 0f;
        private float currentTurnRate = 0f;

        private Transform pivot;
        private Transform lookAt;
        private Vector3 pivotOrigin;
        private Vector3 targetPivotPosition;
        private Vector3 forceDirection;
        private Camera cam;
        private CameraState camState;

        private void Start()
        {
            forceDirection = Vector3.zero;
            camState = CameraState.Grounded;
            cam = GetComponentInChildren<Camera>();

            pivot = cam.transform.parent;
            pivotOrigin = pivot.localPosition;
            targetPivotPosition = pivot.localPosition;
        }

        private void LateUpdate()
        {
            if (camState == CameraState.Freeze)
                return;

            HandleMovement();
            HandleRotation();
        }

        private void HandleRotation()
        {
            if (mouseControl)
            {
                float x = Input.GetAxis(mouseX);
                float y = Input.GetAxis(mouseY);

                if (x != 0f || y != 0f)
                    lastMouseMove = Time.time;

                if (camState == CameraState.Grounded || camState == CameraState.Climb)
                    yaw += x * rotationSpeed * Time.deltaTime;
                else if (camState == CameraState.Combat)
                    yaw = Quaternion.LookRotation((lookAt.position - TargetController.transform.position).normalized, Vector3.up).eulerAngles.y;

                if (camState == CameraState.Climb)
                    LimitYaw(80f, ref yaw);

                pitch -= y * rotationSpeed * Time.deltaTime; // Negative so mouse up = cam down
                pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
            }

            if (LAUTurning && camState == CameraState.Grounded)
                DoExtraRotation();

            Quaternion targetRot = Quaternion.Euler(pitch, yaw, 0.0f);

            if (rotationSmoothing != 0f)
                pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRot, rotationSmoothing * Time.deltaTime);
            else
                pivot.rotation = targetRot;

            pivot.localPosition = Vector3.Lerp(pivot.localPosition, targetPivotPosition, Time.deltaTime * 2f);
        }

        private void LimitYaw(float range, ref float yaw)
        {
            float yawMax = TargetController.transform.eulerAngles.y + range;
            float yawMin = TargetController.transform.eulerAngles.y - range;

            yaw = Mathcam.ClampAngle(yaw, yawMin, yawMax);

            StartCoroutine(UnsmoothRotationSet());
        }

        private void HandleMovement()
        {
            if (translationSmoothing != 0f)
                transform.position = Vector3.Lerp(transform.position, TargetController.transform.position, Time.deltaTime * translationSmoothing);
            else
                transform.position = TargetController.transform.position;
        }

        private void DoExtraRotation()
        {
            float axisValue = Input.GetAxis("Horizontal");
            float vertAxis = Input.GetAxis("Vertical");

            if (Time.time - lastMouseMove < 0.75f)
                currentTurnRate = 0f;
            else
                currentTurnRate = turnRate - (vertAxis * verticalTurnInfluence);

            yaw += currentTurnRate * axisValue * Time.deltaTime;
        }

        public IEnumerator UnsmoothRotationSet()
        {
            float oldSmoothValue = rotationSmoothing;
            rotationSmoothing = 0f;

            yield return null;

            rotationSmoothing = oldSmoothValue;
        }

        public void PivotOnHead()
        {
            targetPivotPosition = Vector3.up * 1.7f;
        }

        public void PivotOnTarget()
        {
            targetPivotPosition = Vector3.zero;
        }

        public void PivotOnHip()
        {
            targetPivotPosition = Vector3.up;
        }

        public void PivotOnPivot()
        {
            targetPivotPosition = pivotOrigin;
        }

        #region Public Properties

        public bool LAUTurning { get; set; }

        public CameraState State
        {
            get { return camState; }
            set { camState = value; }
        }

        public Vector3 ForceDirection
        {
            get { return forceDirection; }
            set { forceDirection = value; }
        }

        public Transform LookAt
        {
            get { return lookAt; }
            set { lookAt = value; }
        }

        #endregion
    }

    // enum incase of future extensions
    public enum CameraState
    {
        Freeze,
        Grounded,
        Combat,
        Climb
    }


}


