using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class john_bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float bulletSpeed;
    public float bulletDamage = 1;

    public float cameraOffestStrength;

    public GameObject john;

    // Start is called before the first frame update
    void Start()
    {
        if (john == null)
        {
            john = GameObject.Find("John");
        }
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
            //命中mob 並且mob死亡
            //觸發動畫 計算兩者角度並產生碰撞動畫
            collision.gameObject.GetComponent<MobBase>().GetInjured(bulletDamage);

            //float hitDiraction = Mathf.Atan2(john.transform.position.y - collision.gameObject.transform.position.y, john.transform.position.x - collision.gameObject.transform.position.x) * Mathf.Rad2Deg;
            float hitDiraction = Mathf.Atan2(collision.gameObject.transform.position.y -  john.transform.position.y, collision.gameObject.transform.position.x - john.transform.position.x) * Mathf.Rad2Deg;
            Debug.Log("擊中角度：" + hitDiraction);

            collision.gameObject.GetComponent<MobBase>().deadFromDiraction = hitDiraction;

            //camera offest
            GameObject theCamera = GameObject.Find("UICore");
            float xCom = Mathf.Cos(hitDiraction);
            float yCom = Mathf.Sin(hitDiraction);

            float sum = Mathf.Abs(xCom) + Mathf.Abs(yCom);

            xCom /= sum;
            yCom /= sum;

            Debug.Log("X分量：" +xCom);
            Debug.Log("Y分量" + yCom);

            theCamera.transform.position += new Vector3(yCom,xCom,0) * cameraOffestStrength;

            theCamera.GetComponent<UI_Core>().shake();
        }

        if (collision.gameObject.tag == "Wall")
        {
            //子彈破壞
        }
        
        if (collision.gameObject.tag == "")
        {

        }
    }

    public void damageTarget()
    {

    }
}
