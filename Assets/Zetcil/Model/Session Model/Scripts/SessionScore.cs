using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using TechnomediaLabs;

namespace Zetcil
{
    public class SessionScore : MonoBehaviour
    {

        [System.Serializable]
        public class CTaskSession
        {
            public int ID;
            public int KeyToken;
            public int UserToken;
            public string KeyAnswer;
            public string UserAnswer;
            public float KeyScore;
            public float UserScore;
        }

        [System.Serializable]
        public class CLevelSession
        {
            public int Level;
            public bool Status;
            public float Score;
            public int Star;
            public List<CTaskSession> TaskSession;
        }

        [System.Serializable]
        public class CHighScore
        {
            public string Name;
            public float Score;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public bool LoadOnStart;
        public VarConfig SessionConfig;

        [Header("Session Settings")]
        public VarStringList SessionList;
        public VarString SessionName;

        [Header("HighScore Settings")]
        public GameObject TargetContent;
        public GameObject TargetContentRow;
        public List<CHighScore> TargetHighScore;

        [Header("ReadOnly Status")]
        [ReadOnly] public int CurrentLevel;
        [ReadOnly] public int CurrentTask;

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
                LoadSessionScore();            
            }
        }

        char GetChar(int aIndex)
        {
            return (char) aIndex;
        }

        public void LoadSessionScore()
        {

            TargetHighScore.Clear();

            for (int i = 0; i < SessionList.StringListValue.Count; i++)
            {
                CHighScore newscore = new CHighScore();

                float totalScore = 0;
                string[] tempName = SessionList.StringListValue[i].Split('.');
                newscore.Name = tempName[0];

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
                        string ScoreFileName = newscore.Name + ".Level" + GetChar(LetterIndex) + NumberIndex + ".Score.xml";
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
                        else
                        {
                            loopNumber = false;
                        }

                        NumberIndex++;
                    }

                    LetterIndex++;
                }

                newscore.Score = totalScore;
                //Add New Score
                TargetHighScore.Add(newscore);

                /*
                string FullPathFile = SessionConfig.GetSessionDirectory() + SessionList.StringListValue[i];
                if (File.Exists(FullPathFile))
                {
                    string tempxml = System.IO.File.ReadAllText(FullPathFile);

                    XmlDocument xmldoc;
                    XmlNodeList xmlnodelist;
                    XmlNode xmlnode;
                    xmldoc = new XmlDocument();
                    xmldoc.LoadXml(tempxml);

                    xmlnodelist = xmldoc.GetElementsByTagName("LevelTotal");
                    int total = int.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    for (int j = 0; j < total; j++)
                    {
                        xmlnodelist = xmldoc.GetElementsByTagName("Score" + (j + 1).ToString());
                        tempScore += float.Parse(xmlnodelist.Item(0).InnerText.Trim());
                    }
                }

                newscore.Score = tempScore;

                //Add New Score
                TargetHighScore.Add(newscore);
                */
            }

            //TargetHighScore.Sort(SortByScore);

            //--Add List To UI
            for (int i = 0; i < TargetHighScore.Count; i++)
            {
                GameObject tempScoreRow = GameObject.Instantiate(TargetContentRow, TargetContent.transform);
                
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionNo.text = (i + 1).ToString();
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionName.text = TargetHighScore[i].Name;
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionScore.text = TargetHighScore[i].Score.ToString();

            }


        }

        static int SortByScore(CHighScore p1, CHighScore p2)
        {
            return p2.Score.CompareTo(p1.Score);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
