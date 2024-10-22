﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using TechnomediaLabs;

namespace Zetcil
{

    public class LOADRequest : MonoBehaviour
    {
        public enum CInstantiateType { InstantiateData, InstantiateRuntime, InstantiateAll }

        [Space(10)]
        public bool isEnabled;

        [Header("Data Request Settings")]
        public bool usingDataRequest;
        public DATARequest DataRequest;

        [Header("Load Settings")]
        public bool usingLoadSettings;
        public string DirectoryName = "Data";
        public string FileName;
        [TextArea(15, 20)]
        public string DataValue;

        [Header("Instantiate Settings")]
        public bool usingInstantiateSettings;
        public CInstantiateType InstantiateType;
        public VarString ActivePrefab;
        public VarPointer ActivePointer;
        public Vector3 ActiveOffset;

        public void SetDirectoryName(string aValue)
        {
            DirectoryName = aValue;
        }

        public void SetFileName(string aValue)
        {
            FileName = aValue;
        }

        public void SetDataValue(string aValue)
        {
            DataValue = aValue;
        }

        Vector3 GetXMLVector(string aData)
        {
            Vector3 result = Vector3.zero;

            string[] temp = aData.Split(';');

            result.x = float.Parse(temp[0]);
            result.y = float.Parse(temp[1]);
            result.z = float.Parse(temp[2]);

            return result;
        }

        public void ExecuteLoadRequest()
        {
            if (isEnabled && usingLoadSettings)
            {
                DataValue = FILERequest.LoadFile(DirectoryName, FileName);
                if (usingDataRequest)
                {
                    XmlDocument xmldoc;
                    XmlNodeList xmlnodelist;
                    XmlNode xmlnode;
                    xmldoc = new XmlDocument();
                    xmldoc.LoadXml(DataValue);

                    int total = 0;
                    xmlnodelist = xmldoc.GetElementsByTagName("TotalData");
                    for (int j = 0; j <= xmlnodelist.Count - 1; j++)
                    {
                        xmlnode = xmlnodelist.Item(j);
                        total = int.Parse(xmlnode.FirstChild.InnerText);
                    }

                    for (int k = 0; k < total; k++)
                    {
                        xmlnodelist = xmldoc.GetElementsByTagName("XMLObject" + k.ToString()); 
                        xmlnode = xmlnodelist.Item(0);
                        XmlNode currentNode = xmlnode.FirstChild;

                        DATAObject.CXMLData tempXMLData = new DATAObject.CXMLData();
                        tempXMLData.DATAObject_Name = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_ID = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_User = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Group = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Prefab = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Machine = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Tag = currentNode.InnerText;

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Layer = int.Parse(currentNode.InnerText);

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Position = GetXMLVector(currentNode.InnerText);

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Rotation = GetXMLVector(currentNode.InnerText);

                        currentNode = currentNode.NextSibling;
                        tempXMLData.DATAObject_Scale = GetXMLVector(currentNode.InnerText);

                        DataRequest.AddData(tempXMLData);

                    }
                }
            }
        }

        public void ExecuteInstantiateAllRequest()
        {
            if (isEnabled && usingInstantiateSettings)
            {
                for (int i = 0; i <= DataRequest.DATAObjectCollection.Count - 1; i++)
                {
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance = (GameObject) Instantiate(Resources.Load(DataRequest.DATAObjectCollection[i].DATAObject_Prefab), DataRequest.DATAObjectCollection[i].DATAObject_Position, Quaternion.Euler(DataRequest.DATAObjectCollection[i].DATAObject_Rotation));

                    //-- instance
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.name = DataRequest.DATAObjectCollection[i].DATAObject_Name;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.tag = DataRequest.DATAObjectCollection[i].DATAObject_Tag;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.layer = DataRequest.DATAObjectCollection[i].DATAObject_Layer;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.transform.position = DataRequest.DATAObjectCollection[i].DATAObject_Position;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.transform.eulerAngles = DataRequest.DATAObjectCollection[i].DATAObject_Rotation;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.transform.localScale = DataRequest.DATAObjectCollection[i].DATAObject_Scale;

                    //-- single xmldata
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Name = DataRequest.DATAObjectCollection[i].DATAObject_Name;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_ID = DataRequest.DATAObjectCollection[i].DATAObject_ID;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_User = DataRequest.DATAObjectCollection[i].DATAObject_User;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Group = DataRequest.DATAObjectCollection[i].DATAObject_Group;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Tag = DataRequest.DATAObjectCollection[i].DATAObject_Tag;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Layer = DataRequest.DATAObjectCollection[i].DATAObject_Layer;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Prefab = DataRequest.DATAObjectCollection[i].DATAObject_Prefab;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Machine = DataRequest.DATAObjectCollection[i].DATAObject_Machine;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Position = DataRequest.DATAObjectCollection[i].DATAObject_Position;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Rotation = DataRequest.DATAObjectCollection[i].DATAObject_Rotation;
                    DataRequest.DATAObjectCollection[i].DATAObject_Instance.GetComponent<DATAObject>().XMLData.DATAObject_Scale = DataRequest.DATAObjectCollection[i].DATAObject_Scale;
                }
            }
        }

        public void ExecuteInstantiateRuntimeRequest(VarString aPrefabname)
        {
            if (isEnabled && usingInstantiateSettings)
            {
                Vector3 hitPoint = ActivePointer.CurrentPointerPosition3D;
                hitPoint = hitPoint + ActiveOffset;

                GameObject temp = (GameObject) Instantiate(Resources.Load(aPrefabname.CurrentValue), hitPoint, Quaternion.identity);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            {
                ExecuteLoadRequest();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
            {
                if (InstantiateType == CInstantiateType.InstantiateData || InstantiateType == CInstantiateType.InstantiateAll)
                {
                    ExecuteInstantiateAllRequest();
                }
            }
        }
    }
}
