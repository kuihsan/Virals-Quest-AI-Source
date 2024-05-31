using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class LoginModel : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Login Settings")]
        public VarString ServerURL;
        public VarString LoginURL;
        [HideInInspector] public string SubmitURL;

        [Header("Input Settings")]
        public InputField Username;
        public InputField Password;

        [Header("Output Settings")]
        public VarString RequestOutput;
        public bool PrintDebugConsole;

        [Header("Login Result")]
        public UnityEvent SuccessEvent;
        public UnityEvent FailedEvent;

        [Header("Session Event")]
        public bool usingSessionEvent;
        public UnityEvent SessionEvent;

        public bool isGuestLogin()
        {
            bool result = false;
            if (Username.text == "GUEST" && Password.text == "GUEST")
            {
                result = true;
                SuccessEvent.Invoke();

                if (usingSessionEvent)
                {
                    SessionEvent.Invoke();
                }
            }
            return result;
        }

        public void InvokeGuestLogin()
        {
            if (Username.text == "GUEST" && Password.text == "GUEST")
            {
                SuccessEvent.Invoke();
            }
            else
            {
                FailedEvent.Invoke();
            }
        }

        public void InvokeAdminLogin()
        {
            if (Username.text == "ADMIN" && Password.text == "ADMIN")
            {
                SuccessEvent.Invoke();
            } else
            {
                FailedEvent.Invoke();
            }
        }

        public void InvokeLogin()
        {
            if (!isGuestLogin())
            {
                SubmitURL = ServerURL.CurrentValue + LoginURL.CurrentValue +
                           "/" + Username.text +
                           "/" + Password.text;
                StartCoroutine(StartPHPRequest());
            }
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

            string[] resLogin = RequestOutput.CurrentValue.Split('|');

            if (resLogin[0] == "LOGIN_FAILED")
            {
                FailedEvent.Invoke();
            }
            else if (resLogin[0] == "LOGIN_SUCCESS")
            {
                SuccessEvent.Invoke();

                if (usingSessionEvent)
                {
                    SessionEvent.Invoke();
                }
            }
        }

        float ToFloat(string aValue)
        {
            float result = 0;
            if (aValue == "")
            {
                result = 0;
            } else 
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

        public void LoginFailed()
        {
            Debug.Log("Message::LoginFailed");
        }

        public void LoginSuccess()
        {
            Debug.Log("Message::LoginSuccess");
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
