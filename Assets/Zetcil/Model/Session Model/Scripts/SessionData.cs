using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using TechnomediaLabs;

namespace Zetcil
{

    public class SessionData : MonoBehaviour
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
        public class CCharacterSession
        {
            public int Index;
            public string Name;
            public bool Status;
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

        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public bool LoadOnStart;
        public VarConfig SessionConfig;

        [Header("Session Settings")]
        public VarStringList SessionList;
        public VarString SessionName;

        [Header("Character Settings")]
        public List<CCharacterSession> CharacterSession;

        [Header("Level Settings")]
        public List<CLevelSession> LevelSession;

        [Header("ReadOnly Status")]
        [ReadOnly] public int CurrentIndex;
        [ReadOnly] public int CurrentLevel;
        [ReadOnly] public int CurrentTask;

        public void CreateSession(VarString aValue)
        {
            SessionName.CurrentValue = aValue.CurrentValue;
            if (isSessionExists(SessionName.CurrentValue))
            {
                PlayerPrefs.SetString("CurrentSession", SessionName.CurrentValue);
                LoadSessionName(SessionName.CurrentValue);
                LoadSession();
            } else
            {
                SaveSessionName(SessionName.CurrentValue);
                SaveSession();
            }
        }

        public void ClearCharacterStatus()
        {
            for (int i = 0; i < CharacterSession.Count; i++)
            {
                CharacterSession[i].Status = false;
            }
        }

        public void SaveCharacterIndex(int aValue)
        {
            ClearCharacterStatus();
            for (int i = 0; i < CharacterSession.Count; i++)
            {
                if (i == aValue)
                {
                    CharacterSession[i].Status = true;
                }
            }
        }

        public void SaveIntPrefLevelStatus(string aID)
        {
            CurrentIndex = CurrentLevel - 1;
            PlayerPrefs.SetInt(aID, LevelSession[CurrentIndex].Level);
        }

        public void SaveIntPrefStarStatus(string aID)
        {
            CurrentIndex = CurrentLevel - 1;
            PlayerPrefs.SetInt(aID, LevelSession[CurrentIndex].Star);
        }

        public void SaveBoolPrefLevelStatus(string aID)
        {
            CurrentIndex = CurrentLevel - 1;
            PlayerPrefs.SetString(aID, LevelSession[CurrentIndex].Status.ToString());
        }

        public void SaveFloatPrefLevelStatus(string aID)
        {
            CurrentIndex = CurrentLevel - 1;
            PlayerPrefs.SetFloat(aID, LevelSession[CurrentIndex].Score);
        }

        public void SaveSessionName(VarString aValue)
        {
            SessionName.CurrentValue = aValue.CurrentValue;
            PlayerPrefs.SetString("CurrentSession", SessionName.CurrentValue);
        }

        public void SaveSessionName(string aValue)
        {
            SessionName.CurrentValue = aValue;
            PlayerPrefs.SetString("CurrentSession", SessionName.CurrentValue);
        }

        public void SaveCurrentSession()
        {
            SaveSession();
        }

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

        #region // CurrentLevel Function //

        public void SetCurrentLevel(VarInteger aValue)
        {
            CurrentLevel = aValue.CurrentValue;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }

        public void SetCurrentLevel(int aValue)
        {
            CurrentLevel = aValue;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }

        public void GetCurrentLevel()
        {
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        public void SetCurrentLevelStatus(VarBoolean aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Status = aValue;
        }

        public void SetCurrentLevelStatus(bool aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Status = aValue;
        }

        public bool GetCurrentLevelStatus()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].Status;
        }

        public void SetCurrentLevelScore(VarFloat aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Score = aValue.CurrentValue;
        }

        public void SetCurrentLevelScore(VarScore aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Score = aValue.CurrentValue;
        }

        public void SetCurrentLevelScore(float aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Score = aValue;
        }

        public float GetCurrentLevelScore()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].Score;
        }

        public void GetCurrentLevelScore(VarFloat aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            aValue.CurrentValue = LevelSession[CurrentIndex].Score;
        }

        public void SetCurrentLevelStar(VarInteger aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Star = aValue.CurrentValue;
        }

        public void SetCurrentLevelStar(int aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].Star = aValue;
        }

        public void GetCurrentLevelStar(VarInteger aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            aValue.CurrentValue = LevelSession[CurrentIndex].Star;

        }

        public int GetCurrentLevelStar()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].Star;
        }

        #endregion

        #region // Current Task Function //

        public void SetCurrentTask(VarInteger aValue)
        {
            CurrentTask = aValue.CurrentValue;
        }

        public void SetCurrentTask(int aValue)
        {
            CurrentTask = aValue;
        }

        public int GetCurrentTask()
        {
            return CurrentTask;
        }

        public void SetCurrentTaskKeyToken(VarInteger aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyToken = aValue.CurrentValue;
        }

        public void SetCurrentTaskKeyToken(int aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyToken = aValue;
        }

        public int GetCurrentTaskKeyToken()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyToken;
        }

        public void SetCurrentTaskUserToken(VarInteger aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserToken = aValue.CurrentValue;
        }

        public void SetCurrentTaskUserToken(int aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserToken = aValue;
        }

        public int GetCurrentTaskUserToken()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].UserToken;
        }

        public void SetCurrentTaskKeyAnswer(VarString aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyAnswer = aValue.CurrentValue;
        }

        public void SetCurrentTaskKeyAnswer(string aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyAnswer = aValue;
        }

        public string GetCurrentTaskKeyAnswer()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyAnswer;
        }

        public void SetCurrentTaskUserAnswer(VarString aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserAnswer = aValue.CurrentValue;
        }

        public void SetCurrentTaskUserAnswer(string aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserAnswer = aValue;
        }

        public string GetCurrentTaskUserAnswer()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].UserAnswer;
        }

        public void SetCurrentTaskKeyScore(VarFloat aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyScore = aValue.CurrentValue;
        }

        public void SetCurrentTaskKeyScore(float aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyScore = aValue;
        }

        public float GetCurrentTaskKeyScore()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].KeyScore;
        }

        public void SetCurrentTaskUserScore(VarFloat aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserScore = aValue.CurrentValue;
        }

        public void SetCurrentTaskUserScore(float aValue)
        {
            CurrentIndex = CurrentLevel - 1;
            LevelSession[CurrentIndex].TaskSession[CurrentTask].UserScore = aValue;
        }

        public float GetCurrentTaskUserScore()
        {
            CurrentIndex = CurrentLevel - 1;
            return LevelSession[CurrentIndex].TaskSession[CurrentTask].UserScore;
        }

        #endregion

        public void SaveSession()
        {
            string header = "<SessionData>\n";
            string footer = "</SessionData>";
            string result = "";

            string characterTotal = SessionConfig.SetXMLValueSingle("CharacterTotal", CharacterSession.Count.ToString());
            result += characterTotal;
            for (int i = 0; i < CharacterSession.Count; i++)
            {
                string charOpen = SessionConfig.SetXMLOpenTag("Character" + CharacterSession[i].Index.ToString());
                string charClose = SessionConfig.SetXMLCloseTag("Character" + CharacterSession[i].Index.ToString());
                string charName = SessionConfig.SetXMLValueSingle("CharacterName" + CharacterSession[i].Index.ToString(), CharacterSession[i].Name.ToString(), 2);
                string charStatus = SessionConfig.SetXMLValueSingle("CharacterStatus" + CharacterSession[i].Index.ToString(), CharacterSession[i].Status.ToString(), 2);

                result += charOpen +
                          charName +
                          charStatus +
                          charClose;
            }

            string levelTotal = SessionConfig.SetXMLValueSingle("LevelTotal", LevelSession.Count.ToString());

            result += levelTotal;
            for (int i=0; i<LevelSession.Count; i++)
            {
                string levelOpen = SessionConfig.SetXMLOpenTag("Level" + LevelSession[i].Level.ToString());
                string levelClose = SessionConfig.SetXMLCloseTag("Level" + LevelSession[i].Level.ToString());
                string levelStatus = SessionConfig.SetXMLValueSingle("Status" + LevelSession[i].Level.ToString(), LevelSession[i].Status.ToString(), 2);
                string levelScore = SessionConfig.SetXMLValueSingle("Score" + LevelSession[i].Level.ToString(), LevelSession[i].Score.ToString(), 2);
                string levelStar = SessionConfig.SetXMLValueSingle("Star" + LevelSession[i].Level.ToString(), LevelSession[i].Star.ToString(), 2);

                string taskTotal = SessionConfig.SetXMLValueSingle("TaskTotal_" + i.ToString(), LevelSession[i].TaskSession.Count.ToString(), 2);
                string taskStatus = taskTotal;
                for (int j = 0; j < LevelSession[i].TaskSession.Count; j++)
                {
                    string task_open = SessionConfig.SetXMLOpenTag("Task_" + LevelSession[i].Level.ToString() + "_" + j.ToString() + " ID=\"" + LevelSession[i].TaskSession[j].ID.ToString()+"\"", 2);
                    string task_close = SessionConfig.SetXMLCloseTag("Task_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), 2);

                    string task_keytoken = SessionConfig.SetXMLValueSingle("KeyToken_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].KeyToken.ToString(), 3);
                    string task_usertoken = SessionConfig.SetXMLValueSingle("UserToken_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].UserToken.ToString(), 3);

                    string task_keyanswer = SessionConfig.SetXMLValueSingle("KeyAnswer_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].KeyAnswer.ToString(), 3);
                    string task_useranswer = SessionConfig.SetXMLValueSingle("UserAnswer_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].UserAnswer.ToString(), 3);

                    string task_keyscore = SessionConfig.SetXMLValueSingle("KeyScore_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].KeyScore.ToString(), 3);
                    string task_userscore = SessionConfig.SetXMLValueSingle("UserScore_" + LevelSession[i].Level.ToString() + "_" + j.ToString(), LevelSession[i].TaskSession[j].UserScore.ToString(), 3);

                    taskStatus += task_open +
                        task_keytoken +
                        task_usertoken +
                        task_keyanswer +
                        task_useranswer +
                        task_keyscore +
                        task_userscore +
                        task_close
                        ;
                }

                result += levelOpen +
                          levelStatus +
                          levelScore +
                          levelStar +
                          taskStatus + 
                          levelClose;
            }

            result = header + result + footer;

            string DirName = SessionConfig.GetSessionDirectory();
            var sr = File.CreateText(DirName + SessionName.CurrentValue + ".xml");
            sr.WriteLine(result);
            sr.Flush();
            sr.Close();
        }

        public void LoadSession()
        {
            string FullPathFile = SessionConfig.GetSessionDirectory() + SessionName.CurrentValue +  ".xml";
            if (File.Exists(FullPathFile))
            {
                string tempxml = System.IO.File.ReadAllText(FullPathFile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(tempxml);

                CharacterSession.Clear();
                xmlnodelist = xmldoc.GetElementsByTagName("CharacterTotal");
                int ctotal = int.Parse(xmlnodelist.Item(0).InnerText.Trim());
                for (int i = 0; i < ctotal; i++)
                {
                    CCharacterSession charSession = new CCharacterSession();

                    charSession.Index = i + 1;

                    xmlnodelist = xmldoc.GetElementsByTagName("CharacterName" + (i + 1).ToString());
                    charSession.Name = xmlnodelist.Item(0).InnerText.Trim();

                    xmlnodelist = xmldoc.GetElementsByTagName("CharacterStatus" + (i + 1).ToString());
                    charSession.Status = bool.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    //--Add Character
                    CharacterSession.Add(charSession);
                }

                LevelSession.Clear();
                xmlnodelist = xmldoc.GetElementsByTagName("LevelTotal");
                int total = int.Parse(xmlnodelist.Item(0).InnerText.Trim());
                for (int i = 0; i<total; i++)
                {
                    CLevelSession levelSession = new CLevelSession();
                    levelSession.TaskSession = new List<CTaskSession>();

                    //--Parameters
                    levelSession.Level = (i + 1);

                    xmlnodelist = xmldoc.GetElementsByTagName("Status" + (i+1).ToString());
                    levelSession.Status = bool.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    xmlnodelist = xmldoc.GetElementsByTagName("Score" + (i + 1).ToString());
                    levelSession.Score = float.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    xmlnodelist = xmldoc.GetElementsByTagName("Star" + (i + 1).ToString());
                    levelSession.Star = int.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    xmlnodelist = xmldoc.GetElementsByTagName("TaskTotal_" + (i).ToString());
                    int task_total = int.Parse(xmlnodelist.Item(0).InnerText.Trim());

                    for (int j = 0; j < task_total; j++)
                    {
                        CTaskSession taskSession = new CTaskSession();

                        xmlnodelist = xmldoc.GetElementsByTagName("KeyToken_" + (i + 1).ToString() + "_" +j.ToString());
                        taskSession.KeyToken = int.Parse(xmlnodelist.Item(0).InnerText.Trim());

                        xmlnodelist = xmldoc.GetElementsByTagName("UserToken_" + (i + 1).ToString() + "_" + j.ToString());
                        taskSession.UserToken = int.Parse(xmlnodelist.Item(0).InnerText.Trim());

                        xmlnodelist = xmldoc.GetElementsByTagName("KeyAnswer_" + (i + 1).ToString() + "_" + j.ToString());
                        taskSession.KeyAnswer = xmlnodelist.Item(0).InnerText.Trim();

                        xmlnodelist = xmldoc.GetElementsByTagName("UserAnswer_" + (i + 1).ToString() + "_" + j.ToString());
                        taskSession.UserAnswer = xmlnodelist.Item(0).InnerText.Trim();

                        xmlnodelist = xmldoc.GetElementsByTagName("KeyScore_" + (i + 1).ToString() + "_" + j.ToString());
                        taskSession.KeyScore = float.Parse(xmlnodelist.Item(0).InnerText.Trim());

                        xmlnodelist = xmldoc.GetElementsByTagName("UserScore_" + (i + 1).ToString() + "_" + j.ToString());
                        taskSession.UserScore = float.Parse(xmlnodelist.Item(0).InnerText.Trim());

                        //--Add Task
                        levelSession.TaskSession.Add(taskSession);
                    }

                    //--Add Level
                    LevelSession.Add(levelSession);
                }
            }
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
                LoadCurrentSession();
                LoadSession();
                GetCurrentLevel();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
