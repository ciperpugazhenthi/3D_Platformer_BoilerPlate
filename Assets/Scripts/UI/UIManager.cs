using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject startScreen;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject hud;

    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        hud.SetActive(false);
    }

    public void ShowHUD()
    {
        startScreen.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        hud.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        hud.SetActive(false);
    }
}