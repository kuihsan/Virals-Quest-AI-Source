/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menunjukkan dasar-dasar pergerakan dalam Unity yang terdiri dari Position, Rotation, & Scale.
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class InputController : MonoBehaviour
    {

        [System.Serializable]
        public class CKeyboardArray
        {
            [SearchableEnum] public KeyCode InputKey;

            [Space(10)]
            public UnityEvent KeyDownEvent;
            public UnityEvent KeyPressEvent;
            public UnityEvent KeyUpEvent;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Input Settings")]
        public List<CKeyboardArray> KeyboardInput;

        // Use this for initialization
        void Start()
        {

        }

        public void InvokeKeyDownEvent()
        {
            if (isEnabled)
            {
                for (int i = 0; i < KeyboardInput.Count; i++)
                {
                    KeyboardInput[i].KeyDownEvent.Invoke();
                }
            }
        }

        public void InvokeKeyUpEvent()
        {
            if (isEnabled)
            {
                for (int i = 0; i < KeyboardInput.Count; i++)
                {
                    KeyboardInput[i].KeyUpEvent.Invoke();
                }
            }
        }

        public void InvokeKeyPressEvent()
        {
            if (isEnabled)
            {
                for (int i = 0; i < KeyboardInput.Count; i++)
                {
                    KeyboardInput[i].KeyPressEvent.Invoke();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                for (int i = 0; i < KeyboardInput.Count; i++)
                {
                    if (Input.GetKeyDown(KeyboardInput[i].InputKey))
                    {
                        KeyboardInput[i].KeyDownEvent.Invoke();
                    }
                    if (Input.GetKey(KeyboardInput[i].InputKey))
                    {
                        KeyboardInput[i].KeyPressEvent.Invoke();
                    }
                    if (Input.GetKeyUp(KeyboardInput[i].InputKey))
                    {
                        KeyboardInput[i].KeyUpEvent.Invoke();
                    }
                }
            }
        }
    }
}
