using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutActiveToggle : MonoBehaviour
{
    public GameObject TargetObject;

    public void SetActiveToggle(Toggle aToggle)
    {
        TargetObject.SetActive(aToggle.isOn);
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
