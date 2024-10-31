using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Core : MonoBehaviour
{
    public int level;

    public bool allowingControlTimeScale = true;


    // Start is called before the first frame update
    void Start()
    {

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