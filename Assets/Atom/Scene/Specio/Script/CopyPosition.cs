using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{

    public GameObject TargetObject;

    public void InvokeCopyPosition(GameObject aPosition)
    {
        TargetObject.transform.position = aPosition.transform.position;
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
