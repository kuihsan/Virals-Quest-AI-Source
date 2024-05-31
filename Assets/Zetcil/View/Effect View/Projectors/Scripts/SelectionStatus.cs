using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{

    public class SelectionStatus : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Projector Setting")]
        public Projector TargetProjector;

        [Header("Selection Setting")]
        public VarBoolean VarSelected;
        public float RotationSelectorSpeed;

        // Use this for initialization
        void Start()
        {
            TargetProjector.enabled = VarSelected.CurrentValue;
        }

        // Update is called once per frame
        void Update()
        {
            if (this.gameObject.activeSelf)
            {
                if (VarSelected.CurrentValue)
                {
                    TargetProjector.enabled = VarSelected.CurrentValue;
                    transform.Rotate(new Vector3(0, 0, RotationSelectorSpeed) * Time.deltaTime);
                } else if (!VarSelected.CurrentValue)
                {
                    TargetProjector.enabled = VarSelected.CurrentValue;
                }
            }
        }
    }
}
