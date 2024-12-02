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
            soloTargetPos();
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
        if (Vector2.Distance(gameObject.transform.position, johnObject.transform.position) > fallowRadis && isTracking == true)
        {
            //diraction分量取得 並朝該方向前進

        }
    }
}
