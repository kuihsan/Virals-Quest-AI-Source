using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil {
    public class WordButton : MonoBehaviour
    {
        // Start is called before the first frame update

        [Header("Main Settings")]
        public Toggle TargetButtonWord;
        public Text Letter;

        [Header("Output Settings")]
        public VarString TargetWord;

        bool BeginDrag = false;

        public void SetBeginDrag(bool aValue)
        {
            BeginDrag = aValue;
            TargetWord.CurrentValue = "";
        }

        public void ResetWord()
        {
            TargetButtonWord.GetComponent<Toggle>().isOn = false;
            BeginDrag = false;
        }


        public void SetTargetWord()
        {
            if (BeginDrag)
            {
                if (!TargetButtonWord.GetComponent<Toggle>().isOn)
                {
                    TargetButtonWord.GetComponent<Toggle>().isOn = true;
                    TargetWord.CurrentValue = TargetWord.CurrentValue + Letter.text;
                }
            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ResetWord();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                BeginDrag = true;
            }
        }

    }
}
