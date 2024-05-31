using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class FILERequest : MonoBehaviour
    {

        public static string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + aDirectoryName + "/");
            }
            return Application.persistentDataPath + "/" + aDirectoryName + "/";
        }

        public static void SaveFile(string aDirectoryName, string aFileName, string aValue)
        {
            string DirName = GetDirectory(aDirectoryName);
            string FileName = aFileName;
            var sr = File.CreateText(DirName + FileName);
            sr.WriteLine(aValue);
            sr.Close();
        }

        public static string LoadFile(string aDirectoryName, string aFileName)
        {
            string result = "NULL";
            string FullPathFile = GetDirectory(aDirectoryName) + aFileName;
            if (File.Exists(FullPathFile))
            {
                string temp = System.IO.File.ReadAllText(FullPathFile);
                result = temp;
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
