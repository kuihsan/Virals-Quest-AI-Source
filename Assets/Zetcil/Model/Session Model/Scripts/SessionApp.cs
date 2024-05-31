using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using TechnomediaLabs;

namespace Zetcil
{
    public class SessionApp : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public VarConfig SessionConfig;

        [Header("Application Settings")]
        public string Name;
        public string Machine;
        public string License;
        public string Token;

        public void SaveFile()
        {
            string header = "<SessionApp>\n";
            string footer = "</SessionApp>";
            string result = "";

            string tagName = SessionConfig.SetXMLValueSingle("Name", Name);
            string tagMachine = SessionConfig.SetXMLValueSingle("Machine", Machine);
            string tagLicense = SessionConfig.SetXMLValueSingle("License", License);
            string tagToken = SessionConfig.SetXMLValueSingle("Token", Token);

            result = tagName +
                     tagMachine +
                     tagLicense +
                     tagToken;
            result = header + result + footer;

            string DirName = SessionConfig.GetConfigDirectory();
            var sr = File.CreateText(DirName + "Session.xml");
            sr.WriteLine(result);
            sr.Close();
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
