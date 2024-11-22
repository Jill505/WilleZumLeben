using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Core : MonoBehaviour
{
    public Text showText; //顯示的Text
    public string[] hanas; //對話劇本，載入時將其替換為任意字串陣列即可改變
    public int loadingTextNum = 0; //正在載入的行數
    public int hanasNum; 
    Coroutine coroutine; //Debug協程

    [Range(0.01f, 1.5f)] public float wordSpeed = 0.1f; //顯示速率

    public bool isClog = false; //正在播放與否

    // Start is called before the first frame update
    void Start()
    {
        loadingTextNum = -1;
        hanasNum = hanas.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isClog)
            {
                if (loadingTextNum + 1 < hanasNum)
                {
                    loadingTextNum++;
                    coroutine = StartCoroutine(loadTexter(hanas[loadingTextNum]));
                }
                else
                {
                    Debug.Log("超出選擇");
                }
            }
            else
            {
                Debug.Log("給我停下！");
                StopCoroutine(coroutine);
                showText.text = hanas[loadingTextNum];
                isClog = false;
            }
        }
    }

    public void startDialog()
    {
        loadingTextNum = -1;
        hanasNum = hanas.Length;
    }

    IEnumerator loadTexter(string theText)
    {
        isClog = true;

        string swapStr = "";
        int loopTimes = theText.Length;
        for (int i = 0; i < loopTimes; i++)
        {
            swapStr += theText[i];
            showText.text = swapStr;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log("顯示結束");

        isClog = false;
    }
}
