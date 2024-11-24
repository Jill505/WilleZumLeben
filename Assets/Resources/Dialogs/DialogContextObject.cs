using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogContextObject : MonoBehaviour
{
    public float dialogLength;

    public string[] dialog;

    public void Start()
    {
        dialogLength = dialog.Length;
    }
}