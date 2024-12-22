using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_shotGun : MobBase
{
   private Transform John;
   public Transform barrel;
   public Transform barrel80;
   public Transform barrel70;   
   public Transform barrel110;
   public Transform barrel100;

   public Rigidbody2D bullet;
   private Rigidbody2D rb;
   private bool shotSound = false;
   private int i = 0;

   public float bulletspeed = 500f;
   public float shootingRange;
   public float hearingRange;
   public float lineOfDetect;

   public float bulletCount = 2; // 要連續發射的子彈數量
   public float interval = 1f; // 每顆子彈之間的時間間隔
   [Range(0.4f,5f)] public float fireRate;
   public float nextFireTime;
   public float recoilForce = 5f;     // 後座力大小
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
        JohnTest.OnPlayerShot += hearing;                                        //商討一下需要在主John那加入些委託
    }

    void FixedUpdate()
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
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
                while(nextFireTime <Time.time)
               {
                //Shoot();
                StartCoroutine(ShootBullets());
                nextFireTime = Time.time + fireRate;
               }
            }
            rb.rotation = angle;
           
        }
        else if (distanceFromPlayer <lineOfDetect && distanceFromPlayer>shootingRange)
        {
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
                 while(nextFireTime <Time.time)
                {
                  //oneShoot();
                  StartCoroutine(ShootBullets());
                  nextFireTime = Time.time + fireRate;
                }
            }
            rb.rotation = angle;
        }
        else if(distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            if (!isRecoiling)
            {
              //oneShoot();
              StartCoroutine(ShootBullets());
              nextFireTime = Time.time + fireRate;            
            }
        }
        else if(distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angle;
        }
        else
        {
            rb.angularVelocity = 0f;
        }
    }
    void hearing()
    {
        shotSound = true; 
    }
    
    //shootOnce
    /*private void Shoot()
    {
        var spawnedBullet =Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.AddForce(barrel.up * bulletspeed);
        var spawnedBullet2 =Instantiate(bullet, barrel80.position, barrel80.rotation);
        spawnedBullet2.AddForce(barrel80.up * bulletspeed);
        var spawnedBullet3 =Instantiate(bullet, barrel70.position, barrel70.rotation);
        spawnedBullet3.AddForce(barrel70.up * bulletspeed);
        var spawnedBullet4 =Instantiate(bullet, barrel110.position, barrel110.rotation);
        spawnedBullet4.AddForce(barrel110.up * bulletspeed);
        var spawnedBullet5 =Instantiate(bullet, barrel100.position, barrel100.rotation);
        spawnedBullet5.AddForce(barrel100.up * bulletspeed);
                

        Vector2 recoilDirection = (transform.position - barrel.position).normalized;
        isRecoiling = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        StartCoroutine(StopRecoilAfterDelay());
        
    }*/
    private IEnumerator ShootBullets()
{
    for (int i = 0; i < bulletCount; i++)
    {
        // 生成子彈
        var spawnedBullet =Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.AddForce(barrel.up * bulletspeed);
        var spawnedBullet2 =Instantiate(bullet, barrel80.position, barrel80.rotation);
        spawnedBullet2.AddForce(barrel80.up * bulletspeed);
        var spawnedBullet3 =Instantiate(bullet, barrel70.position, barrel70.rotation);
        spawnedBullet3.AddForce(barrel70.up * bulletspeed);
        var spawnedBullet4 =Instantiate(bullet, barrel110.position, barrel110.rotation);
        spawnedBullet4.AddForce(barrel110.up * bulletspeed);
        var spawnedBullet5 =Instantiate(bullet, barrel100.position, barrel100.rotation);
        spawnedBullet5.AddForce(barrel100.up * bulletspeed);

        Vector2 recoilDirection = (transform.position - barrel.position).normalized;
        isRecoiling = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        StartCoroutine(StopRecoilAfterDelay());

        // 等待間隔時間
        yield return new WaitForSeconds(interval);
    }
}

   IEnumerator StopRecoilAfterDelay()
    {
        yield return new WaitForSeconds(recoilDuration);  // 等待一段時間
        isRecoiling = false;

        // 停止後座力影響，將速度設置為零
        rb.velocity = Vector2.zero;  // 停止敵人的移動
        if (Vector2.Distance(John.position, transform.position) <= shootingRange)
        {
            // 繼續射擊邏輯
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            // 恢復正常的追擊速度
            transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfDetect);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position , shootingRange);

        Gizmos.color = Color.red; // 聽力範圍用紅色
        Gizmos.DrawWireSphere(transform.position, hearingRange );    }
}
