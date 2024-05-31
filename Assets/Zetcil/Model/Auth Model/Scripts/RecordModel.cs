using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class RecordModel : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Login Settings")]
        public VarString MainURL;
        public VarString ActivityURL;
        public string FunctionURL;
        [HideInInspector] public string SubmitURL;

        [Header("Variable Settings")]
        public VarString SessionName;
        public VarString SceneName;
        public VarString Problem;
        public VarString Solution;
        public VarString RecordAnswer;
        public VarString Description;
        public VarTime Duration;
        public VarString Behavior;
        public VarString Notes;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        public void InvokeRecordModel()
        {
            SubmitURL = MainURL.CurrentValue + ActivityURL.CurrentValue + FunctionURL +
                       "/" + SessionName.CurrentValue +
                       "/" + SceneName.CurrentValue +
                       "/" + Problem.CurrentValue +
                       "/" + Solution.CurrentValue +
                       "/" + RecordAnswer.CurrentValue +
                       "/" + Description.CurrentValue +
                       "/" + Duration.CurrentValue.ToString() +
                       "/" + Behavior.CurrentValue +
                       "/" + Notes.CurrentValue
                       ;
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
