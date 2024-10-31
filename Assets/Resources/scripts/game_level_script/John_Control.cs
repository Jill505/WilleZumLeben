using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class John_Control : MonoBehaviour
{
    public Level_Core level_Core;
    public Rigidbody2D rb2d;
    public GameObject johnBullet;
    public GameObject johnBarrelPosition;

    public bool johnControlAble = true;

    public GameObject pistolShootSoundEffect;


    [Range(4f,20f)]public float johnSpeed = 1.0f;
    [Range(2f, 15f)] public float johnMaxmentSpeed = 7f;
    [Range(0.001f, 2f)] public float johnSpeedEndNum=0.2f;
    [Range(0.1f, 20f)] public float johnSlowSpeed = 0.7f;
    [Range(1f, 20f)] public float johnOptSideSpeed = 2f;
    public float johnXspeed = 0f;
    public float johnYspeed = 0f;

    public float bulletNumber = 7;
    public float bulletNowNumber = 7;
    public float bulletSpeed;

    public float reloadTime = 0.8f;
    public float reloadNowTime = 0f;

    private void Awake()
    {
        if (level_Core == null)
        {
            level_Core = GameObject.Find("LevelCore").GetComponent<Level_Core>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (All_GameCore.OperatorMode == 0)//Solo
        {
            JohnSoloControl();
        }
        else
        {

        }
    }

    public void JohnSoloControl()
    {
        rb2d.velocity = Vector2.zero;

        bool Ycast = false;
        bool Xcast = false;

        if (Input.GetKey(KeyCode.W))
        {
            float velocityMutiply = 1;
            if (johnYspeed < 0)
            {
                velocityMutiply = johnOptSideSpeed;
            }

            johnYspeed += Time.deltaTime * johnSpeed * velocityMutiply;
            Ycast = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            float velocityMutiply = 1;
            if (johnYspeed > 0)
            {
                velocityMutiply = johnOptSideSpeed;
            }

            johnYspeed -= Time.deltaTime * johnSpeed;
            Ycast = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            float velocityMutiply = 1;
            if (johnXspeed < 0)
            {
                velocityMutiply = johnOptSideSpeed;
            }

            johnXspeed -= Time.deltaTime * johnSpeed * velocityMutiply;
            Xcast = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            float velocityMutiply = 1;
            if (johnXspeed > 0)
            {
                velocityMutiply = johnOptSideSpeed;
            }

            johnXspeed += Time.deltaTime * johnSpeed*velocityMutiply;
            Xcast = true;
        }
        if (!Ycast && johnYspeed != 0 )//¨S¦³«ö«öÁä Y´î³t
        {
             if (Mathf.Abs(johnYspeed) > johnSpeedEndNum)
            {
                if (johnYspeed > 0)
                {
                    johnYspeed -= johnSlowSpeed *Time.deltaTime;
                }
                else
                {
                    johnYspeed += johnSlowSpeed * Time.deltaTime;
                }
            }
             else
            {
                johnYspeed = 0;
                Debug.Log("YÂk¹s");
            }
        }
        if (!Xcast && johnXspeed != 0)//¨S¦³«ö«öÁä X´î³t
        {
            if (Mathf.Abs(johnXspeed) > johnSpeedEndNum)
            {
                if (johnXspeed > 0)
                {
                    johnXspeed -= johnSlowSpeed * Time.deltaTime;
                }
                else
                {
                    johnXspeed += johnSlowSpeed * Time.deltaTime;
                }
            }
            else
            {
                johnXspeed = 0;
                Debug.Log("XÂk¹s");
            }
        }



        if (Mathf.Abs(johnXspeed) > johnMaxmentSpeed)
        {
            if (johnXspeed > 0)
            {
                johnXspeed = johnMaxmentSpeed * 1;
            }
            else
            {
                johnXspeed = johnMaxmentSpeed * -1;
            }
        }

        if (Mathf.Abs(johnYspeed) > johnMaxmentSpeed)
        {
            if (johnYspeed > 0)
            {
                johnYspeed = johnMaxmentSpeed * 1;
            }
            else
            {
                johnYspeed = johnMaxmentSpeed * -1;
            }
        }

        rb2d.velocity = new Vector2(johnXspeed, johnYspeed);

        //¨¤¦â´Â¦V·Æ¹«¦ì¸m
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float facingDiraction = Mathf.Atan2(transform.position.y - worldMousePosition.y, transform.position.x - worldMousePosition.x); 

        //sync Diraction
        gameObject.transform.rotation = Quaternion.Euler(0, 0, facingDiraction * Mathf.Rad2Deg);

        if (bulletNowNumber > 0 && johnControlAble && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject theBulletSummoned = Instantiate(johnBullet, johnBarrelPosition.transform.position,Quaternion.Euler(0,0, facingDiraction * Mathf.Rad2Deg));
            //johnBullet.transform.rotation = Quaternion.Euler(0, 0, facingDiraction * Mathf.Rad2Deg);

            Instantiate(pistolShootSoundEffect);
        }
    }
}
