using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;
using UnityEngine.Events;

namespace Zetcil
{
    public class KeyboardController : MonoBehaviour
    {
        [System.Serializable]
        public class CKeyboardKey
        {
            [SearchableEnum] public KeyCode UpKey = KeyCode.UpArrow;
            [SearchableEnum] public KeyCode LeftKey = KeyCode.LeftArrow;
            [SearchableEnum] public KeyCode DownKey = KeyCode.DownArrow;
            [SearchableEnum] public KeyCode RightKey = KeyCode.RightArrow;
            [SearchableEnum] public KeyCode JumpKey;
            [SearchableEnum] public KeyCode ShiftKey;
            [Header("Event Settings")]
            public bool usingEventSettings;
            public UnityEvent KeyboardEvent;

        }

        [Space(10)]
        public bool isEnabled;

        [Header("Keyboard Settings")]
        public bool usingKeyboardSettings = true;
        public CKeyboardKey PrimaryKeyboardKey;
        public CKeyboardKey AltKeyboardKey;
        public CKeyboardKey SecondaryKeyboardKey;
        public CKeyboardKey AltSecondaryKeyboardKey;

        [System.Serializable]
        public class CKeyboardArray
        {
            [SearchableEnum] public KeyCode InputKeyDown;
            public UnityEvent KeyDownEvent;

            [Space(10)]
            [SearchableEnum] public KeyCode InputKey;
            public UnityEvent KeyEvent;

            [Space(10)]
            [SearchableEnum] public KeyCode InputKeyUp;
            public UnityEvent KeyUpEvent;
        }

        [System.Serializable]
        public class CShiftKeyboardArray
        {
            [Header("ShiftKey Settings")]
            [Space(10)]
            [SearchableEnum] public KeyCode TargetShiftKey;

            [Header("MainKey Settings")]
            [SearchableEnum] public KeyCode InputKeyDown;
            public UnityEvent KeyDownEvent;

            [Space(10)]
            [SearchableEnum] public KeyCode InputKey;
            public UnityEvent KeyEvent;

            [Space(10)]
            [SearchableEnum] public KeyCode InputKeyUp;
            public UnityEvent KeyUpEvent;
        }

        [Header("Shift Settings")]
        public bool usingShiftSettings;
        public List<CShiftKeyboardArray> ShiftKeyboardArray;

        [Header("Additional Settings")]
        public bool usingAdditionalSettings;
        public List<CKeyboardArray> KeyboardArray;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        public bool TriggerKeyPress()
        {
            bool result = Input.GetKey(PrimaryKeyboardKey.DownKey) ||
                          Input.GetKey(PrimaryKeyboardKey.UpKey) ||
                          Input.GetKey(PrimaryKeyboardKey.LeftKey) ||
                          Input.GetKey(PrimaryKeyboardKey.RightKey) ||
                          Input.GetKey(AltKeyboardKey.DownKey) ||
                          Input.GetKey(AltKeyboardKey.UpKey) ||
                          Input.GetKey(AltKeyboardKey.LeftKey) ||
                          Input.GetKey(AltKeyboardKey.RightKey) ||
                          Input.GetKey(SecondaryKeyboardKey.DownKey) ||
                          Input.GetKey(SecondaryKeyboardKey.UpKey) ||
                          Input.GetKey(SecondaryKeyboardKey.LeftKey) ||
                          Input.GetKey(SecondaryKeyboardKey.RightKey) ||
                          Input.GetKey(AltSecondaryKeyboardKey.DownKey) ||
                          Input.GetKey(AltSecondaryKeyboardKey.UpKey) ||
                          Input.GetKey(AltSecondaryKeyboardKey.LeftKey) ||
                          Input.GetKey(AltSecondaryKeyboardKey.RightKey)
                          ;

            return result;
        }

        public bool TriggerKeyDown()
        {
            bool result = Input.GetKeyDown(PrimaryKeyboardKey.DownKey) ||
                          Input.GetKeyDown(PrimaryKeyboardKey.UpKey) ||
                          Input.GetKeyDown(PrimaryKeyboardKey.LeftKey) ||
                          Input.GetKeyDown(PrimaryKeyboardKey.RightKey) ||
                          Input.GetKeyDown(AltKeyboardKey.DownKey) ||
                          Input.GetKeyDown(AltKeyboardKey.UpKey) ||
                          Input.GetKeyDown(AltKeyboardKey.LeftKey) ||
                          Input.GetKeyDown(AltKeyboardKey.RightKey) ||
                          Input.GetKeyDown(SecondaryKeyboardKey.DownKey) ||
                          Input.GetKeyDown(SecondaryKeyboardKey.UpKey) ||
                          Input.GetKeyDown(SecondaryKeyboardKey.LeftKey) ||
                          Input.GetKeyDown(SecondaryKeyboardKey.RightKey) ||
                          Input.GetKeyDown(AltSecondaryKeyboardKey.DownKey) ||
                          Input.GetKeyDown(AltSecondaryKeyboardKey.UpKey) ||
                          Input.GetKeyDown(AltSecondaryKeyboardKey.LeftKey) ||
                          Input.GetKeyDown(AltSecondaryKeyboardKey.RightKey)
                          ;

            return result;
        }

        public bool TriggerKeyUp()
        {
            bool result = Input.GetKeyUp(PrimaryKeyboardKey.DownKey) ||
                          Input.GetKeyUp(PrimaryKeyboardKey.UpKey) ||
                          Input.GetKeyUp(PrimaryKeyboardKey.LeftKey) ||
                          Input.GetKeyUp(PrimaryKeyboardKey.RightKey) ||
                          Input.GetKeyUp(AltKeyboardKey.DownKey) ||
                          Input.GetKeyUp(AltKeyboardKey.UpKey) ||
                          Input.GetKeyUp(AltKeyboardKey.LeftKey) ||
                          Input.GetKeyUp(AltKeyboardKey.RightKey) ||
                          Input.GetKeyUp(SecondaryKeyboardKey.DownKey) ||
                          Input.GetKeyUp(SecondaryKeyboardKey.UpKey) ||
                          Input.GetKeyUp(SecondaryKeyboardKey.LeftKey) ||
                          Input.GetKeyUp(SecondaryKeyboardKey.RightKey) ||
                          Input.GetKeyUp(AltSecondaryKeyboardKey.DownKey) ||
                          Input.GetKeyUp(AltSecondaryKeyboardKey.UpKey) ||
                          Input.GetKeyUp(AltSecondaryKeyboardKey.LeftKey) ||
                          Input.GetKeyUp(AltSecondaryKeyboardKey.RightKey)
                          ;

            return result;
        }



        // Update is called once per frame
        void Update()
        {
            if (usingShiftSettings)
            {
                for (int i = 0; i < ShiftKeyboardArray.Count; i++)
                {
                    if (Input.GetKey(ShiftKeyboardArray[i].TargetShiftKey) && Input.GetKeyDown(ShiftKeyboardArray[i].InputKeyDown))
                    {
                        ShiftKeyboardArray[i].KeyDownEvent.Invoke();
                    }
                    else if (Input.GetKey(ShiftKeyboardArray[i].TargetShiftKey) && Input.GetKey(ShiftKeyboardArray[i].InputKey))
                    {
                        ShiftKeyboardArray[i].KeyEvent.Invoke();
                    }
                    else if (Input.GetKey(ShiftKeyboardArray[i].TargetShiftKey) && Input.GetKeyUp(ShiftKeyboardArray[i].InputKeyUp))
                    {
                        ShiftKeyboardArray[i].KeyUpEvent.Invoke();
                    } else
                    {
                        if (usingAdditionalSettings)
                        {
                            for (int xi = 0; xi < KeyboardArray.Count; xi++)
                            {
                                if (Input.GetKeyDown(KeyboardArray[xi].InputKeyDown))
                                {
                                    KeyboardArray[xi].KeyDownEvent.Invoke();
                                }
                                else if (Input.GetKey(KeyboardArray[xi].InputKey))
                                {
                                    KeyboardArray[xi].KeyEvent.Invoke();
                                }
                                else if (Input.GetKeyUp(KeyboardArray[xi].InputKeyUp))
                                {
                                    KeyboardArray[xi].KeyUpEvent.Invoke();
                                } else
                                {
                                    if (TriggerKeyPress())
                                    {
                                        if (PrimaryKeyboardKey.usingEventSettings)
                                        {
                                            PrimaryKeyboardKey.KeyboardEvent.Invoke();
                                        }
                                        if (SecondaryKeyboardKey.usingEventSettings)
                                        {
                                            SecondaryKeyboardKey.KeyboardEvent.Invoke();
                                        }
                                        if (AltKeyboardKey.usingEventSettings)
                                        {
                                            AltKeyboardKey.KeyboardEvent.Invoke();
                                        }
                                        if (AltSecondaryKeyboardKey.usingEventSettings)
                                        {
                                            AltSecondaryKeyboardKey.KeyboardEvent.Invoke();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (TriggerKeyPress())
                            {
                                if (PrimaryKeyboardKey.usingEventSettings)
                                {
                                    PrimaryKeyboardKey.KeyboardEvent.Invoke();
                                }
                                if (SecondaryKeyboardKey.usingEventSettings)
                                {
                                    SecondaryKeyboardKey.KeyboardEvent.Invoke();
                                }
                                if (AltKeyboardKey.usingEventSettings)
                                {
                                    AltKeyboardKey.KeyboardEvent.Invoke();
                                }
                                if (AltSecondaryKeyboardKey.usingEventSettings)
                                {
                                    AltSecondaryKeyboardKey.KeyboardEvent.Invoke();
                                }
                            }
                        }
                    }
                }
            } else if (usingAdditionalSettings)
            {
                for (int xi = 0; xi < KeyboardArray.Count; xi++)
                {
                    if (Input.GetKeyDown(KeyboardArray[xi].InputKeyDown))
                    {
                        KeyboardArray[xi].KeyDownEvent.Invoke();
                    }
                    else if (Input.GetKey(KeyboardArray[xi].InputKey))
                    {
                        KeyboardArray[xi].KeyEvent.Invoke();
                    }
                    else if (Input.GetKeyUp(KeyboardArray[xi].InputKeyUp))
                    {
                        KeyboardArray[xi].KeyUpEvent.Invoke();
                    }
                    else
                    {
                        if (TriggerKeyPress())
                        {
                            if (PrimaryKeyboardKey.usingEventSettings)
                            {
                                PrimaryKeyboardKey.KeyboardEvent.Invoke();
                            }
                            if (SecondaryKeyboardKey.usingEventSettings)
                            {
                                SecondaryKeyboardKey.KeyboardEvent.Invoke();
                            }
                            if (AltKeyboardKey.usingEventSettings)
                            {
                                AltKeyboardKey.KeyboardEvent.Invoke();
                            }
                            if (AltSecondaryKeyboardKey.usingEventSettings)
                            {
                                AltSecondaryKeyboardKey.KeyboardEvent.Invoke();
                            }
                        }
                    }
                }
            }
            else {

                if (TriggerKeyPress())
                {
                    if (PrimaryKeyboardKey.usingEventSettings)
                    {
                        PrimaryKeyboardKey.KeyboardEvent.Invoke();
                    }
                    if (SecondaryKeyboardKey.usingEventSettings)
                    {
                        SecondaryKeyboardKey.KeyboardEvent.Invoke();
                    }
                    if (AltKeyboardKey.usingEventSettings)
                    {
                        AltKeyboardKey.KeyboardEvent.Invoke();
                    }
                    if (AltSecondaryKeyboardKey.usingEventSettings)
                    {
                        AltSecondaryKeyboardKey.KeyboardEvent.Invoke();
                    }
                }
            }
        }
    }
}

