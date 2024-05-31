using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCharacter : MonoBehaviour
{
    public GameObject TargetObject;
    public bool isFind;

    // Start is called before the first frame update
    void Start()
    {
        isFind = false;
        InvokeRepeating("FindCharacter", 1, 1);
    }

    void FindCharacter()
    {
        if (!isFind)
        {
            TargetObject = GameObject.Find("Boy.Player.Controller");
            if (!TargetObject)
            {
                TargetObject = GameObject.Find("Girl.Player.Controller");
            }
            if (TargetObject)
            {
                isFind = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject)
        {
            transform.LookAt(TargetObject.transform.GetChild(0).transform);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}
