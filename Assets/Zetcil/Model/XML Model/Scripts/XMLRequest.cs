using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TechnomediaLabs;
using System.Xml;
using System.Xml.Serialization;

namespace Zetcil
{

    public class XMLRequest : MonoBehaviour
    {
        public enum CXMLBasePath { StreamingPath, URL }
        public enum CXMLDataType { DataSingle, DataArray }
        public enum CXMLConnect { ByAwake, ByEvent }

        [Space(10)]
        public bool isEnabled;
        [Header("Main URL Settings")]
        public CXMLConnect RequestType;
        public CXMLBasePath XMLPath;
        public string XMLDirectory;
        public string XMLFile;

        [Header("XML Data Settings")]
        public CXMLDataType DataType;
        public string TagName;
        public string TagCount;
        public List<string> TagList;

        [Header("Output Settings")]
        public VarString XMLString;
        public bool PrintDebugConsole;

        // Start is called before the first frame update
        void Start()
        {
            if (RequestType == CXMLConnect.ByAwake)
            {
                StartCoroutine(StartXMLRequest());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public string DataDirectory()
        {
            string folder = "/" + XMLDirectory + "/";
            if (!Directory.Exists(Application.persistentDataPath + folder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folder);
            }
            return Application.persistentDataPath + folder;
        }

        public void ExecuteXMLRequest()
        {
            StartCoroutine(StartXMLRequest());
        }

        IEnumerator StartXMLRequest() 
        {

            string xmlfile = DataDirectory() + XMLFile + ".xml";

            if (File.Exists(xmlfile))
            {
                string xmlfile_node = "";
                string xmlfile_result = System.IO.File.ReadAllText(xmlfile);

                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(xmlfile_result);

                if (DataType == CXMLDataType.DataSingle)
                {

                    string json_begin = "[{ ";
                    string json_content = "";
                    string json_end = " }]";

                    xmlnodelist = xmldoc.GetElementsByTagName(TagName);
                    for (int i = 0; i <= xmlnodelist.Count - 1; i++)
                    {
                        xmlnode = xmlnodelist.Item(i);
                        for (int j = 0; j < TagList.Count; j++)
                        {
                            json_content = json_content + "\"" + TagList[j] + "\" : " + "\"" + xmlnode.SelectSingleNode(TagList[j]).InnerText + "\" ,";
                        }
                    }

                    json_content = json_content.Remove(json_content.Length - 1);
                    XMLString.CurrentValue = json_begin + json_content + json_end;

                }
                if (DataType == CXMLDataType.DataArray)
                {

                    string json_begin = "[ ";
                    string json_array = "";
                    string json_end = " ]";

                    for (int i=0; i < int.Parse(TagCount); i++)
                    {
                        string json_content = "";

                        xmlnodelist = xmldoc.GetElementsByTagName(TagName + i.ToString());
                        for (int j = 0; j <= xmlnodelist.Count - 1; j++)
                        {
                            xmlnode = xmlnodelist.Item(j);
                            json_content = json_content + "{ ";
                            for (int k = 0; k < TagList.Count; k++)
                            {
                                json_content = json_content + "\"" + TagList[k] + "\" : " + "\"" + xmlnode.SelectSingleNode(TagList[k]).InnerText + "\" ,";
                            }
                            json_content = json_content.Remove(json_content.Length - 1);
                            json_content = json_content + "},";
                        }

                        json_array = json_array + json_content;
                    }

                    json_array = json_array.Remove(json_array.Length - 1);
                    XMLString.CurrentValue = json_begin + json_array + json_end;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

}