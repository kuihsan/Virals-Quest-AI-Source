﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinScript : MonoBehaviour
{
    public GameObject TargetObject1;
    public GameObject TargetObject2;
    public GameObject TargetObject3;
    public UnityEvent EventObject;
    bool isWin = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckWinStatus", 1, 1);
    }

    void CheckWinStatus()
    {
        if (!isWin && TargetObject1.activeSelf && TargetObject2.activeSelf && TargetObject3.activeSelf)
        {
            isWin = true;
            EventObject.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
