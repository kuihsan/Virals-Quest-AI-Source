using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class SAVERequest : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Save Settings")]
        public string DirectoryName = "Data";
        public string FileName;
        [HideInInspector]
        public string DataValue;

        [Header("Tag Settings")]
        public bool usingTagName;
        [Tag] public string TagName;

        [Header("DATA Object Settings")]
        public bool usingDATAObject;

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

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void ExecuteSaveRequest()
        {
            if (isEnabled)
            {
                DataValue = CreateSaveValue();
                FILERequest.SaveFile(DirectoryName, FileName, DataValue);
            }
        }

        bool isValidXMLData(GameObject obj)
        {
            return (obj.GetComponent<DATAObject>());
        }

        string SetXMLVector(Vector3 aData)
        {
            string result = "";

            result = aData.x.ToString() + ";" + aData.y.ToString() + ";" + aData.z.ToString() + ";";

            return result;
        }

        string CreateXMLData(GameObject obj, int index)
        {
            string userName = "NONE";
            string groupName = "NONE";
            string prefabName = "ERROR";

            if (isValidXMLData(obj))
            {
                userName = obj.GetComponent<DATAObject>().Username;
                groupName = obj.GetComponent<DATAObject>().Groupname;
                prefabName = obj.GetComponent<DATAObject>().PrefabName;
            }

            string result = "\t<DATAObject" + index.ToString() + ">\n" +
                               "\t\t<DATAObject_Name>" + obj.transform.name + "</DATAObject_Name>\n" +
                               "\t\t<DATAObject_ID>" + obj.GetInstanceID() + "</DATAObject_ID>\n" +
                               "\t\t<DATAObject_User>" + userName + "</DATAObject_User>\n" +
                               "\t\t<DATAObject_Group>" + groupName + "</DATAObject_Group>\n" +
                               "\t\t<DATAObject_Prefab>" + prefabName + "</DATAObject_Prefab>\n" +
                               "\t\t<DATAObject_Machine>" + SystemInfo.deviceUniqueIdentifier + "</DATAObject_Machine>\n" +
                               "\t\t<DATAObject_Tag>" + obj.transform.tag + "</DATAObject_Tag>\n" +
                               "\t\t<DATAObject_Layer>" + obj.transform.gameObject.layer.ToString() + "</DATAObject_Layer>\n" +
                               "\t\t<DATAObject_Position>" + SetXMLVector(obj.transform.position) + "</DATAObject_Position>\n" +
                               "\t\t<DATAObject_Rotation>" + SetXMLVector(obj.transform.eulerAngles) + "</DATAObject_Rotation>\n" +
                               "\t\t<DATAObject_Scale>" + SetXMLVector(obj.transform.localScale) + "</DATAObject_Scale>\n" +
                               "\t</DATAObject" + index.ToString() + ">\n";
            return result;
        }

        string CreateSaveValue()
        {
            string header = "<DATAObjectCollection>\n";
            string footer = "</DATAObjectCollection>";
            string result = "";

            int index = 0;
            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            {
                string temp = "";
                if (usingTagName)
                {
                    if (obj.transform.tag == TagName)
                    {
                        if (usingDATAObject)
                        {
                            if (isValidXMLData(obj)) 
                            { 
                                temp = CreateXMLData(obj, index);
                                result = result + temp;
                                index++;
                            }
                        }
                        else
                        {
                            temp = CreateXMLData(obj, index);
                            result = result + temp;
                            index++;
                        }
                    }
                } else 
                {
                    if (usingDATAObject)
                    {
                        if (isValidXMLData(obj))
                        {
                            temp = CreateXMLData(obj, index);
                            result = result + temp;
                            index++;
                        }
                    }
                    else
                    {
                        temp = CreateXMLData(obj, index);
                        result = result + temp;
                        index++;
                    }
                }

            }

            string total = "\t<TotalData>"+index.ToString()+"</TotalData>\n";

            return header + total + result + footer;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            {
                ExecuteSaveRequest();
            }
        }
    }
}
