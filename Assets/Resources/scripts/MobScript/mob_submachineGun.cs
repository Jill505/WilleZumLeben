using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_submachineGun : MobBase
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

    void FixedUpdate()
    {
        Vector3 direction = John.position - transform.position; //得到兩個物件在 x, y, z 軸上各自的距離差
        
        // 計算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer =Vector2.Distance(John.position , transform.position);

        if (distanceFromPlayer <lineOfDetect && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position,John.position,speed*Time.deltaTime);
            // 設置槍的本地旋轉，使槍對著目標玩家
            rb.rotation = angle;
            while(nextFireTime <Time.time)
            {
            StartCoroutine(ShootBullets());
            nextFireTime = Time.time + fireRate;
            }
        }
        else if(distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            StartCoroutine(ShootBullets());
            nextFireTime = Time.time + fireRate;
        }
        else if(distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angle;
        }
    }
    private IEnumerator ShootBullets()
{
    int bulletCount = 3; // 要連續發射的子彈數量
    float interval = 0.1f; // 每顆子彈之間的時間間隔

    for (int i = 0; i < bulletCount; i++)
    {
        // 生成子彈
        Instantiate(bullet, barrel.position, barrel.rotation);

        // 等待間隔時間
        yield return new WaitForSeconds(interval);
    }
}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfDetect);
        Gizmos.DrawWireSphere(transform.position , shootingRange);
    }
}
