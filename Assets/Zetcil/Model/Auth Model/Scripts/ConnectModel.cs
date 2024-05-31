using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TechnomediaLabs;
using System.Net;
using System.IO;

namespace Zetcil
{
    public class ConnectModel : MonoBehaviour
    {
        public enum CConnectType { GoogleSite, CustomSite }

        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Main Settings")]
        public CConnectType ConnectType;
        public VarString ServerURL;
        public VarString Result;

        [Header("Timed Out Settings")]
        public int MaxTimeOut;
        public int connectStatus;
        int TryConnection = 0;

        [Header("Output Setting")]
        [TextArea(5, 10)]
        public string HTMLResult;

        [Header("Event Setings")]
        public UnityEvent NoConnectionEvent;
        public UnityEvent RedirectingEvent;
        public UnityEvent ConnectedEvent;

        // Start is called before the first frame update
        void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
            {
                InvokeConnectController();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnStart)
            {
                InvokeConnectController();
            }
            if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
            {
                InvokeRepeating("CheckConnection", 1, 1);
            }
        }

        void InvokeConnectController()
        {
            Invoke("CheckConnection", 1);
        }

        // Update is called once per frame
        void Update()
        {
            if (connectStatus == 0)
            {
                NoConnectionEvent.Invoke();
            }
        }

        public void RestartConnection()
        {
            TryConnection = 0;
        }

        public void CheckConnection()
        {
            connectStatus = 0;
            HTMLResult = "";
            Result.CurrentValue = "Connection Status: Failure";

            if (TryConnection < MaxTimeOut)
            {
                if (ConnectType == CConnectType.GoogleSite)
                {
                    ServerURL.CurrentValue = "https://www.google.com";
                }

                HTMLResult = GetHtmlFromUri(ServerURL.CurrentValue);
                if (ConnectType == CConnectType.GoogleSite)
                {
                    if (HTMLResult == "")
                    {
                        NoConnectionEvent.Invoke();
                    }
                    else if (HTMLResult.Contains("schema.org/WebPage"))
                    {
                        //success
                        TryConnection = MaxTimeOut;
                        ConnectedEvent.Invoke();
                    } else
                    {
                        //Redirecting since the beginning of googles html contains that 
                        //phrase and it was not found
                        RedirectingEvent.Invoke();
                    }
                }else
                {
                    Result.CurrentValue = HTMLResult;
                    if (connectStatus == 0)
                    {
                        NoConnectionEvent.Invoke();
                    }
                    else if (HTMLResult == "" || connectStatus == -1)
                    {
                        NoConnectionEvent.Invoke();
                    }
                    else if (HTMLResult.Contains("CONNECTED") || connectStatus == 1)
                    {
                        string[] temp = HTMLResult.Split(':');
                        if (temp.Length > 1)
                        {
                            Result.CurrentValue = temp[1];
                        } 
                        else
                        {
                            Result.CurrentValue = "CONNECTED";
                        }
                        TryConnection = MaxTimeOut;
                        ConnectedEvent.Invoke();
                    }
                    else
                    {
                        Result.CurrentValue = HTMLResult;
                        RedirectingEvent.Invoke();
                    }
                }
            }
            TryConnection++;
        }

        public string GetHtmlFromUri(string resource)
        {
            string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                    if (isSuccess)
                    {
                        using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                        {
                            //We are limiting the array to 80 so we don't have
                            //to parse the entire html document feel free to 
                            //adjust (probably stay under 300)
                            char[] cs = new char[80];
                            reader.Read(cs, 0, cs.Length);
                            foreach (char ch in cs)
                            {
                                html += ch;
                            }

                            connectStatus = 1;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                connectStatus = -1;
                Result.CurrentValue = e.Message;
                return e.Message;
            }
            return html;
        }
    }
}

