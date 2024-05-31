/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script yang berisi rangkuman seluruh mekanik touch pada mobile. 
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class TouchController : MonoBehaviour
    {
        public enum CFilterSelection { ByName, ByTag }
        [Space(10)]
        public bool isEnabled;

        [Header("Camera Settings")]
        public bool usingMainCamera = true;
        public Camera MainCamera;
        [Space(10)]
        public bool usingCameraPan;
        public float PanSpeed;
        [Space(10)]
        public bool usingCameraScale;
        public float perspectiveZoomSpeed = 0.5f;
        public float orthoZoomSpeed = 0.5f;
        [Space(10)]
        public bool usingCameraRotate;
        Vector3 FirstPoint;
        Vector3 SecondPoint;
        float xAngle;
        float yAngle;
        float xAngleTemp;
        float yAngleTemp;

        [Header("Object Settings")]
        public bool usingSelectedObject = true;
        public VarObject SelectedObject;
        [Space(10)]
        public CFilterSelection ObjectSelection;
        public string ObjectName;
        [Tag] public string[] ObjectTag;

        [Header("Tap Settings")]
        public bool usingTapSelection;
        [Space(10)]
        public bool usingTapScale;
        public float TapScaleSpeed;
        Vector3 initialScale;
        float initialFingersDistance;
        [Space(10)]
        public bool usingTapRotate;
        public float TapRotationSpeed = 1.0f;
        [Space(10)]
        public bool usingTapDrag;
        public float TapDragSpeed = 0f;

        [Header("Swipe Settings")]
        public bool usingSwipe;
        public string swipeStatus;
        Vector3 firstTouch;
        Vector3 lastTouch;
        float FingerTouchX;
        float FingerTouchY;
        float dragDistance;
        [Space(10)]
        public bool usingSwipeUpEvent;
        public UnityEvent SwipeUpEvent;
        public bool usingSwipeUpRightEvent;
        public UnityEvent SwipeUpRightEvent;
        public bool usingSwipeRightEvent;
        public UnityEvent SwipeRightEvent;
        public bool usingSwipeDownRightEvent;
        public UnityEvent SwipeDownRightEvent;
        public bool usingSwipeDownEvent;
        public UnityEvent SwipeDownEvent;
        public bool usingSwipeDownLeftEvent;
        public UnityEvent SwipeDownLeftEvent;
        public bool usingSwipeLeftEvent;
        public UnityEvent SwipeLeftEvent;
        public bool usingSwipeUpLeftEvent;
        public UnityEvent SwipeUpLeftEvent;

        [Header("Touch Value Settings")]
        [ReadOnly] public int SelectedObjectType = 0;
        [ReadOnly] public string SelectedObjectTag;
        [ReadOnly] public string SelectedObjectName;
        [ReadOnly] public int FingerTouchCount;
        [ReadOnly] public Vector3 BeginTouch;
        [ReadOnly] public Vector3 MovedTouch;
        [ReadOnly] public Vector3 EndedTouch;

        Vector3 lastPosition;
        Vector3 startPosition;
        Vector3 targetPosition;

        // Use this for initialization
        void Start()
        {
            xAngle = 0;
            yAngle = 0;
            MainCamera.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
            dragDistance = dragDistance = Screen.height * 15 / 100;
        }

        public void SetUsingCameraPan(bool aValue)
        {
            usingCameraPan = aValue;
        }

        public void SetUsingCameraRotate(bool aValue)
        {
            usingCameraRotate = aValue;
        }

        public void SetUsingCameraScale(bool aValue)
        {
            usingCameraScale = aValue;
        }

        public void SetUsingTapScale(bool aValue)
        {
            usingTapScale = aValue;
        }

        public void SetUsingTapDrag(bool aValue)
        {
            usingTapDrag = aValue;
        }

        public void SetUsingTapRotate(bool aValue)
        {
            usingTapRotate = aValue;
        }

        public void SetUsingTapSelection(bool aValue)
        {
            usingTapSelection = aValue;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (usingCameraScale)
                {
                    IsCameraScale();
                }
                if (usingCameraRotate)
                {
                    IsCameraRotate();
                }
                if (usingCameraPan)
                {
                    IsCameraPan();
                }
                if (usingTapSelection)
                {
                    IsPrimaryTap();
                }
                if (usingSwipe)
                {
                    IsSwipe();
                }
            }
        }

        bool IsValidSelection(string SelectedObjectTag, string SelectedObjectName)
        {
            bool result = false;
            if (ObjectSelection == CFilterSelection.ByTag)
            {
                for (int i = 0; i < ObjectTag.Length; i++)
                {
                    if (ObjectTag[i] == SelectedObjectTag)
                    {
                        result = true;
                        break;
                    }
                }
            }
            if (ObjectSelection == CFilterSelection.ByName)
            {
                if (ObjectName == SelectedObjectName)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool IsCameraRotate()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    MainCamera.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                    result = true;
                }
            }

            return result;
        }

        public bool IsCameraScale()
        {
            bool result = false;

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (MainCamera.orthographic)
                {
                    if (MainCamera.orthographicSize > 10)
                    {
                        MainCamera.orthographicSize -= 1 * orthoZoomSpeed;
                    }
                }
                else
                {
                    MainCamera.transform.Translate(Vector3.forward * perspectiveZoomSpeed * Time.deltaTime);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (MainCamera.orthographic)
                {
                    MainCamera.orthographicSize += 1 * orthoZoomSpeed;
                }
                else
                {
                    MainCamera.transform.Translate(Vector3.back * perspectiveZoomSpeed * Time.deltaTime);
                }
            }


            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;
                    }

                    if (Input.touchCount == 2)
                    {
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                        if (MainCamera.orthographic)
                        {
                            MainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                            MainCamera.orthographicSize = Mathf.Max(MainCamera.orthographicSize, 0.1f);
                        }
                        else
                        {
                            MainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                            MainCamera.fieldOfView = Mathf.Clamp(MainCamera.fieldOfView, 0.1f, 179.9f);
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool IsCameraPan()
        {
            bool result = false;

            if (Input.GetMouseButtonDown(2))
            {
                lastPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                MainCamera.transform.Translate(-delta.x * PanSpeed, -delta.y * PanSpeed, 0);
                lastPosition = Input.mousePosition;
                targetPosition = MainCamera.transform.position;
            }

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;
                        BeginTouch = GetWorldPosition();
                    }

                    if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        Vector3 direction = BeginTouch - GetWorldPosition();
                        MainCamera.transform.position += direction * PanSpeed;
                        result = true;
                    }
                }

            }
            return result;
        }

        bool IsSwipe()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        firstTouch = fingerTouch.position;
                        lastTouch = fingerTouch.position;

                        FingerTouchX = fingerTouch.position.x;
                        FingerTouchY = fingerTouch.position.y;

                    }
                    else if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        lastTouch = fingerTouch.position;
                    }
                    else if (fingerTouch.phase == TouchPhase.Ended)
                    {
                        lastTouch = fingerTouch.position;

                        if (Mathf.Abs(lastTouch.x - firstTouch.x) > dragDistance || Mathf.Abs(lastTouch.y - firstTouch.y) > dragDistance)
                        {
                            //-- HORIZONTAL SWIPE
                            if (Mathf.Abs(lastTouch.x - firstTouch.x) > Mathf.Abs(lastTouch.y - firstTouch.y))
                            {
                                float checkVertical = lastTouch.y - firstTouch.y;
                                float offsetVertical = Screen.height / 5;
                                if ((lastTouch.x > firstTouch.x))
                                {
                                    if (checkVertical > 0 && checkVertical > offsetVertical)
                                    {
                                        swipeStatus = "UPRIGHT";

                                        if (usingSwipeUpRightEvent)
                                        {
                                            SwipeUpRightEvent.Invoke();
                                        }

                                    }
                                    else if (checkVertical < 0 && checkVertical < -offsetVertical)
                                    {
                                        swipeStatus = "DOWNRIGHT";

                                        if (usingSwipeDownRightEvent)
                                        {
                                            SwipeDownRightEvent.Invoke();
                                        }
                                    }
                                    else
                                    {
                                        swipeStatus = "RIGHT";

                                        if (usingSwipeRightEvent)
                                        {
                                            SwipeRightEvent.Invoke();
                                        }
                                    }
                                }
                                else
                                {
                                    if (checkVertical > 0 && checkVertical > offsetVertical)
                                    {
                                        swipeStatus = "UPLEFT";

                                        if (usingSwipeUpLeftEvent)
                                        {
                                            SwipeUpLeftEvent.Invoke();
                                        }
                                    }
                                    else if (checkVertical < 0 && checkVertical < -offsetVertical)
                                    {
                                        swipeStatus = "DOWNLEFT";

                                        if (usingSwipeDownLeftEvent)
                                        {
                                            SwipeDownLeftEvent.Invoke();
                                        }
                                    }
                                    else
                                    {
                                        swipeStatus = "LEFT";

                                        if (usingSwipeLeftEvent)
                                        {
                                            SwipeLeftEvent.Invoke();
                                        }
                                    }
                                }
                            }
                            //-- VERTICAL SWIPE
                            else
                            {
                                if (lastTouch.y > firstTouch.y)
                                {
                                    swipeStatus = "UP";

                                    if (usingSwipeUpEvent)
                                    {
                                        SwipeUpEvent.Invoke();
                                    }
                                }
                                else
                                {
                                    swipeStatus = "DOWN";

                                    if (usingSwipeDownEvent)
                                    {
                                        SwipeDownEvent.Invoke();
                                    }
                                }
                            }
                        }
                    }

                }
            }

            return result;
        }

        void IsPrimaryTap()
        {
            if (Input.touchCount > 0)
            {
                bool ValidCollision = false;
                FingerTouchCount = Input.touchCount;

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;

                        //-- cek tabrakan dengan objeck 2d
                        Ray ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                        RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

                        if (raycastHit2D.collider != null)
                        {
                            SelectedObjectName = raycastHit2D.collider.gameObject.name;
                            SelectedObjectTag = raycastHit2D.collider.gameObject.tag;
                            if (IsValidSelection(raycastHit2D.collider.gameObject.tag, raycastHit2D.collider.gameObject.name))
                            {
                                SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                                SelectedObjectType = 2;
                                ValidCollision = true;
                            }
                        }
                        else
                        {
                            //-- cek tabrakan dengan objeck 3d
                            ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                            RaycastHit raycastHit3D;

                            if (Physics.Raycast(ray, out raycastHit3D))
                            {
                                SelectedObjectName = raycastHit3D.collider.gameObject.name;
                                SelectedObjectTag = raycastHit3D.collider.gameObject.tag;
                                if (IsValidSelection(raycastHit3D.collider.gameObject.tag, raycastHit3D.collider.gameObject.name))
                                {
                                    SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
                                    SelectedObjectType = 3;
                                    ValidCollision = true;
                                }
                            }
                        }

                        if (!ValidCollision)
                        {
                            SelectedObject.CurrentValue = null;
                        }
                    }

                    //========================================================== CHECK FOR ANOTHER GESTURE
                    if (usingTapScale)
                    {
                        IsTapScale();
                    }
                    if (usingTapRotate)
                    {
                        IsTapRotate();
                    }
                    if (usingTapDrag)
                    {
                        IsTapDrag();
                    }

                }
            }
        }

        void IsTapScale()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                var scaleFactor = 10f;
                initialScale = SelectedObject.CurrentValue.transform.localScale;
                SelectedObject.CurrentValue.transform.localScale = initialScale * scaleFactor * TapScaleSpeed;
            }

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (Input.touches.Length == 2 && SelectedObject != null)
                    {
                        Touch t1 = Input.touches[0];
                        Touch t2 = Input.touches[1];

                        if (SelectedObject.CurrentValue != null)
                        {
                            if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                            {
                                initialFingersDistance = Vector2.Distance(t1.position, t2.position);
                                initialScale = SelectedObject.CurrentValue.transform.localScale;
                            }
                            else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                            {
                                var currentFingersDistance = Vector2.Distance(t1.position, t2.position);
                                var scaleFactor = currentFingersDistance / initialFingersDistance;
                                if (SelectedObject.CurrentValue != null)
                                {
                                    SelectedObject.CurrentValue.transform.localScale = initialScale * scaleFactor * TapScaleSpeed;
                                }
                            }
                        }
                    }
                }
            }
        }

        void IsTapRotate()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        if (SelectedObject != null && SelectedObjectType == 2)
                        {
                            if (SelectedObject.CurrentValue != null)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(0, 0, -fingerTouch.deltaPosition.x * TapRotationSpeed, Space.World);
                            }
                        }
                        if (SelectedObject != null && SelectedObjectType == 3)
                        {
                            if (SelectedObject.CurrentValue != null)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(fingerTouch.deltaPosition.y * TapRotationSpeed, -fingerTouch.deltaPosition.x * TapRotationSpeed, 0, Space.World);
                            }
                        }
                    }

                }
            }
        }

        void IsTapDrag()
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (SelectedObject.CurrentValue != null)
                    {
                        if (fingerTouch.phase == TouchPhase.Stationary || fingerTouch.phase == TouchPhase.Moved || Input.GetKey(KeyCode.Mouse0))
                        {
                            // get the touch position from the screen touch to world point
                            Vector3 touchedPos = MainCamera.ScreenToWorldPoint(new Vector3(fingerTouch.position.x, fingerTouch.position.y, 10));

                            if (TapDragSpeed == 0)
                            {
                                SelectedObject.CurrentValue.transform.position = touchedPos;
                            }
                            else
                            {
                                SelectedObject.CurrentValue.transform.position = Vector3.Lerp(SelectedObject.CurrentValue.transform.position, touchedPos, Time.deltaTime * TapDragSpeed);
                            }
                        }
                    }
                }

                if (SelectedObject.CurrentValue != null)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        // get the touch position from the screen touch to world point
                        Vector3 touchedPos = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

                        if (TapDragSpeed == 0)
                        {
                            SelectedObject.CurrentValue.transform.position = touchedPos;
                        }
                        else
                        {
                            SelectedObject.CurrentValue.transform.position = Vector3.Lerp(SelectedObject.CurrentValue.transform.position, touchedPos, Time.deltaTime * TapDragSpeed);
                        }

                        Debug.Log("A");


                    }
                }

            }
        }

        private Vector3 GetWorldPosition()
        {
            Ray mousePos = MainCamera.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            ground.Raycast(mousePos, out distance);
            return mousePos.GetPoint(distance);
        }
    }
}
