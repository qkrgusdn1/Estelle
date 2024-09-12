using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ChooseMgr : MonoBehaviour
{
    private static ChooseMgr instance;
    public static ChooseMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject[] buttonLevelObject;

    public TMP_Text mainNameText;

    public TMP_Text questionText;

    public TMP_Text descriptionText;
    public string[] description;

    ChooseType chooseType;

    public GameObject questionPanel;

    public string myName;
    public string customerName;

    public int chooseTextIndex;
    public int questionIndex;

    public int[] questionMoments;

    bool questionTime;

    public ChooseTextGroup chooseTextGroup;

    

    readonly string[] initialConsonants = { "ぁ", "あ", "い", "ぇ", "え", "ぉ", "け", "げ", "こ", "さ", "ざ", "し", "じ", "す", "ず", "せ", "ぜ", "そ", "ぞ" };
    readonly string[] medialVowels = { "た", "だ", "ち", "ぢ", "っ", "つ", "づ", "て", "で", "と", "ど", "な", "に", "ぬ", "ね", "の", "は", "ば", "ぱ", "ひ", "び" };
    readonly string[] finalConsonants = { "", "ぁ", "あ", "ぃ", "い", "ぅ", "う", "ぇ", "ぉ", "お", "か", "が", "き", "ぎ", "く", "ぐ", "け", "げ", "ご", "さ", "ざ", "し", "じ", "ず", "せ", "ぜ", "そ", "ぞ" };

    private void Start()
    {
        SetDescriptionText(0);
        chooseType = ChooseType.normal;
        StartCoroutine(TypeText(GetCurrentTexts()[chooseTextIndex].chooseTexts, GetCurrentTexts()[chooseTextIndex].mine));
        
        for (int i = 0; i < buttonLevelObject.Length; i++)
        {
            if (i != questionIndex)
            {
                buttonLevelObject[i].SetActive(false);
            }
            else
            {
                buttonLevelObject[i].SetActive(true);
            }
        }
        chooseTextIndex++;
    }
    void SetDescriptionText(int index)
    {
        if (index < description.Length)
        {
            descriptionText.text = WrapText(description[index], 9);
        }
    }
    private string WrapText(string text, int maxLineLength)
    {
        if (string.IsNullOrEmpty(text) || maxLineLength <= 0)
            return text;

        string wrappedText = "";
        int currentLength = 0;

        foreach (char c in text)
        {
            wrappedText += c;
            currentLength++;

            if (currentLength >= maxLineLength)
            {
                wrappedText += "\n";
                currentLength = 0;
            }
        }

        return wrappedText;
    }
    ChooseText[] GetCurrentTexts()
    {
        switch (chooseType)
        {
            case ChooseType.hit:
                return chooseTextGroup.hitTexts;
            case ChooseType.alive:
                return chooseTextGroup.aliveTexts;
            default:
                return chooseTextGroup.chooseTexts;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !questionTime)
        {
            if(chooseTextIndex == questionMoments[questionIndex] && questionIndex < questionMoments.Length)
            {
                questionPanel.SetActive(true);
                questionIndex++;
                questionTime = true;
                return;
            }
            else if(chooseTextIndex < GetCurrentTexts().Length && !questionTime)
            {
                StopAllCoroutines();
                StartCoroutine(TypeText(GetCurrentTexts()[chooseTextIndex].chooseTexts, GetCurrentTexts()[chooseTextIndex].mine));
                chooseTextIndex++;
            }
            
        }
    }

    

    IEnumerator TypeText(string text, bool isMine)
    {
        if (isMine)
        {
            mainNameText.text = myName;
            mainNameText.color = Color.black;
            questionText.color = Color.black;
        }
        else
        {
            mainNameText.text = customerName;
            mainNameText.color = Color.red;
            questionText.color = Color.red;
        }

        questionText.text = "";
        foreach (char letter in text)
        {
            if (letter >= 0xAC00 && letter <= 0xD7A3)
            {
                int unicode = letter - 0xAC00;
                int initialIndex = unicode / (21 * 28);
                int medialIndex = (unicode % (21 * 28)) / 28;
                int finalIndex = unicode % 28;


                questionText.text += initialConsonants[initialIndex];
                yield return new WaitForSeconds(0.05f);


                questionText.text += medialVowels[medialIndex];
                yield return new WaitForSeconds(0.05f);

                if (finalIndex != 0)
                {
                    questionText.text += finalConsonants[finalIndex];
                    yield return new WaitForSeconds(0.05f);
                }

                char combinedChar = (char)(0xAC00 + (initialIndex * 21 * 28) + (medialIndex * 28) + finalIndex);
                questionText.text = questionText.text.Substring(0, questionText.text.Length - (finalIndex != 0 ? 3 : 2));
                questionText.text += combinedChar;
            }
            else
            {
                questionText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void OnClickedChooseBtn(string chooseType)
    {
        if (Enum.TryParse(chooseType, out ChooseType parsedType))
        {
            questionTime = false;
            this.chooseType = parsedType;
            questionPanel.SetActive(false);
            chooseTextIndex = 0;
            StartCoroutine(TypeText(GetCurrentTexts()[chooseTextIndex].chooseTexts, GetCurrentTexts()[chooseTextIndex].mine));
            chooseTextIndex++;
            for (int i = 0; i < buttonLevelObject.Length; i++)
            {
                if (i != questionIndex)
                {
                    buttonLevelObject[i].SetActive(false);
                }
                else
                {
                    buttonLevelObject[i].SetActive(true);
                }
            }
            
            SetDescriptionText(chooseTextIndex);
        }
    }
}

[System.Serializable]
public class ChooseText
{
    public string chooseTexts;
    public bool mine;
}

[System.Serializable]
public class ChooseTextGroup
{
    public ChooseText[] chooseTexts;
    public ChooseText[] hitTexts;
    public ChooseText[] aliveTexts;
}

public enum ChooseType
{
    normal,
    hit,
    alive
}
