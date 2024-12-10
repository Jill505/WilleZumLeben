using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    [Range(1,5)]public int health = 1;
    public float speed = 1f;

    public float deadFromDiraction;

    public bool isDead = false;
    

    // 定义一个 AnimationCurve 类型的变量，供 Inspector 使用
    public AnimationCurve timeCurve = AnimationCurve.Linear(0.5f, 1, 1, 1);

    // 影响时间流速的系数
    public float animationDuration = 1f;

    private float timer = 10f;
    private float alpha = 0.5f;

    public GameObject deadSoundEffect;
    public GameObject hitSoundEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Test

        //FunctionTest
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //timer = 0;
            Debug.Log("抽幀");
        }

        TimeCurver();
    }

    public virtual void GetInjured(int damage) //Dead
    {
        health -= damage;
        if (health > 0)
        {
            //survive
            Instantiate(hitSoundEffect);
            timer = 0.2f;

        }
        else
        {
            //dead and play special effect

            timer = 0;
            isDead = true;
            Dead();

        }
    }

    public void Dead()
    {
        Instantiate(deadSoundEffect);

                CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
                SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();
                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                Color color = currentRenderer.color;
                color.a = Mathf.Clamp01(alpha);
                currentRenderer.color = color;

                circleCollider2D.isTrigger = true;

                rb.velocity = Vector2.zero; 
                rb.angularVelocity = 0f;

                foreach (Transform child in transform)
                {
                    SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
                    Color childColor = childRenderer.color;
                    childColor.a = Mathf.Clamp01(alpha);
                    childRenderer.color = childColor;

                    rb.velocity = Vector2.zero; 
                    rb.angularVelocity = 0f;
                }
    }
    public virtual void slashDeadAnimation()
    {

    }
    public IEnumerator slashDeadCoroutine()
    {

        yield return null;
    }

    public void TimeCurver()
    {
        if (timer > 5)
        {
            //Debug.Log("防止一直卡在time scale <1裡面");
            //Time.timeScale = 1f;
        }

        // 文件自帶筆記=> 计时器控制动画进度，时间会被重置以循环播放
        //計時 控制動畫播放時間
        timer += Time.deltaTime;
        //if (timer > animationDuration)
        //{
        //    timer = 0f;
        //}

        // 文件自帶筆記=> 计算曲线进度（0 到 1 的范围内）
        //曲線播放進度
        float progress = timer / animationDuration;

        if (progress > 1)
        {
            Time.timeScale = 1f;
        }
        else
        {
            float timeScale = timeCurve.Evaluate(progress);
            Time.timeScale = timeScale;

        }
    }
}
