using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class john_bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float bulletSpeed;
    public float bulletDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,8f);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = transform.right *-1 * bulletSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            //�R��mob �åBmob���`
            //Ĳ�o�ʵe �p���̨��רò��͸I���ʵe
            collision.gameObject.GetComponent<MobBase>().GetInjured(bulletDamage);

            float hitDiraction = Mathf.Atan2(gameObject.transform.position.y - collision.gameObject.transform.position.y, gameObject.transform.position.x - collision.gameObject.transform.position.x) * Mathf.Rad2Deg;

            collision.gameObject.GetComponent<MobBase>().deadFromDiraction = 1f;
        }

        if (collision.gameObject.tag == "Wall")
        {
            //�l�u�}�a
        }
        
        if (collision.gameObject.tag == "")
        {

        }
    }

    public void damageTarget()
    {

    }
}
