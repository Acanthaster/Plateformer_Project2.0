using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALR_MenuInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    [SerializeField] ALR_ButtonAction targetButton;

    private void Update()
    {
       if (Input.GetButtonDown("Pause Menu"))
        {
            isPaused = !isPaused;
        }

       if (isPaused)
        {
            ActivatePause();
        }
       else
        {
            DeactivatePause();
        }
    }

    void ActivatePause()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;           //pour le son plus tard
        targetButton.isFromGame();
        pauseMenuUI.SetActive(true);
        
    }

    public void DeactivatePause()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;          //pour le son plus tard
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }


}
