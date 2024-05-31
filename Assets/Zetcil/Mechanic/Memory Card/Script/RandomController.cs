using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class RandomController : MonoBehaviour
    {
        public bool isEnabled;
        [ReadOnly] public bool Validation;

        [Header("Card Settings")]
        public List<GameObject> Card;

        [Header("Container Settings")]
        public List<GameObject> Container;

        [Header("Replacement Settings")]
        public bool isUIObject;

        // Start is called before the first frame update
        void Start()
        {
            if (Card.Count == Container.Count)
            {
                Validation = true;
            } else
            {
                Validation = false;
            }
            Shuffle(Card);
            if (isUIObject)
            {
                ReplacementUI();
            }
            else
            {
                Replacement();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Replacement()
        {
            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].GetComponent<MeshRenderer>().enabled = false;
            }
            for (int i = 0; i < Card.Count; i++)
            {
                Card[i].transform.localScale = Container[i].transform.localScale;
                Card[i].transform.position = Container[i].transform.position;
            }
        }

        void ReplacementUI()
        {
            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].GetComponent<Image>().enabled = false;
            }
            for (int i = 0; i < Card.Count; i++)
            {
                //Card[i].transform.localScale = Container[i].transform.localScale;
                //Card[i].transform.position = Container[i].transform.position;
                Card[i].GetComponent<RectTransform>().localScale = Container[i].GetComponent<RectTransform>().localScale;
                Card[i].GetComponent<RectTransform>().position = Container[i].GetComponent<RectTransform>().position;
            }
        }

        void Shuffle(List<GameObject> a)
        {
            // Loop array
            for (int i = a.Count - 1; i > 0; i--)
            {
                // Randomize a number between 0 and i (so that the range decreases each time)
                int rnd = UnityEngine.Random.Range(0, i);

                // Save the value of the current i, otherwise it'll overwrite when we swap the values
                GameObject temp = a[i];

                // Swap the new and old values
                a[i] = a[rnd];
                a[rnd] = temp;
            }
        }
    }
}
