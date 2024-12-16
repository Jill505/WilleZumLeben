using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class guane_door_detec_zone : MonoBehaviour
{
    door myDoor;
    
    // Start is called before the first frame update
    void Start()
    {
        myDoor = gameObject.transform.parent.gameObject.GetComponent<door>();
    }

    // Update is called once per    
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "John")
        {
            myDoor.isPlayerStaying = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "John")
        {
            myDoor.isPlayerStaying = false;
        }
    }

}
