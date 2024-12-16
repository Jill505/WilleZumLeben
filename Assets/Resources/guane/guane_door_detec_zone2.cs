using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guane_door_detec_zone2 : MonoBehaviour
{
    guane_door mydoor;
    // Start is called before the first frame update
    void Start()
    {
        mydoor = gameObject.transform.parent.gameObject.GetComponent<guane_door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="John")
        {
            mydoor.isPlayerStaying = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="John")
        {
            mydoor.isPlayerStaying = false;
        }
    }
}
