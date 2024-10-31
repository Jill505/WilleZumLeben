using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffecter : MonoBehaviour
{
    public AudioSource myAS;
    public float volume = 1f;

    public float deadTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        if (myAS == null)
        {
            myAS = gameObject.GetComponent<AudioSource>();
        }

        myAS.volume = All_GameCore.volume;

        Destroy(gameObject, deadTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
