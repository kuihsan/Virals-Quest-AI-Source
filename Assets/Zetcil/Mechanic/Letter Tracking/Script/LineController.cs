using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class LineController : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Line Settings")]
        public LineRenderer TargetLine;
        public Material TargetMaterial;
        public Vector2 LineWidth;

        [Header("Positions Settings")]
        public List<Vector3> LinePosition;

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                TargetLine.material = TargetMaterial;
                TargetLine.SetWidth(LineWidth.x, LineWidth.y);
            }
    }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddVector2(VarVector2 aPosition)
        {
            if (aPosition.CurrentValue != Vector2.zero)
            {
                LinePosition.Add(aPosition.CurrentValue);
                TargetLine.positionCount += 1;
                TargetLine.SetPosition(TargetLine.positionCount - 1, aPosition.CurrentValue);
            }
        }

        public void AddVector3(VarVector3 aPosition)
        {
            if (aPosition.CurrentValue != Vector3.zero)
            {
                LinePosition.Add(aPosition.CurrentValue);
                TargetLine.positionCount += 1;
                TargetLine.SetPosition(TargetLine.positionCount - 1, aPosition.CurrentValue);
            }
        }
    }
}
