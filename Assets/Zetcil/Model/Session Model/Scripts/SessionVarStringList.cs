using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using TechnomediaLabs;

namespace Zetcil
{
    public class SessionVarStringList : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public VarConfig SessionConfig;
        public VarString SessionName;

        [Header("Session Data")]
        public VarStringList ValueList;

        // Start is called before the first frame update
        void Start()
        {
            DirectoryInfo info = new DirectoryInfo(SessionConfig.GetSessionDirectory());
            FileInfo[] fileInfo = info.GetFiles();
            ValueList.StringListValue.Clear();
            foreach (var currfile in fileInfo)
            {
                if (currfile.Extension == ".xml")
                {
                    ValueList.StringListValue.Add(currfile.Name);
                }
            }

        }

        public bool isSessionExists(string aValue)
        {
            bool result = false;
            string compName = aValue + ".xml";

            for (int i = 0; i < ValueList.StringListValue.Count; i++)
            {
                if (compName == ValueList.StringListValue[i])
                {
                    result = true;
                }
            }

            return result;
        }

        // Update is called once per frame
        void Update()
        {

        }



    }
}
