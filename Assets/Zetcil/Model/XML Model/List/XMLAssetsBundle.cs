using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class XMLAssetsBundle : MonoBehaviour
    {

        public string AssetsBundleName;

        // Use this for initialization
        void Start()
        {
            PlayerPrefs.SetString("AssetsBundleName", AssetsBundleName);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
