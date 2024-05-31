using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Xml;
using System.Xml.Serialization;
using TechnomediaLabs;

namespace Zetcil
{

    public class XMLPlaylist : MonoBehaviour
    {
        public bool isEnabled;

        [Header("XML Settings")]
        public XMLController xmlController;
        public XMLContent xmlContent;
        public XMLButton xmlButton;

        [Header("Path Extension Settings")]
        public string XMLPath = "/XML/Playlist/";
        [ReadOnly] public string FileExtension = ".xml";

        // Use this for initialization
        void Start()
        {
            ShowXMLFile();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool isValid(string aFileExt)
        {
            bool result = false;
            if (FileExtension == aFileExt)
            {
                result = true;
            }
            return result;
        }

        public string XMLDirectory()
        {
            return Application.persistentDataPath + XMLPath;
        }

        public void ShowXMLFile()
        {
            if (isEnabled)
            {

                DirectoryInfo tempDir;
                FileInfo[] tempFile = null;

                if (Directory.Exists(XMLDirectory()))
                {
                    tempDir = new DirectoryInfo(XMLDirectory());
                    tempFile = tempDir.GetFiles();
                }

                for (int i = 0; i < xmlContent.transform.childCount; i++)
                {
                    Destroy(xmlContent.transform.GetChild(i).gameObject);
                }

                Vector2 contentWidth = new Vector2(0, 100); // XMLButtonParent.GetComponent<RectTransform>().sizeDelta;

                for (int i = 0; i < tempFile.Length; i++)
                {
                    string temp = Path.GetExtension(tempFile[i].FullName);

                    if (isValid(temp))
                    {
                        GameObject tempButton = Instantiate(xmlButton.gameObject, xmlContent.transform);
                        tempButton.GetComponentInChildren<Text>().text = tempFile[i].Name;

                        string xmlfile = System.IO.File.ReadAllText(tempFile[i].FullName);

                        if (File.Exists(tempFile[i].FullName))
                        {
                            XmlDocument xmldoc;
                            XmlNodeList xmlnodelist;
                            XmlNode xmlnode;
                            xmldoc = new XmlDocument();
                            xmldoc.LoadXml(xmlfile);

                            int total = 0;
                            xmlnodelist = xmldoc.GetElementsByTagName("TotalData");
                            for (int j = 0; j <= xmlnodelist.Count - 1; j++)
                            {
                                xmlnode = xmlnodelist.Item(j);
                                total = int.Parse(xmlnode.FirstChild.InnerText);
                            }

                            for (int k = 0; k < total; k++)
                            {
                                xmlnodelist = xmldoc.GetElementsByTagName("Data" + k.ToString());

                                xmlnode = xmlnodelist.Item(0);
                                XmlNode currentNode = xmlnode.FirstChild;

                                XMLButton.CXMLData tempXMLData = new XMLButton.CXMLData();

                                tempXMLData.ID = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                //XMLButtonObject.name = tempXMLData.ID;

                                tempXMLData.Caption = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Group = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Prefab = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Position = currentNode.InnerText;
                                string[] strPosition = currentNode.InnerText.Split(':');
                                tempXMLData.PositionVector = new Vector3(float.Parse(strPosition[0]), float.Parse(strPosition[1]), float.Parse(strPosition[2]));
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Rotation = currentNode.InnerText;
                                string[] strRotation = currentNode.InnerText.Split(':');
                                tempXMLData.RotationVector = new Vector3(float.Parse(strRotation[0]), float.Parse(strRotation[1]), float.Parse(strRotation[2]));
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Scale = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Desc = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempXMLData.Link = currentNode.InnerText;
                                currentNode = currentNode.NextSibling;

                                tempButton.GetComponent<XMLButton>().XMLData = tempXMLData;
                            }
                        }

                        //xmlButton = tempButton.GetComponent<XMLButton>();
                        //xmlButton.XMLData.Add(tempXMLData);

                        //-- load xml data to button
                        //-- LoadDataXMLToButton(tempFile[i].FullName, tempButton);

                        contentWidth.y += 50;
                        xmlContent.GetComponent<RectTransform>().sizeDelta = contentWidth;


                    }
                }

            }
        }

        //unused coz saya bingung
        public void LoadDataXMLToButton(string aFileName, GameObject XMLButtonObject)
        {
            Debug.Log(DateTime.Now);

            string xmlfile = System.IO.File.ReadAllText(aFileName);

            if (File.Exists(aFileName))
            {
                XmlDocument xmldoc;
                XmlNodeList xmlnodelist;
                XmlNode xmlnode;
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(xmlfile);

                int total = 0;
                xmlnodelist = xmldoc.GetElementsByTagName("TotalData");
                for (int i = 0; i <= xmlnodelist.Count - 1; i++)
                {
                    xmlnode = xmlnodelist.Item(i);
                    total = int.Parse(xmlnode.FirstChild.InnerText);
                }

                for (int i = 0; i < total; i++)
                {
                    xmlnodelist = xmldoc.GetElementsByTagName("Data" + i.ToString());

                    xmlnode = xmlnodelist.Item(0);
                    XmlNode currentNode = xmlnode.FirstChild;

                    XMLButton.CXMLData tempXMLData = new XMLButton.CXMLData();

                    tempXMLData.ID = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    //XMLButtonObject.name = tempXMLData.ID;

                    tempXMLData.Caption = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Group = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Prefab = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Position = currentNode.InnerText;
                    string[] strPosition = currentNode.InnerText.Split(':');
                    tempXMLData.PositionVector = new Vector3(float.Parse(strPosition[0]), float.Parse(strPosition[1]), float.Parse(strPosition[2]));
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Rotation = currentNode.InnerText;
                    string[] strRotation = currentNode.InnerText.Split(':');
                    tempXMLData.RotationVector = new Vector3(float.Parse(strRotation[0]), float.Parse(strRotation[1]), float.Parse(strRotation[2]));
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Scale = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Desc = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    tempXMLData.Link = currentNode.InnerText;
                    currentNode = currentNode.NextSibling;

                    XMLButtonObject.GetComponent<XMLButton>().XMLData = tempXMLData;
                }

            }
        }
    }
}
