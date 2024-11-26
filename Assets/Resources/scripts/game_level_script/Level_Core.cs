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

    public string nextLevelName = "lobby";

    void Start()
    {
        exit.nextLevelName = nextLevelName;

        John.transform.position = entry.subObject_JohnPosition.transform.position;
        May.transform.position = entry.subObject_MayPotation.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void LoadNextScene()
    {
        //Load Next Scene
    }
}