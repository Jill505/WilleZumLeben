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
            MaySoloAttack();
        }
        else
        {

        }
    }

    public void soloTargetPos()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //�]�w�ؼЦ�m
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPos = worldMousePosition;

            moveMissionActing = true;

            //�ͦ��@�Ӥp�ʵe
        }
        
        if (targetPos == (Vector2)gameObject.transform.position)
        {
            //��t ���n��������
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
        // �p���V���V�q
        Vector2 direction = (johnObject.transform.position - gameObject.transform.position).normalized;

        // �ˬd�Z���O�_�j����H�b�|
        if (Vector2.Distance(gameObject.transform.position, johnObject.transform.position) > fallowRadis && isTracking)
        {
            // �[�t
            if (Mathf.Abs(mayVectorSpeed.x) < maySpeed)
            {
                mayVectorSpeed.x += direction.x * mayASpeed * Time.deltaTime;
            }
            if (Mathf.Abs(mayVectorSpeed.y) < maySpeed)
            {
                mayVectorSpeed.y += direction.y * mayASpeed * Time.deltaTime;
            }

            // �]�m����t��
            rb2d.velocity = mayVectorSpeed;
        }
        else
        {
            // ��t
            if (Mathf.Abs(mayVectorSpeed.x) > mayEndSpeed)
            {
                mayVectorSpeed.x -= Mathf.Sign(mayVectorSpeed.x) * mayDeASpeed * Time.deltaTime;
                if (Mathf.Abs(mayVectorSpeed.x) < mayEndSpeed)
                {
                    mayVectorSpeed.x = 0; // ����
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
                    mayVectorSpeed.y = 0; // ����
                }
            }
            else
            {
                mayVectorSpeed.y = 0;
            }

            // �]�m����t��
            rb2d.velocity = mayVectorSpeed;
        }

        /*
        float deg = Mathf.Atan2(gameObject.transform.position.y - johnObject.transform.position.y, gameObject.transform.position.x - johnObject.transform.position.x) * Mathf.Rad2Deg;

        float xCom = Mathf.Sin(deg);
        float yCom = Mathf.Cos(deg);

        float sum = Mathf.Abs(xCom + yCom);

        xCom = xCom / sum;
        yCom = yCom / sum;
        //�G���q

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

    public void MaySoloAttack()
    {
        if (mayControlalbe)//�i�ާ@ �ӥB���j�����
        {
            if (Input.GetKey(KeyCode.Space))
            {
                mayAiming = true;

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
                Debug.Log("�Z��=" + forceRatio + " / ���i=" + mayExtend) ;
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
                //��} �ô¤�V���X�h

                mayPointer.SetActive(false);
                Invoke("trackRec", 0.58f);//���ݤ@�q�ɶ���۵M�^��john����
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
}
