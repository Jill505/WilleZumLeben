using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Object : MonoBehaviour
{
    public Level_Core level_Core;
    public string nextLevelName = "lobby";

    private void Awake()
    {
        if (level_Core == null)
        {
            level_Core = GameObject.Find("Level_Core").GetComponent<Level_Core>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "John")
            {
                if (nextLevelName == "")
                {
                    Debug.Log("防呆是有極限的 最多幫到這了 場景名稱是空白的哥哥");
                }
                else
                {
                    Debug.Log("載入場景：" + nextLevelName);
                    Debug.Log("如果出bug 要不場景沒有丟到build setting裡面 要不名字寫錯 注意大小寫和空白鍵有無");
                }
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "John")
            {
                if (nextLevelName == "")
                {
                    Debug.Log("防呆是有極限的 最多幫到這了 場景名稱是空白的哥哥");
                }
                else
                {
                    Debug.Log("載入場景：" + nextLevelName);
                    Debug.Log("如果出bug 要不場景沒有丟到build setting裡面 要不名字寫錯 注意大小寫和空白鍵有無");
                    level_Core.LoadNextScene(nextLevelName);
                    //SceneManager.LoadScene(nextLevelName);
                }
            }
        }
    }
}
