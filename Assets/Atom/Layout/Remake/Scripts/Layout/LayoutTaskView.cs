using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Zetcil
{

    public class LayoutTaskView : MonoBehaviour
    {
        [System.Serializable]
        public class CTaskView
        {
            [Header("Task Settings")]
            public Text TextElement;
            public Image TextStrike;
            public bool TaskStatus;

            [Header("Task Event")]
            public bool usingTaskEvent;
            public UnityEvent TaskEvent;

            public void SetStart()
            {
                TaskStatus = false;
                TextStrike.gameObject.SetActive(false);
            }
            public void SetFinish()
            {
                TaskStatus = true;
                TextStrike.gameObject.SetActive(true);
                if (usingTaskEvent)
                {
                    TaskEvent.Invoke();
                }
            }
            public void CheckStatus()
            {
                if (TaskStatus)
                {
                    TextStrike.gameObject.SetActive(true);
                }
            }
        }


        [Space(10)]
        public bool isEnabled;

        [Header("Task View")]
        public VarInteger TaskCount;
        public List<CTaskView> TaskView;

        public void SetFinish(int aIndex)
        {
            TaskView[aIndex].SetFinish();
        }

        public void UpdateTaskCount()
        {
            int total = 0;
            for (int i = 0; i < TaskView.Count; i++)
            {
                if (TaskView[i].TaskStatus)
                {
                    total++;
                }
            }

            TaskCount.CurrentValue = total;
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < TaskView.Count; i++)
            {
                TaskView[i].SetStart();
            }
            InvokeRepeating("UpdateTaskCount", 1, 1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
