using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_pressurePlate : MonoBehaviour
{
    public GameObject dialogContext;
    public Dialogue_Core diaCore;

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
}
