using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class DATARequest : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Data Object Collection")]
        public List<DATAObject.CXMLData> DATAObjectCollection;

        public void AddData(DATAObject.CXMLData aData)
        {
            DATAObjectCollection.Add(aData);
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
