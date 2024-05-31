using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class MeshTweenView : MonoBehaviour
    {
        public enum CTranslateType { None, Up, Down, Left, Right }
        public enum CScaleType { None, scaleIn, scaleOut }

        public enum CRotationType { None, RotationLeft, RotationRight }

        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public GameObject TargetObject;
        public float duration = 3.0f;

        [Header("Translate Settings")]
        public CTranslateType TranslateType;
        public float translatemax = 1f;
        public float translatestep = 0.1f;

        [Header("Rotation Settings")]
        public CRotationType RotationType;
        public float RotationSpeed = 1f;

        [Header("Scale Settings")]
        public CScaleType ScaleType;
        public float scalemax = 1f;
        public float scalestep = 0.1f;

        [Header("Event Settings")]
        public bool usingUIEvent;
        public UnityEvent UIEvent;

        private float startTime;
        private float totalTime;
        private bool execEvent = false;

        // Start is called before the first frame update
        public void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }

        public void SetEnabled(bool aValue)
        {
            isEnabled = aValue;
        }
        void Start()
        {
            startTime = -9;
            totalTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (startTime == -9)
                {
                    startTime = Time.time;
                }

                if (TranslateType == CTranslateType.Up && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.position;
                    temp.y += translatestep;
                    TargetObject.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Down && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.position;
                    temp.y -= translatestep;
                    TargetObject.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Left && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.position;
                    temp.x -= translatestep;
                    TargetObject.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Right && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.position;
                    temp.x += translatestep;
                    TargetObject.transform.position = temp;
                }

                if (RotationType == CRotationType.RotationLeft && totalTime < scalemax)
                {
                    TargetObject.transform.Rotate(0, -RotationSpeed, 0);
                }

                if (RotationType == CRotationType.RotationRight && totalTime < scalemax)
                {
                    TargetObject.transform.Rotate(0, RotationSpeed, 0);
                }

                if (ScaleType == CScaleType.scaleIn && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.localScale;
                    temp.x += scalestep;
                    temp.y += scalestep;
                    temp.z += scalestep;
                    TargetObject.transform.localScale = temp;
                }

                if (ScaleType == CScaleType.scaleOut && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetObject.transform.localScale;
                    temp.x -= scalestep;
                    temp.y -= scalestep;
                    temp.z -= scalestep;
                    TargetObject.transform.localScale = temp;
                }

                if (totalTime >= translatemax || totalTime >= scalemax)
                {
                    execEvent = true;
                }

                if (usingUIEvent)
                {
                    if (execEvent)
                    {
                        UIEvent.Invoke();
                    }
                }
            }
        }
    }
}
