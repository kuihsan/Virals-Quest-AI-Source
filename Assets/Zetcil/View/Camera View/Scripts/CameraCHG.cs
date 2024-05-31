using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{

    public class CameraCHG : MonoBehaviour
    {

        public bool isEnabled;

        public VarBoolean SwitchVariable;
        public Camera PrimaryCamera;
        public Camera SecondaryCamera;

        public void SwitchOnMinimap()
        {
            PrimaryCamera.gameObject.SetActive(true);
            SecondaryCamera.gameObject.SetActive(true);

            if (SwitchVariable.CurrentValue)
            {
                SetPrimaryCameraView();
            } else
            {
                SetSecondaryCameraView();
            }
        }


        public void SwitchOffMinimap()
        {
            if (SwitchVariable.CurrentValue)
            {
                SecondaryCamera.gameObject.SetActive(false);
            } else
            {
                PrimaryCamera.gameObject.SetActive(false);
            }
        }

        public void SetPrimaryCameraView()
        {
            PrimaryCamera.rect = new Rect(0, 0, 1, 1);
            PrimaryCamera.depth = 0;
            SecondaryCamera.rect = new Rect(0.7f, 0.5f, 0.29f, 0.49f);
            SecondaryCamera.depth = 1;
        }

        public void SetSecondaryCameraView()
        {
            PrimaryCamera.rect = new Rect(0.7f, 0.5f, 0.29f, 0.49f);
            PrimaryCamera.depth = 1;
            SecondaryCamera.rect = new Rect(0, 0, 1, 1);
            SecondaryCamera.depth = 0;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
