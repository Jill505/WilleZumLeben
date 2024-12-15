using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Core : MonoBehaviour
{
    public int level;

    public bool allowingControlTimeScale = true;

    public UI_Core uiCore;
    public Entry_Object entry;
    public Exit_Object exit;

    public GameObject John;
    public GameObject May;

    public Animator canvasAnimator;
    public GameObject swapRetry;

    public GameObject[] registeringObjects = new GameObject[0];//���U�������� �|�̷�y�ȶi��e��Ƨ�
    public bool sorting = true;

    public string nextLevelName = "lobby";

    public bool gameFail = false;

    void Start()
    {
        exit.nextLevelName = nextLevelName;

        John.transform.position = entry.subObject_JohnPosition.transform.position;
        May.transform.position = entry.subObject_MayPotation.transform.position;

        registerAllObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFail == true)
        {
            //allow player press button R to retry the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                LoadNextScene(getNowSceneName());
            }
        }
        if (sorting)
        {

            for (int i = 0; i < registeringObjects.Length; i++)
            {
                for (int j = i + 1; j < registeringObjects.Length; j++)
                {
                    if (registeringObjects[i].transform.position.y < registeringObjects[j].transform.position.y)
                    {
                        GameObject swapObj = registeringObjects[i];
                        registeringObjects[i] = registeringObjects[j];
                        registeringObjects[j] = swapObj;
                    }
                }
            }

            //After sorting
            for (int i = 0; i < registeringObjects.Length; i++)
            {
                if (registeringObjects[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
                    sr.sortingOrder = i;
                }
                else
                {
                    Debug.Log("�A�O�b���A��" + i + "�Ӫ���" + registeringObjects[i].name+"�ڥ��N�S��SpriteRenderer�O�n���Ƨǰ�");
                }
                //registeringObjects[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }
    }

    public void ControlTimeScale()
    {
    }
    public IEnumerator timeScaleCoroutine()
    {
        yield return null;
    }

    public void ArriveNextScene()
    {

    }

    public void LoadNextScene(string nextLevelNameS)
    {
        nextLevelName = nextLevelNameS;
        StartCoroutine(LoadNextSceneCoroutine());
    }
    public IEnumerator LoadNextSceneCoroutine()
    {
        //Add Animator and wait
        canvasAnimator.SetTrigger("LoadOut");
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(nextLevelName);
        //Load Next Scene
    }

    public void gameFailFunction()
    {
        gameFail = true;
        //�ʵe���J ���\��R���� �åB�s�� �ϩҦ��\�ఱ��B��
        swapRetry.SetActive(true);
    }

    public string getNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void registerAllObject()//�Ƶ� �o���k�ܼɤO �٬O��ĳ��ʧ�C�Ӫ����ʥ[�J����U��
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // �Ω�s�x�]�t SpriteRenderer �� GameObject
        List<GameObject> objectsWithSpriteRenderer = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // �ˬd�O�_�� SpriteRenderer �ե�
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                objectsWithSpriteRenderer.Add(obj);
            }
        }

        // �N���G�s�J registeringObjects
        registeringObjects = objectsWithSpriteRenderer.ToArray();
    }
}