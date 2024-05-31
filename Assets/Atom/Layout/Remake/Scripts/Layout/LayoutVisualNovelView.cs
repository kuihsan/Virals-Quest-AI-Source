using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;
using System.IO;
using System.Xml;

namespace Zetcil
{
    public class LayoutVisualNovelView : MonoBehaviour
    {
        public enum CCharacterPosition { None, Left, Center, Right }

        [System.Serializable]
        public class CVisualCharacter
        {
            [TextArea(3, 5)]
            public string CharacterDialog;
            public bool usingXML;
            [ConditionalField("usingXML")] public string XMLID;
        }

        [System.Serializable]
        public class CStorytelling
        {
            public CCharacterPosition CharacterPosition;
            public int DialogIndex;
            public bool usingStorytellingEvent;
            public UnityEvent StorytellingEvent;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Visual Novel Setting")]
        public GameObject DialogPanel;
        public GameObject DialogNext;
        public AudioSource DialogSound;
        public Text DialogText;
        public float TextDelay;

        [Header("XML Setting")]
        public bool usingXML;
        public string XMLFilename;

        [Header("Left Character")]
        public GameObject LeftCharacterPortrait;
        public Text LeftCharacterName;
        public AudioClip LeftCharacterSound;

        [Header("Center Character")]
        public GameObject CenterCharacterPortrait;
        public Text CenterCharacterName;
        public AudioClip CenterCharacterSound;

        [Header("Right Character")]
        public GameObject RightCharacterPortrait;
        public Text RightCharacterName;
        public AudioClip RightCharacterSound;

        [Header("Left Character Dialog")]
        public bool usingLeftCharacter;
        public List<CVisualCharacter> LeftCharacter;

        [Header("Center Character Dialog")]
        public bool usingCenterCharacter;
        public List<CVisualCharacter> CenterCharacter;

        [Header("Right Character Dialog")]
        public bool usingRightCharacter;
        public List<CVisualCharacter> RightCharacter;

        [Header("Storytelling Setting")]
        public bool usingStorytelling;
        public KeyCode StorytellingTrigger = KeyCode.Mouse0;
        public List<CStorytelling> Storytelling;

        [Header("Final Dialog")]
        public UnityEvent FinalDialogEvent;

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
        string VisualNovelDirectory = "VisualNovel";

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

        string GetCurrentDialog (string DialogID)
        {
            LoadConfig();

            string result = "";
            string FullPathFile = GetDirectory(VisualNovelDirectory) + FileName;

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
            LoadConfig();

            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                RestartInvokeVisualNovel();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
            {
                Invoke("RestartInvokeVisualNovel", Delay);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(StorytellingTrigger))
            {
                InvokeVisualNovel();
            }
        }

        public void RestartInvokeVisualNovel()
        {
            CurrentStorytellingIndex = 0;
            DialogText.text = "";
            InvokeVisualNovel();
        }

        public void InvokeVisualNovel()
        {
            if (CurrentStorytellingIndex < Storytelling.Count)
            {
                DialogPanel.SetActive(true);

                if (Storytelling[CurrentStorytellingIndex].CharacterPosition == CCharacterPosition.Left)
                {
                    if (usingLeftCharacter)
                    {
                        LeftCharacterPortrait.SetActive(true);
                        LeftCharacterName.gameObject.SetActive(true);
                        CenterCharacterPortrait.SetActive(false);
                        CenterCharacterName.gameObject.SetActive(false);
                        RightCharacterPortrait.SetActive(false);
                        RightCharacterName.gameObject.SetActive(false);

                        DialogSound.clip = LeftCharacterSound;
                        DialogSound.Play();

                        //-- maindialog
                        CurrentDialog = LeftCharacter[Storytelling[CurrentStorytellingIndex].DialogIndex].CharacterDialog;
                        if (LeftCharacter[Storytelling[CurrentStorytellingIndex].DialogIndex].usingXML)
                        {
                            CurrentDialog = GetCurrentDialog(LeftCharacter[Storytelling[CurrentStorytellingIndex].DialogIndex].XMLID);
                        }
                        StartCoroutine("PlayText");

                    }
                }

                else if (Storytelling[CurrentStorytellingIndex].CharacterPosition == CCharacterPosition.Center)
                {
                    if (usingCenterCharacter)
                    {
                        LeftCharacterPortrait.SetActive(false);
                        LeftCharacterName.gameObject.SetActive(false);
                        CenterCharacterPortrait.SetActive(true);
                        CenterCharacterName.gameObject.SetActive(true);
                        RightCharacterPortrait.SetActive(false);
                        RightCharacterName.gameObject.SetActive(false);

                        DialogSound.clip = CenterCharacterSound;
                        DialogSound.Play();

                        //-- maindialog
                        CurrentDialog = CenterCharacter[Storytelling[CurrentStorytellingIndex].DialogIndex].CharacterDialog;
                        StartCoroutine("PlayText");
                    }
                }

                else if (Storytelling[CurrentStorytellingIndex].CharacterPosition == CCharacterPosition.Right)
                {
                    if (usingRightCharacter)
                    {
                        LeftCharacterPortrait.SetActive(false);
                        LeftCharacterName.gameObject.SetActive(false);
                        CenterCharacterPortrait.SetActive(false);
                        CenterCharacterName.gameObject.SetActive(false);
                        RightCharacterPortrait.SetActive(true);
                        RightCharacterName.gameObject.SetActive(true);

                        DialogSound.clip = RightCharacterSound;
                        DialogSound.Play();

                        //-- maindialog
                        CurrentDialog = RightCharacter[Storytelling[CurrentStorytellingIndex].DialogIndex].CharacterDialog;
                        StartCoroutine("PlayText");
                    }
                }
            }
            else
            {
                DialogPanel.SetActive(false);
                FinalDialogEvent.Invoke();
            }
        }

        IEnumerator PlayText()
        {
            DialogText.text = "";
            DialogNext.SetActive(false);

            foreach (char c in CurrentDialog)
            {
                DialogText.text += c;
                yield return new WaitForSeconds(TextDelay);
            }

            CurrentStorytellingIndex++;
            DialogNext.SetActive(true);
        }
    }
}
