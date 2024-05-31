using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class HighScoreModel : MonoBehaviour
    {

        public bool isEnabled;

        [System.Serializable]
        public class CHighScore
        {
            public string ID;
            public string Name;
            public string Score;
        }

        [Header("HighScore Settings")]
        public VarString MainURL;
        public string Limit;
        [HideInInspector] public string SubmitURL;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        [Header("HighScore Settings")]
        public GameObject TargetContent;
        public GameObject TargetContentRow;
        public List<CHighScore> TargetHighScore;

        public void InvokeHighScore()
        {
            SubmitURL = MainURL.CurrentValue +
                       "/" + Limit;
            StartCoroutine(StartPHPRequest());
        }

        IEnumerator StartPHPRequest()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(SubmitURL);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
            else
            {
                RequestOutput.CurrentValue = webRequest.downloadHandler.text;
                LoadHighScore();
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }

        }

        public void LoadHighScore()
        {
            TargetHighScore.Clear();

            string[] HighScoreView = RequestOutput.CurrentValue.Split('|');
            for (int i = 0; i < HighScoreView.Length - 1; i++)
            {
                CHighScore newscore = new CHighScore();

                string[] HighScoreElement = HighScoreView[i].Split(';');

                newscore.ID = HighScoreElement[0];
                newscore.Name = HighScoreElement[2];
                newscore.Score = HighScoreElement[4];

                TargetHighScore.Add(newscore);
            }

            //--Add List To UI
            for (int i = 0; i < TargetHighScore.Count; i++)
            {
                GameObject tempScoreRow = GameObject.Instantiate(TargetContentRow, TargetContent.transform);

                tempScoreRow.GetComponent<SessionScoreRow>().CaptionNo.text = (i + 1).ToString();
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionID.text = TargetHighScore[i].ID;
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionName.text = TargetHighScore[i].Name;
                tempScoreRow.GetComponent<SessionScoreRow>().CaptionScore.text = TargetHighScore[i].Score.ToString();

            }

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
