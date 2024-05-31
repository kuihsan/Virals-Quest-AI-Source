using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class SpriteTweenView : MonoBehaviour
    {
        public enum CTranslateType { None, Up, Down, Left, Right }
        public enum CScaleType { None, scaleIn, scaleOut }

        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public SpriteRenderer TargetSprite;
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
        public bool usingSpriteEvent;
        public UnityEvent SpriteEvent;

        private float startTime;
        private float totalTime;
        private bool execEvent = false;

        public void SetEnabled(bool aValue)
        {
            isEnabled = aValue;
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
                    Vector3 temp = TargetSprite.transform.position;
                    temp.y += translatestep;
                    TargetSprite.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Down && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetSprite.transform.position;
                    temp.y -= translatestep;
                    TargetSprite.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Left && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetSprite.transform.position;
                    temp.x -= translatestep;
                    TargetSprite.transform.position = temp;
                }

                if (TranslateType == CTranslateType.Right && totalTime < translatemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetSprite.transform.position;
                    temp.x += translatestep;
                    TargetSprite.transform.position = temp;
                }

                if (ScaleType == CScaleType.scaleIn && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetSprite.transform.localScale;
                    temp.x += scalestep;
                    temp.y += scalestep;
                    temp.z += scalestep;
                    TargetSprite.transform.localScale = temp;
                }

                if (ScaleType == CScaleType.scaleOut && totalTime < scalemax)
                {
                    totalTime = (Time.time - startTime) / duration;
                    Vector3 temp = TargetSprite.transform.localScale;
                    temp.x -= scalestep;
                    temp.y -= scalestep;
                    temp.z -= scalestep;
                    TargetSprite.transform.localScale = temp;
                }

                if (totalTime >= translatemax || totalTime >= scalemax)
                {
                    execEvent = true;
                }

                if (usingSpriteEvent)
                {
                    if (execEvent)
                    {
                        SpriteEvent.Invoke();
                    }
                }
            }
        }
    }
}
