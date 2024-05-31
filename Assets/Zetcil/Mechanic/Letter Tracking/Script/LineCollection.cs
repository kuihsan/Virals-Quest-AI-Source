using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{

    public class LineCollection : MonoBehaviour
    {

        public bool isEnabled;

        [Header("Line Controller")]
        public GameObject TargetPrefab;

        [Header("Override Settings")]
        public bool usingOverrideSettings;
        public Material TargetMaterial;
        public Vector2 LineWidth;

        [Header("Line Collection")]
        public List<LineController> LineCollections;
        public int CurrentIndex;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddVector3(VarVector3 aPosition)
        {
            if (aPosition.CurrentValue != Vector3.zero)
            {
                GameObject newLine = GameObject.Instantiate(TargetPrefab, aPosition.CurrentValue, this.transform.rotation);
                if (newLine.GetComponent<LineController>() != null)
                {
                    LineCollections.Add(newLine.GetComponent<LineController>());
                    newLine.GetComponent<LineController>().AddVector3(aPosition);
                    CurrentIndex = LineCollections.Count - 1;
                }
            }
        }

        public void DrawVector3(VarVector3 aPosition)
        {
            if (aPosition.CurrentValue != Vector3.zero)
            {
                if (CurrentIndex >= 0) { 
                    LineCollections[CurrentIndex].GetComponent<LineController>().AddVector3(aPosition);
                }
            }
        }

    }
}
