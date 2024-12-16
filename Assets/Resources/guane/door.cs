using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Animator doorAnimator;

    public GameObject doorL;
    public GameObject doorR;

    public bool isPlayerStaying = false;
    public float leaveNum = 0f;
    public float basicNum = 1f;
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
            doorAnimator.SetBool("open", false);
        }

        doorL.transform.position = new Vector2(gameObject.transform.position.x + basicNum + leaveNum, gameObject.transform.position.y);
        doorR.transform.position = new Vector2(gameObject.transform.position.x - basicNum - leaveNum, gameObject.transform.position.y);
    }
}
