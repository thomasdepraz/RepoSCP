using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PLAY!");
        SceneManager.LoadScene("MainScene"); 
    }
    public void ExitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
