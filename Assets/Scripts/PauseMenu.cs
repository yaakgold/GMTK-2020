using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public MainMenu mm;

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        GameController.Instance.tutTimeScale = 0.0f;
        Time.timeScale = 0.0f;
    }

    public void ClosePanel()
    {
        Debug.Log("Hello");
        GameController.Instance.tutTimeScale = 1.0f;
        Time.timeScale = 1.0f;

        gameObject.SetActive(false);
        Debug.Log(GameController.Instance.tutTimeScale);
    }

    public void ExitToMenu(bool tut)
    {
        if(tut)
            SceneManager.LoadScene(0);
        else
        {
            ClosePanel();
            mm.RestartObj();
        }
    }
}
