using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool mayAiming = false;

    //caling variable
    public float mayDashForce = 15f;
    public float mayDirection;
    public float mayReadDashForce = 1f;
    public float mayDashConst = 1f;
    public float mayDashPointerExtendConst = 1f;
    public float mayDashPointerExtendMax = 2.2f;
    public float mayDashMaxForce = 15f;
    public GameObject mayPointer;
    public float mayAimingTimeScale = 0.8f;

    //Anger relative
    public float mayAngerMaxment = 5f;
    public float mayDashConsume = 5f;
    public float mayNowAnger = 5f;

    public Animator angerAnimator;


    private void Awake()
    {
        if (level_Core == null)
        {
            level_Core = GameObject.Find("LevelCore").GetComponent<Level_Core>();
        }
        if (angerAnimator == null)
        {
            angerAnimator = GameObject.Find("tape").GetComponent<Animator>();
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
            MaySoloAttack();
            AngerUISync();
        }
        else
        {

        }
    }

    public void soloTargetPos()
    {
        if (isTracking && mayControlalbe)
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
    }

    public void dulControl()
    {

    }

    public void following()
    {
        if (isTracking && mayControlalbe)
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

    public void MaySoloAttack()
    {
        if (mayControlalbe && mayNowAnger>=mayDashConsume)//可操作 而且怒氣大於消耗
        {
            if (Input.GetKey(KeyCode.Space))
            {
                mayAiming = true;
                isTracking = false;

                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                mayDirection = Mathf.Atan2(gameObject.transform.position.y - worldMousePosition.y, gameObject.transform.position.x - worldMousePosition.x) * Mathf.Rad2Deg;
                mayPointer.transform.rotation = Quaternion.Euler(0, 0, mayDirection);

                float forceRatio = Vector2.Distance(gameObject.transform.position, worldMousePosition);
                mayDashForce = forceRatio * mayDashConst;
                if (mayDashForce > mayDashMaxForce)
                {
                    mayDashForce = mayDashMaxForce;
                }

                mayPointer.SetActive(true);

                float mayExtend = forceRatio * mayDashPointerExtendConst;
                //Debug.Log("距離=" + forceRatio + " / 延展=" + mayExtend) ;
                if (mayExtend > mayDashPointerExtendMax)
                {
                    mayExtend = mayDashPointerExtendMax;
                }
                mayPointer.transform.localScale = new Vector3(mayExtend, 1,1);
                Time.timeScale = mayAimingTimeScale;
            }
            else
            {
                mayAiming = false;
                Time.timeScale = 1f;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //放開 並朝方向飛出去
                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 directionVector = (worldMousePosition - (Vector2)gameObject.transform.position).normalized;
                Debug.Log("Dash Vector: " + directionVector);
                Vector2 dashForceWithDirection = directionVector * mayDashForce;
                Debug.Log("Dash Force Vector: " + dashForceWithDirection);
                //rb2d.AddForce(dashForceWithDirection);
                rb2d.velocity += dashForceWithDirection;

                mayPointer.SetActive(false);
                Invoke("trackRec", 3f);//等待一段時間後自然回到john身邊
            }
        }
        if (mayAiming)
        {
            //cal force

        }
    }
    public void trackRec()
    {
        isTracking = true;
    }

    public void AngerUISync()
    {
        float AngerRate = 1f + (mayNowAnger / mayAngerMaxment);
        if (AngerRate < 1f) AngerRate = 1f;
        angerAnimator.speed = AngerRate;
    }
}
