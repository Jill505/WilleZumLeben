using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_handGun : MobBase
{
   private Transform John;
   public Transform barrel;
   public Rigidbody2D bullet;
   private Rigidbody2D rb;

   [Header ("Shoot")]
   public float bulletspeed = 500f;
   [Range(0.2f,4f)] public float fireRate;
   float rotationTimer = 0f; 
   float requiredTime = 1f; 
   private float nextFireTime = 0f;
   public float recoilForce = 5f;     
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  
   [Header ("Range")]
   public float shootingRange;
   public float lineOfDetect;
   
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
        
        if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > shootingRange)
        {
            rb.rotation = angle;
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            }
            if (!isRecoiling && nextFireTime <Time.time)
            {
                //forDebug
                rotationTimer += Time.deltaTime;
                
                if (rotationTimer >= requiredTime)
                {
                    Shoot();
                    rotationTimer = 0f; 
                }
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
    }
    
}
