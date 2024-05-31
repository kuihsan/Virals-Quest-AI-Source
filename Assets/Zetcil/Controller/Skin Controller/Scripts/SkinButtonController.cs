using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{
    public class SkinButtonController : MonoBehaviour
    {
        public enum CFontColor { Default, Black, White, Red, Green, Blue }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Tag Settings")]
        [Tag] public string TargetTag;

        [Header("Image Settings")]
        public Sprite SkinImage;

        [Header("Button Settings")]
        public CFontColor NormalColor;
        public CFontColor HighlightColor;
        public CFontColor PressedColor;
        public CFontColor SelectedColor;
        public CFontColor DisabledColor;

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
                foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (go.tag == TargetTag)
                    {
                        if (go.GetComponent<Button>())
                        {
                            go.GetComponent<Button>().image.sprite = SkinImage;

                            ColorBlock tempColor = go.GetComponent<Button>().colors;

                            tempColor.normalColor = ChangeColor(NormalColor);
                            tempColor.highlightedColor = ChangeColor(HighlightColor);
                            tempColor.pressedColor = ChangeColor(PressedColor);
                            tempColor.selectedColor = ChangeColor(SelectedColor);
                            tempColor.disabledColor = ChangeColor(DisabledColor);

                            go.GetComponent<Button>().colors = tempColor;

                            if (go.GetComponentInChildren<Text>())
                            {
                                go.GetComponentInChildren<Text>().font = FontName;
                                go.GetComponentInChildren<Text>().fontSize = FontSize;
                                if (FontColor != CFontColor.Default)
                                    go.GetComponentInChildren<Text>().color = ChangeColor(FontColor);
                            }
                        }
                    }
                    /*
                    GameObject[] uiObject = GameObject.FindGameObjectsWithTag(TargetTag);
                    for (int i = 0; i < uiObject.Length; i++)
                    {
                        if (uiObject[i].GetComponent<Button>())
                        {
                            uiObject[i].GetComponent<Button>().image.sprite = SkinImage;

                            ColorBlock tempColor = uiObject[i].GetComponent<Button>().colors;

                            tempColor.normalColor = ChangeColor(NormalColor);
                            tempColor.highlightedColor = ChangeColor(HighlightColor);
                            tempColor.pressedColor = ChangeColor(PressedColor);
                            tempColor.selectedColor = ChangeColor(SelectedColor);
                            tempColor.disabledColor = ChangeColor(DisabledColor);

                            uiObject[i].GetComponent<Button>().colors = tempColor;

                            if (uiObject[i].GetComponentInChildren<Text>())
                            {
                                uiObject[i].GetComponentInChildren<Text>().font = FontName;
                                uiObject[i].GetComponentInChildren<Text>().fontSize = FontSize;
                                if (FontColor != CFontColor.Default)
                                    uiObject[i].GetComponentInChildren<Text>().color = ChangeColor(FontColor);
                            }
                        }
                    }
                    */
                }
            }
        }

        Color ChangeColor(CFontColor targetColor)
        {
            Color result = Color.black;
            if (targetColor == CFontColor.Default) result = Color.white;
            if (targetColor == CFontColor.Black) result = Color.black;
            if (targetColor == CFontColor.White) result = Color.white;
            if (targetColor == CFontColor.Red) result = Color.red;
            if (targetColor == CFontColor.Green) result = Color.green;
            if (targetColor == CFontColor.Blue) result = Color.blue;
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
