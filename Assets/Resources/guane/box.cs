using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimytry : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Die()
    {
        Debug.Log("�ĤH����");
        Destroy(gameObject);//�R���ĤH����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�T�{�O�_�O���c�l
        if (collision.gameObject.CompareTag("box")) ;
        {
            Die();
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}