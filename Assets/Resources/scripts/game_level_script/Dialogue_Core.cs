using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Core : MonoBehaviour
{

    public DialogContextObject loadingDialogContext;

    //�H����ø
    public Image DefultCharacterSprite;




    //===============================================


    public Text showText; //��ܪ�Text
    public string[] hanas; //��ܼ@���A���J�ɱN����������N�r��}�C�Y�i����
    public int loadingTextNum = 0; //���b���J�����
    public int hanasNum; 
    Coroutine coroutine; //Debug��{

    [Range(0.01f, 1.5f)] public float wordSpeed = 0.1f; //��ܳt�v

    public bool isClog = false; //���b����P�_

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
            Debug.Log("Ankhra Error 001�G�S�����J��ܪ���Dialog Object");
        }
    }

    public void textJudge(string judgeString)
    {
        switch (judgeString)
        {
            case (""):
                Debug.Log("��������ۤ����e ���L����");
                    break;

            case ("Comm/SoundEffectPlay/GunShot"):
                Debug.Log("�ͦ��j�T");
                break;

            case ("Comm/ShowPicture/John"):
                Debug.Log("��ø��� John");
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
                    Debug.Log("�W�X���");
                    DialogEnd();
                }
            }
            else
            {
                Debug.Log("���ڰ��U�I");
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
        Debug.Log("��ܵ���");

        isClog = false;
    }
}
