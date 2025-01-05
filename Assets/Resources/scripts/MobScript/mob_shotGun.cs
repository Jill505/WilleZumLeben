using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_shotGun : MobBase
{
   private Transform John;
    public Transform barrel; 
    public Rigidbody2D bullet; 

    [Header("Shoot")]
    public float bulletSpeed = 500f;
    public int bulletCount = 5; 
    public float spreadAngle = 40f; 
    [Range(0.4f, 5f)] public float fireRate = 1f; 
    private float nextFireTime;

    [Header("Recoil")]
    public float recoilForce = 5f;
    public float recoilDuration = 0.5f;
    private bool isRecoiling = false;

    [Header("Range")]
    public float chaseRange = 10f;
    public float stopRange = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
    }

    void FixedUpdate()
    {
        if (isDead)
        { 
            return;
        }
        Move();
    }

    void Move()
    {
        Vector3 direction = John.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer = Vector2.Distance(John.position, transform.position);

        if (distanceFromPlayer < chaseRange && distanceFromPlayer > stopRange)
        {
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);

                if (nextFireTime < Time.time)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
            rb.rotation = angle;
        }
        else if (distanceFromPlayer <= stopRange && nextFireTime < Time.time)
        {
            if (!isRecoiling)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            rb.rotation = angle;
        }
        else if (distanceFromPlayer <= stopRange)
        {
            rb.rotation = angle;
        }
        else
        {
            rb.angularVelocity = 0f;
        }
    }

    void Shoot()
    {
        float startAngle = -spreadAngle / 2; // 起始角度
        float angleStep = spreadAngle / (bulletCount - 1); // 每顆子彈之間的角度

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z + currentAngle);

            var spawnedBullet = Instantiate(bullet, barrel.position, rotation);
            spawnedBullet.AddForce(rotation * Vector2.up * bulletSpeed);
        }
        Recoil(); 
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

        if (Vector2.Distance(John.position, transform.position) <= stopRange)
        {
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
