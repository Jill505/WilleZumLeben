using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_shieldBearer : MobBase
{
   private Transform John;
   public Transform Shield;
   private Rigidbody2D rb;

   [Header ("Attack")]
   public float BashTime = 5f;
   private float chargeTimer = 0f; 
   public float chargeTime = 1f; 

   [Header ("Range")]
   public float AttackRange;
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
        
        if (distanceFromPlayer < lineOfDetect && distanceFromPlayer > AttackRange)
        {
            rb.rotation = angle;
            transform.position = Vector2.MoveTowards(this.transform.position, John.position, speed * Time.deltaTime);
            chargeTimer = 0f;
        }
        else if(distanceFromPlayer <= AttackRange)
        {
            rb.rotation = angle;
            chargeTimer += Time.deltaTime;
                
                if (chargeTimer >= chargeTime)
                {
                    Bash();
                    chargeTimer = 0f; 
                }
        }
        else
        {
            //for debug
            rb.angularVelocity = 0f;        
        }
    }
    void Bash()
    {
        rb.AddForce(Shield.right * 20f,ForceMode2D.Impulse);
        StartCoroutine(Stop());
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(BashTime);
        rb.velocity = Vector2.zero; 
        rb.angularVelocity = 0f;  
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfDetect);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position , AttackRange);
    }
}
