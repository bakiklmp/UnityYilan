using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayQuit : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("Snake");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
