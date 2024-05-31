using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class LayoutTutorialView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Button Setting")]
        public GameObject ButtonPrev;
        public GameObject ButtonNext;

        [Header("Tutorial Setting")]
        public Text TutorialIndicator;
        public List<Image> TutorialContent;
        int TutorialIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            HideAll();
            Show(0);
        }

        void HideAll()
        {
            for (int i = 0; i < TutorialContent.Count; i++)
            {
                TutorialContent[i].gameObject.SetActive(false);
            }
        }

        void ShowButtonPrevNext()
        {
            ButtonPrev.gameObject.SetActive(true);
            ButtonNext.gameObject.SetActive(true);
            if (TutorialIndex == 0)
            {
                ButtonPrev.gameObject.SetActive(false);
                ButtonNext.gameObject.SetActive(true);
            }
            else if (TutorialIndex == TutorialContent.Count - 1)
            {
                ButtonPrev.gameObject.SetActive(true);
                ButtonNext.gameObject.SetActive(false);
            }
        }

        public void Show(int aIndex)
        {
            TutorialIndex = aIndex;
            if (TutorialIndex >= 0 && TutorialIndex < TutorialContent.Count)
            {
                TutorialContent[TutorialIndex].gameObject.SetActive(true);
                TutorialIndicator.text = (TutorialIndex + 1).ToString() + "/" + TutorialContent.Count.ToString();
            }
            ShowButtonPrevNext();
        }

        public void Prev()
        {
            HideAll();
            
            TutorialIndex--;
            if (TutorialIndex > -1) 
            { 
                //do nothing
            } 
            else
            {
                TutorialIndex = 0;
            }
            TutorialContent[TutorialIndex].gameObject.SetActive(true);
            TutorialIndicator.text = (TutorialIndex + 1).ToString() + "/" + TutorialContent.Count.ToString();
            ShowButtonPrevNext();
        }

        public void Next()
        {
            HideAll();

            TutorialIndex++;
            if (TutorialIndex < TutorialContent.Count)
            {
                //do nothing
            }
            else
            {
                TutorialIndex = TutorialContent.Count-1;
            }

            TutorialContent[TutorialIndex].gameObject.SetActive(true);
            TutorialIndicator.text = (TutorialIndex + 1).ToString() + "/" + TutorialContent.Count.ToString();
            ShowButtonPrevNext();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
