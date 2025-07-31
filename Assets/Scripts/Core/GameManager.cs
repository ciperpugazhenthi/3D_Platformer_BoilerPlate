using UnityEngine;

public enum GameState { Start, Playing, Paused, GameOver }

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; } = GameState.Start;

    void Start() => SetState(GameState.Start);

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Start:
                Time.timeScale = 1;
                UIManager.Instance.ShowStartScreen();
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                UIManager.Instance.ShowHUD();
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                UIManager.Instance.ShowPauseMenu();
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                UIManager.Instance.ShowGameOver();
                break;
        }
    }

    public void TogglePause()
    {
        if (CurrentState == GameState.Playing)
            SetState(GameState.Paused);
        else if (CurrentState == GameState.Paused)
            SetState(GameState.Playing);
    }
}
