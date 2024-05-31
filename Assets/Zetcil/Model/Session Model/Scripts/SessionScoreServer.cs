using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace Zetcil
{
    public class SessionScoreServer : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [System.Serializable]
        public class CHighScore
        {
            public string ID;
            public float Score;
        }

        [Header("Config Settings")]
        public bool LoadOnStart;
        public VarConfig SessionConfig;

        [Header("Session Settings")]
        public VarStringList SessionList;
        public VarString SessionName;

        public void LoadSessionName(VarString aValue)
        {
            SessionName.CurrentValue = aValue.CurrentValue;
        }

        public void LoadSessionName(string aValue)
        {
            SessionName.CurrentValue = aValue;
        }

        public void LoadCurrentSession()
        {
            SessionName.CurrentValue = PlayerPrefs.GetString("CurrentSession");
            Debug.Log("Current Session: " + SessionName.CurrentValue);
        }

        public bool isSessionExists(string aValue)
        {
            bool result = false;
            string compName = aValue + ".xml";

            for (int i = 0; i < SessionList.StringListValue.Count; i++)
            {
                if (compName == SessionList.StringListValue[i])
                {
                    result = true;
                }
            }

            return result;
        }

        // Start is called before the first frame update
        void Start()
        {
            DirectoryInfo info = new DirectoryInfo(SessionConfig.GetSessionDirectory());
            FileInfo[] fileInfo = info.GetFiles();
            SessionList.StringListValue.Clear();
            foreach (var currfile in fileInfo)
            {
                if (currfile.Extension == ".xml")
                {
                    SessionList.StringListValue.Add(currfile.Name);
                }
            }

            if (LoadOnStart)
            {
                Invoke("LoadSessionScore", 1);
            }
        }

        char GetChar(int aIndex)
        {
            return (char)aIndex;
        }

        string SetNumberStringZero(int aValue)
        {
            string result = "01";

            if (aValue < 9)
            {
                result = "0" + aValue.ToString();
            } else
            {
                result = aValue.ToString();
            }

            return result;
        }

        public void LoadSessionScore()
        {

            for (int i = 0; i < SessionList.StringListValue.Count; i++)
            {
                float totalScore = 0;
                string[] tempName = SessionList.StringListValue[i].Split('.');

                CHighScore newscore = new CHighScore();

                newscore.ID = tempName[0];

                int LetterIndex = 65;
                int MaxLetterIndex = 90;
                bool loopLetter = true;

                while (loopLetter && LetterIndex <= MaxLetterIndex)
                {
                    int NumberIndex = 1;
                    int MaxNumberIndex = 15;
                    bool loopNumber = true;

                    while (loopNumber && NumberIndex <= MaxNumberIndex)
                    {
                        string ScoreFileName = newscore.ID + ".LV" + GetChar(LetterIndex) + ".Play" + SetNumberStringZero(NumberIndex) + ".Score.xml";
                        string FullPathFile = SessionConfig.GetDataSessionDirectory() + ScoreFileName;

                        if (File.Exists(FullPathFile))
                        {
                            string tempxml = System.IO.File.ReadAllText(FullPathFile);

                            XmlDocument xmldoc;
                            XmlNodeList xmlnodelist;
                            XmlNode xmlnode;
                            xmldoc = new XmlDocument();
                            xmldoc.LoadXml(tempxml);

                            xmlnodelist = xmldoc.GetElementsByTagName("SessionValue");
                            totalScore += float.Parse(xmlnodelist.Item(0).InnerText.Trim());
                        }

                        NumberIndex++;
                    }

                    LetterIndex++;
                }

                newscore.Score = totalScore;

                if (SessionName.CurrentValue == newscore.ID)
                {
                    //Debug.Log(newscore.Score);
                }

                //-- input to server
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
