/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk pembuatan bullet standar
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBulletController : MonoBehaviour
{
    public enum CTranslate { VectorUp, VectorDown, VectorForward, VectorBack, VectorLeft, VectorRight }

    [Space(10)]
    public bool isEnabled;

    [Header("Angle Settings")]
    public float BulletAngle;

    [Header("Bullet Settings")]
    public CTranslate TranslateType;
    public float Speed;

    [Header("Destroy Settings")]
    public bool usingDestroy;
    public float DestroyDelay;

    void Awake()
    {
        transform.localRotation = Quaternion.Euler(0, 0, BulletAngle);
    }

    // Use this for initialization
    void Start()
    {
        if (usingDestroy)
        {
            Destroy(this.gameObject, DestroyDelay);
        }
    }

    public void SetAngle(float aAngle)
    {
        BulletAngle = aAngle;
    }

    public void ShootLeft()
    {
        TranslateType = CTranslate.VectorLeft;
        BulletAngle = 180;
    }

    public void ShootRight()
    {
        TranslateType = CTranslate.VectorRight;
        BulletAngle = 0;
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
