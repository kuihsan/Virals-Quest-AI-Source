using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendshapes : MonoBehaviour {

    SkinnedMeshRenderer skinMR;

    private void Start()
    {
        skinMR = GetComponent<SkinnedMeshRenderer>();
    }
    void Update ()
    {
        //if Input.GetAxis("Hroizontal")

        skinMR.SetBlendShapeWeight(18, Input.GetAxis("Horizontal")*100f);
        skinMR.SetBlendShapeWeight(19, Input.GetAxis("Horizontal") * -100f);
        skinMR.SetBlendShapeWeight(16, Input.GetAxis("Vertical") * 100f);
        skinMR.SetBlendShapeWeight(17, Input.GetAxis("Vertical") * -100f);

    }
}
