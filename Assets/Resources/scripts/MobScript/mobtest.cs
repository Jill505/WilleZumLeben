using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobtest : MobBase
{
    private Transform John;
    public Transform barrel;
    public Rigidbody2D bullet;
    private Rigidbody2D rb;
    [Header ("Shoot")]
    public float bulletspeed = 500f;
    [Range(0.2f, 4f)] public float fireRate;
    float rotationTimer = 0f; 
    float requiredTime = 1f; 
    private float nextFireTime = 0f;
    public float recoilForce = 5f;     // 後座力大小
    public float recoilDuration = 0.5f;
    private bool isRecoiling = false;  
    private bool isOrbiting = false;  

    public float rotationRadius = 5f; // 旋轉半徑
    public float rotationSpeed = 2f; // 旋轉速度
    private float angle = 0f; // 當前角度

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
        Vector3 direction = John.position - transform.position; // 得到兩個物件在 x, y, z 軸上各自的距離差

        // 計算角度
        float angleToPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer = Vector2.Distance(John.position, transform.position);

        if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > shootingRange && !isOrbiting)
        {
            rb.rotation = angleToPlayer;
            InitializeOrbit();
            
            if (!isRecoiling && nextFireTime < Time.time)
            {
                rotationTimer += Time.deltaTime;

                if (rotationTimer >= requiredTime)
                {
                    Shoot();
                    rotationTimer = 0f; 
                }
            }
        }
        else if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > shootingRange && isOrbiting)
        {
            InitializeOrbit();
            rb.rotation = angleToPlayer;
            if (!isRecoiling && isOrbiting)
            {
                // 旋轉邊轉邊向主角前進
                OrbitPlayer();
            }
            
            if (!isRecoiling && nextFireTime < Time.time)
            {
                rotationTimer += Time.deltaTime;

                if (rotationTimer >= requiredTime)
                {
                    Shoot();
                    rotationTimer = 0f; 
                }
            }
        }
        else if (distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angleToPlayer;
            if (!isRecoiling && nextFireTime < Time.time)
            {
                Shoot();
            }
        }
        else
        {
            rb.angularVelocity = 0f;        
        }
    }

    void OrbitPlayer()
    {
        if (!isRecoiling)  // 後座力期間不更新位置
        {
            // 逐漸縮小旋轉半徑，讓敵人逐漸靠近玩家
            rotationRadius = Mathf.Max(rotationRadius - speed * Time.fixedDeltaTime, shootingRange);

            // 增長角度
            angle = Mathf.Repeat(angle + rotationSpeed * Time.fixedDeltaTime, Mathf.PI * 2);

            // 計算新的位置
            Vector2 newPosition = new Vector2(John.position.x + Mathf.Cos(angle) * rotationRadius,John.position.y + Mathf.Sin(angle) * rotationRadius);

            // 平滑移動敵人位置
            rb.MovePosition(newPosition);
        }

        // 讓敵人朝向玩家
        Vector3 direction = John.position - transform.position;
        float angleToPlayer = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angleToPlayer;
    }
    void InitializeOrbit()
    {
        // 計算敵人當前的角度（相對於玩家）
        Vector2 directionToPlayer = (transform.position - John.position).normalized;
        angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);  // 計算角度

        // 繞圈半徑保持敵人到玩家的當前距離
        rotationRadius = Vector2.Distance(transform.position, John.position);

        isOrbiting = true;  // 標記敵人已開始繞圈
    }
    

    void Shoot()
    {
        var spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.AddForce(barrel.up * bulletspeed);
        nextFireTime = Time.time + fireRate;

        Vector2 recoilDirection = (transform.position - barrel.position).normalized;
        isRecoiling = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;   
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        StartCoroutine(StopRecoilAfterDelay());
    }

    IEnumerator StopRecoilAfterDelay()
    {
        yield return new WaitForSeconds(recoilDuration);  // 等待一段時間
        isRecoiling = false;
        rb.velocity = Vector2.zero;  // 停止後座力影響，將速度設置為零
        

        Vector2 directionToPlayer = (transform.position - John.position).normalized;
        angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);  // 根據當前位置重新計算角度
        Vector2 correctedPosition = new Vector2(John.position.x + Mathf.Cos(angle) * rotationRadius,John.position.y + Mathf.Sin(angle) * rotationRadius);
        rb.position = correctedPosition;
        isOrbiting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfDetect);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}