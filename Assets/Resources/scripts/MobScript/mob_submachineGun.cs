using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_submachineGun : MobBase
{
   public Transform barrel;
   private Transform John;
   public Rigidbody2D bullet;
   private Rigidbody2D rb;
   private bool shotSound = false;
   [Header ("Shoot")]
   public float bulletspeed = 500f;
   public float bulletCount = 10; // 要連續發射的子彈數量
   public float interval = 0.1f; // 每顆子彈之間的時間間隔
   [Range(0.4f,5f)] public float fireRate;
   float rotationTimer = 0f; 
   float requiredTime = 1f; 
   private float nextFireTime;
   public float recoilForce = 5f;     // 後座力大小
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  

   [Header ("Range")]
   public float shootingRange;
   public float hearingRange;
   public float lineOfDetect;



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
                while (nextFireTime <Time.time)
                 {
                    //forDebug
                    rotationTimer += Time.deltaTime;
                
                    if (rotationTimer >= requiredTime)
                    {
                        StartCoroutine(ShootBullets());
                        rotationTimer = 0f; 
                        nextFireTime = Time.time + fireRate; 
                    }
                 }
                
            }           
        }
        else if (distanceFromPlayer <lineOfDetect && distanceFromPlayer>shootingRange)
        {
            rb.rotation = angle;
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
                 while (nextFireTime <Time.time)
                 {
                    //forDebug
                    rotationTimer += Time.deltaTime;
                
                    if (rotationTimer >= requiredTime)
                    {
                        StartCoroutine(ShootBullets());
                        rotationTimer = 0f; 
                        nextFireTime = Time.time + fireRate; 
                    }
                 }
            }
        }
        else if(distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angle;
            while (nextFireTime <Time.time)
                 {
                    //forDebug
                    rotationTimer += Time.deltaTime;
                
                    if (rotationTimer >= requiredTime)
                    {
                        StartCoroutine(ShootBullets());
                        rotationTimer = 0f; 
                        nextFireTime = Time.time + fireRate; 
                    }
                 }
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
    
    private IEnumerator ShootBullets()
{
    for (int i = 0; i < bulletCount; i++)
    {
        // 生成子彈
        var spawnedBullet =Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.AddForce(barrel.up * bulletspeed);

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
