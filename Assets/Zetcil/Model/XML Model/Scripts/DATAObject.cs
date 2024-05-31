using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class DATAObject : MonoBehaviour
    {
        public bool isEnabled;

        [Header("User Settings")]
        public string Username = "Username";
        public string Groupname = "Groupname";

        [Header("Main Settings")]
        public string PrefabName;
        [Tag] public string TagName;
        [Layer] public int LayerName;

        [System.Serializable]
        public class CXMLData
        {
            public string DATAObject_Name;
            public string DATAObject_ID;
            public string DATAObject_User;
            public string DATAObject_Group;
            [Tag] public string DATAObject_Tag;
            [Layer] public int DATAObject_Layer;
            public string DATAObject_Prefab;
            public string DATAObject_Machine;
            public Vector3 DATAObject_Position;
            public Vector3 DATAObject_Rotation;
            public Vector3 DATAObject_Scale;

            [Header("GameObject Instance")]
            public GameObject DATAObject_Instance;
        }

        [Header("Data Structure")]
        public CXMLData XMLData;

        public void SetUsername(string aValue)
        {
            Username = aValue;
        }

        public void SetGroupname(string aValue)
        {
            Groupname = aValue;
        }

        // Start is called before the first frame update
        void Awake()
        {
            transform.tag = TagName;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
