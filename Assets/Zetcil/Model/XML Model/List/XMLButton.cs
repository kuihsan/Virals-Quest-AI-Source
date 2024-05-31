using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class XMLButton : MonoBehaviour
    {

        [System.Serializable]
        public class CXMLData
        {
            public string AssetsBundleURL;

            public string ID;
            public string Caption;
            public string Group;
            public string Prefab;
            public string Position;
            [ReadOnly]
            public Vector3 PositionVector;
            public string Rotation;
            [ReadOnly]
            public Vector3 RotationVector;
            public string Scale;
            [ReadOnly]
            public Vector3 ScaleVector;
            public string Desc;
            public string Link;

            public GameObject PrefabObject;
            public GameObject PrefabParent;
        }

        [Header("XMLData Settings")]
        public CXMLData XMLData;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            XMLData.AssetsBundleURL = Application.persistentDataPath + " / AssetsBundle/ZetAssets.bundle";

        }

        public void InstantiatePrefabFromResource(string PrefabParentName)
        {
            GameObject tempParent = GameObject.Find(PrefabParentName);
            XMLData.PrefabObject = GameObject.Instantiate(Resources.Load(XMLData.Prefab) as GameObject, tempParent.transform);
        }

        public void InstantiatePrefabFromAssetsBundle(string PrefabParentName)
        {
            XMLData.PrefabParent = GameObject.Find(PrefabParentName);
            LoadBundle(XMLData.Prefab, XMLData.PrefabParent.transform);
        }

        public void LoadBundle(string runtimeObjectName, Transform parentPosition)
        {
            if (XMLData.PrefabObject)
            {
                Destroy(XMLData.PrefabObject);
            }

            StartCoroutine(InvokeDownload(runtimeObjectName, parentPosition));
        }

        public IEnumerator InvokeDownload(string runtimeObjectName, Transform parentPosition)
        {

            while (!Caching.ready)
            {
                yield return null;
            }

            //Begin download
            WWW www = WWW.LoadFromCacheOrDownload(XMLData.AssetsBundleURL, 0);
            yield return www;

            //Load the downloaded bundle
            AssetBundle bundle = www.assetBundle;

            //Load an asset from the loaded bundle
            AssetBundleRequest bundleRequest = bundle.LoadAssetAsync(runtimeObjectName, typeof(GameObject));
            yield return bundleRequest;

            //get object
            //GameObject obj = bundleRequest.asset as GameObject;

            XMLData.PrefabObject = bundleRequest.asset as GameObject;

            //XMLData.PrefabObject = Instantiate(obj, XMLData.PrefabParent.transform.position, Quaternion.identity) as GameObject;

            bundle.Unload(false);
            www.Dispose();
        }

    }
}
