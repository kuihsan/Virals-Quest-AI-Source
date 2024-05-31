using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.UI;
using Platinio.TweenEngine;

namespace Zetcil
{
    public class LayoutRotateView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public Ease ease;

        [Header("Object Settings")]
        public RectTransform RotateObject;
        public float Angle;

        [Header("Animation Settings")]
        public float InvokeTime;
        public float EaseSpeed;
        public float Delay;
        public float Interval;

        private float currentAngle = 0.0f;
        private Vector2 startPosition = Vector2.zero;

        private void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                ExecuteRotateAnimation();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                Invoke("ExecuteRotateAnimation", Delay);
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
            {
                InvokeRepeating("ExecuteRotateAnimation", 1, Interval);
            }
        }
        public void ExecuteRotateAnimation()
        {
            currentAngle += Angle;

            if (currentAngle > 360.0f) currentAngle = 0.0f;

            RotateObject.RotateTween(Vector3.forward, currentAngle, InvokeTime * EaseSpeed).
                SetEase(ease).
                SetDelay(Delay).
                SetOnComplete(null);

        }

    }

}

