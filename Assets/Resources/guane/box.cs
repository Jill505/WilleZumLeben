using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public float flyForce = 10f;// ����c�l���X���O�q
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Fly(5f);
    }
    public void Fly(float direction)
    {
        Vector2 force = new Vector2(direction, 0).normalized * flyForce; // �ھڤ�V�p��c�l���X���O�q
        rb.AddForce(force, ForceMode2D.Impulse);// �K�[�����O
    }


    // Update is called once per frame
    void Update()
    {

    }
}