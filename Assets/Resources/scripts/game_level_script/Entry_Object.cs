using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry_Object : MonoBehaviour
{
    public GameObject subObject_JohnPosition; //John初始位置
    public GameObject subObject_MayPotation; //May初始位置

    public Level_Core level_Core;

    private void Awake()
    {
        if (level_Core == null)
        {
            level_Core = GameObject.Find("Level_Core").GetComponent<Level_Core>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
