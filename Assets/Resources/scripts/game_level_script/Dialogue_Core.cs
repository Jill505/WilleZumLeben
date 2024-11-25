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

    public Image backgroundImage; //��ܮحI�� ���ܹϼ����ӴN�n
    public Text showText; //��ܪ�Text 
    public Text sayingName;//���b���ܪ��}��
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
            //�}�Ҭ�������
            sayingName.gameObject.SetActive(true);
            showText.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);

            loadingTextNum = -1;
            hanasNum = hanas.Length;
            //�۰ʶ}�l�Ĥ@�y ���n�o�ӳ]�m���ܥi�H����
            loadNextHanas();
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
                loadNextHanas();
                    break;

            case ("Comm/DialogOpen"):
                Debug.Log("�}�ҹ�ܮ�");
                loadNextHanas();
                DialogEnd();
                break;

            case ("Comm/DialogEnd"):
                Debug.Log("������ܮ�");
                loadNextHanas();
                DialogEnd();
                break;

            case ("Comm/ClugOpen"):
                GameObject.Find("John").GetComponent<John_Control>().johnControlAble = false;

                GameObject.Find("John").GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                Debug.Log("�Ȱ����\���a�ާ@");
                loadNextHanas();
                break;

            case ("Comm/ClugClose"):
                GameObject.Find("John").GetComponent<John_Control>().johnControlAble = true;
                Debug.Log("�}�񪱮a���\�ާ@");
                loadNextHanas();
                break;

            case ("Comm/John Speaking"):
                Debug.Log("���ܤ������������John");
                loadNextHanas();
                sayingName.text = "John �j��";
                break;

            case ("Comm/May Speaking"):
                Debug.Log("���ܤ������������May");
                loadNextHanas();
                sayingName.text = "May �p��";
                break;

            case ("Comm/Narrowtive Speaking"):
                Debug.Log("���ܤ�������������ǥ�");
                loadNextHanas();
                sayingName.text = "�ǥ�";
                break;

            case ("Comm/SoundEffectPlay/GunShot"):
                Debug.Log("�ͦ��j�T");
                loadNextHanas();
                break;

            case ("Comm/ShowPicture/John"):
                Debug.Log("��ø��� John");
                loadNextHanas();
                break;

            default:
                coroutine = StartCoroutine(loadTexter(judgeString));
                break;
        }
    }


    public void DialogEnd()
    {
        //������������
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

    public void loadNextHanas()
    {
        if (loadingTextNum + 1 < hanasNum)
        {
            loadingTextNum++;

            textJudge(hanas[loadingTextNum]);
        }
    }
}
