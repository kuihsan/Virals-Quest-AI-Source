using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class LayoutNodeView : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Caption Setting")]
        public Button MainButton;
        public string Expand = ">>";
        public string Collapse = "<<";

        [Header("Child Object")]
        public GameObject ChildObject;
        bool isExpand = false;
        bool isInvoke = false;

        public void InvokeExpand()
        {
            isInvoke = false;
            isExpand = true;
            MainButton.GetComponentInChildren<Text>().text = Collapse;
        }

        public void InvokeCollapse()
        {
            isInvoke = false;
            isExpand = false;
            MainButton.GetComponentInChildren<Text>().text = Expand;
        }

        public void InvokeNode()
        {
            isInvoke = false;
            isExpand = !isExpand;
            if (isExpand)
            {
                MainButton.GetComponentInChildren<Text>().text = Collapse;
            } else
            {
                MainButton.GetComponentInChildren<Text>().text = Expand;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isInvoke && isExpand)
            {
                isInvoke = true;
                ChildObject.SetActive(true);
            }
            if (!isInvoke && !isExpand)
            {
                isInvoke = true;
                ChildObject.SetActive(false);
            }
        }
    }
}
