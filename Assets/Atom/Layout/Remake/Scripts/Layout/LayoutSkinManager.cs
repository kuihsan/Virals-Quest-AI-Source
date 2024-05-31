using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class LayoutSkinManager : MonoBehaviour
    {
        public bool isEnabled;

        [System.Serializable]
        public class CSkinCollection {
            public string Name;
            public Sprite Dialog;
            public Sprite Header100;
            public Sprite Header300;
            public Sprite MainButton;
            public Sprite SquareButton;
            public Sprite RectButton;
        }

        [Header("Index Settings")]
        public int CurrentSkinIndex;

        [Header("Skin Settings")]
        public List<CSkinCollection> SkinCollection;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
