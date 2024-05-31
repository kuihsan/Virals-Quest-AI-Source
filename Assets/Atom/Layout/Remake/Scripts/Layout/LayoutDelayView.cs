using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutDelayView : MonoBehaviour
{
    [Space(10)]
    public bool isEnabled;

    [Header("Layout Settings")]
    public float Delay = 1;
    public List<GameObject> LayoutObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        for (int i = 0; i < LayoutObject.Count; i++)
        {
            LayoutObject[i].SetActive(false);
        }
        Invoke("ActivateObject", Delay);

        yield return new WaitForSeconds(1);
    }

    void ActivateObject()
    {
        for (int i = 0; i < LayoutObject.Count; i++)
        {
            LayoutObject[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
