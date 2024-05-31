using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Zetcil
{
    public class PHPRequest : MonoBehaviour
    {
        public enum CPHPConnect { ByAwake, ByEvent }

        [Space(10)]
        public bool isEnabled;
        [Header("Main URL Settings")]
        public CPHPConnect RequestType;
        public string MainURL;
        [HideInInspector] public string SubmitURL;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        // Start is called before the first frame update
        void Start()
        {
            if (RequestType == CPHPConnect.ByAwake)
            {
                StartCoroutine(StartPHPRequest());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExecutePHPRequest()
        {
            StartCoroutine(StartPHPRequest());
        }

        IEnumerator StartPHPRequest()
        {
            SubmitURL = MainURL;
            UnityWebRequest webRequest = UnityWebRequest.Get(MainURL);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debugger.Save(webRequest.error);
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
            else
            {
                RequestOutput.CurrentValue = webRequest.downloadHandler.text;
                Debugger.Save(webRequest.downloadHandler.text);
                if (PrintDebugConsole)
                {
                    Debug.Log(SubmitURL);
                    Debug.Log(webRequest.downloadHandler.text);
                }
            }
        } 
    }
}
