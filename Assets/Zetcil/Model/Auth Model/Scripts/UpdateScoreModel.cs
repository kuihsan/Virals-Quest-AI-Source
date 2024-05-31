using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class UpdateScoreModel : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Login Settings")]
        public VarString MainURL;
        [HideInInspector] public string SubmitURL;

        [Header("Variable Settings")]
        public VarString SessionName;
        public VarFloat LocalScore;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        public void InvokeUpdateScore()
        {
            SubmitURL = MainURL.CurrentValue +
                       "/" + SessionName.CurrentValue +
                       "/" + LocalScore.CurrentValue.ToString();
            StartCoroutine(StartPHPRequest());
        }

        IEnumerator StartPHPRequest()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(SubmitURL);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
            else
            {
                RequestOutput.CurrentValue = webRequest.downloadHandler.text;
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
        }

        float ToFloat(string aValue)
        {
            float result = 0;
            if (aValue == "")
            {
                result = 0;
            }
            else
            {
                result = float.Parse(aValue);
            }
            return result;
        }

        int ToInteger(string aValue)
        {
            int result = 0;
            if (aValue == "")
            {
                result = 0;
            }
            else
            {
                result = int.Parse(aValue);
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
    }
}
