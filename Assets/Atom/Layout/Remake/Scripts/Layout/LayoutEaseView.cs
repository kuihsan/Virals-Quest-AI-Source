using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;

namespace Zetcil
{
    public class LayoutEaseView : MonoBehaviour
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
        public Transform StartPosition;
        public Transform FinishPosition;

        [Header("Hide Settings")]
        public bool HideMarker;

        private Vector3 startPosition = Vector3.zero;

        private void Awake()
        {
            if (HideMarker)
            {
                if (StartPosition.GetComponent<Image>())
                {
                    StartPosition.GetComponent<Image>().enabled = false;
                }
                if (FinishPosition.GetComponent<Image>())
                {
                    FinishPosition.GetComponent<Image>().enabled = false;
                }
            }
        }

        private void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                ExecuteEaseAnimation();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                Invoke("ExecuteEaseAnimation", Delay);
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
            {
                InvokeRepeating("ExecuteEaseAnimation", 1, Interval);
            }
        }
        public void ExecuteEaseAnimation()
        {
            StartPosition.Move(FinishPosition, InvokeTime * EaseSpeed).
                SetEase(ease).
                SetOnComplete(null);
        }

    }

}

