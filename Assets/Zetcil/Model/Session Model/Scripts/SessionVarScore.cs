using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using TechnomediaLabs;

namespace Zetcil
{
    public class SessionVarScore : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public VarConfig SessionConfig;
        public VarString SessionName;

        [Header("Session Settings")]
        public VarString SessionKey;
        public VarScore SessionValue;

        [Header("Load Setting")]
        public bool LoadOnStart;
        public float LoadDelay;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("SessionLoad", LoadDelay);
        }

        void SessionLoad()
        {
            if (LoadOnStart)
            {
                LoadSession(SessionKey);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSessionName(VarString aValue)
        {
            SessionName.CurrentValue = aValue.CurrentValue;
        }

        public void SetSessionName(string aValue)
        {
            SessionName.CurrentValue = aValue;
        }

        public void SetSessionName(InputField aValue)
        {
            SessionName.CurrentValue = aValue.text;
        }

        public void SetSessionName(Text aValue)
        {
            SessionName.CurrentValue = aValue.text;
        }

        public void SetSessionValue(VarScore aValue)
        {
            SessionValue.CurrentValue = aValue.CurrentValue;
        }

        public void SetSessionValue(float aValue)
        {
            SessionValue.CurrentValue = aValue;
        }

        public string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public bool isSessionExists(string aValue)
        {
            bool result = false;
            string compName = aValue + ".xml";

            DirectoryInfo info = new DirectoryInfo(SessionConfig.GetSessionDirectory());
            FileInfo[] fileInfo = info.GetFiles();

            foreach (var currfile in fileInfo)
            {
                if (currfile.Extension == ".xml")
                {
                    if (compName == currfile.Name)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public void CreateSession()
        {
            if (isSessionExists(SessionName.CurrentValue))
            {
                //do nothing
            }
            else
            {
                //-- Create SessionUser
                string header = "<SessionUser>\n";
                string footer = "</SessionUser>";
                string result = "";
                string singleValue = SessionConfig.SetXMLValueSingle("SessionValue", SessionName.CurrentValue);
                result = header + singleValue + footer;
                string DirName = SessionConfig.GetSessionDirectory();
                var sr = File.CreateText(DirName + SessionName.CurrentValue + ".xml");
                sr.WriteLine(result);
                sr.Flush();
                sr.Close();
            }

            //-- Create CurrentUser
            string cheader = "<SessionUser>\n";
            string cfooter = "</SessionUser>";
            string cresult = "";
            string csingleValue = SessionConfig.SetXMLValueSingle("SessionValue", SessionName.CurrentValue);
            cresult = cheader + csingleValue + cfooter;
            string cDirName = SessionConfig.GetDataSessionDirectory();
            var csr = File.CreateText(cDirName + "Default.SessionName.xml");
            csr.WriteLine(cresult);
            csr.Flush();
            csr.Close();

        }

        public void SaveSession(string aSessionName)
        {
            string header = "<SessionData>\n";
            string footer = "</SessionData>";
            string result = "";

            string singleValue = SessionConfig.SetXMLValueSingle("SessionValue", SessionValue.CurrentValue.ToString());

            result = header + singleValue + footer;

            string DirName = SessionConfig.GetDataSessionDirectory();
            var sr = File.CreateText(DirName + SessionName.CurrentValue + "." + aSessionName + ".xml");
            sr.WriteLine(result);
            sr.Flush();
            sr.Close();
        }

        public void SaveSession(VarString aSessionName)
        {
            SaveSession(aSessionName.CurrentValue);
        }

        public void SaveSession(InputField aSessionName)
        {
            SaveSession(aSessionName.text);
        }

        public void SaveSession(Text aSessionName)
        {
            SaveSession(aSessionName.text);
        }

        public void LoadSession(string aSessionKey)
        {
            string FullPathFile = "";

            FullPathFile = SessionConfig.GetDataSessionDirectory() + SessionName.CurrentValue + "." + aSessionKey + ".xml";

            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                xmlnodelist = xmldoc.GetElementsByTagName("SessionValue");
                SessionValue.CurrentValue = float.Parse(xmlnodelist.Item(0).InnerText.Trim());
            }
        }

        public void LoadSession(VarString aSessionKey)
        {
            LoadSession(aSessionKey.CurrentValue);
        }
        public void LoadSession(InputField aSessionKey)
        {
            LoadSession(aSessionKey.text);
        }

        public void LoadSession(Text aSessionKey)
        {
            LoadSession(aSessionKey.text);
        }
    }
}
