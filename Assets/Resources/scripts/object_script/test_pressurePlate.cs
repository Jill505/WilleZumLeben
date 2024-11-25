using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_pressurePlate : MonoBehaviour
{
    public GameObject dialogContext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "John")
        {
            Debug.Log("Ä²µo¾÷Ãö");
            if (dialogContext != null)
            {
                GameObject.Find("ConverSysMaster").GetComponent<Dialogue_Core>().hanas = dialogContext.GetComponent<DialogContextObject>().dialog;
                GameObject.Find("ConverSysMaster").GetComponent<Dialogue_Core>().startDialog();
            }
        }
    }
}
