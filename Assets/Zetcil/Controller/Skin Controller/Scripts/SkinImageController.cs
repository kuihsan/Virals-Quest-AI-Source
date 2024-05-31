using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{
    public class SkinImageController : MonoBehaviour
    {
        public enum CSkinColor { Default, Black, White, Red, Green, Blue }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Tag Settings")]
        [Tag] public string TargetTag;

        [Header("Image Settings")]
        public Sprite SkinImage;
        public CSkinColor SkinColor;

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
                    if (uiObject[i].GetComponent<Image>())
                    {
                        uiObject[i].GetComponent<Image>().sprite = SkinImage;
                        if (SkinColor != CSkinColor.Default)
                            uiObject[i].GetComponent<Image>().color = ChangeImageColor();

                    }
                }
            }
        }

        Color ChangeImageColor()
        {
            Color result = Color.black;
            if (SkinColor == CSkinColor.Default) result = Color.white; 
            if (SkinColor == CSkinColor.Black) result = Color.black;
            if (SkinColor == CSkinColor.White) result = Color.white;
            if (SkinColor == CSkinColor.Red) result = Color.red;
            if (SkinColor == CSkinColor.Green) result = Color.green;
            if (SkinColor == CSkinColor.Blue) result = Color.blue;
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
