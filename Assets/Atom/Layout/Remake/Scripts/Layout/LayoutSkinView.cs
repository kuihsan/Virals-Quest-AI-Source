using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

namespace Zetcil
{
    public class LayoutSkinView : MonoBehaviour
    {
        public enum CSkinSettings { Default, Dialog, Header100, Header300, MainButton, SquareButton, RectButton }
        public enum CLayoutColor { Header, Footer, Background, Panel, Icon, PrimaryButton, SecondaryButton }

        [Space(10)]
        public bool isEnabled;

        [Header("Skin Settings")]
        public CSkinSettings SkinSetting;

        [Header("Color Settings")]
        public CLayoutColor LayoutColor;

        string ConfigDirectory = "Config";
        string LayoutDirectory = "Layout";

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        void InvokeSkin()
        {
            GameObject skinManager = GameObject.FindGameObjectWithTag("UISkin");
            int skinIndex = skinManager.GetComponent<LayoutSkinManager>().CurrentSkinIndex;
            if (SkinSetting == CSkinSettings.Dialog)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].Dialog;
            }
            if (SkinSetting == CSkinSettings.MainButton)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].MainButton;
            }
            if (SkinSetting == CSkinSettings.Header100)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].Header100;
            }
            if (SkinSetting == CSkinSettings.Header300)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].Header300;
            }
            if (SkinSetting == CSkinSettings.RectButton)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].RectButton;
            }
            if (SkinSetting == CSkinSettings.SquareButton)
            {
                GetComponent<Image>().sprite = skinManager.GetComponent<LayoutSkinManager>().SkinCollection[skinIndex].SquareButton;
            }
        }

        Color GetCurrentLayout(string xmlTag)
        {
            Color result = Color.black;

            string FullPathFile = GetDirectory(ConfigDirectory) + "Layout.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName(xmlTag);

                result.r = float.Parse(xmlnodelist.Item(0).Attributes[0].Value);
                result.g = float.Parse(xmlnodelist.Item(0).Attributes[1].Value);
                result.b = float.Parse(xmlnodelist.Item(0).Attributes[2].Value);
                result.a = float.Parse(xmlnodelist.Item(0).Attributes[3].Value);
            }

            return result;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (LayoutColor == CLayoutColor.Header)
                {
                    GetComponent<Image>().color = GetCurrentLayout("HeaderColor");
                }
                else if (LayoutColor == CLayoutColor.Footer)
                {
                    GetComponent<Image>().color = GetCurrentLayout("FooterColor"); 
                }
                else if (LayoutColor == CLayoutColor.Background)
                {
                    GetComponent<Image>().color = GetCurrentLayout("BackgroundColor"); 
                }
                else if (LayoutColor == CLayoutColor.Panel)
                {
                    GetComponent<Image>().color = GetCurrentLayout("PanelColor"); 
                }
                else if (LayoutColor == CLayoutColor.Icon)
                {
                    GetComponent<Image>().color = GetCurrentLayout("IconColor"); 
                }
                else if (LayoutColor == CLayoutColor.PrimaryButton)
                {
                    if (GetComponent<Button>())
                    {
                        ColorBlock tempColor = GetComponent<Button>().colors;

                        tempColor.normalColor = GetCurrentLayout("PrimaryNormalColor");
                        tempColor.highlightedColor = GetCurrentLayout("PrimaryHighlightColor");
                        tempColor.pressedColor = GetCurrentLayout("PrimaryPressedColor");
                        tempColor.selectedColor = GetCurrentLayout("PrimarySelectedColor");
                        tempColor.disabledColor = GetCurrentLayout("PrimaryDisabledColor");

                        GetComponent<Button>().colors = tempColor;
                    } 
                    else if (GetComponent<Toggle>())
                    {
                        ColorBlock tempColor = GetComponent<Toggle>().colors;

                        tempColor.normalColor = GetCurrentLayout("PrimaryNormalColor");
                        tempColor.highlightedColor = GetCurrentLayout("PrimaryHighlightColor");
                        tempColor.pressedColor = GetCurrentLayout("PrimaryPressedColor");
                        tempColor.selectedColor = GetCurrentLayout("PrimarySelectedColor");
                        tempColor.disabledColor = GetCurrentLayout("PrimaryDisabledColor");

                        GetComponent<Toggle>().colors = tempColor;
                    }


                }
                else if (LayoutColor == CLayoutColor.SecondaryButton)
                {
                    if (GetComponent<Button>())
                    {
                        ColorBlock tempColor = GetComponent<Button>().colors;

                        tempColor.normalColor = GetCurrentLayout("PrimaryNormalColor");
                        tempColor.highlightedColor = GetCurrentLayout("PrimaryHighlightColor");
                        tempColor.pressedColor = GetCurrentLayout("PrimaryPressedColor");
                        tempColor.selectedColor = GetCurrentLayout("PrimarySelectedColor");
                        tempColor.disabledColor = GetCurrentLayout("PrimaryDisabledColor");

                        GetComponent<Button>().colors = tempColor;
                    }
                    else if (GetComponent<Toggle>())
                    {
                        ColorBlock tempColor = GetComponent<Toggle>().colors;

                        tempColor.normalColor = GetCurrentLayout("PrimaryNormalColor");
                        tempColor.highlightedColor = GetCurrentLayout("PrimaryHighlightColor");
                        tempColor.pressedColor = GetCurrentLayout("PrimaryPressedColor");
                        tempColor.selectedColor = GetCurrentLayout("PrimarySelectedColor");
                        tempColor.disabledColor = GetCurrentLayout("PrimaryDisabledColor");

                        GetComponent<Toggle>().colors = tempColor;
                    }
                }

                InvokeSkin();
            }
        }

    }
}
