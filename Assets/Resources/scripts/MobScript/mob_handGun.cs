using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_handGun : MobBase
{
   private Transform John;
   private bool shotSound = false;
   public float shootingRange;
   public float hearingRange;
   public float lineOfDetect;
   [Range(0.2f,4f)] public float fireRate;
   public float nextFireTime;
   public float recoilForce = 5f;     // 後座力大小
   public float recoilDuration = 0.5f;
   private bool isRecoiling = false;  
   

   public GameObject bullet;
   public Transform barrel;
   private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        John = GameObject.FindGameObjectWithTag("John").transform;
        JohnTest.OnPlayerShot += hearing;
    }

    void Update()
    {
        Vector3 direction = John.position - transform.position; //得到兩個物件在 x, y, z 軸上各自的距離差
        
        // 計算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distanceFromPlayer =Vector2.Distance(John.position , transform.position);
        if (distanceFromPlayer < hearingRange && distanceFromPlayer > lineOfDetect && shotSound)
        {
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            }
            rb.rotation = angle;
        }
        else if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > shootingRange)
        {
            if (!isRecoiling)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            }
            rb.rotation = angle;
        }
        else if(distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            if (!isRecoiling)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }

        }
        else if(distanceFromPlayer <= shootingRange)
        {
            rb.rotation = angle;
        }
    }
    void hearing()
    {
        shotSound = true; 
    }

    void Shoot()
    {
        Instantiate(bullet,barrel.position,barrel.rotation);
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
        Gizmos.DrawWireSphere(transform.position, hearingRange );    
    }
}
