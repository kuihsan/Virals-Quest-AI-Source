using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{
    public class LayoutStarView : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Variabel Settings")]
        public VarInteger VarStar;

        [Header("Star Empty Settings")]
        public Image StarEmpty1;
        public Image StarEmpty2;
        public Image StarEmpty3;

        [Header("Star Fill Settings")]
        public Image StarFill1;
        public Image StarFill2;
        public Image StarFill3;

        [Header("Audio Star")]
        public bool usingAudioStar;
        public AudioSource AudioStar;

        bool Star1, Star2, Star3 = false;

        public void HideStar()
        {
            Star1 = false;
            Star2 = false;
            Star3 = false;
            StarFill1.gameObject.SetActive(false);
            StarFill2.gameObject.SetActive(false);
            StarFill3.gameObject.SetActive(false);
        }

        public void UpdateStar()
        {
            if (VarStar.CurrentValue == 1)
            {
                if (!Star1)
                {
                    StarFill1.gameObject.SetActive(true);
                    StarFill2.gameObject.SetActive(false);
                    StarFill3.gameObject.SetActive(false);
                    if (usingAudioStar)
                    {
                        AudioStar.Play();
                    }
                    Star1 = true;
                }
            }
            else if (VarStar.CurrentValue == 2)
            {
                if (!Star2)
                {
                    StarFill1.gameObject.SetActive(true);
                    StarFill2.gameObject.SetActive(true);
                    StarFill3.gameObject.SetActive(false);
                    if (usingAudioStar)
                    {
                        AudioStar.Play();
                    }
                    Star2 = true;
                }
            }
            else if (VarStar.CurrentValue == 3)
            {
                if (!Star3)
                {
                    StarFill1.gameObject.SetActive(true);
                    StarFill2.gameObject.SetActive(true);
                    StarFill3.gameObject.SetActive(true);
                    if (usingAudioStar)
                    {
                        AudioStar.Play();
                    }
                    Star3 = true;
                }
            }
        }

        public void StartInitialize()
        {
            if (isEnabled)
            {
                InvokeRepeating("UpdateStar", 1, 1);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            HideStar();
            StartInitialize();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
