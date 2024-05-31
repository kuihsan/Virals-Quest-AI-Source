using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Zetcil
{
    public class LayoutLeftSidebarView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Leftbar Settings")]
        public InputField SearchText;
        public List<Button> ButtonMenu;

        [Header("Key Settings")]
        public bool usingKeyEvent;
        public KeyCode KeyCodeEvent;
        public UnityEvent KeyEvent;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (usingKeyEvent)
            {
                if (Input.GetKey(KeyCodeEvent))
                {
                    KeyEvent.Invoke();
                }
            }
        }

        public void HideMenu()
        {
            for (int i = 0; i < ButtonMenu.Count; i++)
            {
                ButtonMenu[i].gameObject.SetActive(false);
            }
        }

        public void ShowMenu()
        {
            for (int i = 0; i < ButtonMenu.Count; i++)
            {
                ButtonMenu[i].gameObject.SetActive(true);
            }
        }

        public void SearchingText(InputField aSearch)
        {
            HideMenu();

            for (int i = 0; i < ButtonMenu.Count; i++)
            {
                if (ButtonMenu[i].GetComponentInChildren<Text>())
                {
                    if (ButtonMenu[i].GetComponentInChildren<Text>().text.Contains(aSearch.text))
                    {
                        ButtonMenu[i].gameObject.SetActive(true);
                    }
                }
            }

            if (aSearch.text== "")
            {
                ShowMenu();
            }
        }
    }
}

