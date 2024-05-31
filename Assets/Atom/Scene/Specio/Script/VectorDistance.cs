using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorDistance : MonoBehaviour
{

    public bool isEnabled;

    [Header("Distance")]
    public GameObject StartObject;
    public GameObject FinishObject;
    public float Distance;

    [Header("Distance Slider")]
    public bool usingSliderUI;
    public float MaxDistanceOffest;
    public Slider SliderUI;

    [Header("Boy Character")]
    public GameObject BoyCamera;
    public GameObject BoyIcon;
    public GameObject StartBoy;
    public GameObject FinishBoy;
    public float SliderBoy;

    [Header("Girl Character")]
    public GameObject GirlCamera;
    public GameObject GirlIcon;
    public GameObject StartGirl;
    public GameObject FinishGirl;
    public float SliderGirl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(StartObject.transform.position, FinishObject.transform.position);
        if (usingSliderUI)
        {
            if (BoyCamera.activeSelf)
            {
                SliderBoy = Vector3.Distance(StartBoy.transform.position, FinishBoy.transform.position);
                SliderUI.value = MaxDistanceOffest - SliderBoy;
                BoyIcon.SetActive(true);
            }
            else
            {
                BoyIcon.SetActive(false);
            }
            if (GirlCamera.activeSelf)
            {
                SliderGirl = Vector3.Distance(StartGirl.transform.position, FinishGirl.transform.position);
                SliderUI.value = MaxDistanceOffest - SliderGirl;
                GirlIcon.SetActive(true);
            }
            else
            {
                GirlIcon.SetActive(false);
            }
        }
    }
}
