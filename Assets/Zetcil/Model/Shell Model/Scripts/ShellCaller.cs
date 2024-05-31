using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{

    public class ShellCaller : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Application Settings")]
        public string ApplicationName;

        [Header("Shell Settings")]
        public Shell TargetShell;


        // Start is called before the first frame update
        void Awake()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GetComponent<Canvas>().enabled = false;
                TargetShell.DebugLogReset();
            }
            if (Input.GetKey("`"))
            {
                GetComponent<Canvas>().enabled = true;
                if (GetComponent<Canvas>().enabled)
                {
                    TargetShell.TerminalInput.Select();
                    TargetShell.TerminalInput.ActivateInputField();
                }
            }
        }

        public void MessageLog(string Value)
        {
            TargetShell.MessageLog(Value);
        }

        public void MessageLogNoDate(string Value)
        {
            TargetShell.MessageLogNoDate(Value);
        }

    }
}
