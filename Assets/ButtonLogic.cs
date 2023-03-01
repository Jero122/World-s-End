using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{

    public void Play()
    {
        Invoke("loadGame", 0.5f);
       
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void loadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
