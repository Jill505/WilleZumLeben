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

    public GameObject[] registeringObjects = new GameObject[0];//註冊中的物件 會依照y值進行前後排序
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
                    Debug.Log("你是在哭，第" + i + "個物件" + registeringObjects[i].name+"根本就沒有SpriteRenderer是要怎麼排序啦");
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
        //動畫介入 允許按R重試 並且廣播 使所有功能停止運算
        swapRetry.SetActive(true);
    }

    public string getNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void registerAllObject()//備註 這格方法很暴力 還是建議手動把每個物件手動加入到註冊表中
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // 用於存儲包含 SpriteRenderer 的 GameObject
        List<GameObject> objectsWithSpriteRenderer = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 檢查是否有 SpriteRenderer 組件
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                objectsWithSpriteRenderer.Add(obj);
            }
        }

        // 將結果存入 registeringObjects
        registeringObjects = objectsWithSpriteRenderer.ToArray();
    }
}