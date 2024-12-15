using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobBullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("John");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity =  new Vector2(moveDir.x,moveDir.y);
        Destroy(this.gameObject,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "John")
        {
            //Trigger it's injurd function
            collision.gameObject.GetComponent<John_Control>().johnInjurd();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "May")
        {
            //Trigger it's injurd function
            collision.gameObject.GetComponent<May_Control>().getAnger();
            Destroy(gameObject);
        }
    }
}
