using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Core : MonoBehaviour
{

    public DialogContextObject loadingDialogContext;

    //人物立繪
    public Image DefultCharacterSprite;




    //===============================================


    public Text showText; //顯示的Text
    public string[] hanas; //對話劇本，載入時將其替換為任意字串陣列即可改變
    public int loadingTextNum = 0; //正在載入的行數
    public int hanasNum; 
    Coroutine coroutine; //Debug協程

    [Range(0.01f, 1.5f)] public float wordSpeed = 0.1f; //顯示速率

    public bool isClog = false; //正在播放與否

    //===============================================
    public void loadDialogContext(DialogContextObject dialogs)
    {
        hanas = dialogs.dialog;
    }

    public void startDialog()
    {
        if (loadingDialogContext != null)
        {




            loadingTextNum = -1;
            hanasNum = hanas.Length;
        }
        else
        {
            Debug.Log("Ankhra Error 001：沒有載入對話物件Dialog Object");
        }
    }

    public void textJudge(string judgeString)
    {
        switch (judgeString)
        {
            case (""):
                Debug.Log("未偵測到自元內容 跳過此行");
                    break;

            case ("Comm/SoundEffectPlay/GunShot"):
                Debug.Log("生成槍響");
                break;

            case ("Comm/ShowPicture/John"):
                Debug.Log("立繪顯示 John");
                break;

            default:
                coroutine = StartCoroutine(judgeString);
                break;
        }
    }


    public void DialogEnd()
    {

    }












    //===============================================
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!isClog)
            {
                if (loadingTextNum + 1 < hanasNum)
                {
                    loadingTextNum++;

                    textJudge(hanas[loadingTextNum]);
                }
                else
                {
                    Debug.Log("超出選擇");
                    DialogEnd();
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
