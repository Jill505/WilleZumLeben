using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class May_DamageZone : MonoBehaviour
{
    public May_Control may;
    // Start is called before the first frame update
    void Start()
    {
        if (may == null)
        {
            may = gameObject.transform.parent.gameObject.GetComponent<May_Control>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (may.isAttacking)
        {
            if (collision.gameObject.tag == "Mob")
            {
                //Make damage by may attack
                collision.gameObject.GetComponent<MobBase>().GetInjured(5);
            }
            if (collision.gameObject.tag == "BreakBoard")
            {
                //break it
            }

        }
    }
}
