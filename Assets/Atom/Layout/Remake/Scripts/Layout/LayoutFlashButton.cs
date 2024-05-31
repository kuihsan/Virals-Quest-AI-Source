using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class LayoutFlashButton : MonoBehaviour
    {
        public Image FlashButton;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("InvokeFlashButton", 1, 0.25f);
        }

        void InvokeFlashButton()
        {
            FlashButton.enabled = !FlashButton.enabled;
        }

        // Update is called once per frame
        void LateUpdate()
        {
        }
    }
}
