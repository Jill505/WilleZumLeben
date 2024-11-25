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

    public Image backgroundImage; //對話框背景 漸變圖樣應該就好
    public Text showText; //顯示的Text 
    public Text sayingName;//正在說話的腳色
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
            //開啟相關物件
            sayingName.gameObject.SetActive(true);
            showText.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);

            loadingTextNum = -1;
            hanasNum = hanas.Length;
            //自動開始第一句 不要這個設置的話可以關掉
            loadNextHanas();
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
                loadNextHanas();
                    break;

            case ("Comm/DialogOpen"):
                Debug.Log("開啟對話框");
                loadNextHanas();
                DialogEnd();
                break;

            case ("Comm/DialogEnd"):
                Debug.Log("關閉對話框");
                loadNextHanas();
                DialogEnd();
                break;

            case ("Comm/ClugOpen"):
                GameObject.Find("John").GetComponent<John_Control>().johnControlAble = false;

                GameObject.Find("John").GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                Debug.Log("暫停允許玩家操作");
                loadNextHanas();
                break;

            case ("Comm/ClugClose"):
                GameObject.Find("John").GetComponent<John_Control>().johnControlAble = true;
                Debug.Log("開放玩家允許操作");
                loadNextHanas();
                break;

            case ("Comm/John Speaking"):
                Debug.Log("說話中的角色切換為John");
                loadNextHanas();
                sayingName.text = "John 大壯";
                break;

            case ("Comm/May Speaking"):
                Debug.Log("說話中的角色切換為May");
                loadNextHanas();
                sayingName.text = "May 小梅";
                break;

            case ("Comm/Narrowtive Speaking"):
                Debug.Log("說話中的角色切換為旁白");
                loadNextHanas();
                sayingName.text = "旁白";
                break;

            case ("Comm/SoundEffectPlay/GunShot"):
                Debug.Log("生成槍響");
                loadNextHanas();
                break;

            case ("Comm/ShowPicture/John"):
                Debug.Log("立繪顯示 John");
                loadNextHanas();
                break;

            default:
                coroutine = StartCoroutine(loadTexter(judgeString));
                break;
        }
    }


    public void DialogEnd()
    {
        //關閉相關物件
        sayingName.gameObject.SetActive(false);
        showText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);

    }


    //===============================================
    void Start()
    {
        //startDialog();
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

    public void loadNextHanas()
    {
        if (loadingTextNum + 1 < hanasNum)
        {
            loadingTextNum++;

            textJudge(hanas[loadingTextNum]);
        }
    }
}
