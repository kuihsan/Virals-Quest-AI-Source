/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk pembuatan bullet standar
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum CTranslate { VectorUp, VectorDown, VectorForward, VectorBack, VectorLeft, VectorRight }

    [Space(10)]
    public bool isEnabled;

    [Header("Bullet Settings")]
    public CTranslate TranslateType;
    public float Speed;

    [Header("Destroy Settings")]
    public bool usingDestroy;
    public float DestroyDelay;

    // Use this for initialization
    void Start()
    {
        if (usingDestroy)
        {
            Destroy(this.gameObject, DestroyDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            switch (TranslateType)
            {
                case CTranslate.VectorUp:
                    this.transform.Translate(Vector3.up * Speed * Time.deltaTime);
                    break;
                case CTranslate.VectorDown:
                    this.transform.Translate(Vector3.down * Speed * Time.deltaTime);
                    break;
                case CTranslate.VectorForward:
                    this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case CTranslate.VectorBack:
                    this.transform.Translate(Vector3.back * Speed * Time.deltaTime);
                    break;
                case CTranslate.VectorLeft:
                    this.transform.Translate(Vector3.left * Speed * Time.deltaTime);
                    break;
                case CTranslate.VectorRight:
                    this.transform.Translate(Vector3.right * Speed * Time.deltaTime);
                    break;
            }
        }
    }
}
