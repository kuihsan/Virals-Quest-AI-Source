using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{

    public class VarConcat : MonoBehaviour
    {
        [Header("Input Settings")]
        public List<VarString> InputVarString;

        [Header("Output Settings")]
        public VarString OutputVarString;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OutputVarString.CurrentValue = "";
            for (int i = 0; i < InputVarString.Count; i++)
            {
                OutputVarString.CurrentValue += InputVarString[i].CurrentValue;
            }
        }
    }
}
