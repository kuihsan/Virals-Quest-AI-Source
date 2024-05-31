using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class LayoutLocationRemake : MonoBehaviour
    {
        [System.Serializable]
        public class CTargetLocation
        {
            public string LocationName;
            public bool usingXML;
            [ConditionalField("usingXML")] public VarLanguage LanguageID;
            [ConditionalField("usingXML")] public string XMLID;
            public GameObject LocationTarget;

            [Header("Event Location")]
            public bool usingLocationEvent;
            public UnityEvent LocationEvent;
        }

        [Space(10)]
        public bool isEnabled;

        [Header("Main Settings")]
        public GameObject TargetPlayer;
        public InputField TargetText;

        [Header("Location Settings")]
        public List<CTargetLocation> TargetLocation;
        int CurrentIndex;

        // Start is called before the first frame update
        void Start()
        {
            CurrentIndex = 0;
            SetCurrentIndex();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Prev()
        {
            CurrentIndex--;
            if (CurrentIndex <= 0)
            {
                CurrentIndex = 0;
            }

            SetCurrentIndex();
        }

        public void Next()
        {
            CurrentIndex++;
            if (CurrentIndex >= TargetLocation.Count)
            {
                CurrentIndex = TargetLocation.Count - 1;
            }

            SetCurrentIndex();
        }

        public void SetCurrentIndex()
        {
            if (TargetLocation[CurrentIndex].usingXML)
            {
                TargetText.text = TargetLocation[CurrentIndex].LanguageID.GetLanguageDialog(TargetLocation[CurrentIndex].XMLID);
            } else
            {
                TargetText.text = TargetLocation[CurrentIndex].LocationName;
            }
            TargetPlayer.transform.position = TargetLocation[CurrentIndex].LocationTarget.transform.position;
            TargetPlayer.transform.rotation = TargetLocation[CurrentIndex].LocationTarget.transform.rotation;

            if (TargetLocation[CurrentIndex].usingLocationEvent)
            {
                TargetLocation[CurrentIndex].LocationEvent.Invoke();
            }
        }
    }
}
