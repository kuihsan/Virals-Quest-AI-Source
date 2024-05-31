/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk bikin efek kamera bergetarrrrr
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class CameraHIT : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;
        [MustBeAssigned] public Camera TargetCamera;

        [Header("Shake Settings")]
        public float power = 0.7f;
        public float duration = 1.0f;
        public float slowDownAmount = 1.0f;

        [Header("Debug Variable")]
        public bool startShake;

        Vector3 startPosition;
        float initialDuration;

        // Use this for initialization
        void Start()
        {
            startPosition = TargetCamera.transform.localPosition;
            initialDuration = duration;
        }

        public void StartShake()
        {
            startPosition = TargetCamera.transform.localPosition;
            initialDuration = duration;
            startShake = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (startShake)
            {
                if (duration > 0)
                {
                    TargetCamera.transform.localPosition = startPosition + Random.insideUnitSphere * power;
                    duration -= Time.deltaTime * slowDownAmount;
                }
                else
                {
                    startShake = false;
                    duration = initialDuration;
                    TargetCamera.transform.localPosition = startPosition;
                }
            }
        }
    }
}
