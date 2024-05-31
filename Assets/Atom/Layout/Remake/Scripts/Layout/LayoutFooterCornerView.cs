using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class LayoutFooterCornerView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Toggle Settings")]
        public int ToggleCount;
        public List<Toggle> ToggleMenu;

        // Start is called before the first frame update
        void Start()
        {
            ShowCheckedOnly();
        }

        public void ShowCheckedOnly()
        {
            for (int i = 0; i < ToggleCount; i++)
            {
                ToggleMenu[i].gameObject.SetActive(false);
                if (ToggleMenu[i].isOn)
                {
                    ToggleMenu[i].gameObject.SetActive(true);
                }
            }
        }

        public void ShowAll()
        {
            for (int i = 0; i < ToggleCount; i++)
            {
                ToggleMenu[i].gameObject.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
