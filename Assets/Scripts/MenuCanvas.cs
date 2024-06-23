using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{

    public GameObject topbar, mainMenuPanel, gameOverScreen;
    public  TMP_Text gameOverScreenText;
    

    void Start(){
        topbar.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameOverScreen.SetActive(false);
        gameOverScreenText.text = string.Empty;
    }
    
    public void OnePlayerButton(){
        GameController.instance.isSinglePlayer = true;
        topbar.SetActive(true);
        mainMenuPanel.SetActive(false);
         gameOverScreenText.text = string.Empty;
        GameController.instance.StartGameSession();
    }


    public void TwoPlayerButton(){
        GameController.instance.isSinglePlayer = false;
        topbar.SetActive(true);
        mainMenuPanel.SetActive(false);
         gameOverScreenText.text = string.Empty;
        GameController.instance.StartGameSession();
    }


    public void RestartButton(){
        gameOverScreen.SetActive(false);
        mainMenuPanel.SetActive(true);
        topbar.SetActive(false);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ShowGameOverScreen(string msg)
    {
        gameOverScreenText.text = msg;
        gameOverScreen.SetActive(true);
        mainMenuPanel.SetActive(false);
        topbar.SetActive(false);
    }
}
