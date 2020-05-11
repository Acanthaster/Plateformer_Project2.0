using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ALR_ButtonAction : MonoBehaviour
{

    static bool IS_FROM_GAME = false;
    private string currentScene;
    static string oldScene;

    /*private void Awake()
    {
        IS_FROM_GAME = false;
    }*/

    public void isFromGame()
    {
        IS_FROM_GAME = true;
    }

    public void LoadScene(int i)
    {
            SceneManager.LoadScene(i); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game !");
        Application.Quit();
    }
}
