using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_pressurePlate : MonoBehaviour
{
    public GameObject dialogContext;
    public Dialogue_Core diaCore;
    public CameraLocate CameraLocateContext;

    public enum PressurePlateMode
    {
        Dialog,
        CameraLocate,
        CameraLocateToJohn,
    }
    public PressurePlateMode pressurePlateMode = PressurePlateMode.Dialog;

    public bool onceClug = false;
    public bool everyClug = false;

    // Start is called before the first frame update
    void Start()
    {
        diaCore = GameObject.Find("ConverSysMaster").GetComponent<Dialogue_Core>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (pressurePlateMode)
        {
            case (PressurePlateMode.Dialog):
                if (diaCore == null)
                {
                    //try find diaCore
                    diaCore = GameObject.Find("ConverSysMaster").GetComponent<Dialogue_Core>();
                }


                if (collision.gameObject.tag == "John")
                {
                    Debug.Log("觸發機關");
                    if (dialogContext != null)
                    {
                        diaCore.hanas = dialogContext.GetComponent<DialogContextObject>().dialog;
                        diaCore.loadingDialogContext = dialogContext.GetComponent<DialogContextObject>();
                        diaCore.startDialog();
                        Debug.Log("觸發對話");
                    }
                    else
                    {
                        Debug.Log("沒有對話可以觸發 缺乏對話物件");
                    }

                    if (onceClug)
                    {
                        gameObject.SetActive(false);
                    }

                    if (everyClug)
                    {
                        everyClug = !everyClug;
                    }
                }
                break;


            case (PressurePlateMode.CameraLocate):
                if (collision.gameObject.tag == "John")
                {
                    if (CameraLocateContext != null)
                    {
                        CameraLocateContext.callCamera();

                        if (onceClug)
                        {
                            gameObject.SetActive(false);
                        }

                        if (everyClug)
                        {
                            everyClug = !everyClug;
                        }
                    }
                }
                break;

            case (PressurePlateMode.CameraLocateToJohn):
                if (collision.gameObject.tag == "John")
                {
                    GameObject.Find("UICore").GetComponent<UI_Core>().JohnCameraLocateLerp();

                    if (onceClug)
                    {
                        gameObject.SetActive(false);
                    }

                    if (everyClug)
                    {
                        everyClug = !everyClug;
                    }
                }
                break;
        }

    }
}
