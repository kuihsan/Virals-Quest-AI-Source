using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;

namespace Zetcil
{
    public class LayoutNotificationView : MonoBehaviour
    {
        [System.Serializable]
        public class CNotification
        {
            public LayoutFadeView NotificationPanel;
            public Text NotificationText;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Notification Settings")]
        public CNotification Notification1;
        public CNotification Notification2;
        public CNotification Notification3;
        public CNotification Notification4;
        public CNotification Notification5;

        [Header("XML Setting")]
        public bool usingXML;

        [Header("Delay Notification")]
        public float NotificationDelay;
        int CurrentNotification = 1;

        [Header("Delay Settings")]
        public bool usingDelay;
        public float Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public float Interval;

        string NotificationText1 = "";
        string NotificationText2 = "";
        string NotificationText3 = "";
        string NotificationText4 = "";
        string NotificationText5 = "";

        string FileName;
        string ConfigDirectory = "Config";
        string NotificationDirectory = "Notification";

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
                FileName = xmlnodelist.Item(0).InnerText.Trim() + ".xml";
            }
        }

        string GetCurrentDialog(string DialogID)
        {
            LoadConfig();

            string result = "";
            string FullPathFile = GetDirectory(NotificationDirectory) + FileName;

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

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InvokeNotification(string aContent)
        {
            switch (CurrentNotification)
            {
                case 1:
                    CurrentNotification++;
                    Notification1.NotificationPanel.gameObject.SetActive(true);
                    NotificationText1 = aContent;
                    if (usingXML)
                    {
                        NotificationText1 = GetCurrentDialog(aContent);
                    }
                    StartCoroutine("PlayNotification1");
                    Invoke("ShutdownNotification1", NotificationDelay);
                    break;
                case 2:
                    CurrentNotification++;
                    Notification2.NotificationPanel.gameObject.SetActive(true);
                    NotificationText2 = aContent;
                    if (usingXML)
                    {
                        NotificationText2 = GetCurrentDialog(aContent);
                    }
                    StartCoroutine("PlayNotification2");
                    Invoke("ShutdownNotification2", NotificationDelay);
                    break;
                case 3:
                    CurrentNotification++;
                    Notification3.NotificationPanel.gameObject.SetActive(true);
                    NotificationText3 = aContent;
                    if (usingXML)
                    {
                        NotificationText3 = GetCurrentDialog(aContent);
                    }
                    StartCoroutine("PlayNotification3");
                    Invoke("ShutdownNotification3", NotificationDelay);
                    break;
                case 4:
                    CurrentNotification++;
                    Notification4.NotificationPanel.gameObject.SetActive(true);
                    NotificationText4 = aContent;
                    if (usingXML)
                    {
                        NotificationText4 = GetCurrentDialog(aContent);
                    }
                    StartCoroutine("PlayNotification4");
                    Invoke("ShutdownNotification4", NotificationDelay);
                    break;
                case 5:
                    CurrentNotification = 1;
                    Notification5.NotificationPanel.gameObject.SetActive(true);
                    NotificationText5 = aContent;
                    if (usingXML)
                    {
                        NotificationText5 = GetCurrentDialog(aContent);
                    }
                    StartCoroutine("PlayNotification5");
                    Invoke("ShutdownNotification5", NotificationDelay);
                    break;
            }

        }

        public void ShutdownNotification1()
        {
            Notification1.NotificationPanel.FadeOut();
        }

        public void ShutdownNotification2()
        {
            Notification2.NotificationPanel.FadeOut();
        }
        public void ShutdownNotification3()
        {
            Notification3.NotificationPanel.FadeOut();
        }
        public void ShutdownNotification4()
        {
            Notification4.NotificationPanel.FadeOut();
        }
        public void ShutdownNotification5()
        {
            Notification5.NotificationPanel.FadeOut();
        }

        IEnumerator PlayNotification1()
        {
            Notification1.NotificationText.text = "";

            foreach (char c in NotificationText1)
            {
                Notification1.NotificationText.text += c;
                yield return new WaitForSeconds(0.01f);
            }

        }

        IEnumerator PlayNotification2()
        {
            Notification2.NotificationText.text = "";

            foreach (char c in NotificationText2)
            {
                Notification2.NotificationText.text += c;
                yield return new WaitForSeconds(0.01f);
            }

        }

        IEnumerator PlayNotification3()
        {
            Notification3.NotificationText.text = "";

            foreach (char c in NotificationText3)
            {
                Notification3.NotificationText.text += c;
                yield return new WaitForSeconds(0.01f);
            }

        }

        IEnumerator PlayNotification4()
        {
            Notification4.NotificationText.text = "";

            foreach (char c in NotificationText4)
            {
                Notification4.NotificationText.text += c;
                yield return new WaitForSeconds(0.01f);
            }

        }

        IEnumerator PlayNotification5()
        {
            Notification5.NotificationText.text = "";

            foreach (char c in NotificationText5)
            {
                Notification5.NotificationText.text += c;
                yield return new WaitForSeconds(0.01f);
            }

        }
    }
}
