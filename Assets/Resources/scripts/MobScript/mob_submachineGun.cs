using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_submachineGun : MobBase
{
   public Transform barrel;
   private Transform John;
   public Rigidbody2D bullet;
   private Rigidbody2D rb;

   [Header ("Shoot")]
   public float bulletspeed = 500f;
   public float bulletCount = 10; // 要連續發射的子彈數量
   public float interval = 0.1f; // 每顆子彈之間的時間間隔
   [Range(0.4f,5f)] public float fireRate;
   private float nextFireTime;
   private int currentShotIndex = 0; 
   public float[] shotAngles = { -15f, 15f};

   [Header ("Recoil")]
   public float recoilForce = 5f;    
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  

   [Header ("Range")]
   public float chaseRange;
   public float stopRange;

   #region Debug
   private float rotationTimer = 0f; 
   private float requiredTime = 1f;    
   
   #endregion



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
    }

    void FixedUpdate()
    {
        if(isDead) 
        {
            return;
        }
        Move();
    }
    
    void Move()
    {
        Vector3 direction = John.position - transform.position; //得到兩個物件在 x, y, z 軸上各自的距離差
        
        // 計算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer =Vector2.Distance(John.position , transform.position);

         if (distanceFromPlayer < chaseRange && distanceFromPlayer > stopRange)
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
        else if(distanceFromPlayer <= stopRange)
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
    
    private IEnumerator ShootBullets()
    {        
        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = shotAngles[currentShotIndex % shotAngles.Length]; // 循環使用方向
            Quaternion offsetRotation = Quaternion.Euler(0, 0, currentAngle);
            var spawnedBullet =Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.AddForce(offsetRotation * barrel.up * bulletspeed);
            Recoil();
            currentShotIndex++;
            yield return new WaitForSeconds(interval);
        }
    }

    void Recoil()
    {
        Vector2 recoilDirection = (transform.position - barrel.position).normalized;
        isRecoiling = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        StartCoroutine(StopRecoilAfterDelay());
    }
    IEnumerator StopRecoilAfterDelay()
    {
        yield return new WaitForSeconds(recoilDuration);  
        isRecoiling = false;
        rb.velocity = Vector2.zero; 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , chaseRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position , stopRange);

    }
}
