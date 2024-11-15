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

    public float recoli = 0.5f;//0~1 0=完全停止 1=完全無後座力
    public float recoliRememberMaxSpeed = 7f;
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
        recoliRememberMaxSpeed = johnMaxmentSpeed;
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
        if (!Ycast && johnYspeed != 0 )//沒有按按鍵 Y減速
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
                Debug.Log("Y歸零");
            }
        }
        if (!Xcast && johnXspeed != 0)//沒有按按鍵 X減速
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
                Debug.Log("X歸零");
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

        //角色朝向滑鼠位置
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

            //後座力

            //float hitDiraction = Mathf.Atan2(john.transform.position.y - collision.gameObject.transform.position.y, john.transform.position.x - collision.gameObject.transform.position.x) * Mathf.Rad2Deg;
            float backPushAngle = Mathf.Atan2(gameObject.transform.position.y - worldMousePosition.y, gameObject.transform.position.x - worldMousePosition.x) * Mathf.Rad2Deg;
            Debug.Log("後座力角度：" + backPushAngle);

            //camera offest
            float xCom = Mathf.Cos(backPushAngle);
            float yCom = Mathf.Sin(backPushAngle);

            float sum = Mathf.Abs(xCom) + Mathf.Abs(yCom);

            xCom /= sum;
            yCom /= sum;

            Debug.Log("後座力 X分量：" + xCom);
            Debug.Log("後座力 Y分量" + yCom);

            Vector2 backForce = new Vector2(xCom * recoli, yCom * recoli);

            rb2d.velocity  +=backForce;
            Debug.Log(backForce);

            StartCoroutine(recoilCoroutine(backPushAngle));

        }
    }

    IEnumerator recoilCoroutine(float recoilDiraction)
    {
        johnMaxmentSpeed = johnMaxmentSpeed * recoli;
        yield return new WaitForSeconds(0.12f);

        johnMaxmentSpeed = recoliRememberMaxSpeed;

        yield return null;
    }
}
