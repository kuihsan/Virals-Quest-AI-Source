/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk mengatur mekanisme raycast
 **************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class FindObjectController : MonoBehaviour
    {
        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }

        [Space(10)]
        public bool isEnabled;

        [Header("Object Settings")]
        public Camera TargetCamera;
        public VarObject SelectedObject;

        [Header("Click Settings")]
        public CClickType ClickType;

        [System.Serializable]
        public class CObjectSelection
        {
            public GameObject TargetObject;

            [Header("Animation Settings")]
            public bool usingAnimationEvent;
            public UnityEvent EventSetings;
        }

        [Header("Object Selection Settings")]
        public List<CObjectSelection> ObjectSelection;

        [Header("Selected Value Settings")]
        [ReadOnly] public int SelectedObjectType = 0;
        [ReadOnly] public string SelectedObjectName;

        bool IsValidSelection(string SelectedObjectName)
        {
            bool result = false;
            for (int i = 0; i < ObjectSelection.Count; i++)
            {
                if (ObjectSelection[i].TargetObject.name == SelectedObjectName)
                {
                    result = true;
                    if (ObjectSelection[i].usingAnimationEvent)
                    {
                        ObjectSelection[i].EventSetings.Invoke();
                    }
                    break;
                }
            }
            return result;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                CastingRay();
            }
        }

        bool ValidClick()
        {
            bool result = false;
            if (ClickType == CClickType.LeftMouse && Input.GetKeyUp(KeyCode.Mouse0) ||
                ClickType == CClickType.Touch && Input.GetKeyUp(KeyCode.Mouse0))
            {
                result = true;
            }
            if (ClickType == CClickType.MiddleMouse && Input.GetKeyUp(KeyCode.Mouse2))
            {
                result = true;
            }
            if (ClickType == CClickType.RightMouse && Input.GetKeyUp(KeyCode.Mouse1))
            {
                result = true;
            }
            return result;
        }

        public void CastingRay()
        {
            if (ValidClick())
            {
                bool ValidCollision = false;

                //-- cek tabrakan dengan objeck 2d
                Ray ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

                if (raycastHit2D.collider != null)
                {
                    Debug.Log("Hit 2D Collider");
                    SelectedObjectName = raycastHit2D.collider.gameObject.name;
                    if (IsValidSelection(raycastHit2D.collider.gameObject.name))
                    {
                        SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                        SelectedObjectType = 2;
                        ValidCollision = true;
                    }
                }
                else
                {
                    //-- cek tabrakan dengan objeck 3d
                    ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit raycastHit3D;

                    if (Physics.Raycast(ray, out raycastHit3D))
                    {
                        Debug.Log("Hit 3D Collider");
                        SelectedObjectName = raycastHit3D.collider.gameObject.name;
                        if (IsValidSelection(raycastHit3D.collider.gameObject.name))
                        {
                            SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
                            SelectedObjectType = 3;
                            ValidCollision = true;
                        }
                    }
                }
            }
        }
    }
}
