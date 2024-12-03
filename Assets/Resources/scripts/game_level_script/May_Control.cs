using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class May_Control : MonoBehaviour
{
    public Level_Core level_Core;

    public Vector2 targetPos;
    public Rigidbody2D rb2d;

    public bool mayControlalbe = true;

    public bool moveMissionActing = false;

    [Range(0.5f,20f)]public float maySpeed = 8;
    public float mayASpeed = 0.4f;
    public float mayDeASpeed = 1f;
    public Vector2 mayVectorSpeed;
    public float mayEndSpeed = 0.3f;

    public GameObject johnObject;

    public float fallowRadis = 2f;
    public bool isTracking;

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
            //soloTargetPos();
            following();
        }
        else
        {

        }
    }

    public void soloTargetPos()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //設定目標位置
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPos = worldMousePosition;

            moveMissionActing = true;

            //生成一個小動畫
        }
        
        if (targetPos == (Vector2)gameObject.transform.position)
        {
            //減速 不要直接停止
            moveMissionActing = false;
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            //character move to target position tracking
            //Vector2 theVec = (Vector2)gameObject.transform.position - targetPos;
            //float diraction = Mathf.Atan2(theVec.y,theVec.x);
            //rb2d.velocity = targetPos * maySpeed;
        }

        if (moveMissionActing)
        {
            //Vector2 theVec = (Vector2)gameObject.transform.position - targetPos;
            //float diraction = Mathf.Atan2(theVec.y, theVec.x);
            //rb2d.velocity = targetPos * maySpeed;
        }
    }

    public void dulControl()
    {

    }

    public void following()
    {
        // 計算方向單位向量
        Vector2 direction = (johnObject.transform.position - gameObject.transform.position).normalized;

        // 檢查距離是否大於跟隨半徑
        if (Vector2.Distance(gameObject.transform.position, johnObject.transform.position) > fallowRadis && isTracking)
        {
            // 加速
            if (Mathf.Abs(mayVectorSpeed.x) < maySpeed)
            {
                mayVectorSpeed.x += direction.x * mayASpeed * Time.deltaTime;
            }
            if (Mathf.Abs(mayVectorSpeed.y) < maySpeed)
            {
                mayVectorSpeed.y += direction.y * mayASpeed * Time.deltaTime;
            }

            // 設置剛體速度
            rb2d.velocity = mayVectorSpeed;
        }
        else
        {
            // 減速
            if (Mathf.Abs(mayVectorSpeed.x) > mayEndSpeed)
            {
                mayVectorSpeed.x -= Mathf.Sign(mayVectorSpeed.x) * mayDeASpeed * Time.deltaTime;
                if (Mathf.Abs(mayVectorSpeed.x) < mayEndSpeed)
                {
                    mayVectorSpeed.x = 0; // 停止
                }
            }
            else
            {
                mayVectorSpeed.x = 0;
            }

            if (Mathf.Abs(mayVectorSpeed.y) > mayEndSpeed)
            {
                mayVectorSpeed.y -= Mathf.Sign(mayVectorSpeed.y) * mayDeASpeed * Time.deltaTime;
                if (Mathf.Abs(mayVectorSpeed.y) < mayEndSpeed)
                {
                    mayVectorSpeed.y = 0; // 停止
                }
            }
            else
            {
                mayVectorSpeed.y = 0;
            }

            // 設置剛體速度
            rb2d.velocity = mayVectorSpeed;
        }





        /*
        float deg = Mathf.Atan2(gameObject.transform.position.y - johnObject.transform.position.y, gameObject.transform.position.x - johnObject.transform.position.x) * Mathf.Rad2Deg;

        float xCom = Mathf.Sin(deg);
        float yCom = Mathf.Cos(deg);

        float sum = Mathf.Abs(xCom + yCom);

        xCom = xCom / sum;
        yCom = yCom / sum;
        //二分量

        if (Vector2.Distance(gameObject.transform.position, johnObject.transform.position) > fallowRadis && isTracking == true)
        {
            if (Mathf.Abs(mayVectorSpeed.x) < maySpeed)
            {
                mayVectorSpeed.x += xCom* mayASpeed * Time.deltaTime;
            }
            if (Mathf.Abs(mayVectorSpeed.y) < maySpeed)
            {
                mayVectorSpeed.y += yCom * mayASpeed * Time.deltaTime;
            }

            rb2d.velocity = mayVectorSpeed;
        }
        else
        {
            //Speed decrease
            if (Mathf.Abs(mayVectorSpeed.x) > mayEndSpeed)
            {
                if (mayVectorSpeed.x > 0)
                {
                    // - 
                    mayVectorSpeed.x -= mayDeASpeed * Time.deltaTime;
                }
                else
                {
                    // + 
                    mayVectorSpeed.x += mayDeASpeed * Time.deltaTime;
                }
            }
            else
            {
                mayVectorSpeed.x = 0;
            }

            if (Mathf.Abs(mayVectorSpeed.y) > mayEndSpeed)
            {
                if (mayVectorSpeed.y > 0)
                {
                    // - 
                    mayVectorSpeed.y -= mayDeASpeed * Time.deltaTime;
                }
                else
                {
                    // + 
                    mayVectorSpeed.y += mayDeASpeed * Time.deltaTime;
                }
            }
            else
            {
                mayVectorSpeed.y = 0;
            }

            rb2d.velocity = mayVectorSpeed;
        }*/
    }
}
