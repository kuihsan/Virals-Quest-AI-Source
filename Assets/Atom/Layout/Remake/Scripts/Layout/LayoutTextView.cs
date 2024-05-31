/**************************************************************************************************************
 * Author : Rickman Roedavan
 * Version: 2.12
 * Desc   : Script untuk menampilkan nilai variabel
 **************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class LayoutTextView : MonoBehaviour
    {
        public enum CTimeFormat { None, SS, MMSS, HHMMSS }
        [Space(10)]
        public bool isEnabled;

        [Header("Name Variables")]
        public bool usingCharacterName;
        public VarString CharacterName;
        public Text CharacterNameText;
        public string CharacterNamePrefix;
        public string CharacterNamePostfix;

        [Header("Score Variables")]
        public bool usingCharacterScore;
        public VarScore CharacterScore;
        public Text CharacterScoreText;
        public string CharacterScorePrefix;
        public string CharacterScorePostfix;

        [Header("Time Variables")]
        public bool usingCharacterTime;
        public VarTime CharacterTime;
        public Text CharacterTimeText;
        public string CharacterTimePrefix;
        public string CharacterTimePostfix;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (usingCharacterName)
            {
                CharacterNameText.text = CharacterNamePrefix + CharacterName.CurrentValue.ToString() + CharacterNamePostfix;
            }
            if (usingCharacterScore)
            {
                CharacterScoreText.text = CharacterScorePrefix + CharacterScore.CurrentValue.ToString() + CharacterScorePostfix;
            }
            if (usingCharacterTime)
            {
                CharacterTimeText.text = CharacterTimePrefix + CharacterTime.CurrentValue.ToString() + CharacterTimePostfix;
            }
        }

        public string FloatToTime(float toConvert, string format)
        {
            switch (format)
            {
                case "None":
                    return string.Format("{0:00}",
                        Mathf.Floor(toConvert) //seconds
                        );//miliseconds

                case "SS":
                    return string.Format("{0:00}",
                        Mathf.Floor(toConvert) % 60//seconds
                        );//miliseconds

                case "MMSS":
                    return string.Format("{0:00}:{1:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60//seconds
                        );//miliseconds

                case "HHMMSS":
                    return string.Format("{0:00}:{1:00}:{2:00}",
                        Mathf.Floor(toConvert / 3600),//hours
                        Mathf.Floor(toConvert / 60) - (Mathf.Floor(toConvert / 3600) * 60), //minutes
                        Mathf.Floor(toConvert) % 60//seconds
                        );//miliseconds

                case "00.0":
                    return string.Format("{0:00}:{1:0}:{2:0}",
                        Mathf.Floor(toConvert) / 3600,//seconds
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case "#0.0":
                    return string.Format("{0:#0}:{1:0}",
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case "00.00":
                    return string.Format("{0:00}:{1:00}",
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 100) % 100));//miliseconds

                case "00.000":
                    return string.Format("{0:00}:{1:000}",
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 1000) % 1000));//miliseconds

                case "#00.000":
                    return string.Format("{0:#00}:{1:000}",
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 1000) % 1000));//miliseconds

                case "#0:00":
                    return string.Format("{0:#0}:{1:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60);//seconds

                case "#00:00":
                    return string.Format("{0:#00}:{1:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60);//seconds

                case "0:00.0":
                    return string.Format("{0:0}:{1:00}.{2:0}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case "#0:00.0":
                    return string.Format("{0:#0}:{1:00}.{2:0}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case "0:00.00":
                    return string.Format("{0:0}:{1:00}.{2:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 100) % 100));//miliseconds

                case "#0:00.00":
                    return string.Format("{0:#0}:{1:00}.{2:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 100) % 100));//miliseconds

                case "0:00.000":
                    return string.Format("{0:0}:{1:00}.{2:000}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 1000) % 1000));//miliseconds

                case "#0:00.000":
                    return string.Format("{0:#0}:{1:00}.{2:000}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 1000) % 1000));//miliseconds

            }
            return "error";
        }

    }
}
