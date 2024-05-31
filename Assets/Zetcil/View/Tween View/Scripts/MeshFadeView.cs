using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class MeshFadeView : MonoBehaviour
    {
        public enum CFadeType { None, FadeIn, FadeOut }

        [Space(10)]
        public bool isEnabled;
        public CFadeType FadeType;

        [Header("Main Settings")]
        public MeshRenderer TargetMesh;
        public float duration = 5.0f;
        public float delay = 1.0f;

        [Header("Event Settings")]
        public bool usingSpriteEvent;
        public UnityEvent SpriteEvent;

        float minimum = 0.0f;
        float maximum = 1f;
        float status = 0;
        private bool execEvent = false;

        private float startTime;
        private float totalTime;

        public void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }

        public void SetEnabled(bool aValue)
        {
            isEnabled = aValue;
        }
        // Start is called before the first frame update
        void Start()
        {
            if (FadeType == CFadeType.FadeIn)
            {
                minimum = 0f;
                maximum = 1f;
                status = 0f;

                Material mat = TargetMesh.material;
                float alpha = 0;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
                TargetMesh.material = mat;
            }
            if (FadeType == CFadeType.FadeOut)
            {
                minimum = 1f;
                maximum = 0f;
                status = 1f;

                Material mat = TargetMesh.material;
                float alpha = 1;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
                TargetMesh.material = mat;
            }
            startTime = -9;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (startTime == -9)
                {
                    startTime = Time.time;
                }

                if (FadeType == CFadeType.FadeIn && status < 1)
                {
                    
                    Material mat = TargetMesh.material;
                    float alpha = mat.color.a;
                    
                    totalTime = (Time.time - startTime) / duration;
                    status = Mathf.SmoothStep(minimum, maximum, totalTime);
                    alpha = alpha + totalTime * Time.deltaTime;

                    if (alpha > 1) alpha = 1;

                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
                    TargetMesh.material = mat;
                    
                }
                else if (FadeType == CFadeType.FadeOut && status > 0)
                {
                    Material mat = TargetMesh.material;
                    float alpha = mat.color.a;

                    totalTime = (Time.time - startTime) / duration;
                    status = Mathf.SmoothStep(minimum, maximum, totalTime);
                    alpha = alpha - totalTime * Time.deltaTime;

                    if (alpha < 0) alpha = 0;

                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
                    TargetMesh.material = mat;
                }

                if (totalTime > delay)
                {
                    if (usingSpriteEvent)
                    {
                        SpriteEvent.Invoke();
                    }
                }
            }
        }
    }
}
