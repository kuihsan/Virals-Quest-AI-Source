using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class ImageTweenView : MonoBehaviour
    {
        public enum CTranslateType { None, Up, Down, Left, Right }
        public enum CScaleType { None, scaleIn, scaleOut }

        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public Image TargetUI;
        public float duration = 3.0f;

        [Header("Translate Settings")]
        public CTranslateType TranslateType;
        public float translatemax = 1f;
        public float translatestep = 0.1f;

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
            if (TranslateType == CTranslateType.Up)
            {
            }
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
                    Vector3 temp = TargetUI.transform.position;
                    temp.y += translatestep;
                    TargetUI.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Down && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetUI.transform.position;
                    temp.y -= translatestep;
                    TargetUI.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Left && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetUI.transform.position;
                    temp.x -= translatestep;
                    TargetUI.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Right && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetUI.transform.position;
                    temp.x += translatestep;
                    TargetUI.transform.position = temp;
                }

                if (ScaleType == CScaleType.scaleIn && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetUI.transform.localScale;
                    temp.x += scalestep;
                    temp.y += scalestep;
                    temp.z += scalestep;
                    TargetUI.transform.localScale = temp;
                }

                if (ScaleType == CScaleType.scaleOut && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetUI.transform.localScale;
                    temp.x -= scalestep;
                    temp.y -= scalestep;
                    temp.z -= scalestep;
                    TargetUI.transform.localScale = temp;
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
