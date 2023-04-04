using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static private ScoreManager S;

    [Header("Set in I")]
    public List<float> scoreFontSizes = new List<float> { 36, 64, 64, 1 };
    public Vector3 scoreMidPoint = new Vector3(1, 1, 0);
    public float scoreTravelTime = 3f;
    public float scoreComboDelay = 0.5f;

    private RectTransform rectTrans;
    private void Awake()
    {
        S = this;
        rectTrans = GetComponent<RectTransform>();
    }
    // ���� ����� ����� ������� ��� ScoreManager.SCORE() �� ������ �����
    static public void SCORE(Wyrd wyrd, int comb)
    {
        S.Score(wyrd, comb);
    }
    // �������� ���� �� ��� �����
    // int combo - ����� ����� ����� � ����������
    void Score(Wyrd wyrd, int combo)
    {
        // ������� ������ List<Vector2> � �������, ������������� ������ �����
        // ��� FloatingScore
        List<Vector3> pts = new List<Vector3>();

        // �������� ������� ������ � ������ ������ � wyrd
        Vector3 pt = wyrd.letters[0].transform.position;
        pt = Camera.main.WorldToViewportPoint(pt);
        pt.z = 0;

        pts.Add(pt);// ������� pt ������ ������ ������ �����

        //�������� ������ ����� ������
        pts.Add(scoreMidPoint);

        // ������� Scoreboard ��������� ������ ������ �����
        pts.Add(Scoreboard.S.transform.position);

        // ���������� �������� ��� FloatingScore
        int value = wyrd.letters.Count * combo;
        FloatingScore fs = Scoreboard.S.CreateFloatingScore(value, pts);

        fs.timeDuration = 2f;
        // fs.timeDuration = scoreTravelTime;
        //fs.timeStart = Time.time + combo * scoreComboDelay;
        fs.fontSizes = scoreFontSizes;

        // ������� ������ InOut �� Easing
        fs.easingCurve = Easing.InOut + Easing.InOut;

        // ������� � FloatingScore ����� ���� "3 x 2�
        string txt = wyrd.letters.Count.ToString();
        if (combo > 1)
        {
            txt += " x " + combo;
        }
        fs.GetComponent<TextMeshPro>().text = txt;
    }
}
