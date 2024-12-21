using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_handGun : MobBase
{
   private Transform John;
   public Transform barrel;
   public Rigidbody2D bullet;
   private Rigidbody2D rb;
   private bool shotSound = false;
   private int i = 0;
   public float bulletspeed = 500f;
   public float shootingRange;
   public float hearingRange;
   public float lineOfDetect;
   [Range(0.2f,4f)] public float fireRate;
   private float nextFireTime = 0f;
   public float recoilForce = 5f;     // 後座力大小
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
        JohnTest.OnPlayerShot += hearing;
    }

    void Update()
    {
        if(isDead) 
        {
        while(i<=1)
        {
            Dead();
            i++;
        }
        return;
        }

        Vector3 direction = John.position - transform.position; //得到兩個物件在 x, y, z 軸上各自的距離差
        
        // 計算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer =Vector2.Distance(John.position , transform.position);
        if (distanceFromPlayer < hearingRange && distanceFromPlayer > lineOfDetect && shotSound)
        {
            rb.rotation = angle;
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            }
        }
        else if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > shootingRange)
        {
            rb.rotation = angle;
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            }
        }
        else if(distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angle;
            if (!isRecoiling && nextFireTime <Time.time)
            {
                Shoot();
            }
        }
        else
        {
            //for debug
            rb.angularVelocity = 0f;        
        }
    }
    void hearing()
    {
        shotSound = true; 
    }

    void Shoot()
    {
        var spawnedBullet =Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.AddForce(barrel.up * bulletspeed);
        nextFireTime = Time.time + fireRate;

        
        Vector2 recoilDirection = (transform.position - barrel.position).normalized;
        isRecoiling = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        StartCoroutine(StopRecoilAfterDelay());
    }
    IEnumerator StopRecoilAfterDelay()
    {
        yield return new WaitForSeconds(recoilDuration);  // 等待一段時間
        isRecoiling = false;
        rb.velocity = Vector2.zero;  // 停止後座力影響，將速度設置為零
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfDetect);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position , shootingRange);

        Gizmos.color = Color.red; // 聽力範圍用紅色
        Gizmos.DrawWireSphere(transform.position, hearingRange );    
    }
}
