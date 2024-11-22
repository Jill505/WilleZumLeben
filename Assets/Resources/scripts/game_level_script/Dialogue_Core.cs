using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Core : MonoBehaviour
{
    public Text showText; //��ܪ�Text
    public string[] hanas; //��ܼ@���A���J�ɱN����������N�r��}�C�Y�i����
    public int loadingTextNum = 0; //���b���J�����
    public int hanasNum; 
    Coroutine coroutine; //Debug��{

    [Range(0.01f, 1.5f)] public float wordSpeed = 0.1f; //��ܳt�v

    public bool isClog = false; //���b����P�_

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
                    Debug.Log("�W�X���");
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
        Debug.Log("��ܵ���");

        isClog = false;
    }
}
