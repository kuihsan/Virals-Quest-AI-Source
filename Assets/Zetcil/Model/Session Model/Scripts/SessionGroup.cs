using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class SessionGroup : MonoBehaviour
    {

        [Space(10)]
        public bool isEnabled;

        [Header("Config Settings")]
        public VarConfig SessionConfig;

        [Header("Group Settings")]
        public List<SessionData> TargetSession;

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
