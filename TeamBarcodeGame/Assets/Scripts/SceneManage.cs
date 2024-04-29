using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
 

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 0;
        }
        else if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 1;
        }
        else if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 2;
        }
        else if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 3;
        }
        else if (gameObject.CompareTag(""))
        { 
            GameManager.Instance.currentEncounter = 4;
        }
        else if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 5;
        }
        else if (gameObject.CompareTag(""))
        {
            GameManager.Instance.currentEncounter = 6;
        }
        SceneManager.LoadScene("Combat");
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Main Menu");
    }


}
