using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class TextTweenView : MonoBehaviour
    {
        public enum CTranslateType { None, Up, Down, Left, Right }
        public enum CScaleType { None, scaleIn, scaleOut }

        [Space(10)]
        public bool isEnabled;
        public CTranslateType TranslateType;
        public CScaleType ScaleType;

        [Header("Main Settings")]
        public Text TargetText;
        public float duration = 3.0f;

        [Header("Translate Settings")]
        public float translatemax = 1f;
        public float translatestep = 0.1f;

        [Header("scale Settings")]
        public float scalemax = 1f;
        public float scalestep = 0.1f;

        [Header("Event Settings")]
        public bool usingUIEvent;
        public UnityEvent UIEvent;

        private float startTime;
        private float totalTime;
        private bool execEvent = false;

        public void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }

        // Start is called before the first frame update
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
                    Vector3 temp = TargetText.transform.position;
                    temp.y += translatestep;
                    TargetText.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Down && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetText.transform.position;
                    temp.y -= translatestep;
                    TargetText.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Left && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetText.transform.position;
                    temp.x -= translatestep;
                    TargetText.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Right && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetText.transform.position;
                    temp.x += translatestep;
                    TargetText.transform.position = temp;
                }

                if (ScaleType == CScaleType.scaleIn && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetText.transform.localScale;
                    temp.x += scalestep;
                    temp.y += scalestep;
                    temp.z += scalestep;
                    TargetText.transform.localScale = temp;
                }

                if (ScaleType == CScaleType.scaleOut && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetText.transform.localScale;
                    temp.x -= scalestep;
                    temp.y -= scalestep;
                    temp.z -= scalestep;
                    TargetText.transform.localScale = temp;
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
