using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TechnomediaLabs;


namespace Zetcil
{
    public class ForgotModel : MonoBehaviour
    {

        public bool isEnabled;

        [Header("Forgot Settings")]
        public VarString ServerURL;
        public VarString ForgotURL;
        [HideInInspector] public string SubmitURL;

        [Header("Variable Settings")]
        public InputField Email;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        public void InvokeForgot()
        {
            SubmitURL = ServerURL.CurrentValue + ForgotURL.CurrentValue +
                       "/" + Email.text;
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
