using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;
using System.IO;
using System.Xml;

namespace Zetcil
{
    public class LayoutCornerView : MonoBehaviour
    {
        [System.Serializable]
        public class CVisualCharacter
        {
            [TextArea(3, 10)]
            public string CharacterDialog;
            public VarString DialogVar;
            public bool usingXML;
            [ConditionalField("usingXML")] public string XMLID;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Corner Dialog Setting")]
        public LayoutFadeView DialogPanel;
        public Text DialogText;
        public float TextDelay;
        public float FadeDelay;

        [Header("XML Setting")]
        public bool usingXML;
        public string XMLFilename;

        [Header("Left Corner Character")]
        public bool usingLeftCornerName;
        [ConditionalField("usingLeftCornerName")] public Text LeftCornerCharacterName;
        [ConditionalField("usingLeftCornerName")] public Image LeftCornerCharacterPanel;
        public bool usingLeftCornerPortrait;
        [ConditionalField("usingLeftCornerPortrait")] public GameObject LeftCornerCharacterPortrait;

        [Header("Right Corner Character")]
        public bool usingRightCornerName;
        [ConditionalField("usingRightCornerName")] public Text RightCornerCharacterName;
        [ConditionalField("usingRightCornerName")] public Image RightCornerCharacterPanel;
        public bool usingRightCornerPortrait;
        [ConditionalField("usingRightCornerPortrait")] public GameObject RightCornerCharacterPortrait;

        [Header("Character Dialog")]
        public bool usingCornerDialog;
        public List<CVisualCharacter> CornerDialog;

        [Header("ReadOnly Status")]
        [ReadOnly] public string CurrentDialog;
        [ReadOnly] public int CurrentStorytellingIndex;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        string FileName;
        string ConfigDirectory = "Config";
        string CornerDirectory = "Corner";

        string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public void LoadConfig()
        {
            string FullPathFile = GetDirectory(ConfigDirectory) + "Language.xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("Language");
                FileName = XMLFilename + xmlnodelist.Item(0).InnerText.Trim() + ".xml";
            }
        }

        string GetCurrentDialog(string DialogID)
        {
            LoadConfig();

            string result = "";
            string FullPathFile = GetDirectory(CornerDirectory) + FileName;

            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName(DialogID);
                result = xmlnodelist.Item(0).InnerText.Trim();

            }

            return result;
        }
        // Start is called before the first frame update
        void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                RestartCornerDialog();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                Invoke("RestartCornerDialog", Delay);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RestartCornerDialog()
        {
            CurrentStorytellingIndex = 0;
            DialogText.text = "";
            InvokeCornerDialog();
        }

        public void InvokeCornerDialog()
        {
            if (CurrentStorytellingIndex < CornerDialog.Count)
            {
                DialogPanel.gameObject.SetActive(true);

                LeftCornerCharacterName.gameObject.SetActive(false);
                LeftCornerCharacterPanel.gameObject.SetActive(false);
                LeftCornerCharacterPortrait.gameObject.SetActive(false);
                if (usingLeftCornerName)
                {
                    LeftCornerCharacterName.gameObject.SetActive(true);
                    LeftCornerCharacterPanel.gameObject.SetActive(true);
                }
                if (usingLeftCornerPortrait)
                {
                    LeftCornerCharacterPortrait.SetActive(true);
                }

                RightCornerCharacterName.gameObject.SetActive(false);
                RightCornerCharacterPanel.gameObject.SetActive(false);
                RightCornerCharacterPortrait.gameObject.SetActive(false);
                if (usingRightCornerName)
                {
                    RightCornerCharacterName.gameObject.SetActive(true);
                    RightCornerCharacterPanel.gameObject.SetActive(true);
                }
                if (usingRightCornerPortrait)
                {
                    RightCornerCharacterPortrait.SetActive(true);
                }

                //-- maindialog
                CurrentDialog = CornerDialog[CurrentStorytellingIndex].CharacterDialog;
                if (usingXML)
                {
                    CurrentDialog = GetCurrentDialog(CornerDialog[CurrentStorytellingIndex].XMLID);
                }
                if (CornerDialog[CurrentStorytellingIndex].DialogVar)
                {
                    CurrentDialog += " " + CornerDialog[CurrentStorytellingIndex].DialogVar.CurrentValue;
                }

                CurrentStorytellingIndex++;
                if (CurrentStorytellingIndex == CornerDialog.Count)
                {
                    Invoke("FadeCorner", FadeDelay);
                }
                else
                {
                    Invoke("InvokeCornerDialog", FadeDelay);
                }

                StartCoroutine("PlayText");
            } 
        }

        void FadeCorner()
        {
            DialogPanel.FadeOut();
        }

        IEnumerator PlayText()
        {
            DialogText.text = "";
            foreach (char c in CurrentDialog)
            {
                DialogText.text += c;
                yield return new WaitForSeconds(TextDelay);
            }
        }
    }
}
