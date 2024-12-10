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

    public string nextLevelName = "lobby";

    public bool gameFail = false;

    void Start()
    {
        exit.nextLevelName = nextLevelName;

        John.transform.position = entry.subObject_JohnPosition.transform.position;
        May.transform.position = entry.subObject_MayPotation.transform.position;
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
        //動畫介入 允許按R重試
        swapRetry.SetActive(true);
    }

    public string getNowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}