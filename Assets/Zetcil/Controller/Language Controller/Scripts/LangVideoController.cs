using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Xml;
using System.IO;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class LangVideoController : MonoBehaviour
    {
        public enum CLanguageType { Indonesian, English, Arabic, Korean, Japanese, Chinese }

        [Space(10)]
        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        public CLanguageType LanguageType;
        public VideoPlayer CurrentClip;
        string CurrentLanguage;

        [Header("Operation Settings")]
        public VideoClip IndonesianClip;
        public VideoClip EnglishClip;
        public VideoClip ArabicClip;
        public VideoClip KoreanClip;
        public VideoClip JapaneseClip;
        public VideoClip ChineseClip;

        [Header("Event Settings")]
        public bool usingLanguageEvent;
        public UnityEvent LanguageEvent;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        string ConfigDirectory = "Config";

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
                CurrentLanguage = xmlnodelist.Item(0).InnerText.Trim();
            }
        }

        void LoadCurrentLanguage(string aLanguage)
        {
            if (aLanguage == "ARABIC") LanguageType = CLanguageType.Arabic;
            if (aLanguage == "INDONESIAN") LanguageType = CLanguageType.Indonesian;
            if (aLanguage == "ENGLISH") LanguageType = CLanguageType.English;
            if (aLanguage == "KOREAN") LanguageType = CLanguageType.Korean;
            if (aLanguage == "JAPANESE") LanguageType = CLanguageType.Japanese;
            if (aLanguage == "CHINESE") LanguageType = CLanguageType.Chinese;

            CurrentClip.Stop();

            if (LanguageType == CLanguageType.Indonesian)
            {
                CurrentClip.clip = IndonesianClip;
            }
            if (LanguageType == CLanguageType.English)
            {
                CurrentClip.clip = EnglishClip;
            }
            if (LanguageType == CLanguageType.Arabic)
            {
                CurrentClip.clip = ArabicClip;
            }
            if (LanguageType == CLanguageType.Korean)
            {
                CurrentClip.clip = KoreanClip;
            }
            if (LanguageType == CLanguageType.Japanese)
            {
                CurrentClip.clip = JapaneseClip;
            }
            if (LanguageType == CLanguageType.Chinese)
            {
                CurrentClip.clip = ChineseClip;
            }

        }

        public void InvokeLangVideoController()
        {
            LoadConfig();
            LoadCurrentLanguage(CurrentLanguage);
            CurrentClip.Play();
            if (usingLanguageEvent)
            {
                LanguageEvent.Invoke();
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
                {
                    InvokeLangVideoController();
                }
            }
        }

        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnStart)
                {
                    InvokeLangVideoController();
                }
                if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    if (usingDelay)
                    {
                        Invoke("InvokeLangVideoController", Delay);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnInterval && usingInterval)
                {
                    InvokeLangVideoController();
                }
            }
        }
    }
}
