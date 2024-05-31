using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamController : MonoBehaviour
{
    public bool isEnabled;

    [Header("Main Settings")]
    public Renderer TargetRenderer;
    WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            webcamTexture = new WebCamTexture();
            TargetRenderer.material.mainTexture = webcamTexture;
        }
        catch
        {


        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeWebCamera()
    {
        if (isEnabled)
        {
            try
            {
                if (!webcamTexture.isPlaying)
                {
                    webcamTexture.Play();
                }
            } catch
            {

            }
        }
    }

    public void StopWebCamera()
    {
        try
        {
            webcamTexture.Stop();
        } catch
        {

        }
    }

}
