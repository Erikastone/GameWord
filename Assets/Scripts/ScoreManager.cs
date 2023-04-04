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
    // Этот метод можно вызвать как ScoreManager.SCORE() из любого места
    static public void SCORE(Wyrd wyrd, int comb)
    {
        S.Score(wyrd, comb);
    }
    // Добавить очки за это слово
    // int combo - номер этого слова в комбинации
    void Score(Wyrd wyrd, int combo)
    {
        // Создать список List<Vector2> с точками, определяющими кривую Безье
        // для FloatingScore
        List<Vector3> pts = new List<Vector3>();

        // Получить позицию плитки с первой буквой в wyrd
        Vector3 pt = wyrd.letters[0].transform.position;
        pt = Camera.main.WorldToViewportPoint(pt);
        pt.z = 0;

        pts.Add(pt);// Сделать pt первой точкой кривой Безье

        //Добавить вторую точку кривой
        pts.Add(scoreMidPoint);

        // Сделать Scoreboard последней точкой кривой Безье
        pts.Add(Scoreboard.S.transform.position);

        // Определить значение для FloatingScore
        int value = wyrd.letters.Count * combo;
        FloatingScore fs = Scoreboard.S.CreateFloatingScore(value, pts);

        fs.timeDuration = 2f;
        // fs.timeDuration = scoreTravelTime;
        //fs.timeStart = Time.time + combo * scoreComboDelay;
        fs.fontSizes = scoreFontSizes;

        // Удвоить эффект InOut из Easing
        fs.easingCurve = Easing.InOut + Easing.InOut;

        // Вывести в FloatingScore текст вида "3 x 2”
        string txt = wyrd.letters.Count.ToString();
        if (combo > 1)
        {
            txt += " x " + combo;
        }
        fs.GetComponent<TextMeshPro>().text = txt;
    }
}
