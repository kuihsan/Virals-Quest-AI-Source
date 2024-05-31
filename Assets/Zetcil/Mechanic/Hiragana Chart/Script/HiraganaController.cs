using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaController : MonoBehaviour
{
    public bool isEnabled;

    [System.Serializable]
    public class CHiraganaSelection
    {
        public GameObject SpeechController;
        public GameObject ColumnSelection;
        public List<GameObject> LetterSelection;
    }

    [Header("Hiragana Selection Settings")]
    public List<CHiraganaSelection> HiraganaSelection;

    [Header("Index Status")] 
    public int ColumnIndex;
    public int LetterIndex;

    public void HideAllColumnSelection()
    {
        foreach (CHiraganaSelection temp in HiraganaSelection)
        {
            temp.ColumnSelection.SetActive(false);
            temp.SpeechController.SetActive(false);
        }
    }

    public void HideAllLetterSelection()
    {
        foreach (GameObject temp in HiraganaSelection[ColumnIndex].LetterSelection)
        {
            temp.SetActive(false);
        }
    }

    public void ShowColumnSelection(int aIndex)
    {
        HideAllColumnSelection();
        HiraganaSelection[aIndex].ColumnSelection.SetActive(true);
        HiraganaSelection[aIndex].SpeechController.SetActive(true);
        ColumnIndex = aIndex;
        HideAllLetterSelection();
    }

    public void ShowLetterSelection(int aIndex)
    {
        HideAllLetterSelection();
        HiraganaSelection[ColumnIndex].LetterSelection[aIndex].SetActive(true);
        LetterIndex = aIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        HideAllColumnSelection();
        ShowColumnSelection(0);
    }

    public void ShowNextHiragana()
    {
        ColumnIndex++;
        if (ColumnIndex > HiraganaSelection.Count - 1)
        {
            ColumnIndex = HiraganaSelection.Count - 1;
        }
        HideAllColumnSelection();
        ShowColumnSelection(ColumnIndex);
        HideAllLetterSelection();
    }

    public void ShowPrevHiragana()
    {
        ColumnIndex--;
        if (ColumnIndex < 0)
        {
            ColumnIndex = 0;
        }
        HideAllColumnSelection();
        ShowColumnSelection(ColumnIndex);
        HideAllLetterSelection();
    }

}
