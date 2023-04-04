using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [Header("Set in I")]
    public float timeDuration = 0.5f;
    public string easingCuve = Easing.InOut;// ������� �����������

    [Header("Set D")]
    public TextMeshPro tMesh;//���������� ������
    public Renderer tRend;//����� ���������� ��������� �������
    public bool big = false;//������� � ����� ������ �������� �� �������

    //���� �������� ������������
    public List<Vector3> pts = null;
    public float timeStart = -1;

    private char _c;//������ �� ������
    private Renderer rend;
    private void Awake()
    {
        tMesh = GetComponentInChildren<TextMeshPro>();
        tRend = tMesh.GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        visible = false;
    }
    // �������� ��� ������/������ ����� � ���� _�, ������������ �������� 3D Text  
    public char c
    {
        get { return _c; }
        set
        {
            _c = value;
            tMesh.text = _c.ToString();
        }
    }
    // �������� ��� ������/������ ����� � ���� _� � ���� ������
    public string str
    {
        get { return _c.ToString(); }
        set { c = value[0]; }
    }
    // ��������� ��� ��������� ����������� 3D Text, ��� ������ �����
    // ������� ��� ��������� ��������������.
    public bool visible
    {
        get { return tRend.enabled; }
        set { tRend.enabled = value; }
    }
    //�������� ��� ������ � ������ ����� ������
    public Color color
    {
        get { return tMesh.GetComponent<Renderer>().sharedMaterial.color; }
        set { tMesh.GetComponent<Renderer>().sharedMaterial.color = value; }
    }
    // �������� ��� ������/������ ��������� ������
    // ������ ����������� ������ ����� ��� �������� ����������� � ����� ����������
    public Vector3 pos
    {
        set
        { //transform.position = value;}
          // ����� ������� ����� �� ��������� ���������� �� �����������
          // ������� ����� ����� ������� � ����� (value) ���������

            Vector3 mid = (transform.position + value) / 2f;

            // ��������� ���������� �� ��������� 1/4 ����������
            // �� ����������� ������� �����
            float mag = (transform.position - value).magnitude;
            mid += Random.insideUnitSphere * mag * 0.25f;

            // ������� List<Vector3> �����, ������������ ������ �����
            pts = new List<Vector3>() { transform.position, mid, value };

            // ���� timeStart �������� �������� �� ��������� -1,
            // ���������� ������� �����
            if (timeStart == -1)
            {
                timeStart = Time.time;
            }
        }
    }
    // ���������� ���������� � ����� �������
    public Vector3 posInmmediate
    {
        set { transform.position = value; }
    }
    // ���, ����������� ������������ ������
    private void Update()
    {
        if (timeStart == -1)
        {
            return;
        }

        // ����������� �������� ������������
        float u = (Time.time - timeStart) / timeDuration;
        u = Mathf.Clamp01(u);
        float u1 = Easing.Ease(u, easingCuve);
        Vector3 v = Utils.Bezier(u1, pts);
        transform.position = v;

        // ���� ������������ ���������, �������� -1 � timestart
        if (u == 1)
        {
            timeStart = -1;
        }
    }
}
