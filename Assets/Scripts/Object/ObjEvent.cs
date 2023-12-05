using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEvent : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    [SerializeField] private Color highlightColor = Color.white;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color; // ������ ������ ����
    }

    void Update()
    {

    }
    private void OnMouseEnter()
    {
        rend.material.color = highlightColor; //��Ŀ���� ���̶���Ʈ ���� ����
    }
    private void OnMouseExit()
    {
        rend.material.color = originalColor;
    }
}
