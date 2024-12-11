using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public float flyForce = 10f;// 控制箱子飛出的力量
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Fly(5f);
    }
    public void Fly(float direction)
    {
        Vector2 force = new Vector2(direction, 0).normalized * flyForce; // 根據方向計算箱子飛出的力量
        rb.AddForce(force, ForceMode2D.Impulse);// 添加衝擊力
    }


    // Update is called once per frame
    void Update()
    {

    }
}