using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image TargetImage1;
    public Image TargetImage2;
    public Image TargetImage3;

    public void SetWhite()
    {
        TargetImage1.color = Color.white;
        TargetImage2.color = Color.white;
        TargetImage3.color = Color.white;
    }

    public void SetGray()
    {
        TargetImage1.color = Color.gray;
        TargetImage2.color = Color.gray;
        TargetImage3.color = Color.gray;
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
