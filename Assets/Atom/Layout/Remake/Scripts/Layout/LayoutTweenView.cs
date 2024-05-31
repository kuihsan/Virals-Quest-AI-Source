using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;

namespace Zetcil
{
    public class LayoutTweenView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;
        public Ease ease;

        [Header("Animation Settings")]
        public float InvokeTime;
        public float EaseSpeed;
        public float Delay;
        public float Interval;

        [Header("Location Settings")]
        public Transform InPosition;
        public Transform OutPosition;
        public Transform CurrentPosition;

        [Header("Hide Settings")]
        public bool HideMarker;

        private Vector3 startPosition = Vector3.zero;

        private void Awake()
        {
            if (HideMarker)
            {
                if (InPosition.GetComponent<Image>())
                {
                    InPosition.GetComponent<Image>().enabled = false;
                }
                if (OutPosition.GetComponent<Image>())
                {
                    OutPosition.GetComponent<Image>().enabled = false;
                }
                if (CurrentPosition.GetComponent<Image>())
                {
                    CurrentPosition.GetComponent<Image>().enabled = false;
                }
            }
        }

        private void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                TweenIn();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                Invoke("TweenIn", Delay);
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
            {
                InvokeRepeating("TweenIn", 1, Interval);
            }
        }

        public void TweenIn()
        {
            CurrentPosition.Move(InPosition, InvokeTime * EaseSpeed).
                SetEase(ease).
                SetOnComplete(null);
        }

        public void TweenOut()
        {
            CurrentPosition.Move(OutPosition, InvokeTime * EaseSpeed).
                SetEase(ease).
                SetOnComplete(null);
        }
    }

}

