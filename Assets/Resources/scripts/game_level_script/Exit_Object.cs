using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Object : MonoBehaviour
{
    public Level_Core level_Core;

    private void Awake()
    {
        if (level_Core == null)
        {
            level_Core = GameObject.Find("Level_Core").GetComponent<Level_Core>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "John")
            {
                //Load Next Level
            }
        }
    }
}
