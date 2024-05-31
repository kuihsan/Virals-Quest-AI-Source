using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMyDocuments(string itemPath)
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        itemPath = path + itemPath;
        print(itemPath);
        itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
    }

    public void ShowExplorer(string itemPath)
    {
        itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
