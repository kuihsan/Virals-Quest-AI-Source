using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playanimation : MonoBehaviour
{
    public string anim;
    public bool delayed;
    public bool happy;
    public bool sad;
    public bool angry;
    public bool amazed;
    public bool disgust;

    void Start()
    {
        if (delayed) StartCoroutine("playanim");
        else
        {
            GetComponent<Animator>().Play(anim);
            if (happy)        GetComponent<Animator>().SetLayerWeight(1, 1f);
            else if (sad)     GetComponent<Animator>().SetLayerWeight(2, 1f);
            else if (angry)   GetComponent<Animator>().SetLayerWeight(3, 1f);
            else if (amazed)  GetComponent<Animator>().SetLayerWeight(4, 1f);
            else if (disgust) GetComponent<Animator>().SetLayerWeight(5, 1f);
        }
    }

    IEnumerator playanim()
    {
        yield return new WaitForSeconds(Random.Range(0f,0.75f));
        GetComponent<Animator>().Play(anim);
        if (happy)        GetComponent<Animator>().SetLayerWeight(1, 1f);
        else if (sad)     GetComponent<Animator>().SetLayerWeight(2, 1f);
        else if (angry)   GetComponent<Animator>().SetLayerWeight(3, 1f);
        else if (amazed)  GetComponent<Animator>().SetLayerWeight(4, 1f);
        else if (disgust) GetComponent<Animator>().SetLayerWeight(5, 1f);        
    }
}
