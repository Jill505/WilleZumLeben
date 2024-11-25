using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_handGun : MobBase
{
    private Transform John;
   public float shootingRange;
   public float lineOfDetect;
   public float fireRate = 0.5f;
   public float nextFireTime;

   public GameObject bullet;
   public Transform barrel;
   private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
    }

    void Update()
    {
        Vector3 direction = John.position - transform.position; //得到兩個物件在 x, y, z 軸上各自的距離差
        
        // 計算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer =Vector2.Distance(John.position , transform.position);
        if (distanceFromPlayer <lineOfDetect && distanceFromPlayer>shootingRange )
        {
            transform.position = Vector2.MoveTowards(this.transform.position,John.position,speed*Time.deltaTime);
            // 設置槍的本地旋轉，使槍對著目標玩家
            rb.rotation = angle;
        }
        else if(distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            rb.rotation = angle;
            Instantiate(bullet,barrel.position,barrel.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfDetect);
        Gizmos.DrawWireSphere(transform.position , shootingRange);
    }
}
