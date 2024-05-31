using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.UI;
using Platinio.TweenEngine;

namespace Zetcil
{
    public class LayoutPivotView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Canvas Settings")]
        public RectTransform CanvasPivot = null;
        public RectTransform StartPosition;
        public RectTransform FinishPosition;
        [HideInInspector]
        public Vector2 finishPos;

        [Header("Animation Settings")]
        public float InvokeTime;
        public float EaseSpeed;
        public float Delay;
        public float Interval;
        [Header("Hide Settings")]
        public PivotPreset anchor;

        [Header("Hide Settings")]
        public bool HideMarker;

        private Vector2 startPosition = Vector2.zero;

        private void Awake()
        {
            if (HideMarker)
            {
                if (StartPosition.GetComponent<Image>())
                {
                    StartPosition.GetComponent<Image>().enabled = false;
                }
            }
        }

        private void Start()
        {
            startPosition = StartPosition.anchoredPosition;

            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                ExecuteLayoutAnimation();
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
        public void ExecuteLayoutAnimation()
        {
            StartPosition.Move(finishPos, CanvasPivot, InvokeTime * EaseSpeed, anchor);
        }

    }

}

