using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class SpriteFadeView : MonoBehaviour
    {
        public enum CFadeType { None, FadeIn, FadeOut }

        [Space(10)]
        public bool isEnabled;
        public CFadeType FadeType;

        [Header("Main Settings")]
        public SpriteRenderer TargetSprite;
        public float duration = 5.0f;
        public float delay = 1.0f;

        [Header("Event Settings")]
        public bool usingSpriteEvent;
        public UnityEvent SpriteEvent;

        float minimum = 0.0f;
        float maximum = 1f;
        float status = 0;
        private bool execEvent = false;

        private float startTime;
        private float totalTime;

        public void SetEnabled(bool aValue)
        {
            isEnabled = aValue;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (FadeType == CFadeType.FadeIn)
            {
                minimum = 0f;
                maximum = 1f;
                status = 0f;
            }
            if (FadeType == CFadeType.FadeOut)
            {
                minimum = 1f;
                maximum = 0f;
                status = 1f;
            }
            startTime = -9;
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

                if (FadeType == CFadeType.FadeIn && status < 1)
                {
                    totalTime = (Time.time - startTime) / duration;
                    status = Mathf.SmoothStep(minimum, maximum, totalTime);
                    TargetSprite.color = new Color(1f, 1f, 1f, status);
                }
                else if (FadeType == CFadeType.FadeOut && status > 0)
                {
                    totalTime = (Time.time - startTime) / duration;
                    status = Mathf.SmoothStep(minimum, maximum, totalTime);
                    TargetSprite.color = new Color(1f, 1f, 1f, status);
                }

                if (totalTime > delay)
                {
                    if (usingSpriteEvent)
                    {
                        SpriteEvent.Invoke();
                    }
                }
            }
        }
    }
}
