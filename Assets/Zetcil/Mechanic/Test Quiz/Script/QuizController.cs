using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TechnomediaLabs;

namespace Zetcil
{

    public class QuizController : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [System.Serializable]
        public class CQuizChoice
        {
            public string QuizAnswer;
            public Text QuizAnswerUI;
            public Toggle UserAnswerUI;
            [ReadOnly] public bool LastToggleStatus;
        }

        [System.Serializable]
        public class CQuizSettings
        {
            [Header("Image Settings")]
            public bool usingImage;
            public Image QuizImage;

            [Header("Question Settings")]
            public bool usingText;
            public string QuizQuestion;
            public Text QuizQuestionUI;

            [Header("Choice Settings")]
            public CQuizChoice QuizChoice1;
            public CQuizChoice QuizChoice2;
            public CQuizChoice QuizChoice3;
            public CQuizChoice QuizChoice4;

            [Header("Answer Settings")]
            public Toggle QuizAnswer;

            [Header("Correct Answer Status")]
            [ReadOnly] public bool UserQuizStatus;

        }

        [Header("Quiz Settings")]
        [ReadOnly] public int CurrentIndex = 0;
        public CQuizSettings[] QuizSettings;

        [System.Serializable]
        public class CQuizResults
        {
            public VarScore TrueVar;
            public VarScore FalseVar;
            public Text TrueAnswer;
            public Text FalseAnswer;
            public Text TotalResult;
        }


        [Header("Score Settings")]
        public bool usingResultSettings;
        public CQuizResults QuizResults;

        public void StartQuestion()
        {
            if (CurrentIndex < QuizSettings.Length)
            {
                CurrentIndex = 0;
                if (QuizSettings[CurrentIndex].usingText)
                {
                    QuizSettings[CurrentIndex].QuizQuestionUI.text = QuizSettings[CurrentIndex].QuizQuestion;
                    QuizSettings[CurrentIndex].QuizChoice1.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice1.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice2.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice2.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice3.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice3.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice4.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice4.QuizAnswer;

                    QuizSettings[CurrentIndex].QuizChoice1.UserAnswerUI.isOn = true;
                    QuizSettings[CurrentIndex].QuizChoice2.UserAnswerUI.isOn = false;
                    QuizSettings[CurrentIndex].QuizChoice3.UserAnswerUI.isOn = false;
                    QuizSettings[CurrentIndex].QuizChoice4.UserAnswerUI.isOn = false;

                    GetUserAnswer();
                }
            }
        }

        public void NextQuestion() {
            CurrentIndex++;
            if (CurrentIndex >= QuizSettings.Length)
            {
                CurrentIndex = QuizSettings.Length - 1;
            }
            if (CurrentIndex < QuizSettings.Length) {
                
                if (QuizSettings[CurrentIndex].usingText)
                {
                    QuizSettings[CurrentIndex].QuizQuestionUI.text = QuizSettings[CurrentIndex].QuizQuestion;
                    QuizSettings[CurrentIndex].QuizChoice1.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice1.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice2.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice2.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice3.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice3.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice4.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice4.QuizAnswer;

                    QuizSettings[CurrentIndex].QuizChoice1.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice1.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice2.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice2.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice3.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice3.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice4.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice4.LastToggleStatus;
                }
            }
        }

        public void PrevQuestion()
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
                if (QuizSettings[CurrentIndex].usingText)
                {
                    QuizSettings[CurrentIndex].QuizQuestionUI.text = QuizSettings[CurrentIndex].QuizQuestion;
                    QuizSettings[CurrentIndex].QuizChoice1.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice1.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice2.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice2.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice3.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice3.QuizAnswer;
                    QuizSettings[CurrentIndex].QuizChoice4.QuizAnswerUI.text = QuizSettings[CurrentIndex].QuizChoice4.QuizAnswer;

                    QuizSettings[CurrentIndex].QuizChoice1.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice1.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice2.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice2.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice3.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice3.LastToggleStatus;
                    QuizSettings[CurrentIndex].QuizChoice4.UserAnswerUI.isOn = QuizSettings[CurrentIndex].QuizChoice4.LastToggleStatus;
                }
            }
        }

        public void GetUserAnswer()
        {
            QuizSettings[CurrentIndex].UserQuizStatus = QuizSettings[CurrentIndex].QuizAnswer.isOn;
            QuizSettings[CurrentIndex].QuizChoice1.LastToggleStatus = QuizSettings[CurrentIndex].QuizChoice1.UserAnswerUI.isOn;
            QuizSettings[CurrentIndex].QuizChoice2.LastToggleStatus = QuizSettings[CurrentIndex].QuizChoice2.UserAnswerUI.isOn;
            QuizSettings[CurrentIndex].QuizChoice3.LastToggleStatus = QuizSettings[CurrentIndex].QuizChoice3.UserAnswerUI.isOn;
            QuizSettings[CurrentIndex].QuizChoice4.LastToggleStatus = QuizSettings[CurrentIndex].QuizChoice4.UserAnswerUI.isOn;
        }

        public void GenerateResult()
        {
            float TrueResult = 0;
            float FalseResult = 0;
            float MaxUserResult = 0;
            for (int i=0; i< QuizSettings.Length; i++) {
                if (QuizSettings[i].UserQuizStatus)
                {
                    TrueResult += 1;
                }
                else {
                    FalseResult += 1;
                }
            }
            MaxUserResult = (TrueResult / QuizSettings.Length) * 100;

            if (usingResultSettings) {
                if (QuizResults.TrueAnswer != null) QuizResults.TrueAnswer.text = TrueResult.ToString();
                if (QuizResults.FalseAnswer != null) QuizResults.FalseAnswer.text = FalseResult.ToString();
                if (QuizResults.TotalResult != null) QuizResults.TotalResult.text = MaxUserResult.ToString();

                if (QuizResults.TrueVar != null) QuizResults.TrueVar.CurrentValue = TrueResult;
                if (QuizResults.FalseVar != null) QuizResults.FalseVar.CurrentValue = FalseResult;
            }
        }

        // Use this for initialization
        void Start()
        {
            StartQuestion();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
