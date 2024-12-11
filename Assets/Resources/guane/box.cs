using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimytry : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Die()
    {
        Debug.Log("敵人葛屁");
        Destroy(gameObject);//刪除敵人物件
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //確認是否是那箱子
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