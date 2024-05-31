using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Zetcil
{

    public class XMLBundleCreator : Editor
    {
        [MenuItem("Assets/Assets Bundle Packer")]
        static void BuildAllAssetsBundle()
        {
            string AssetsBundleFolder = Application.persistentDataPath + "\\Assets";
            if (!Directory.Exists(AssetsBundleFolder))
            {
                Directory.CreateDirectory(AssetsBundleFolder);
            }

            BuildPipeline.BuildAssetBundles(
                AssetsBundleFolder,
                BuildAssetBundleOptions.DeterministicAssetBundle,
                BuildTarget.StandaloneWindows64);
        }


    }
}
