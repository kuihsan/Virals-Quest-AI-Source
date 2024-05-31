using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{
    public class SkinTextController : MonoBehaviour
    {
        public enum CFontColor { Default, Black, White, Red, Green, Blue }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Tag Settings")]
        [Tag] public string TargetTag;

        [Header("Font Settings")]
        public Font FontName;
        public int FontSize;
        public CFontColor FontColor;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        public void InvokeSkin()
        {
            if (isEnabled)
            {
                GameObject[] uiObject = GameObject.FindGameObjectsWithTag(TargetTag);
                for (int i = 0; i < uiObject.Length; i++)
                {
                    if (uiObject[i].GetComponent<Text>())
                    {
                        uiObject[i].GetComponent<Text>().font = FontName;
                        uiObject[i].GetComponent<Text>().fontSize = FontSize;
                        if (FontColor != CFontColor.Default)
                            uiObject[i].GetComponent<Text>().color = ChangeFontColor();
                    }
                }
            }
        }

        Color ChangeFontColor()
        {
            Color result = Color.black;
            if (FontColor == CFontColor.Default) result = Color.white;
            if (FontColor == CFontColor.Black) result = Color.black;
            if (FontColor == CFontColor.White) result = Color.white;
            if (FontColor == CFontColor.Red) result = Color.red;
            if (FontColor == CFontColor.Green) result = Color.green;
            if (FontColor == CFontColor.Blue) result = Color.blue;
            return result;
        }

        void Awake()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                InvokeSkin();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnStart)
                {
                    InvokeSkin();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    if (usingDelay)
                    {
                        Invoke("InvokeSkin", Delay);
                    }
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
                {
                    if (usingInterval)
                    {
                        InvokeRepeating("InvokeSkin", 1, Interval);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
