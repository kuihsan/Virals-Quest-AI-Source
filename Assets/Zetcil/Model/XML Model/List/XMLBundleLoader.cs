using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{
    public class XMLBundleLoader : MonoBehaviour
    {
        [Header("Assets Path Settings")]
        public bool usingAutomaticPath;
        public string AssetBundleDirectory;

        [Header("Assets Object Settings")]
        public string AssetBundleFile;
        public string AssetParentName;
        public string AssetName;

        [Header("Assets Bundle Status")]
        [ReadOnly] public AssetBundle AssetBundleController;

        public void Start()
        {
            LoadAssetsBundle();
        }

        public void LoadAssetsBundle()
        {
            string AssetsFilePath = AssetBundleDirectory + " / " + AssetBundleFile;
            if (usingAutomaticPath)
            {
                AssetBundleDirectory = Application.persistentDataPath + "/AssetsBundle/";
                AssetsFilePath = AssetBundleDirectory + AssetBundleFile;
            }
            
            AssetBundleController = AssetBundle.LoadFromFile(AssetsFilePath);

            if (AssetBundleController.Contains(AssetName)){
                var prefab = AssetBundleController.LoadAsset(AssetName);
                if (prefab)
                {
                    GameObject tempParent = GameObject.Find(AssetParentName);
                    var NewObject = Instantiate(prefab, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0, 0, 0)), tempParent.transform);
                }
            }
        }
    }
}