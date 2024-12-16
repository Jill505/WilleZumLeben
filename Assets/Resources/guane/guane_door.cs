using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guane_door : MonoBehaviour
{
    public Animator doorAnimator;

    public GameObject doorUP;
    public GameObject doorDOWN;

    public bool isPlayerStaying = false;
    public float leaveNum = 0f;
    public float basicNum = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerStaying)
        {
            doorAnimator.SetBool("open",true);
        }
        else
        {
            doorAnimator.SetBool("open",false);
        }

        doorUP.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + basicNum + leaveNum);
        doorDOWN.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y - basicNum - leaveNum);
    }
}
