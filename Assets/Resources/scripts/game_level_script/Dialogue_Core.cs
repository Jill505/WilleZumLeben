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

    public Image placeA;
    public Image placeB;
    public Image placeC;

    public Sprite[] spriteAssetArray;

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
            ///
            ///  �ټK �ڪ��D���i�H�Φr��ѪR
            ///  ���O�n�i�k��
            ///  �ҥH�N���������ŧi�n�F ���Q�θ� �ϥ�����i�H�A�u�ƹ�
            ///


            case (""):
                Debug.Log("��������ۤ����e ���L����");
                loadNextHanas();
                    break;

            case ("Comm/DialogOpen"):
                Debug.Log("�}�ҹ�ܮ�");
                loadNextHanas();
                break;

            case ("Comm/DialogEnd"):
                Debug.Log("������ܮ�");
                loadNextHanas();
                DialogEnd();
                //�۰������������e
                placeA.color = new Color(1, 1, 1, 0);
                placeB.color = new Color(1, 1, 1, 0);
                placeC.color = new Color(1, 1, 1, 0);
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
                sayingName.text = "John ����";
                break;
            case ("Comm/Benedict Speaking"):
                Debug.Log("���ܤ������������Benedict");
                loadNextHanas();
                sayingName.text = "Benedict �ھ|��";
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

            case ("Comm/ShowPicture/leftPicture"):

                Debug.Log("��̥ܳ��䪺�Ϥ�");
                placeA.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/middlePicture"):

                Debug.Log("��ܤ������Ϥ�");
                placeB.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/rightPicture"):

                Debug.Log("��ܥk�䶡���Ϥ�");
                placeC.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ClosePicture/leftPicture"):

                Debug.Log("�����̥��䪺�Ϥ�");
                placeA.color = new Color(1, 1, 1, 0);
                loadNextHanas();
                break;
            case ("Comm/ClosePicture/middlePicture"):

                Debug.Log("�����������Ϥ�");
                placeB.color = new Color(1, 1, 1, 0);
                loadNextHanas();
                break;
            case ("Comm/ClosePicture/rightPicture"):

                Debug.Log("�����k�䶡���Ϥ�");
                placeC.color = new Color(1, 1, 1, 0);
                loadNextHanas();
                break;


            case ("Comm/ShowPicture/John/L"):
                Debug.Log("��ø��John����");
                placeA.sprite = spriteAssetArray[1];
                placeA.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/John/M"):
                Debug.Log("��ø��John�󤤶�");
                placeB.sprite = spriteAssetArray[1];
                placeB.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/John/R"):
                Debug.Log("��ø��John��k��");
                placeC.sprite = spriteAssetArray[1];
                placeC.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/May/L"):
                Debug.Log("��ø��� May����");
                placeA.sprite = spriteAssetArray[2];
                placeA.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/May/M"):
                Debug.Log("��ø��� May�󤤶�");
                placeB.sprite = spriteAssetArray[2];
                placeB.color = new Color(1, 1, 1, 1);
                loadNextHanas();
                break;
            case ("Comm/ShowPicture/May/R"):
                Debug.Log("��ø��� May��k��");
                placeC.sprite = spriteAssetArray[2];
                placeC.color = new Color(1, 1, 1, 1);
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
        DialogEnd();
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
