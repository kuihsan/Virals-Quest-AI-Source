using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class NovelController : MonoBehaviour
    {

        public enum CCharacterStatus { IsVisible, IsHide }

        public bool isEnabled;

        [System.Serializable]
        public class CDialogCutscene
        {
            [Header("Character Status")]
            public CCharacterStatus CharacterStatus;

            [Header("Character Settings")]
            public bool usingCharacterSettings;
            public Image CharacterImage;
            public Text UICharacterName;
            public Text UICharacterText;

            [Header("Text Settings")]
            public bool usingTextSettings;
            public string CharacterName;
            public string CharacterText;
            public Image TextPanel;
            public Color TextColor;

            [Header("Visible Settings")]
            public bool usingVisibleSettings;
            public Color VisibleColor;

            [Header("Hide Settings")]
            public bool usingHideSettings;
            public Color HideColor;

        }

        [Header("Awake Settings")]
        public bool usingAwake;
        public int AwakeLeftIndex;
        public int AwakeRightIndex;

        [Header("Left Character Settings")]
        public bool usingLeftCharacter;
        public VarInteger LeftIndex;
        public List<CDialogCutscene> LeftCharacter;

        [Header("Right Character Settings")]
        public bool usingRightCharacter;
        public VarInteger RightIndex;
        public List<CDialogCutscene> RightCharacter;

        [Header("Additional Settings")]
        public bool usingLastIndex;
        public UnityEvent LastIndexSettings;

        // Start is called before the first frame update
        void Start()
        {
            if (usingAwake)
            {
                ShowLeftCharacter(AwakeLeftIndex);
                ShowRightCharacter(AwakeRightIndex);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (usingLastIndex)
            {
                if (LeftIndex.CurrentValue >= LeftCharacter.Count || RightIndex.CurrentValue == RightCharacter.Count)
                {
                    LastIndexSettings.Invoke();
                }
            }
        }

        public void ShowDialogCurrentIndex()
        {
            ShowLeftCharacterCurrentIndex();
            ShowRightCharacterCurrentIndex();
        }

        public void ShowDialogIndex(int Index)
        {
            ShowLeftCharacter(Index);
            ShowRightCharacter(Index);
        }

        public void AddDialogCurrentIndex()
        {
            LeftIndex.CurrentValue++;
            RightIndex.CurrentValue++;
            ShowLeftCharacterCurrentIndex();
            ShowRightCharacterCurrentIndex();
        }

        public void SubDialogCurrentIndex()
        {
            LeftIndex.CurrentValue--;
            RightIndex.CurrentValue--;
            ShowLeftCharacterCurrentIndex();
            ShowRightCharacterCurrentIndex();
        }

        public void ShowLeftCharacter(int Index)
        {
            if (isEnabled)
            {
                if (usingLeftCharacter)
                {
                    LeftIndex.CurrentValue = Index;
                    if (LeftIndex.CurrentValue < LeftCharacter.Count)
                    {
                        if (LeftCharacter[LeftIndex.CurrentValue].usingCharacterSettings)
                        {
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && LeftCharacter[LeftIndex.CurrentValue].usingVisibleSettings)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].CharacterImage.color = LeftCharacter[LeftIndex.CurrentValue].VisibleColor;
                            }
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && LeftCharacter[LeftIndex.CurrentValue].usingTextSettings)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterName.text = LeftCharacter[LeftIndex.CurrentValue].CharacterName;
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterText.text = LeftCharacter[LeftIndex.CurrentValue].CharacterText;
                            }
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsHide)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].CharacterImage.color = LeftCharacter[LeftIndex.CurrentValue].HideColor;
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterName.text = "";
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterText.text = "";
                            }
                        }
                    }
                }
            }
        }

        public void ShowLeftCharacterCurrentIndex()
        {
            if (isEnabled)
            {
                if (usingLeftCharacter)
                {
                    if (LeftIndex.CurrentValue < LeftCharacter.Count)
                    {
                        if (LeftCharacter[LeftIndex.CurrentValue].usingCharacterSettings)
                        {
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && LeftCharacter[LeftIndex.CurrentValue].usingVisibleSettings)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].CharacterImage.color = LeftCharacter[LeftIndex.CurrentValue].VisibleColor;
                            }
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && LeftCharacter[LeftIndex.CurrentValue].usingTextSettings)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterName.text = LeftCharacter[LeftIndex.CurrentValue].CharacterName;
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterText.text = LeftCharacter[LeftIndex.CurrentValue].CharacterText;
                            }
                            if (LeftCharacter[LeftIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsHide)
                            {
                                LeftCharacter[LeftIndex.CurrentValue].CharacterImage.color = LeftCharacter[LeftIndex.CurrentValue].HideColor;
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterName.text = "";
                                LeftCharacter[LeftIndex.CurrentValue].UICharacterText.text = "";
                            }
                        }
                    }
                }
            }
        }

        public void ShowRightCharacter(int Index)
        {
            if (isEnabled)
            {
                if (usingRightCharacter)
                {
                    RightIndex.CurrentValue = Index;
                    if (RightIndex.CurrentValue < RightCharacter.Count)
                    {
                        if (RightCharacter[RightIndex.CurrentValue].usingCharacterSettings)
                        {
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && RightCharacter[RightIndex.CurrentValue].usingVisibleSettings)
                            {
                                RightCharacter[RightIndex.CurrentValue].CharacterImage.color = RightCharacter[RightIndex.CurrentValue].VisibleColor;
                            }
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && RightCharacter[RightIndex.CurrentValue].usingTextSettings)
                            {
                                RightCharacter[RightIndex.CurrentValue].UICharacterName.text = RightCharacter[RightIndex.CurrentValue].CharacterName;
                                RightCharacter[RightIndex.CurrentValue].UICharacterText.text = RightCharacter[RightIndex.CurrentValue].CharacterText;
                            }
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsHide)
                            {
                                RightCharacter[RightIndex.CurrentValue].CharacterImage.color = RightCharacter[RightIndex.CurrentValue].HideColor;
                                RightCharacter[RightIndex.CurrentValue].UICharacterName.text = "";
                                RightCharacter[RightIndex.CurrentValue].UICharacterText.text = "";
                            }
                        }
                    }
                }
            }
        }

        public void ShowRightCharacterCurrentIndex()
        {
            if (isEnabled)
            {
                if (usingRightCharacter)
                {
                    if (RightIndex.CurrentValue < RightCharacter.Count)
                    {
                        if (RightCharacter[RightIndex.CurrentValue].usingCharacterSettings)
                        {
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && RightCharacter[RightIndex.CurrentValue].usingVisibleSettings)
                            {
                                RightCharacter[RightIndex.CurrentValue].CharacterImage.color = RightCharacter[RightIndex.CurrentValue].VisibleColor;
                            }
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsVisible && RightCharacter[RightIndex.CurrentValue].usingTextSettings)
                            {
                                RightCharacter[RightIndex.CurrentValue].UICharacterName.text = RightCharacter[RightIndex.CurrentValue].CharacterName;
                                RightCharacter[RightIndex.CurrentValue].UICharacterText.text = RightCharacter[RightIndex.CurrentValue].CharacterText;
                            }
                            if (RightCharacter[RightIndex.CurrentValue].CharacterStatus == CCharacterStatus.IsHide)
                            {
                                RightCharacter[RightIndex.CurrentValue].CharacterImage.color = RightCharacter[RightIndex.CurrentValue].HideColor;
                                RightCharacter[RightIndex.CurrentValue].UICharacterName.text = "";
                                RightCharacter[RightIndex.CurrentValue].UICharacterText.text = "";
                            }
                        }
                    }
                }
            }
        }
    }
}
