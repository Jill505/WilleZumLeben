using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Object : MonoBehaviour
{
    public Level_Core level_Core;
    public string nextLevelName = "lobby";

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

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "John")
            {
                if (nextLevelName == "")
                {
                    Debug.Log("���b�O�������� �̦h����o�F �����W�٬O�ťժ�����");
                }
                else
                {
                    Debug.Log("���J�����G" + nextLevelName);
                    Debug.Log("�p�G�Xbug �n�������S�����build setting�̭� �n���W�r�g�� �`�N�j�p�g�M�ť��䦳�L");
                }
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "John")
            {
                if (nextLevelName == "")
                {
                    Debug.Log("���b�O�������� �̦h����o�F �����W�٬O�ťժ�����");
                }
                else
                {
                    Debug.Log("���J�����G" + nextLevelName);
                    Debug.Log("�p�G�Xbug �n�������S�����build setting�̭� �n���W�r�g�� �`�N�j�p�g�M�ť��䦳�L");
                    level_Core.LoadNextScene(nextLevelName);
                    //SceneManager.LoadScene(nextLevelName);
                }
            }
        }
    }
}
