using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordList : MonoBehaviour
{
    private static WordList S;

    [Header("Set in I")]
    public TextAsset wordListText;
    public int numToParseBeforeYield = 10000;
    public int wordLengthMin = 3;
    public int wordLengtMax = 7;

    [Header("Set in D")]
    public int currLine = 0;
    public int totalLines;
    public int longWordCount;
    public int wordCount;

    private string[] lines;
    private List<string> longWords;
    private List<string> words;
    private void Awake()
    {
        S = this;
    }
    public void Init()
    {
        lines = wordListText.text.Split('\n');
        totalLines = lines.Length;

        StartCoroutine(ParseLines());
    }
    static public void INIT()
    {
        S.Init();
    }
    public IEnumerator ParseLines()
    {
        string word;
        // ���������������� ������ ��� �������� ���������� ����
        // �� ����� ����������
        longWords = new List<string>();
        words = new List<string>();

        for (currLine = 0; currLine < totalLines; currLine++)
        {
            word = lines[currLine];

            // ���� ����� ����� ����� wordLengthMax
            if (word.Length == wordLengtMax)
            {
                longWords.Add(word);// ...��������� ��� � longWords
            }

            // ���� ����� ����� ����� wordLengthMin � wordLengthMax...
            if (word.Length >= wordLengthMin && word.Length <= wordLengtMax)
            {
                words.Add(word);// ...�������� ��� � ������ ���������� ����
            }

            // ����������, �� ���� �� ������� �������
            if (currLine % numToParseBeforeYield == 0)
            {
                // ���������� ����� � ������ ������, ����� ��������,
                // ��� ��������� ������� �������
                longWordCount = longWords.Count;
                wordCount = words.Count;
                yield return null;
            }
        }
        longWordCount = longWords.Count;
        wordCount = words.Count;

        // ������� �������� ������� gameObject ��������� �� ��������� �������
        gameObject.SendMessage("WordListParseComplete");
    }
    // ��� ������ ��������� ������ �������
    // ���������� � ������� ����� List<string�
    static public List<string> GET_WORDS()
    {
        return (S.words);
    }
    static public string GET_WORD(int ndx)
    {
        return (S.words[ndx]);
    }
    static public List<string> GET_LONG_WORDS()
    {
        return (S.longWords);
    }
    static public string GET_LONG_WORD(int ndx)
    {
        return (S.longWords[ndx]);
    }
    static public int WORD_COUNT
    {
        get { return S.wordCount; }
    }
    static public int LONG_WORD_COUNT
    {
        get { return S.longWordCount; }
    }
    static public int NUM_TO_PARSE_BEFORE_YIELD
    {
        get { return S.numToParseBeforeYield; }
    }
    static public int WORD_LENGTH_MIN
    {
        get { return S.wordLengthMin; }
    }
    static public int WORD_LENGTH_MAX
    {
        get { return S.wordLengtMax; }
    }
}
